using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;

namespace LMM01000FRONT
{
    public partial class LMM01010 : R_Page, R_ITabPage
    {
        #region ViewModel
        private LMM01010ViewModel _viewModel = new LMM01010ViewModel();
        private LMM01010SaveBatchViewModel _viewModelSave = new LMM01010SaveBatchViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();
        #endregion

        #region Condutor & Grid
        private R_Conductor _RateUC_conductorRef;
        private R_ConductorGrid _RateUCDetail_conductorRef;
        private R_Grid<LMM01011DTO> _RateUCDetail_gridRef;
        #endregion

        #region Private Property
        private R_RadioGroup<LMM01000UniversalDTO, string> UsageRateMode_RadioGrp;
        private List<LMM01011DTO> ListDetailData = new List<LMM01011DTO>();
        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private bool _IsHMMode;
        #endregion

        #region Inject
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Batch Proses
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = new R_Exception(poEx.ErrorList.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.ErrNo, x.ErrDescp)).ToList());
            this.R_DisplayException(loEx);
        }
        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await this.Close(true, true);
        }
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            LMM01010DTO loParam = null;

            try
            {
                //Assign Action
                _viewModelSave.StateChangeAction = StateChangeInvoke;
                _viewModelSave.ShowErrorAction = ShowErrorInvoke;
                _viewModelSave.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;

                //Load Radio Button
                await _Universal_viewModel.GetUsageRateModelList();
                await _Universal_viewModel.GetRateTypeList();
                await _Universal_viewModel.GetAdminFeeTypeList();

                loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(poParameter);
                await _viewModel.GetProperty(loParam);

                await _RateUC_conductorRef.R_GetEntity(loParam); 
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_MODULE_NAME = "LM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (LMM01010DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                   plSendWithContext: true,
                   plSendWithToken: true,
                   pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM01010",
                        Table_Name = "LMM_UTILITY_RATE_EC_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM01010",
                        Table_Name = "LMM_UTILITY_RATE_EC_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #region Form
        private async Task RateUC_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateEC((LMM01010DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateEC;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_Display(R_DisplayEventArgs eventArgs)
        {
            var loData = (LMM01010DTO)eventArgs.Data;
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await UsageRateMode_RadioGrp.FocusAsync();
                _IsHMMode = loData.CUSAGE_RATE_MODE == "HM";
            }
        }
        private void RateUc_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;
            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }
        private async Task RateUC_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _RateUCDetail_gridRef.R_SaveBatch();
                var loData = (LMM01010DTO)eventArgs.Data;
                if (loData.CUSAGE_RATE_MODE == "HM" && ListDetailData.Count <= 0)
                {
                    loEx.Add("", "Detail Rate cannot be empty");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01010DTO)eventArgs.Data;
                loData.CRATE_EC_LIST = ListDetailData;
                await _viewModelSave.SaveRateEC(loData, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(eventArgs.Data);

                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {

            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(eventArgs.Data);

                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        #endregion

        #region Detail Grid
        private async Task RateUC_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01011DTO>(eventArgs.Parameter);
                await _viewModel.GetRateUCDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateUCDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateUCDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void RateUCDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01010DTO)_RateUC_conductorRef.R_GetCurrentData();
            var loData = (LMM01011DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
        }
        private void RateUCDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loListData = (List<LMM01011DTO>)eventArgs.Data;
            ListDetailData = R_FrontUtility.ConvertCollectionToCollection<LMM01011DTO>(loListData).ToList();
        }
        #endregion
        private async Task RateEC_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateECCheckData((LMM01010DTO)poParam);

                if (loCheck != null)
                {
                    await _RateUC_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Refresh Tab Property
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(poParam);
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    await _RateUC_conductorRef.R_SetCurrentData(null);
                    _RateUCDetail_gridRef.DataSource.Clear();
                }
                else
                {
                    await _viewModel.GetProperty(loParam);
                    await RateEC_CheckData(loParam);
                    await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Method View
        public async Task Button_OnClickPrintAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // set Param
                var loParam = new LMM01010PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RateEC.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RateEC.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RateEC.CCHARGES_ID,
                    CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME,
                };
                loParam.CUSER_ID = clientHelper.UserId;
                loParam.CCOMPANY_ID = clientHelper.CompanyId;

                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                     "R_DefaultServiceUrlLM",
                     "LM",
                     "rpt/LMM01010Print/AllRateECReportPost",
                     "rpt/LMM01010Print/AllStreamRateECReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void UsageRateMode_OnChange(string poParam)
        {
            _viewModel.Data.CUSAGE_RATE_MODE = poParam;
            _IsHMMode = poParam == "HM";
            if (poParam == "SM")
            {
                _RateUCDetail_gridRef.DataSource.Clear();
            }
        }
        #endregion
    }
}
