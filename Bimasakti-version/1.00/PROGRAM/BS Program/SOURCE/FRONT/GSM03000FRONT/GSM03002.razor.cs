using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM03000Common.DTOs;
using GSM03000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM03000FRONT
{
    public partial class GSM03002 : R_Page, R_ITabPage
    {

        private GSM03000ViewModelDeducation Deducation_viewModel = new GSM03000ViewModelDeducation();
        private R_Grid<GSM03000DTO> Deducation_gridRef = new();

        private R_Conductor Deducation_conductorRef;

        private string Deducation_lcLabel = "Activate";
        private bool _pageSupplierOnCRUDmode = false;
        private R_Popup R_ActiveInActiveBtn;
        private R_TextBox Deducation_ChargesIdTextBox;
        private R_TextBox Deducation_ChargesNameTextBox;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (string)poParameter;
                Deducation_viewModel.PropertyValueContext = loParam;

                await Deducation_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (GSM03000DTO)eventArgs.Data;

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
                        Program_Id = "GSM03000",
                        Table_Name = "GSM_OTHER_CHARGES",
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
                        Program_Id = "GSM03000",
                        Table_Name = "GSM_OTHER_CHARGES",
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
                    Deducation_lcLabel = "Activate";
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
                CPROPERTY_ID = Deducation_viewModel.PropertyValueContext,
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
            eventArgs.Allow = !string.IsNullOrEmpty(Deducation_viewModel.PropertyValueContext);
        }
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
        private void GridDeducation_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            Deducation_ChargesIdTextBox.FocusAsync();
        }
        private void GridDeducation_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (GSM03000DTO)Deducation_conductorRef.R_GetCurrentData();
            loParam.CCHARGES_TYPE = "D";
            loParam.CPROPERTY_ID = Deducation_viewModel.PropertyValueContext;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM03001);
        }
        private async Task GridDeducation_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        #endregion
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (string)poParam;
                Deducation_viewModel.PropertyValueContext = loParam;

                await Deducation_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
