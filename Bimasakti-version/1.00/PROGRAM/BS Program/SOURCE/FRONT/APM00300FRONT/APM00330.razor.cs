﻿using APM00300COMMON;
using APM00300FrontResources;
using APM00300MODEL;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
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
    public partial class APM00330 : R_Page
    {
        private APM00330ViewModel _Supplier_viewModel = new APM00330ViewModel();
        private R_Grid<APM00330DTO> _SupllierBank_gridRef;
        private R_Conductor _SupplierBank_conductorRef;

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
                    _Supplier_viewModel.PropertyValue = loData.CPROPERTY_ID;

                    await _SupllierBank_gridRef.R_RefreshGrid(null);
                    await _Supplier_viewModel.GetCurrencyList();
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
                var loData = (APM00330DTO)eventArgs.Data;

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
                        Program_Id = "APM00330",
                        Table_Name = "APM_SUPPLIER_BANK",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID, loData.CBANK_CODE, loData.CBANK_ACCOUNT_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APM00330",
                        Table_Name = "APM_SUPPLIER_BANK",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CSUPPLIER_ID, loData.CBANK_CODE, loData.CBANK_ACCOUNT_NO)
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
        private async Task SupplierBankList_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.GetSupplierBankList();

                eventArgs.ListEntityResult = _Supplier_viewModel.SupplierBankGrid;
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
                eventArgs.Cancel = _SupplierBank_conductorRef.R_ConductorMode != R_BlazorFrontEnd.Enums.R_eConductorMode.Normal;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SupplierBank_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.GetSupplierBank((APM00330DTO)eventArgs.Data);

                eventArgs.Result = _Supplier_viewModel.SupplierBank;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task bankCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00330DTO)_SupplierBank_conductorRef.R_GetCurrentData();
                var loParam = new GSL01200ParameterDTO() { CCB_TYPE = "B", CSEARCH_TEXT = loData.CBANK_CODE };

                LookupGSL01200ViewModel loLookupViewModel = new LookupGSL01200ViewModel();

                var loResult = await loLookupViewModel.GetBank(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loData.CBANK_NAME = "";
                    goto EndBlock;
                }

                loData.CBANK_CODE = loResult.CCB_CODE;
                loData.CBANK_NAME = loResult.CCB_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void cityCode_OnLostFocus(object poParam)
        {
            //_Supplier_viewModel.Data.CCITY_CODE = (string)poParam;
        }
        private void Supplier_Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01200ParameterDTO() { CCB_TYPE = "B" };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void Supplier_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01200DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00330DTO)_SupplierBank_conductorRef.R_GetCurrentData();

                loData.CBANK_CODE = loTempResult.CCB_CODE;
                loData.CBANK_NAME = loTempResult.CCB_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
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
                var loData = (APM00330DTO)_SupplierBank_conductorRef.R_GetCurrentData();

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

        private async Task SupplierBank_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.DeleteSupplierBank((APM00330DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void SupplierBank_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00330DTO)eventArgs.Data;

                bool lCancel;

                lCancel = string.IsNullOrEmpty(loData.CBANK_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "322"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCURRENCY_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "309"));
                }

                lCancel = string.IsNullOrEmpty(loData.CBANK_ACCOUNT_NO);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "323"));
                }

                lCancel = string.IsNullOrEmpty(loData.CBANK_ACCOUNT_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "324"));
                }

                lCancel = string.IsNullOrEmpty(loData.CALIAS_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "325"));
                }

                lCancel = string.IsNullOrEmpty(loData.CBRANCH_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "326"));
                }

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
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SupplierBank_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.SaveSupplierBank((APM00330DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Supplier_viewModel.SupplierBank;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
