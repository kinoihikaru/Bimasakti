using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace LMM01000FRONT
{
    public partial class LMM01020 : R_Page, R_ITabPage
    {
        #region viewModel
        private LMM01020ViewModel _viewModel = new LMM01020ViewModel();
        private LMM01020SaveBatchViewModel _viewModelSave = new LMM01020SaveBatchViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();
        #endregion
        #region Grid Conductor
        private R_Conductor _RateWG_conductorRef;
        private R_ConductorGrid _RateWGDetail_conductorRef;
        private R_Grid<LMM01021DTO> _RateWGDetail_gridRef;
        #endregion
        #region inject
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        #endregion

        private List<LMM01021DTO> ListDetailData = new List<LMM01021DTO>();

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

            try
            {
                //Assign Action
                _viewModelSave.StateChangeAction = StateChangeInvoke;
                _viewModelSave.ShowErrorAction = ShowErrorInvoke;
                _viewModelSave.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;

                LMM01020DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(poParameter);
                await _viewModel.GetProperty(loParam);

                await _Universal_viewModel.GetUsageRateModelList();
                await _Universal_viewModel.GetAdminFeeTypeList();

                await RateWG_CheckData(loParam);
                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateWGCheckData((LMM01020DTO)poParam);

                if (loCheck != null)
                {
                    await _RateWG_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateWG((LMM01020DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateWG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_NumericTextBox<decimal> PipeSize_NumericTextBox;
        private async Task RateWG_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await PipeSize_NumericTextBox.FocusAsync();
            }
        }

        private bool PrintBtnEnable = false;
        private void RateWG_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }

        private bool EnableEdit = false;
        private void RateWG_SetEdit(R_SetEventArgs eventArgs)
        {
            EnableEdit = eventArgs.Enable;
        }

        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RateWG_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;

            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }
        private async Task RateWG_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _RateWGDetail_gridRef.R_SaveBatch();
                var loData = (LMM01020DTO)eventArgs.Data;
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
        private async Task RateWG_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01020DTO)eventArgs.Data;
                loData.CRATE_WG_LIST = ListDetailData;
                await _viewModelSave.SaveRateWG(loData, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                EnableEdit = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(eventArgs.Data);

                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                EnableEdit = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(eventArgs.Data);

                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
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
        private async Task RateWGDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Parameter is null)
                {
                    _viewModel.RateWGDetailList = new();
                    eventArgs.ListEntityResult = _viewModel.RateWGDetailList;
                }
                else
                {
                    var loEventParam = (LMM01020DTO)eventArgs.Parameter;
                    var loParam = R_FrontUtility.ConvertObjectToObject<LMM01021DTO>(loEventParam);

                    await _viewModel.GetRateWGDetailList(loParam);

                    eventArgs.ListEntityResult = _viewModel.RateWGDetailList;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateWGDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void RateWGDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01020DTO)_RateWG_conductorRef.R_GetCurrentData();
            var loData = (LMM01021DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
        }
        private void RateUCDetail_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeePctEnable = false;
        }
        private void RateWGDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loListData = (List<LMM01021DTO>)eventArgs.Data; 
            ListDetailData = R_FrontUtility.ConvertCollectionToCollection<LMM01021DTO>(loListData).ToList();
        }
      
        public async Task RefreshTabPageAsync(object poParam)
        {

            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(poParam);
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    await _RateWG_conductorRef.R_SetCurrentData(null);
                    _RateWGDetail_gridRef.DataSource.Clear();
                }
                else 
                {
                    await _viewModel.GetProperty(loParam);

                    await RateWG_CheckData(loParam);
                    await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickPrintAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // set Param
                var loParam = new LMM01020PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RateWG.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RateWG.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RateWG.CCHARGES_ID,
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
                     "rpt/LMM01020Print/AllRateWGReportPost",
                     "rpt/LMM01020Print/AllStreamRateWGReportGet",
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
            if (poParam == "SM")
            {
                _RateWGDetail_gridRef.DataSource.Clear();
            }
        }
    }
}
