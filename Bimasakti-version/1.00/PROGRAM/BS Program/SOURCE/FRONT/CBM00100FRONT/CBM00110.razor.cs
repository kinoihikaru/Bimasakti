using CBM00100COMMON;
using CBM00100FrontResources;
using CBM00100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace CBM00100FRONT
{
    public partial class CBM00110 : R_Page
    {
        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<CBM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        #endregion

        private CBM00110ViewModel _viewModel = new CBM00110ViewModel();
        private R_TextBox _CurrencyRateType;

        protected async override Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.SystemParameterCB.CBANK_IN_MODE = "D";
                await _CurrencyRateType.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnCreate_OnClick()
        {
            var loEx = new R_Exception();
            bool llValidate = false;

            try
            {
                if (string.IsNullOrEmpty(_viewModel.SystemParameterCB.CRATETYPE_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                    llValidate = true;
                }

                if (_viewModel.CBLinkDate == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V02"));
                    llValidate = true;
                }

                if (string.IsNullOrEmpty(_viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V03"));
                    llValidate = true;
                }

                if (string.IsNullOrEmpty(_viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V04"));
                    llValidate = true;
                }

                if (string.IsNullOrEmpty(_viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NO))
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V05"));
                    llValidate = true;
                }

                if (llValidate)
                {
                    goto EndBlock;
                }
                else
                {
                    var loData = await _viewModel.CreateSystemParamCB();
                    await R_MessageBox.Show("", _localizer["_N02"], R_eMessageBoxButtonType.OK);
                    await this.Close(true, loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnCancel_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N01"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await this.Close(false, null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region CurrencyRateType Lookup
        private async Task CurrencyRateType_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.SystemParameterCB.CRATETYPE_CODE.Length > 0)
                {
                    GSL00800ParameterDTO loParam = new GSL00800ParameterDTO() { CSEARCH_TEXT = _viewModel.SystemParameterCB.CRATETYPE_CODE };

                    LookupGSL00800ViewModel loLookupViewModel = new LookupGSL00800ViewModel();

                    var loResult = await loLookupViewModel.GetCurrencyRateType(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterCB.CRATETYPE_DESCRIPTION = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterCB.CRATETYPE_CODE = loResult.CRATETYPE_CODE;
                    _viewModel.SystemParameterCB.CRATETYPE_DESCRIPTION = loResult.CRATETYPE_DESCRIPTION;
                }
                else
                {
                    _viewModel.SystemParameterCB.CRATETYPE_DESCRIPTION = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
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
            _viewModel.SystemParameterCB.CRATETYPE_CODE = loTempResult.CRATETYPE_CODE;
            _viewModel.SystemParameterCB.CRATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;
        }
        #endregion

        #region Contra Account Lookup
        private async Task ContraAccount_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NO.Length > 0)
                {
                    GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
                    {
                        CPROGRAM_CODE = "GSM06001",
                        CGOA_CODE = "TRF",
                        CSEARCH_TEXT = _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NO
                    };

                    LookupGSL00500ViewModel loLookupViewModel = new LookupGSL00500ViewModel();

                    var loResult = await loLookupViewModel.GetGLAccount(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NO = loResult.CGLACCOUNT_NO;
                    _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                }
                else
                {
                    _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Contact_Before_Open_LookupGLAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
            {
                CPROGRAM_CODE = "GSM06001",
                CGOA_CODE = "TRF"
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
            _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            _viewModel.SystemParameterCB.CCONTRA_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion

        #region Forex Gain Account Lookup
        private async Task ForexGain_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NO.Length > 0)
                {
                    GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
                    {
                        CPROGRAM_CODE = "GSM06001",
                        CGOA_CODE = "CRDVG",
                        CSEARCH_TEXT = _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NO
                    };

                    LookupGSL00500ViewModel loLookupViewModel = new LookupGSL00500ViewModel();

                    var loResult = await loLookupViewModel.GetGLAccount(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NO = loResult.CGLACCOUNT_NO;
                    _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                }
                else
                {
                    _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_ForexGain_Before_Open_LookupGLAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
            {
                CPROGRAM_CODE = "GSM06001",
                CGOA_CODE = "CRDVG"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00500);
        }
        private void R_After_Open_LookupForexGainAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            _viewModel.SystemParameterCB.CCRDVG_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion

        #region Forex Loss Account Lookup
        private async Task ForexLoss_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NO.Length > 0)
                {
                    GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
                    {
                        CPROGRAM_CODE = "GSM06001",
                        CGOA_CODE = "CRDVL",
                        CSEARCH_TEXT = _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NO
                    };

                    LookupGSL00500ViewModel loLookupViewModel = new LookupGSL00500ViewModel();

                    var loResult = await loLookupViewModel.GetGLAccount(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NO = loResult.CGLACCOUNT_NO;
                    _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
                }
                else
                {
                    _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_ForexLoss_Before_Open_LookupGLAccount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00500ParameterDTO loParam = new GSL00500ParameterDTO()
            {
                CPROGRAM_CODE = "GSM06001",
                CGOA_CODE = "CRDVL"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00500);
        }
        private void R_After_Open_ForexLossAccount(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00500DTO loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            _viewModel.SystemParameterCB.CCRDVL_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        }
        #endregion
    }
}
