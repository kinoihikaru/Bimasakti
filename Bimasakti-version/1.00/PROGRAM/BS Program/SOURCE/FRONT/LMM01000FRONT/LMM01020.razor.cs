using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
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
        private LMM01020ViewModel _viewModel = new LMM01020ViewModel();
        private LMM01020SaveBatchViewModel _viewModelSave = new LMM01020SaveBatchViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateWG_conductorRef;
        private R_ConductorGrid _RateWGDetail_conductorRef;
        private R_Grid<LMM01021DTO> _RateWGDetail_gridRef;
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
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

        private async Task RateWG_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelSave.SaveRateWG((LMM01020DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode, clientHelper.CompanyId, clientHelper.UserId);

                eventArgs.Result = _viewModel.RateWG;
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
        private void RateWG_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.RateWGDetailList.ToList().Count > 0)
                    _viewModel.Data.CRATE_WG_LIST = _viewModel.RateWGDetailList.ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task RateWGDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (LMM01020DTO)eventArgs.Parameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01021DTO>(loEventParam);

                await _viewModel.GetRateWGDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateWGDetailList;
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
        private void RateWGDetail_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeePctEnable = false;
            _RateWGDetail_conductorRef.R_SaveBatch();
        }
        private void RateUCDetail_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeePctEnable = false;
        }
        private void RateWGDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            _viewModel.Data.CRATE_WG_LIST = new List<LMM01021DTO>();
            _viewModel.Data.CRATE_WG_LIST = (List<LMM01021DTO>)eventArgs.Data;
        }

        private async Task RateWGDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel = false;
            var loData = (LMM01021DTO)eventArgs.Data;
            try
            {
                foreach (var item in _viewModel.Data.CRATE_WG_LIST)
                {
                    lCancel = item.IUP_TO_USAGE == loData.IUP_TO_USAGE;
                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Duplicate Usage (Kwh)", R_eMessageBoxButtonType.OK);
                    }
                }
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
    }
}
