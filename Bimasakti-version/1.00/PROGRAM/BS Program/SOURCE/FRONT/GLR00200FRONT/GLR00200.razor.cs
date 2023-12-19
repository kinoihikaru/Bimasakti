using BlazorClientHelper;
using GLR00200COMMON;
using GLR00200MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace GLR00200FRONT
{
    public partial class GLR00200 : R_Page
    {
        private GLR00200ViewModel _viewModel = new GLR00200ViewModel();
        
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private R_RadioGroup<RadioModel, string> ReportOptions_RadioGrp;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetSystemParam();
                await _viewModel.GetInitialVar();
                await _viewModel.GetPrintMethod();
                await _viewModel.GetMinMaxAccount();

                _viewModel.IYEAR = int.Parse(_viewModel.SystemParam.CSOFT_PERIOD_YY);
                _viewModel.Data.CFROM_ACCOUNT_NO = _viewModel.MinMaxAccount.CMAX_GLACCOUNT_NO;
                _viewModel.Data.CTO_ACCOUNT_NO = _viewModel.MinMaxAccount.CMIN_GLACCOUNT_NO;

                await _viewModel.GetPriod(_viewModel.IYEAR);
                _viewModel.Data.CFROM_PERIOD_NO = _viewModel.PeriodFromToList.FirstOrDefault().CPERIOD_NO;
                _viewModel.Data.CTO_PERIOD_NO = _viewModel.PeriodFromToList.FirstOrDefault().CPERIOD_NO;

                await ReportOptions_RadioGrp.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Lost Focus
        private void FromAccount_OnLostFocus(object poParam)
        {
            //_viewModel.Data.CFROM_ACCOUNT_NO = (string)poParam;
        }
        private void ToAccount_OnLostFocus(object poParam)
        {
            //_viewModel.Data.CTO_ACCOUNT_NO = (string)poParam;
        }
        private void FromCenter_OnLostFocus(object poParam)
        {
            //_viewModel.Data.CFROM_CENTER_CODE = (string)poParam;
        }
        private void ToCenter_OnLostFocus(object poParam)
        {
            //_viewModel.Data.CTO_CENTER_CODE = (string)poParam;
        }
        #endregion
        private void GLR00200_Account_Before_Open_Lookup(R_BeforeOpenLookupEventArgs e)
        {
            var loParam = new GSL00510ParameterDTO() { CGLACCOUNT_TYPE = "N" };

            e.Parameter = loParam;
            e.TargetPageType = typeof(GSL00510);
        }

        private void GLR00200_Center_Before_Open_Lookup(R_BeforeOpenLookupEventArgs e)
        {
            e.TargetPageType = typeof(GSL00900);
        }

        private void GLR00200_Account_From_After_Open_Lookup(R_AfterOpenLookupEventArgs e)
        {
            var loData = (GSL00510DTO)e.Result;
            if (loData == null)
            {
                return; 
            }

            _viewModel.Data.CFROM_ACCOUNT_NO = loData.CGLACCOUNT_NO;
            _viewModel.FromAccountName = loData.CGLACCOUNT_NAME;
        }

        private void GLR00200_Account_To_After_Open_Lookup(R_AfterOpenLookupEventArgs e)
        {
            var loData = (GSL00510DTO)e.Result;
            if (loData == null)
            {
                return;
            }

            _viewModel.Data.CTO_ACCOUNT_NO = loData.CGLACCOUNT_NO;
            _viewModel.ToAccountName = loData.CGLACCOUNT_NAME;
        }

        private void GLR00200_Center_From_After_Open_Lookup(R_AfterOpenLookupEventArgs e)
        {
            var loData = (GSL00900DTO)e.Result;
            if (loData == null)
            {
                return;
            }

            _viewModel.Data.CFROM_CENTER_CODE = loData.CCENTER_CODE;
            _viewModel.FromCenterName = loData.CCENTER_NAME;
        }

        private void GLR00200_Center_To_After_Open_Lookup(R_AfterOpenLookupEventArgs e)
        {
            var loData = (GSL00900DTO)e.Result;
            if (loData == null)
            {
                return;
            }

            _viewModel.Data.CTO_CENTER_CODE = loData.CCENTER_CODE;
            _viewModel.ToCenterName = loData.CCENTER_NAME;
        }

        private void PriodMode_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Data.CPERIOD_MODE = poParam;
                _viewModel.Data.LINCLUDE_AUDIT_JRN = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task GLR00200_YearOnCHange(int poYear)
        {
            _viewModel.IYEAR = poYear;
            await _viewModel.GetPriod(poYear);
        }
        private void PrintByCenter_OnChange(bool poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.LPrintbyCenter = poParam;
                _viewModel.Data.CFROM_CENTER_CODE = "";
                _viewModel.Data.CTO_CENTER_CODE = "";
                _viewModel.FromCenterName = "";
                _viewModel.ToCenterName = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GLR00200PrintParamDTO)_viewModel.R_GetCurrentData();

                // Set Data
                loData.CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName;
                loData.CUSER_ID = clientHelper.UserId;
                loData.CCOMPANY_ID = clientHelper.CompanyId;

                if (loData.CPERIOD_MODE == "P")
                {
                    loData.CFROM_DATE = "";
                    loData.CTO_DATE = "";

                    loData.CYEAR = _viewModel.IYEAR.ToString();
                }
                else if (loData.CPERIOD_MODE == "D")
                {
                    loData.CYEAR = "";
                    loData.CFROM_PERIOD_NO = "";
                    loData.CTO_PERIOD_NO = "";

                    loData.CFROM_DATE = _viewModel.IFROMDATE.ToString("yyyyMMdd");
                    loData.CTO_DATE = _viewModel.ITODATE.ToString("yyyyMMdd");
                }

                await _viewModel.ValidationGLAccountLedger(loData);

                var loValidate = await R_MessageBox.Show("", "Are you sure to print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    if (_viewModel.LPrintbyCenter)
                    {
                        await _reportService.GetReport(
                                       "R_DefaultServiceUrlGL",
                                       "GL",
                                       "rpt/GLR00210Print/AllGLAccountLedgerPost",
                                       "rpt/GLR00210Print/AllStreamGLAccountLedgersGet",
                                       loData);
                    }
                    else
                    {
                        await _reportService.GetReport(
                                        "R_DefaultServiceUrlGL",
                                        "GL",
                                        "rpt/GLR00200Print/AllGLAccountLedgerPost",
                                        "rpt/GLR00200Print/AllStreamGLAccountLedgersGet",
                                        loData);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
