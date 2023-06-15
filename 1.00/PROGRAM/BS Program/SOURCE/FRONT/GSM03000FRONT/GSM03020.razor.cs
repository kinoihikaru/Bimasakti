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
using System.Reflection.Emit;

namespace GSM03000FRONT
{
    public partial class GSM03020 : R_Page
    {
        private GSM03000ViewModelDeducation Deducation_viewModel = new GSM03000ViewModelDeducation();
        private R_Grid<GSM03000DTO> Deducation_gridRef = new();
        private R_Conductor Deducation_conductorRef;

        private string Deducation_lcLabel = "Activate";
        
        [Inject] IClientHelper clientHelper { get; set; }
        [Parameter] public string PropertyIdVal { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await Deducation_gridRef.R_RefreshGrid(null);
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
                CPROPERTY_ID = PropertyIdVal,
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
            eventArgs.Allow = !string.IsNullOrEmpty(PropertyIdVal);
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

            R_Exception loException = new R_Exception();
            try
            {
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await Deducation_viewModel.ActiveInactiveProcessAsync(loParam);
                }

                await Deducation_gridRef.R_RefreshGrid(null);
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
