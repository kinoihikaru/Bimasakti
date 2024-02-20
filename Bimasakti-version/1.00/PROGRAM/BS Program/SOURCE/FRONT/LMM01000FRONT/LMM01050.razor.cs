using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using LMM01000FrontResources;
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
    public partial class LMM01050 : R_Page, R_ITabPage
    {
        private LMM01050ViewModel _viewModel = new LMM01050ViewModel();
        private LMM01050SaveBatchViewModel _viewModelSave = new LMM01050SaveBatchViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateOT_conductorRef;
        private R_ConductorGrid _RateOTDetailWD_conductorRef;
        private R_ConductorGrid _RateOTDetailWK_conductorRef;

        private R_Grid<LMM01051DTO> _RateOTDetailWD_gridRef;
        private R_Grid<LMM01051DTO> _RateOTDetailWK_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
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


                LMM01050DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<LMM01050DTO>(poParameter);

                await _viewModel.GetProperty(loParam);
                await _Universal_viewModel.GetAdminFeeTypeList();

                await RateOT_CheckData(loParam);
                await _RateOTDetailWD_gridRef.R_RefreshGrid(loParam);
                await _RateOTDetailWK_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task RateOT_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateOTCheckData((LMM01050DTO)poParam);

                if (loCheck != null)
                {
                    await _RateOT_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateOT_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateOT((LMM01050DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateOT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private R_RadioGroup<LMM01000UniversalDTO, string> AdminPerMonth_RadioGrp;
        private async Task RateOT_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await AdminPerMonth_RadioGrp.FocusAsync();
            }
        }
        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RateOT_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;

            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }

        private bool PrintBtnEnable = false;
        private void RateOT_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }
        private async Task RateOT_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                EnableEdit = false;
                await _RateOTDetailWD_gridRef.R_SaveBatch();
                await _RateOTDetailWK_gridRef.R_SaveBatch();
                var loData = (LMM01050DTO)eventArgs.Data;
                if (_viewModel.RateOTWDDetailListData.Count <= 0 && _viewModel.RateOTWKDetailListData.Count <= 0)
                {
                    loEx.Add("", "Must fill 1 detail data");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOT_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM01050DTO)eventArgs.Data;
                loParam.CRATE_OT_LIST = new List<LMM01051DTO>();
                loParam.CRATE_OT_LIST.AddRange(_viewModel.RateOTWDDetailListData);
                loParam.CRATE_OT_LIST.AddRange(_viewModel.RateOTWKDetailListData);

                await _viewModelSave.SaveRateOT(loParam, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = _viewModel.RateOT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool EnableEdit = false;
        private void RateOT_SetEdit(R_SetEventArgs eventArgs)
        {
            EnableEdit = eventArgs.Enable;
        }

        private async Task RateOT_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                EnableEdit = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01050DTO>(eventArgs.Data);

                await _RateOTDetailWD_gridRef.R_RefreshGrid(loParam);
                await _RateOTDetailWK_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOT_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                EnableEdit = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01050DTO>(eventArgs.Data);

                await _RateOTDetailWD_gridRef.R_RefreshGrid(loParam);
                await _RateOTDetailWK_gridRef.R_RefreshGrid(loParam);
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
        private void RateOT_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.RateOTWDDetailList.ToList().Count > 0)
                    _viewModel.RateOTWDDetailListData = _viewModel.RateOTWDDetailList.ToList();
                if (_viewModel.RateOTWKDetailList.ToList().Count > 0)
                    _viewModel.RateOTWKDetailListData = _viewModel.RateOTWKDetailList.ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateOTDetailWK_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (LMM01050DTO)eventArgs.Parameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01051DTO>(loEventParam);

                await _viewModel.GetRateOTWKDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWKDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateOTDetailWD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (LMM01050DTO)eventArgs.Parameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01051DTO>(loEventParam);

                await _viewModel.GetRateOTWDDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWDDetailList;

                _viewModel.SavingBatchListOTWD(_viewModel.RateOTWDDetailList.ToList());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateOTDetailWD_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void RateWGDetailWK_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void RateOTWDDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
            var loData = (LMM01051DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
            loData.CDAY_TYPE = "WD";
        }
        private void RateOTWKDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01050DTO)_RateOT_conductorRef.R_GetCurrentData();
            var loData = (LMM01051DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
            loData.CDAY_TYPE = "WK";
        }
        private void RateOTWDDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (List<LMM01051DTO>)eventArgs.Data;
                _viewModel.SavingBatchListOTWD(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateOTWKDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (List<LMM01051DTO>)eventArgs.Data;

                _viewModel.SavingBatchListOTWK(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01050DTO>(poParam);
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    await _RateOT_conductorRef.R_SetCurrentData(null);

                    _RateOTDetailWD_gridRef.DataSource.Clear();
                    _RateOTDetailWK_gridRef.DataSource.Clear();
                }
                else
                {
                    await _viewModel.GetProperty(loParam);

                    await RateOT_CheckData(loParam);
                    await _RateOTDetailWD_gridRef.R_RefreshGrid(loParam);
                    await _RateOTDetailWK_gridRef.R_RefreshGrid(loParam);
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
                var loParam = new LMM01050PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RateOT.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RateOT.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RateOT.CCHARGES_ID,
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
                     "rpt/LMM01050Print/AllRateOTReportPost",
                     "rpt/LMM01050Print/AllStreamRateOTReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
