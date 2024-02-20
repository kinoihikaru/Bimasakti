using APM00300COMMON;
using APM00300FrontResources;
using APM00300MODEL;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace APM00300FRONT
{
    public partial class APM00320 : R_Page
    {
        private APM00320ViewModel _Supplier_viewModel = new APM00320ViewModel();
        private R_Conductor _Supplier_conductorRef;


        [Inject] IClientHelper clientHelper { get; set; }

        private bool _EnableSaveHasData = false;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)poParameter;

                if (!string.IsNullOrWhiteSpace(loData.CSUPPLIER_ID))
                {
                    // set Has Data
                    _Supplier_viewModel.SupplierRecId = loData.CREC_ID;
                    _Supplier_viewModel.SupplierID = loData.CSUPPLIER_ID;
                    _Supplier_viewModel.SupplierName = loData.CSUPPLIER_NAME;

                    await _Supplier_viewModel.GetSupplierSeqList();
                    await _Supplier_viewModel.GetTaxTypeList();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Supplier_SetHasData(R_SetEventArgs eventArgs)
        {
            _EnableSaveHasData = eventArgs.Enable;
        }

        private async Task Supplier_BtnRefreshOnClick()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_Supplier_viewModel.RecId))
                {
                    await _Supplier_conductorRef.R_GetEntity(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private R_TabStrip _tabSupplierDetailChill;

        private async Task Supplier_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.GetSupplier();

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void cityCode_OnLostFocus(object poParam)
        {
            //_Supplier_viewModel.Data.CCITY_CODE = (string)poParam;
        }
        private void Supplier_City_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL02000);
        }

        private void Supplier_City_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL02000DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }

                var loData = (APM00320DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CCITY_CODE = loTempResult.CCODE;
                loData.CCITY_NAME = loTempResult.CNAME;
                loData.CSTATE_CODE = loTempResult.CCODE_PROVINCE;
                loData.CSTATE_NAME = loTempResult.CNAME_PROVINCE;
                loData.CCOUNTRY_CODE = loTempResult.CCODE_COUNTRY;
                loData.CCOUNTRY_NAME = loTempResult.CNAME_COUNTRY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

      

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void Supplier_Validation(APM00320DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = poParam;

                bool lCancel;

                lCancel = string.IsNullOrEmpty(loData.CADDRESS);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "311"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCITY_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "312"));
                }

                lCancel = string.IsNullOrEmpty(loData.CPHONE1);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "313"));
                }

                lCancel = string.IsNullOrEmpty(loData.CEMAIL1) && IsValidEmail(loData.CEMAIL1);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "314"));
                }

                lCancel = string.IsNullOrEmpty(loData.CEMAIL2) && IsValidEmail(loData.CEMAIL2);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "315"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCONTACT_EMAIL1) && IsValidEmail(loData.CCONTACT_EMAIL1);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "316"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCONTACT_EMAIL2) && IsValidEmail(loData.CCONTACT_EMAIL2);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "317"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_TYPE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "318"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_NAME);

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "319"));
                    }
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_REG_ID);

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "320"));
                    }
                }
                lCancel = _Supplier_viewModel.TaxRegDate == DateTime.MinValue || string.IsNullOrWhiteSpace(_Supplier_viewModel.TaxRegDate.ToString(""));

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "321"));
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParamData = (APM00320DTO)_Supplier_conductorRef.R_GetCurrentData();
                Supplier_Validation(loParamData);

                var loData = await _Supplier_viewModel.SaveSupplier(loParamData);
                await R_MessageBox.Show("", "OneTime modify Successfully”", R_eMessageBoxButtonType.OK);

                await this.Close(true, loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Button_OnClickCancelAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await this.Close(true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
