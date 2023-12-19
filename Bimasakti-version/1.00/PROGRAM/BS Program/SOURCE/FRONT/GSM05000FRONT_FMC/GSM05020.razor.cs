using GSM05000COMMON_FMC;
using GSM05000MODEL_FMC;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000FRONT_FMC
{
    public partial class GSM05020 : R_Page
    {
        private GSM05020DeptViewModel _GSM05000DeptViewModel = new();
        private R_Grid<GSM05020DeptDTO> _gridRefDept;
        private R_Conductor _conductorRefDept;

        private GSM05020ViewModel _GSM05020ViewModel = new();
        private R_Grid<GSM05020DTO> _gridRefApprover;
        private R_ConductorGrid _conductorRefApprover;

        private GSM05021ViewModel _GSM05000ApprovalReplacementViewModel = new();
        private R_ConductorGrid _conductorRefReplacement;
        private R_Grid<GSM05021DTO> _gridRefReplacement;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //Get Param
                var loParam = (GSM05000DTO)poParameter;
                _GSM05020ViewModel.TransactionCode = loParam.CTRANS_CODE;
                _GSM05020ViewModel.HeaderEntity = loParam;

                //Refresh
                await _gridRefDept.R_RefreshGrid(null);
                await _gridRefApprover.R_RefreshGrid(null); 
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Departement
        private async Task GetListDept(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05000DeptViewModel.GetDeptList();

                eventArgs.ListEntityResult = _GSM05000DeptViewModel.GridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void GetRecordDept(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task DisplayDept(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loDeptData = (GSM05020DeptDTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridRefApprover.CurrentSelectedData is not null)
                    {
                        var loApprovData = _gridRefApprover.CurrentSelectedData;
                        var loParam = R_FrontUtility.ConvertObjectToObject<GSM05021DTO>(loApprovData);
                        loParam.CTRANSACTION_CODE = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE;
                        loParam.CDEPT_CODE = loDeptData.CDEPT_CODE;

                        await _gridRefReplacement.R_RefreshGrid(loParam);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Approval User
        private async Task GetListApprover(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05020ViewModel.GetApprovalList();
                eventArgs.ListEntityResult = _GSM05020ViewModel.GridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GetRecordApprover(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05020DTO)eventArgs.Data;
                await _GSM05020ViewModel.GetEntityApproval(loParam);
                eventArgs.Result = _GSM05020ViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task DisplayApprover(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loApprovData = (GSM05020DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_conductorRefDept.R_ConductorMode == R_eConductorMode.Normal)
                    {
                        var loDeptData = _gridRefDept.CurrentSelectedData;
                        var loParam = R_FrontUtility.ConvertObjectToObject<GSM05021DTO>(loApprovData);
                        loParam.CTRANSACTION_CODE = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE;
                        loParam.CDEPT_CODE = loDeptData.CDEPT_CODE;

                        await _gridRefReplacement.R_RefreshGrid(loParam);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterAddApprover(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = _GSM05020ViewModel.GenerateSequence((GSM05020DTO)eventArgs.Data);
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;

                eventArgs.Data = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();
            var loChoice = new R_eMessageBoxResult();

            try
            {
                var loApprovalData = (GSM05020DTO)eventArgs.Data;
                var loDeptData = _gridRefDept.CurrentSelectedData;
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GSM05020ValidateDTO>(loDeptData);
                    loParam.CTRANS_CODE = loApprovalData.CTRANS_CODE;
                    bool llCheckExist = await _GSM05020ViewModel.CheckExistData(loParam);

                    if (llCheckExist && loApprovalData.LREPLACEMENT == false)
                    {
                        loChoice = await R_MessageBox.Show(_localizer["ConfirmLabel"], _localizer["Confirm03"], R_eMessageBoxButtonType.YesNo);
                        eventArgs.Cancel = loChoice == R_eMessageBoxResult.No;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private async Task SaveApprover(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05020DTO)eventArgs.Data;
                await _GSM05020ViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM05020ViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task BeforeDeleteApprover(R_BeforeDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            var loChoice = new R_eMessageBoxResult();

            try
            {
                var loApprovalData = (GSM05020DTO)eventArgs.Data;
                var loDeptData = _gridRefDept.CurrentSelectedData;
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05020ValidateDTO>(loDeptData);
                loParam.CTRANS_CODE = loApprovalData.CTRANS_CODE;
                bool llCheckExist = await _GSM05020ViewModel.CheckExistData(loParam);

                if (llCheckExist && loApprovalData.LREPLACEMENT == true)
                {
                    loChoice = await R_MessageBox.Show(_localizer["ConfirmLabel"], _localizer["Confirm03"], R_eMessageBoxButtonType.YesNo);
                    eventArgs.Cancel = loChoice == R_eMessageBoxResult.No;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        private async Task DeleteApprover(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05020DTO)eventArgs.Data;
                await _GSM05020ViewModel.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupApprover(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL01100ParameterDTO()
                {
                    CTRANSACTION_CODE = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE
                };
                eventArgs.TargetPageType = typeof(GSL01100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupApprover(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL01100DTO)eventArgs.Result;
                var loGetData = (GSM05020DTO)eventArgs.ColumnData;
                if (loTempResult == null)
                    return;

                loGetData.CUSER_ID = loTempResult.CUSER_ID;
                loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Replacement

        private async Task GetListReplacement(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05021DTO)eventArgs.Parameter;
                await _GSM05000ApprovalReplacementViewModel.GetApprovalReplacementList(loParam);
                eventArgs.ListEntityResult = _GSM05000ApprovalReplacementViewModel.GridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetRecordReplacement(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05021DTO)eventArgs.Data;
                await _GSM05000ApprovalReplacementViewModel.GetEntityApprovalReplacement(loParam);

                eventArgs.Result = _GSM05000ApprovalReplacementViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DeleteReplacement(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05021DTO)eventArgs.Data;
                await _GSM05000ApprovalReplacementViewModel.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupReplacement(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //eventArgs.Parameter = new GSL01100ParameterDTO()
                //{
                //    CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANS_CODE
                //};
                //eventArgs.TargetPageType = typeof(GSL01100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupReplacement(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //var loTempResult = (GSL01100DTO)eventArgs.Result;
                //var loGetData = (GSM05000ApprovalReplacementDTO)eventArgs.ColumnData;
                //if (loTempResult == null)
                //    return;

                //loGetData.CUSER_REPLACEMENT = loTempResult.CUSER_ID;
                //loGetData.CUSER_REPLACEMENT_NAME = loTempResult.CUSER_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterAddReplacement(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                //Get Data
                var loApprovData = _gridRefApprover.CurrentSelectedData;
                var loDeptData = _gridRefDept.CurrentSelectedData;
                var loParam = (GSM05021DTO)eventArgs.Data;

                //Set Data
                loParam.CDEPT_CODE = loDeptData.CDEPT_CODE;
                loParam.CTRANSACTION_CODE = loApprovData.CTRANS_CODE;
                loParam.CUSER_ID = loApprovData.CUSER_ID;
                loParam.DVALID_TO = DateTime.Now;
                loParam.DVALID_FROM = DateTime.Now;
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SavingReplacement(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05021DTO)eventArgs.Data;
                loParam.CVALID_TO = loParam.DVALID_TO.ToString("yyyyMMdd");
                loParam.CVALID_FROM = loParam.DVALID_FROM.ToString("yyyyMMdd");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SaveReplacement(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05021DTO)eventArgs.Data;
                await _GSM05000ApprovalReplacementViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _GSM05000ApprovalReplacementViewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        private void CopyToBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _conductorRefDept.R_GetCurrentData();
            eventArgs.TargetPageType = typeof(GSM05021);
        }

        private async Task CopyToAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false || eventArgs.Result is null)
                    return;

                var result = R_FrontUtility.ConvertObjectToObject<GSM05020ParamFromToDeptDTO>(eventArgs.Result);
                result.CTRANS_CODE = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE;
                result.CDEPT_CODE_SELECTED = _gridRefDept.CurrentSelectedData.CDEPT_CODE;

                await _GSM05020ViewModel.CopyToApproval(result);
                await _gridRefApprover.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void CopyFromBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _conductorRefDept.R_GetCurrentData();
            eventArgs.TargetPageType = typeof(GSM05022);
        }

        private async Task CopyFromAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false || eventArgs.Result is null)
                    return;

                var result = R_FrontUtility.ConvertObjectToObject<GSM05020ParamFromToDeptDTO>(eventArgs.Result);
                result.CTRANS_CODE = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE;
                result.CDEPT_CODE_SELECTED = _gridRefDept.CurrentSelectedData.CDEPT_CODE;

                await _GSM05020ViewModel.CopyFromApproval(result);
                await _gridRefApprover.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void ChangeSequenceBeforePopup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _GSM05020ViewModel.HeaderEntity.CTRANS_CODE;
            eventArgs.TargetPageType = typeof(GSM05023);
        }

        private async Task ChangeSequenceAfterPopup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                    return;

                var result = (bool)eventArgs.Result;

                if (result)
                {
                    await _gridRefApprover.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }



        private bool _gridDeptEnabled = true;
        private bool _gridUserEnabled = true;
        private bool _gridReplacementEnabled = true;

        private void SetOtherUser(R_SetEventArgs eventArgs)
        {
            _gridDeptEnabled = eventArgs.Enable;
            _gridReplacementEnabled = eventArgs.Enable;
        }

        private void SetOtherReplacement(R_SetEventArgs eventArgs)
        {
            _gridDeptEnabled = eventArgs.Enable;
            _gridUserEnabled = eventArgs.Enable;
        }
    }
}