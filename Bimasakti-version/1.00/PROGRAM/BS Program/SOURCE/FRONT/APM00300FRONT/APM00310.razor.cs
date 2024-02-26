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
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;

namespace APM00300FRONT
{
    public partial class APM00310 : R_Page
    {
        private APM00310ViewModel _Supplier_viewModel = new APM00310ViewModel();
        private R_Conductor _Supplier_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Param
                var loData = R_FrontUtility.ConvertObjectToObject<APM00310DTO>(poParameter);

                // Combo box
                await _Supplier_viewModel.GetLOBList();
                await _Supplier_viewModel.GetPropertyList();
                await _Supplier_viewModel.GetCurrencyList();

                if (!string.IsNullOrWhiteSpace(loData.CPROPERTY_ID))
                {
                    _Supplier_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                        await _Supplier_conductorRef.R_GetEntity(loData);
                }
                else
                {
                   var loTempData = _Supplier_viewModel.PropertyList.FirstOrDefault();
                    _Supplier_viewModel.PropertyValueContext = loTempData.CPROPERTY_ID;
                }

                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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
                var loData = (APM00310DTO)eventArgs.Data;

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
                        Program_Id = "APM00310",
                        Table_Name = "APM_SUPPLIER",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID) 
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APM00310",
                        Table_Name = "APM_SUPPLIER",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID)
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
        private async Task PropertyComboBox_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _Supplier_viewModel.PropertyValueContext = poParam is null ? "" : poParam;

                // load Term property onChange
                await _Supplier_viewModel.GetPayTermList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Supplier
        private async Task Supplier_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    await _Supplier_viewModel.GetSupplier(loData);

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Lost Focus
        private void jrnlCode_OnLostFocus(object poParam) 
        {
            //_Supplier_viewModel.Data.CJRNGRP_CODE = (string)poParam;
        }
        private void categoryId_OnLostFocus(object poParam)
        {
            //_Supplier_viewModel.Data.CCATEGORY_ID = (string)poParam;
        }
        private void LOBCode_OnLostFocus(object poParam)
        {
            _Supplier_viewModel.Data.CLOB_CODE = (string)poParam;
        }
        #endregion
        private void Supplier_Journal_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL00400ParameterDTO() { CPROPERTY_ID = _Supplier_viewModel.PropertyValueContext, CJRNGRP_TYPE = "50" };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void Supplier_Journal_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
                loData.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
                loData.CJRNGRP_TYPE = loTempResult.CJRNGRP_TYPE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_Category_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01800DTOParameter() { CPROPERTY_ID = _Supplier_viewModel.PropertyValueContext, CCATEGORY_TYPE = "50" };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void Supplier_Category_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01800DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
                loData.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;
                loData.CCATEGORY_TYPE = loTempResult.CCATEGORY_TYPE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_LOB_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL01900);
        }

        private void Supplier_LOB_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01900DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CLOB_CODE = loTempResult.CLOB_CODE;
                loData.CLOB_NAME = loTempResult.CLOB_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Supplier_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.DeleteSupplier((APM00310DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private R_ComboBox<APM00300PropertyDTO, string> Property_ComboBox;
        private async Task Supplier_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (APM00310DTO)eventArgs.Data;
            loData.DUPDATE_DATE = DateTime.Now;
            loData.DCREATE_DATE = DateTime.Now;

            await Property_ComboBox.FocusAsync();
        }
        private R_TextBox SupplName_TextBox;
        private async Task Supplier_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await SupplName_TextBox.FocusAsync();
            }
        }
        private void Supplier_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;

                bool lCancel;

                lCancel = string.IsNullOrEmpty(_Supplier_viewModel.PropertyValueContext);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "301"));
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "304"));
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "305"));
                }

                lCancel = string.IsNullOrEmpty(loData.CJRNGRP_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "306"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCATEGORY_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "307"));
                }

                lCancel = string.IsNullOrEmpty(loData.CPAY_TERM_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "308"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCURRENCY_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "309"));
                }

                lCancel = string.IsNullOrEmpty(loData.CLOB_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "310"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Supplier_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.SaveSupplier((APM00310DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        private void Supplier_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(APM00340);
        }
        private async Task SupplierDetail_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = R_FrontUtility.ConvertObjectToObject<APM00310DTO>(eventArgs.Result);
                if (loTempResult == null)
                {
                    return;
                }

                await _Supplier_conductorRef.Add();
                if (_Supplier_conductorRef.R_ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add)
                {
                    var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                    loData.CPROPERTY_ID = loTempResult.CPROPERTY_ID;
                    loData.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
                    loData.LONETIME = loTempResult.LONETIME;
                    loData.CPAY_TERM_CODE = loTempResult.CPAY_TERM_CODE;
                    loData.CPAY_TERM_NAME = loTempResult.CPAY_TERM_NAME;
                    loData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
                    loData.CLOB_CODE = loTempResult.CLOB_CODE;
                    loData.CLOB_NAME = loTempResult.CLOB_NAME;
                    loData.CSTATUS = loTempResult.CSTATUS;
                    loData.CDELIVERY_OPTION = loTempResult.CDELIVERY_OPTION;
                    loData.CADDRESS = loTempResult.CADDRESS;
                    loData.CCITY_CODE = loTempResult.CCITY_CODE;
                    loData.CCITY_NAME = loTempResult.CCITY_NAME;
                    loData.CSTATE_CODE = loTempResult.CSTATE_CODE;
                    loData.CPOSTAL_CODE = loTempResult.CPOSTAL_CODE;
                    loData.CCOUNTRY_CODE = loTempResult.CCOUNTRY_CODE;
                    loData.CCOUNTRY_NAME = loTempResult.CCOUNTRY_NAME;
                    loData.CPHONE1 = loTempResult.CPHONE1;
                    loData.CPHONE2 = loTempResult.CPHONE2;
                    loData.CEMAIL1 = loTempResult.CEMAIL1;
                    loData.CEMAIL2 = loTempResult.CEMAIL2;
                    loData.CCONTACT_EMAIL1 = loTempResult.CCONTACT_EMAIL1;
                    loData.CCONTACT_NAME1 = loTempResult.CCONTACT_NAME1;
                    loData.CCONTACT_EMAIL2 = loTempResult.CCONTACT_EMAIL2;
                    loData.CCONTACT_NAME2 = loTempResult.CCONTACT_NAME2;
                    loData.CCONTACT_PHONE1 = loTempResult.CCONTACT_PHONE1;
                    loData.CCONTACT_PHONE2 = loTempResult.CCONTACT_PHONE2;
                    loData.CCONTACT_POSITION1 = loTempResult.CCONTACT_POSITION1;
                    loData.CCONTACT_POSITION2 = loTempResult.CCONTACT_POSITION2;
                    loData.CTAX_NAME = loTempResult.CTAX_NAME;
                    loData.CTAX_TYPE = loTempResult.CTAX_TYPE;
                    loData.CTAX_REG_ID = loTempResult.CTAX_REG_ID;
                    loData.CTAX_REG_DATE = loData.CTAX_REG_DATE;
                    loData.NBALANCE = loTempResult.NBALANCE;
                    loData.NBLAST_PAYMENT = loTempResult.NBLAST_PAYMENT;
                    loData.NBBALANCE = loTempResult.NBBALANCE;
                    loData.NBDP_BALANCE = loTempResult.NBDP_BALANCE;
                    loData.NBPO_BALANCE = loTempResult.NBPO_BALANCE;
                    loData.NLAST_PAYMENT = loTempResult.NLAST_PAYMENT;
                    loData.NLAST_PURCHASE = loTempResult.NLAST_PURCHASE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Supplier_OneTime_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00320);
        }

        private void Supplier_OneTime_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
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
        private void Supplier_Bank_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00330);
        }

        private void Supplier_Bank_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
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

        #region Refresh Tab

        private R_TabStrip _tabSupplierDetailChill;
        private R_TabPage _AddressTabPage;
        private R_TabPage _TaxBalanceTabPage;
        private void Supplier_Address_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00311);
        }

        private void Supplier_TaxBalance_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00312);
        }
        #endregion
    }
}
