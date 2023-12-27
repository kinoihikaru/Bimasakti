using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APT00100MODEL.ViewModel;
using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00100;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using Lookup_APCOMMON.DTOs.APL00110;
using R_BlazorFrontEnd.Enums;
using APT00100COMMON.DTOs.APT00130;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.MessageBox;
using GLF00100COMMON;
using GLF00100FRONT;
using APT00100COMMON.DTOs.APT00110Print;
using R_BlazorFrontEnd.Interfaces;

namespace APT00100FRONT
{
    public partial class APT00110 : R_Page
    {
        private APT00110ViewModel loViewModel = new APT00110ViewModel();

        private R_Conductor _conductorRef;

        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private bool IsSupplierEnabled = false;

        private bool IsBaseCurrencyEnabled = false;

        private bool IsLocalCurrencyEnabled = false;

        private bool IsTaxCurrencyEnabled = false;

        private bool IsTaxable = false;

        private bool IsTransStatus00 = false;

        private bool IsTransStatus10 = false;

        private bool IsJournalButtonEnabled = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            InvoiceEntryPredifineParameterDTO loParameter = null;
            try
            {
                loParameter = (InvoiceEntryPredifineParameterDTO)poParameter;
                if (string.IsNullOrWhiteSpace(loParameter.CREC_ID) == false)
                {
                    await loViewModel.GetCompanyInfoAsync();
                    await loViewModel.GetAPSystemParamAsync();
                    await loViewModel.GetPropertyListStreamAsync();
                    await loViewModel.GetCurrencyListStreamAsync();
                    await _conductorRef.R_GetEntity(new APT00110DTO { CREC_ID = loParameter.CREC_ID });
                    await loViewModel.GetPaymentTermListStreamAsync();
                    if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "00")
                    {
                        IsTransStatus00 = true;
                    }
                    else if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "10")
                    {
                        IsTransStatus10 = true;
                    }
                    int lnCompareResult = String.Compare(loViewModel.loInvoiceHeader.CTRANS_STATUS, "00");
                    if (lnCompareResult > 0 && loViewModel.loInvoiceHeader.CGL_REF_NO != "")
                    {
                        IsJournalButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_Before_OpenInvoiceItem_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new InvoiceItemTabParameterDTO()
            {
                CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(APT00111);
        }

        private async Task InvoiceHeader_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00110DTO loData = (APT00110DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                {
                    await loViewModel.GetInvoiceHeaderAsync(loData);
                    eventArgs.Result = loViewModel.loInvoiceHeader;
                }
                else
                {
                    eventArgs.Result = eventArgs.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHeader_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteInvoiceHeaderAsync((APT00110DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHHeader_AfterDelete()
        {
            R_Exception loException = new R_Exception();
            try
            {
                await _conductorRef.R_GetEntity(new APT00110DTO { CREC_ID = loViewModel.loInvoiceHeader.CREC_ID });
            }
            catch (Exception ex)
            {
                loException.Add(ex); 
            }
            loException.ThrowExceptionIfErrors();
        }

        private void InvoiceHeader_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00110DTO loData = (APT00110DTO)eventArgs.Data;
                loViewModel.InvoiceHeaderValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void InvoiceHeader_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            APT00110DTO loData = (APT00110DTO)eventArgs.Data;
            loData.CLOCAL_CURRENCY_CODE = loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE;
            loData.CBASE_CURRENCY_CODE = loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE;
            IsTransStatus00 = false;
            IsTransStatus10 = false;
            IsJournalButtonEnabled = false;
        }

        private void InvoiceHeader_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            IsTaxable = loViewModel.Data.LTAXABLE;
            IsTransStatus00 = false;
            IsTransStatus10 = false;
            IsJournalButtonEnabled = false;
            if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
            {
                IsLocalCurrencyEnabled = true;
                if (loViewModel.Data.LTAXABLE == true)
                {
                    IsTaxCurrencyEnabled = true;
                }
                else
                {
                    IsTaxCurrencyEnabled = false;
                }
            }
            else
            {
                IsTaxCurrencyEnabled = false;
                IsLocalCurrencyEnabled = false;
            }

            if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
            {
                IsBaseCurrencyEnabled = true;
            }
            else
            {
                IsBaseCurrencyEnabled = false;
            }
        }

        private void InvoiceHeader_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsSupplierEnabled = false;
            IsBaseCurrencyEnabled = false;
            IsLocalCurrencyEnabled = false;
            IsTaxCurrencyEnabled = false;
            IsTaxable = false;
            if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "00")
            {
                IsTransStatus00 = true;
            }
            else if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "10")
            {
                IsTransStatus10 = true;
            }
            int lnCompareResult = String.Compare(loViewModel.loInvoiceHeader.CTRANS_STATUS, "00");
            if (lnCompareResult > 0 && loViewModel.loInvoiceHeader.CGL_REF_NO != "")
            {
                IsJournalButtonEnabled = true;
            }
        }

        private async Task InvoiceHeader_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResultDetail = null;
            R_PopupResult loResultSummary = null;

