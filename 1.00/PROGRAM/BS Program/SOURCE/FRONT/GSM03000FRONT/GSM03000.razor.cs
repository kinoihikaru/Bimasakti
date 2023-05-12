using BlazorClientHelper;
using GSM03000Common.DTOs;
using GSM03000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM03000FRONT
{
    public partial class GSM03000 : R_Page
    {
        private GSM03000ViewModel _viewModel = new GSM03000ViewModel();
        private R_Grid<GSM03000DTO> _gridRef;

        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetPropertyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_OnChange(object poParam)
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

            R_DisplayException(loEx);
        }
        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000DTO>(eventArgs.Data);

                await _viewModel.GetOtherChargesDetail(loParam);

                eventArgs.Result = _viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.R_SaveValidation((GSM03000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveOtherCharges((GSM03000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM03000DTO)eventArgs.Data;
                await _viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetOtherChargesList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO {
                CPROPERTY_ID = _viewModel.PropertyValueContext,
                CPROGRAM_CODE = "GSM03000",
                CBSIS = "",
                CDBCR = "D",
                LCENTER_RESTR = false,
                LUSER_RESTR = false,
                CCENTER_CODE = "",
                CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
            } ;
            eventArgs.Parameter = param ;
            eventArgs.TargetPageType = typeof(GSL00500);
        }

        private void After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM03000DTO)_conductorRef.R_GetCurrentData();
            loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
        }

        private R_AddButton R_AddBtn;
        private R_Button R_ActiveInActiveBtn;
        private R_Button R_PrintBtn;
        private void R_SetHasData(R_SetEventArgs eventArgs)
        {
            if (R_AddBtn != null)
                R_AddBtn.Enabled = eventArgs.Enable;

            if (R_ActiveInActiveBtn != null)
                R_ActiveInActiveBtn.Enabled = eventArgs.Enable;

            if (R_PrintBtn != null)
                R_PrintBtn.Enabled = eventArgs.Enable;
        }

        private R_Lookup R_LookupBtn;
        private void R_SetAdd(R_SetEventArgs eventArgs)
        {
            if (R_LookupBtn != null)
                R_LookupBtn.Enabled = eventArgs.Enable;
        }

        private void R_SetEdit(R_SetEventArgs eventArgs)
        {
            if (R_LookupBtn != null)
                R_LookupBtn.Enabled = eventArgs.Enable;
        }
    }
}
