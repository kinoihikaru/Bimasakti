using BlazorClientHelper;
using GLT00100COMMON;
using GLT00100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLT00100FRONT
{
    public partial class GLT00100 : R_Page
    {
        private GLT00100ViewModel _JournalListViewModel = new();
        private R_Conductor _conductorRef;
        private R_Grid<GLT00100DTO> _gridRef;
        private R_Grid<GLT00101DTO> _gridDetailRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _JournalListViewModel.GetAllUniversalData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task OnclickSearch()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_JournalListViewModel.JornalParam.CSEARCH_TEXT))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (!string.IsNullOrEmpty(_JournalListViewModel.JornalParam.CSEARCH_TEXT)
                    && _JournalListViewModel.JornalParam.CSEARCH_TEXT.Length < 3)
                {
                    loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                    goto EndBlock;
                }

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

         EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public async Task OnClickShowAll()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #region JournalGrid
        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _JournalListViewModel.GetJournalList();
                eventArgs.ListEntityResult = _JournalListViewModel.JournalGrid;
                if (_JournalListViewModel.JournalGrid.Count <= 0)
                {
                    loEx.Add("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    await _gridDetailRef.R_RefreshGrid(eventArgs.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00100DTO)_conductorRef.R_GetCurrentData();
                if (!loData.LALLOW_APPROVE)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _JournalListViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "20";

                await _JournalListViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private async Task CommitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loValidate;

            try
            {
                var loData = (GLT00100DTO)_conductorRef.R_GetCurrentData();

                if (loData.CSTATUS == "80")
                {
                    if (_JournalListViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION == 3)
                    {
                        loValidate = await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ", R_eMessageBoxButtonType.YesNo);
                        if (loValidate == R_eMessageBoxResult.No)
                            goto EndBlock;
                    }
                }
                else
                {
                    loValidate = await R_MessageBox.Show("", "Are you sure want to commit this journal? ", R_eMessageBoxButtonType.YesNo);
                    if (loValidate == R_eMessageBoxResult.No)
                        goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _JournalListViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80" ? true : false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? "20" : "80";

                await _JournalListViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region JournalGridDetail
        private async Task JournalGridDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00101DTO>(eventArgs.Parameter);
                await _JournalListViewModel.GetJournalDetailList(loParam);
                eventArgs.ListEntityResult = _JournalListViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region lookupDept
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _JournalListViewModel.JornalParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _JournalListViewModel.JornalParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Predefine Journal Entry
        private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loData = _conductorRef.R_GetCurrentData();
            //eventArgs.TargetPageType = typeof(GLT00100JournalEntry);
            //eventArgs.Parameter = _JournalListViewModel._Journal;
        }
        private async Task AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region RapidApprove
        private async Task R_Before_Open_PopupRapidApprove(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100RapidApprovalValidationDTO>(loData);
                var loValidate = await _JournalListViewModel.ValidationRapidApproval(loParam);
                if (loValidate)
                {
                    await R_MessageBox.Show("", "You don’t have right to approve this journal type!", R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }
                //eventArgs.Parameter = _JournalListViewModel._JournalList;
                //eventArgs.TargetPageType = typeof(RapidApproveGLT00100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            
        }
        private async Task R_After_Open_PopupRapidApprove(R_AfterOpenPopupEventArgs eventArgs)
        {
            //GLT00100ParameterDTO param = new GLT00100ParameterDTO()
            //{
            //    CPERIOD = _JournalListViewModel._Journal.CPERIOD,
            //    CSEARCH_TEXT = _JournalListViewModel._Journal.CSEARCH_TEXT,
            //    CSTATUS = "20",
            //    CDEPT_CODE = _JournalListViewModel._Journal.CDEPT_CODE
            //};

            //await _gridRef.R_RefreshGrid(param);
            //var firstJournal = _JournalListViewModel._JournalList.FirstOrDefault();
            //if (firstJournal != null)
            //{
            //    await _gridDetailRef.R_RefreshGrid(firstJournal);
            //}
            //else
            //{
            //    _JournalListViewModel._JournaDetailList.Clear();
            //}
        }
        #endregion

        #region RapidCommit
        private async Task R_Before_Open_PopupRapidCommit(R_BeforeOpenPopupEventArgs eventArgs)
        {

            //eventArgs.Parameter = _JournalListViewModel._JournalList;
            //eventArgs.TargetPageType = typeof(RapidCommitGLT00100);
        }
        private async Task R_After_Open_PopupRapidCommit(R_AfterOpenPopupEventArgs eventArgs)
        {
            ////GLT00100JournalGridDTO param;
            ////param = _JournalListViewModel.JournalEntity;
            ////param.CSTATUS = "80";

            //GLT00100ParameterDTO param = new GLT00100ParameterDTO()
            //{
            //    CPERIOD = _JournalListViewModel._Journal.CPERIOD,
            //    CSEARCH_TEXT = _JournalListViewModel._Journal.CSEARCH_TEXT,
            //    CSTATUS = "80",
            //    CDEPT_CODE = _JournalListViewModel._Journal.CDEPT_CODE
            //};

            //await _gridRef.R_RefreshGrid(param);
            //var firstJournal = _JournalListViewModel._JournalList.FirstOrDefault();
            //if (firstJournal != null)
            //{
            //    await _gridDetailRef.R_RefreshGrid(firstJournal);
            //}
            //else
            //{
            //    _JournalListViewModel._JournaDetailList.Clear();
            //}
        }
        #endregion
    }
}