            try
            {
                IsSupplierEnabled = false;
                IsBaseCurrencyEnabled = false;
                IsLocalCurrencyEnabled = false;
                IsTaxCurrencyEnabled = false;
                IsTaxable = false;
                if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "00")
                {
                    IsTransStatus00 = true;
                }
                else if (loViewModel.loInvoiceHeader.CTRANS_STATUS == "10")
                {
                    IsTransStatus10 = true;
                }
                int lnCompareResult = String.Compare(loViewModel.loInvoiceHeader.CTRANS_STATUS, "00");
                if (lnCompareResult > 0 && loViewModel.loInvoiceHeader.CGL_REF_NO != "")
                {
                    IsJournalButtonEnabled = true;
                }
                loResultDetail = await PopupService.Show(typeof(APT00100FRONT.APT00120), new InvoiceItemTabParameterDTO()
                {
                    CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
                });
                loResultSummary = await PopupService.Show(typeof(APT00100FRONT.APT00130), new SummaryParameterDTO()
                {
                    CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
                });

                //if (loResultDetail.Success == false || (bool)loResultDetail.Result == false)
                //{
                //    if (loResultSummary.Success == false || (bool)loResultSummary.Result == false)
                //    {

                //    }
                //    if ((bool)loResultSummary.Result)
                //    {
                //        await R_MessageBox.Show("", "AP System Parameter Created Successfully!", R_eMessageBoxButtonType.OK);
                //    }
                //}
                //if ((bool)loResultDetail.Result)
                //{
                //        await R_MessageBox.Show("", "AP System Parameter Created Successfully!", R_eMessageBoxButtonType.OK);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceHeader_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveInvoiceHeaderAsync((APT00110DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loInvoiceHeader;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Lookup
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loProperty.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.loProperty.CPROPERTY_ID,
                CUSER_LOGIN_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            //var loGetData = (APT00100DTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loViewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                CLANGUAGE_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00100);
        }

        private void R_After_Open_LookupSupplier(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00100DTO loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            loViewModel.Data.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            loViewModel.Data.LONETIME = loTempResult.LONETIME;
            loViewModel.Data.CSUPPLIER_SEQ_NO = "";
            if (_conductorRef.R_ConductorMode == R_eConductorMode.Add || _conductorRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                IsSupplierEnabled = loTempResult.LONETIME;
            }
        }

