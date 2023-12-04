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
    public partial class LMM01040 : R_Page, R_ITabPage
    {
        private string chargesID;

        private LMM01040ViewModel _viewModel = new LMM01040ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateGU_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01040DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<LMM01040DTO>(poParameter);

                await _viewModel.GetProperty(loParam);
                await _Universal_viewModel.GetAdminFeeTypeList();

                await RateGU_CheckData(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateGU_AdminFeeType_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Universal_viewModel.GetAdminFeeTypeList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task RateGU_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateGUCheckData((LMM01040DTO)poParam);

                if (loCheck != null)
                {
                    await _RateGU_conductorRef.R_GetEntity(poParam);
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
                await _viewModel.GetRateUG((LMM01040DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateGU;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private R_NumericTextBox<decimal> StandingCharges_NumericTextBox;
        private async Task RateGU_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await StandingCharges_NumericTextBox.FocusAsync();
            }
        }
        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RateGU_Admin_OnChange(string poParam)
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
        private void RateGU_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }

        private async Task RateGU_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveRateGU((LMM01040DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RateGU;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateGU_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeeAmtEnable = false;
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        private void RateGU_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeeAmtEnable = false;
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01040DTO>(poParam);
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    await _RateGU_conductorRef.R_SetCurrentData(null);
                }
                else
                {
                    await _viewModel.GetProperty(loParam);

                    await RateGU_CheckData(loParam);
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
                var loParam = new LMM01040PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RateGU.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RateGU.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RateGU.CCHARGES_ID,
                    CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME,
                };

                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CUSER_ID = clientHelper.UserId;

                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                     "R_DefaultServiceUrlLM",
                     "LM",
                     "rpt/LMM01040Print/AllRateGUReportPost",
                     "rpt/LMM01040Print/AllStreamRateGUReportGet",
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
