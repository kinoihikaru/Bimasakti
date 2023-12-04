using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM03000Common.DTOs;
using GSM03000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

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
        [Inject] private R_PopupService PopupService { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
                if (Additional_viewModel.PropertyList.Count > 0)
                {
                    GSM03000PropertyDTO loParam = Additional_viewModel.PropertyList.FirstOrDefault();
                    Additional_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
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
        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                Additional_viewModel.PropertyValueContext = poParam;
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
        private async Task Additional_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM03000DTO)eventArgs.Data;

                    if (loParam.LACTIVE)
                    {
                            Additional_lcLabel = "Inactive";
                            Additional_viewModel.StatusChange = false;
                    }
                    else
                    {
                        Additional_lcLabel = "Activate";
                        Additional_viewModel.StatusChange = true;
                    }
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await Additional_ChargesNameTextBox.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

            await Task.CompletedTask;
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

        private async Task Additional_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Additional_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                await Additional_viewModel.ValidationOtherChargesAdditioanl((GSM03000DTO)eventArgs.Data);

                var loGetDataParam = (GSM03000DTO)eventArgs.Data;
                var loParam = new GSM03000ActiveParameterDTO()
                {
                    CCHARGES_ID = loGetDataParam.CCHARGES_ID,
                };

                if (eventArgs.ConductorMode == R_eConductorMode.Add && loGetDataParam.LACTIVE)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM03001"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        var loData = await Additional_viewModel.ActiveInactiveProcessAsync(loParam);
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        var loPopupParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM03001" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                        if (loResult.Success == false)
                        {
                            eventArgs.Cancel = true;
                            return;
                        }

                        bool result = (bool)loResult.Result;
                        if (result == true)
                        {
                            var loData = await Additional_viewModel.ActiveInactiveProcessAsync(loParam);
                        }
                        else
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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

        private void AdditionalGLAccount_OnLostFocus(object poParam)
        {
            //Additional_viewModel.Data.CGLACCOUNT_NO = (string)poParam;
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

        private async Task Additional_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = Additional_viewModel.Data.CCHARGES_ID;
                var loParam = new GSM03000ActiveParameterDTO()
                {
                    CCHARGES_ID = loGetData,
                };

                var loGetDataParam = (GSM03000DTO)Additional_viewModel.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM03001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    var loData = await Additional_viewModel.ActiveInactiveProcessAsync(loParam);
                    await Additional_conductorRef.R_SetCurrentData(loData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM03001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

        }

        private async Task Additional_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (GSM03000DTO)Additional_conductorRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000ActiveParameterDTO>(loGetData);

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loData = await Additional_viewModel.ActiveInactiveProcessAsync(loParam);
                    await Additional_conductorRef.R_SetCurrentData(loData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        private void Additional_R_SetHasData(R_SetEventArgs eventArgs)
        {
        }

        private R_TextBox Additional_ChargesIdTextBox;
        private R_TextBox Additional_ChargesNameTextBox;

        private async Task Additioanl_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await Additional_ChargesIdTextBox.FocusAsync();
        }

        private void Additional_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (GSM03000DTO)Deducation_conductorRef.R_GetCurrentData();
            loParam.CCHARGES_TYPE = "A";
            loParam.CPROPERTY_ID = Additional_viewModel.PropertyValueContext;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM03001);
        }

        private void Additional_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = eventArgs.Enable;
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

        private bool _pageSupplierOnCRUDmode = false;
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = !_pageSupplierOnCRUDmode;
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

        private async Task GridDeducation_R_Display(R_DisplayEventArgs eventArgs)
        {

            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM03000DTO)eventArgs.Data;

                if (loParam.LACTIVE)
                {
                    Deducation_lcLabel = "Inactive";
                    Deducation_viewModel.StatusChange = false;
                }
                else
                {
                    Additional_lcLabel = "Activate";
                    Deducation_viewModel.StatusChange = true;
                }
            }

            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await Deducation_ChargesNameTextBox.FocusAsync();
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

        private async Task GridDeducation_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task GridDeducation_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                await Deducation_viewModel.ValidationOtherChargesDeducation((GSM03000DTO)eventArgs.Data);

                var loGetDataParam = (GSM03000DTO)eventArgs.Data;
                var loParam = new GSM03000ActiveParameterDTO()
                {
                    CCHARGES_ID = loGetDataParam.CCHARGES_ID,
                };

                if (eventArgs.ConductorMode == R_eConductorMode.Add && loGetDataParam.LACTIVE)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM03001"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        var loData = await Deducation_viewModel.ActiveInactiveProcessAsync(loParam); //Ganti jadi method ActiveInactive masing masing
                        await Deducation_conductorRef.R_SetCurrentData(loData);
                        return;
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        var loPopupParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM03001" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                        if (loResult.Success == false)
                        {
                            eventArgs.Cancel = true;
                            return;
                        }

                        bool result = (bool)loResult.Result;
                        if (result == true)
                        {
                            var loData = await Deducation_viewModel.ActiveInactiveProcessAsync(loParam); //Ganti jadi method ActiveInactive masing masing
                            await Deducation_conductorRef.R_SetCurrentData(loData);
                        }
                        else
                        {
                            eventArgs.Cancel = true;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

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

            R_DisplayException(loEx);

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

            R_DisplayException(loEx);

        }
        private void DeducationGLAccount_OnLostFocus(object poParam)
        {
            //Deducation_viewModel.Data.CGLACCOUNT_NO = (string)poParam;
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
        private async Task GridDeducation_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = Deducation_viewModel.Data.CCHARGES_ID;
                var loParam = new GSM03000ActiveParameterDTO()
                {
                    CCHARGES_ID = loGetData,
                };

                var loGetDataParam = (GSM03000DTO)Deducation_viewModel.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM03001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    var loData = await Deducation_viewModel.ActiveInactiveProcessAsync(loParam); //Ganti jadi method ActiveInactive masing masing
                    await Deducation_conductorRef.R_SetCurrentData(loData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM03001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task GridDeducation_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (GSM03000DTO)Deducation_conductorRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM03000ActiveParameterDTO>(loGetData);


            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loData = await Deducation_viewModel.ActiveInactiveProcessAsync(loParam); //Ganti jadi method ActiveInactive masing masing
                    await Deducation_conductorRef.R_SetCurrentData(loData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void GridDeducation_R_SetHasData(R_SetEventArgs eventArgs)
        {
        }

        private R_TextBox Deducation_ChargesIdTextBox;
        private R_TextBox Deducation_ChargesNameTextBox;
        private void GridDeducation_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            Deducation_ChargesIdTextBox.FocusAsync();
        }

        private void GridDeducation_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (GSM03000DTO)Deducation_conductorRef.R_GetCurrentData();
            loParam.CCHARGES_TYPE = "D";
            loParam.CPROPERTY_ID = Additional_viewModel.PropertyValueContext;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM03001);
        }
        private void GridDeducation_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = eventArgs.Enable;
        }
        #endregion
    }
}