        private void R_Before_Open_LookupSupplierInfo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00110ParameterDTO loParam = new APL00110ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = loViewModel.Data.CPROPERTY_ID,
                CSUPPLIER_ID = loViewModel.Data.CSUPPLIER_ID,
                CLANGUAGE_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00110);
        }

        private void R_After_Open_LookupSupplierInfo(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00110DTO loTempResult = (APL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CSUPPLIER_SEQ_NO = loTempResult.CSEQ_NO;
            loViewModel.Data.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }

        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            loViewModel.Data.CREF_DATE = loViewModel.Data.DREF_DATE.ToString("yyyyMMdd");
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CCOMPANY_ID = "",
                CTAX_DATE = loViewModel.Data.CREF_DATE,
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00110);
        }

        private void R_After_Open_LookupTax(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00110DTO loTempResult = (GSL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CTAX_ID = loTempResult.CTAX_ID;
            loViewModel.Data.CTAX_NAME = loTempResult.CTAX_NAME;
            loViewModel.Data.NTAX_PCT = loTempResult.NTAX_PERCENTAGE;
        }
        #endregion


        #region OnChange

        private async Task TaxableCheckbox_ValueChanged(bool poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.LTAXABLE = poParam;
                IsTaxable = poParam;
                if (poParam == false)
                {
                    IsTaxCurrencyEnabled = false;
                    loViewModel.Data.CTAX_ID = "";
                    loViewModel.Data.CTAX_NAME = "";
                    loViewModel.Data.NTAX_PCT = 0;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(loViewModel.Data.CCURRENCY_CODE) == false)
                    {
                        if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
                        {
                            IsTaxCurrencyEnabled = true;
                        }
                        else
                        {
                            IsTaxCurrencyEnabled = false;
                        }
                    }
                    else
                    {
                        IsTaxCurrencyEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loProperty.CPROPERTY_ID = poParam;
                loViewModel.Data.CPROPERTY_ID = poParam;
                loViewModel.Data.CDEPT_CODE = "";
                loViewModel.Data.CDEPT_NAME = "";
                loViewModel.Data.CSUPPLIER_ID = "";
                loViewModel.Data.CSUPPLIER_NAME = "";
                loViewModel.Data.CSUPPLIER_SEQ_NO = "";
                await loViewModel.GetPaymentTermListStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DocDateDatePicker_ValueChanged(DateTime poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                if (poParam > loViewModel.Data.DDUE_DATE)
                {
                    return;
                }
                loViewModel.Data.DDOC_DATE = poParam;
                loTimeDiff = loViewModel.Data.DDUE_DATE - loViewModel.Data.DDOC_DATE;
                loTemp = loViewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                if (loTemp != null)
                {
                    loViewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                }
                else
                {
                    loViewModel.Data.CPAY_TERM_CODE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DueDateDatePicker_ValueChanged(DateTime poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                if (poParam < loViewModel.Data.DDOC_DATE)
                {
                    return;
                }
                loViewModel.Data.DDUE_DATE = poParam;
                loTimeDiff = loViewModel.Data.DDUE_DATE - loViewModel.Data.DDOC_DATE;
                loTemp = loViewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                if (loTemp != null)
                {
                    loViewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                }
                else
                {
                    loViewModel.Data.CPAY_TERM_CODE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task TermComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            GetPaymentTermListDTO loTemp = null;

            try
            {
                loViewModel.Data.CPAY_TERM_CODE = poParam;
                loTemp = loViewModel.loPaymentTermList.Where(x => x.CPAY_TERM_CODE == loViewModel.Data.CPAY_TERM_CODE).FirstOrDefault();
                loViewModel.Data.DDUE_DATE = loViewModel.Data.DDOC_DATE.AddDays(loTemp.IPAY_TERM_DAYS);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CurrencyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loCurrencyOrTaxRateParameter.CCURRENCY_CODE = poParam;
                loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CCUR_RATETYPE_CODE;
                loViewModel.loCurrencyOrTaxRateParameter.CREF_DATE = loViewModel.Data.DREF_DATE.ToString("yyyyMMdd");
                loViewModel.Data.CCURRENCY_CODE = poParam;
                await loViewModel.GetCurrencyRateAsync();
                if (loViewModel.loCurrencyRate != null)
                {
                    loViewModel.Data.NLBASE_RATE = loViewModel.loCurrencyRate.NLBASE_RATE_AMOUNT;
                    loViewModel.Data.NLCURRENCY_RATE = loViewModel.loCurrencyRate.NLCURRENCY_RATE_AMOUNT;
                    loViewModel.Data.NBBASE_RATE = loViewModel.loCurrencyRate.NBBASE_RATE_AMOUNT;
                    loViewModel.Data.NBCURRENCY_RATE = loViewModel.loCurrencyRate.NBCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    loViewModel.Data.NLBASE_RATE = 1;
                    loViewModel.Data.NLCURRENCY_RATE = 1;
                    loViewModel.Data.NBBASE_RATE = 1;
                    loViewModel.Data.NBCURRENCY_RATE = 1;
                }

                loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CTAX_RATETYPE_CODE;
                await loViewModel.GetTaxRateAsync();
                if (loViewModel.loTaxRate != null)
                {
                    loViewModel.Data.NTAX_BASE_RATE = loViewModel.loTaxRate.NLBASE_RATE_AMOUNT;
                    loViewModel.Data.NTAX_CURRENCY_RATE = loViewModel.loTaxRate.NLCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    loViewModel.Data.NTAX_BASE_RATE = 1;
                    loViewModel.Data.NTAX_CURRENCY_RATE = 1;
                }

                if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
                {
                    IsLocalCurrencyEnabled = true;
                    if (loViewModel.Data.LTAXABLE == true)
                    {
                        IsTaxCurrencyEnabled = true;
                    }
                    else
                    {
                        IsTaxCurrencyEnabled = false;
                    }
                }
                else
                {
                    IsTaxCurrencyEnabled = false;
                    IsLocalCurrencyEnabled = false;
                }

                if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
                {
                    IsBaseCurrencyEnabled = true;
                }
                else
                {
                    IsBaseCurrencyEnabled = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Button
        private async Task OnClickTax()
        {
            R_Exception loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickAllocate()
        {
            R_Exception loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickPrint()
        {
            R_Exception loEx = new R_Exception();
            APT00110PrintReportParameterDTO loParam = null;
            try
            {
                loParam = new APT00110PrintReportParameterDTO()
                {
                    CLOGIN_COMPANY_ID = clientHelper.CompanyId,
                    CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName,
                    CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
                };

                await _reportService.GetReport(
                    "R_DefaultServiceUrlAP",
                    "AP",
                    "rpt/APT00110Print/PrintReportPost",
                    "rpt/APT00110Print/PrintReportGet",
                    loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickSubmit()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loViewModel.SubmitJournalProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRedraft()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loViewModel.RedraftJournalProcessAsync();
                if (!loEx.HasError)
                {
                    await _conductorRef.R_GetEntity(new APT00110DTO { CREC_ID = loViewModel.loInvoiceHeader.CREC_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_Journal_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GLF00100ParameterDTO()
            {
                CDEPT_CODE = loViewModel.loInvoiceHeader.CDEPT_CODE,
                CTRANS_CODE = "110010",
                CREF_NO = loViewModel.loInvoiceHeader.CGL_REF_NO
            };  
            eventArgs.TargetPageType = typeof(GLF00100);
        }

        private void After_Open_Journal_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        private void Before_Open_Detail_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new InvoiceItemTabParameterDTO()
            {
                CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(APT00120);
        }

        private void After_Open_Detail_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        private void Before_Open_Summary_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new SummaryParameterDTO()
            {
                CREC_ID = loViewModel.loInvoiceHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(APT00130);
        }

        private void After_Open_Summary_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }
        #endregion
    }
}
