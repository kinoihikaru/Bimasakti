using APM00300COMMON;
using APM00300FrontResources;
using APM00300MODEL;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

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

        protected async override Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;

            try
            {
                //if (_Supplier_conductorRef.R_ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                //{
                //    await _Supplier_conductorRef.Cancel();
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_MODULE_NAME = "AP";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (APM00320DTO)eventArgs.Data;
                var loSeqID = _Supplier_viewModel.SupplierSeqList.FirstOrDefault(x => x.CREC_ID == _Supplier_viewModel.RecId);

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
                        Program_Id = "APM00320",
                        Table_Name = "APM_SUPPLIER_INFO",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID, loSeqID.CSEQ_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APM00320",
                        Table_Name = "APM_SUPPLIER_INFO",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID, loSeqID.CSEQ_NO)
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
        
        private async Task Supplier_BtnRefreshOnClick()
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_conductorRef.R_GetEntity(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

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
        private void Supplier_SetHasData(R_SetEventArgs eventArgs)
        {
            _EnableSaveHasData = eventArgs.Enable;
        }
        private async Task Supplier_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.SaveSupplier((APM00320DTO)eventArgs.Data);

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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

        private void Supplier_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00320DTO)eventArgs.Data;

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

        //private async Task Button_OnClickCancelAsync()
        //{
        //    var loEx = new R_Exception();

        //    try
        //    {
        //        if (_Supplier_conductorRef.R_ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
        //        {
        //            await _Supplier_conductorRef.Cancel();
        //        }
        //        await this.Close(true, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}
    }
}
