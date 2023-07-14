using BlazorClientHelper;
using GSM03000Common.DTOs;
using GSM03000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Base;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Enums;

namespace GSM03000FRONT
{
    public partial class GSM03000 : R_Page
    {
        private GSM03000ViewModel Additional_viewModel = new GSM03000ViewModel();
        private R_Grid<GSM03000DTO> Additional_gridRef = new();

        private GSM03000ViewModelDeducation Deducation_viewModel = new GSM03000ViewModelDeducation();
        private R_Grid<GSM03000DTO> Deducation_gridRef = new();

        private R_Conductor Additional_conductorRef;
        private R_Conductor Deducation_conductorRef;

        private string Deducation_lcLabel = "Activate";
        private string Additional_lcLabel = "Activate";
        
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
                if (Additional_viewModel.PropertyList.Count > 0)
                {
                    GSM03000PropertyDTO loParam = new GSM03000PropertyDTO();
                    loParam = Additional_viewModel.PropertyList.FirstOrDefault();
                    Additional_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(null);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Additional_viewModel.GetPropertyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabStrip TabParent;
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                Deducation_viewModel.PropertyValueContext = Additional_viewModel.PropertyValueContext;
                switch (TabParent.ActiveTabIndex)
                {
                    case 0:
                        await Additional_gridRef.R_RefreshGrid(null);
                        break;
                    case 1:
                        await Deducation_gridRef.R_RefreshGrid(null);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Additional
        private void Additional_R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM03000DTO)eventArgs.Data;

                if (loParam.CSTATUS != "80")
                {
                    Additional_lcLabel = "Activate";
                    Additional_viewModel.StatusChange = "80";
                }
                else
                {
                    Additional_lcLabel = "Inactive";
                    Additional_viewModel.StatusChange = "90";
                }
            }
        }

        private async Task Additional_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000DTO>(eventArgs.Data);

                await Additional_viewModel.GetOtherChargesDetail(loParam);

                eventArgs.Result = Additional_viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Additional_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Additional_viewModel.GetOtherChargesList();

                eventArgs.ListEntityResult = Additional_viewModel.OtherChargeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Additional_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Additional_viewModel.SaveOtherCharges((GSM03000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = Additional_viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Additional_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM03000DTO)eventArgs.Data;
                await Additional_viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Additional_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO
            {
                CPROPERTY_ID = Additional_viewModel.PropertyValueContext,
                CPROGRAM_CODE = "GSM03000",
                CBSIS = "",
                CDBCR = "D",
                LCENTER_RESTR = false,
                LUSER_RESTR = false,
                CCENTER_CODE = "",
                CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
            };
            eventArgs.Parameter = param;

            eventArgs.TargetPageType = typeof(GSL00500);
        }

        private void Additional_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            Additional_viewModel.Data.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            Additional_viewModel.Data.CGLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
        }

        private void Additional_R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
             eventArgs.Allow = !string.IsNullOrEmpty(Additional_viewModel.PropertyValueContext);
        }

        private R_Popup Additional_R_ActiveInActiveBtn;
        private void Additional_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "GSM03001";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task Additional_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = Additional_viewModel.Data.CCHARGES_ID;
            var loParam = new GSM03000ActiveParameterDTO()
            {
                CCHARGES_ID = loGetData,
            };

            var loGetDataParam = (GSM03000DTO)Additional_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await Additional_viewModel.ActiveInactiveProcessAsync(loParam);
                    await Additional_conductorRef.R_GetEntity(loGetDataParam);
                    await Additional_conductorRef.R_SetCurrentData(Additional_viewModel.OtherChargesDetail);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private R_Button Additional_R_PrintBtn;

        private void Additional_R_SetHasData(R_SetEventArgs eventArgs)
        {
            if (Additional_R_ActiveInActiveBtn != null)
                Additional_R_ActiveInActiveBtn.Enabled = eventArgs.Enable;

            if (Additional_R_PrintBtn != null)
                Additional_R_PrintBtn.Enabled = eventArgs.Enable;
        }

        private async Task Additional_R_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var llCancel = Additional_viewModel.Data.CSTATUS != "00";

                if (llCancel)
                {
                    eventArgs.Cancel = llCancel;
                    await R_MessageBox.Show("", "Cannot Delete, this charges status is not draft", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox Additional_ChargesIdTextBox;
        private async Task Additional_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Add)
            {
                await Additional_ChargesIdTextBox.FocusAsync();
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await Additional_ChargesIdTextBox.FocusAsync();
            }
        }

        #endregion

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                switch (TabParent.ActiveTabIndex)
                {
                    case 0:
                        await Additional_gridRef.R_RefreshGrid(null);
                        break;
                    case 1:
                        await Deducation_gridRef.R_RefreshGrid(null);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Deducation
        private async Task GridDeducation_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Deducation_viewModel.GetOtherChargesListDeducation();

                eventArgs.ListEntityResult = Deducation_viewModel.OtherChargeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void GridDeducation_R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM03000DTO)eventArgs.Data;

                if (loParam.CSTATUS != "80")
                {
                    Deducation_lcLabel = "Activate";
                    Deducation_viewModel.StatusChange = "80";
                }
                else
                {
                    Deducation_lcLabel = "Inactive";
                    Deducation_viewModel.StatusChange = "90";
                }
            }
        }

        private async Task GridDeducation_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000DTO>(eventArgs.Data);

                await Deducation_viewModel.GetOtherChargesDetail(loParam);

                eventArgs.Result = Deducation_viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GridDeducation_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Deducation_viewModel.SaveOtherCharges((GSM03000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = Deducation_viewModel.OtherChargesDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GridDeducation_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM03000DTO)eventArgs.Data;
                await Deducation_viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void GridDeducation_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO
            {
                CPROPERTY_ID = Additional_viewModel.PropertyValueContext,
                CPROGRAM_CODE = "GSM03000",
                CBSIS = "",
                CDBCR = "C",
                LCENTER_RESTR = false,
                LUSER_RESTR = false,
                CCENTER_CODE = "",
                CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00500);
        }

        private void GridDeducation_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            Deducation_viewModel.Data.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            Deducation_viewModel.Data.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }

        private void GridDeducation_R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = !string.IsNullOrEmpty(Additional_viewModel.PropertyValueContext);
        }

        private R_Popup R_ActiveInActiveBtn;
        private void GridDeducation_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "GSM03001";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task GridDeducation_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = Deducation_viewModel.Data.CCHARGES_ID;
            var loParam = new GSM03000ActiveParameterDTO()
            {
                CCHARGES_ID = loGetData,
            };
            var loGetDataParam = (GSM03000DTO)Additional_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await Deducation_viewModel.ActiveInactiveProcessAsync(loParam);
                    await Additional_conductorRef.R_GetEntity(loGetDataParam);
                    await Additional_conductorRef.R_SetCurrentData(Deducation_viewModel.OtherChargesDetail);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private R_Button R_PrintBtn;
        private void GridDeducation_R_SetHasData(R_SetEventArgs eventArgs)
        {
            if (R_ActiveInActiveBtn != null)
                R_ActiveInActiveBtn.Enabled = eventArgs.Enable;

            if (R_PrintBtn != null)
                R_PrintBtn.Enabled = eventArgs.Enable;
        }

        private R_TextBox Deducation_ChargesIdTextBox;
        private void GridDeducation_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            Deducation_ChargesIdTextBox.FocusAsync();
        }

        private void GridDeducation_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            Deducation_ChargesIdTextBox.FocusAsync();
        }
        #endregion
    }
}
