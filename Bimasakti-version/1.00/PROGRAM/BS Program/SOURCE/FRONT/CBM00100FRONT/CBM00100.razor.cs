using BlazorClientHelper;
using CBM00100COMMON;
using CBM00100FrontResources;
using CBM00100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;

namespace CBM00100FRONT
{
    public partial class CBM00100 : R_Page
    {
        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<CBM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        private CBM00100ViewModel _viewModel = new CBM00100ViewModel();
        private R_Conductor _conductorRef;
        private R_TextBox _CurrencyRateType;

        protected async override Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loValidate = await _viewModel.GetInitialValidate();
                if (loValidate == null)
                {
                    loResult = await PopupService.Show(typeof(CBM00110), null);
                    if (loResult.Success == false)
                    {
                        await this.CloseProgram();
                    }

                    if (loResult.Result != null)
                    {
                        await _conductorRef.R_GetEntity(new CBM00100DTO());

                    }
                }
                else
                {
                    await _conductorRef.R_GetEntity(new CBM00100DTO());
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_MODULE_NAME = "CB";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (CBM00100DTO)eventArgs.Data;

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
                        Program_Id = "CBM00100",
                        Table_Name = "CBM_SYSTEM_PARAM",
                        Key_Value = string.Join("|", clientHelper.CompanyId)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "CBM00100",
                        Table_Name = "CBM_SYSTEM_PARAM",
                        Key_Value = string.Join("|", clientHelper.CompanyId)
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
        #endregion

        #region Form
        private async Task SystemParamCB_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetSystemParamCB();

                eventArgs.Result = _viewModel.SystemParameterCB;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task SystemParamCB_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                if (true)
                {
                    await _CurrencyRateType.FocusAsync();
                }
            }
        }
        private void SystemParamCB_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (CBM00100DTO)eventArgs.Data;

                lCancel = string.IsNullOrEmpty(loData.CRATETYPE_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                }

                lCancel = _viewModel.CBLinkDate == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V02"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCONTRA_ACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V03"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCRDVG_ACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V04"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCRDVL_ACCOUNT_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V05"));
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task SystemParamCB_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveSystemParamCB((CBM00100DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.SystemParameterCB;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task SystemParamCB_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N01"], R_eMessageBoxButtonType.YesNo);
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Currency Rate Type Lookup
        private void R_Before_Open_LookupCurrRateType(R_BeforeOpenLookupEventArgs eventArgs)
        {
           
            GSL00800ParameterDTO loParam = new GSL00800ParameterDTO();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00800);
        }

        private void R_After_Open_LookupCurrRateType(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00800DTO loTempResult = (GSL00800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (CBM00100DTO)_conductorRef.R_GetCurrentData();
            loData.CRATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            loData.CRATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }
        #endregion

        #region Contra Account Lookup
        private void R_Before_Open_LookupGLAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
            {
                CPROGRAM_CODE = "CBM00100"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00500);
        }

        private void R_After_Open_LookupContraAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (CBM00100DTO)_conductorRef.R_GetCurrentData();
            loData.CCONTRA_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loData.CCONTRA_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion

        #region Forex Gain Account Lookup
        private void R_After_Open_LookupForexGainAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (CBM00100DTO)_conductorRef.R_GetCurrentData();
            loData.CCRDVG_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loData.CCRDVG_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion

        #region Forex Loss Account Lookup
        private void R_After_Open_ForexLossAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (CBM00100DTO)_conductorRef.R_GetCurrentData();
            loData.CCRDVL_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loData.CCRDVL_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion
    }
}
