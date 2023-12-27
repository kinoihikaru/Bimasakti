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
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using Lookup_APCOMMON.DTOs.APL00110;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Interfaces;
using APT00300MODEL;
using R_BlazorFrontEnd.Helpers;
using APT00300COMMON;

namespace APT00300FRONT
{
    public partial class APT00310 : R_Page
    {
        private APT00310ViewModel _viewModel = new APT00310ViewModel();
        private R_Conductor _conductorRef;

        #region Inject
        [Inject] R_ILocalizer<APT00300FrontResources.Resources_Dummy_Class> _localizer { get; set;}
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        #endregion

        #region Proerty
        private bool _IsPopModeEnable { get; set; } = false;
        private string _WidthIsPopMode { get; set; } = "auto";
        private bool _HasData { get; set; } = true;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            APT00310DTO loParam = null;

            try
            {
                //Load Initial Process
                await _viewModel.GetAllInitialVar();

                if (poParameter is not null)
                {
                    var loCheckModePopup = R_FrontUtility.ConvertObjectToObject<APT00300InputParameterDTO>(poParameter);

                    if (loCheckModePopup.LPOPUP_MODE)
                    {
                        loParam = R_FrontUtility.ConvertObjectToObject<APT00310DTO>(loCheckModePopup);
                        _IsPopModeEnable = true;
                        _WidthIsPopMode = "1100px";
                    }
                    else
                    {
                        loParam = R_FrontUtility.ConvertObjectToObject<APT00310DTO>(poParameter);
                        _IsPopModeEnable = false;
                        _WidthIsPopMode = "auto";
                    }

                    await _conductorRef.R_GetEntity(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Invoice Header
        private async Task InvoiceHeader_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00310DTO loData = (APT00310DTO)eventArgs.Data;
                await _viewModel.GetPurchaseDebit(loData);

                eventArgs.Result = _viewModel.PurchaseDebit;
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
                await _viewModel.DeletePurchaseDebit((APT00310DTO)eventArgs.Data);
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
                await _conductorRef.R_SetCurrentData(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex); 
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task InvoiceHeader_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00310DTO loData = (APT00310DTO)eventArgs.Data;
                await _viewModel.ValidationPurchaseDebit(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task InvoiceHeader_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResultDetail = null;
            R_PopupResult loResultSummary = null;

            try
            {
                //loResultDetail = await PopupService.Show(typeof(APT00100FRONT.APT00120), new InvoiceItemTabParameterDTO()
                //{
                //    CREC_ID = _viewModel.loInvoiceHeader.CREC_ID
                //});
                //loResultSummary = await PopupService.Show(typeof(APT00100FRONT.APT00130), new SummaryParameterDTO()
                //{
                //    CREC_ID = _viewModel.loInvoiceHeader.CREC_ID
                //});

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
                await _viewModel.SavePurchaseDebit((APT00310DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.PurchaseDebit;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void InvoiceHeader_SetHasData(R_SetEventArgs eventArgs)
        {
            _HasData = eventArgs.Enable;
        }
        #endregion

        #region Lookup

        #region Department
        private void DeptCodeTextBox_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loTempParam = _conductorRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<GSL00710ParameterDTO>(loTempParam);

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

            var loGetData = (APT00310DTO)_conductorRef.R_GetCurrentData();
            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Supplier
        private void SupplierIDTextBox_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loTempParam = _conductorRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<APL00100ParameterDTO>(loTempParam);

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

            var loGetData = (APT00310DTO)_conductorRef.R_GetCurrentData();
            loGetData.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            loGetData.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            loGetData.LONETIME = loTempResult.LONETIME;
            loGetData.CSUPPLIER_SEQ_NO = "";
        }
        #endregion

        #region Supplier Ref No
        private void SupplierRefNoTextBox_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupSupplierInfo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00110ParameterDTO loParam = new APL00110ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSUPPLIER_ID = _viewModel.Data.CSUPPLIER_ID
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
            var loGetData = (APT00310DTO)_conductorRef.R_GetCurrentData();
            loGetData.CSUPPLIER_SEQ_NO = loTempResult.CSEQ_NO;
            loGetData.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }
        #endregion

        #region Tax
        private void TaxIDTextBox_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00110ParameterDTO loParam = new GSL00110ParameterDTO()
            {
                CTAX_DATE = _viewModel.RefDate.ToString("yyyyMMdd"),
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
            var loGetData = (APT00310DTO)_conductorRef.R_GetCurrentData();
            loGetData.CTAX_ID = loTempResult.CTAX_ID;
            loGetData.CTAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NTAX_PCT = loTempResult.NTAX_PERCENTAGE;
        }
        #endregion
        #endregion


        #region OnChange

        private void TaxableCheckbox_ValueChanged(bool poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.LTAXABLE = poParam;
                if (poParam == false)
                {
                    _viewModel.Data.CTAX_ID = "";
                    _viewModel.Data.CTAX_NAME = "";
                    _viewModel.Data.NTAX_PCT = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void PropertyComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.CPROPERTY_ID = poParam;
                _viewModel.Data.CDEPT_CODE = "";
                _viewModel.Data.CDEPT_NAME = "";
                _viewModel.Data.CSUPPLIER_ID = "";
                _viewModel.Data.CSUPPLIER_NAME = "";
                _viewModel.Data.CSUPPLIER_SEQ_NO = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DocDateDatePicker_ValueChanged(DateTime poParam)
        {
            R_Exception loEx = new R_Exception();
            //GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                _viewModel.DocDate = poParam;
                //if (poParam > _viewModel.re)
                //{
                //    return;
                //}
                //loTimeDiff = _viewModel.Data.DDUE_DATE - _viewModel.Data.DDOC_DATE;
                //loTemp = _viewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                //if (loTemp != null)
                //{
                //    _viewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                //}
                //else
                //{
                //    _viewModel.Data.CPAY_TERM_CODE = "";
                //}
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
            //GetPaymentTermListDTO loTemp = null;
            TimeSpan loTimeDiff;
            try
            {
                //if (poParam < _viewModel.Data.DDOC_DATE)
                //{
                //    return;
                //}
                //_viewModel.Data.DDUE_DATE = poParam;
                //loTimeDiff = _viewModel.Data.DDUE_DATE - _viewModel.Data.DDOC_DATE;
                //loTemp = _viewModel.loPaymentTermList.Where(x => x.IPAY_TERM_DAYS == loTimeDiff.Days).FirstOrDefault();
                //if (loTemp != null)
                //{
                //    _viewModel.Data.CPAY_TERM_CODE = loTemp.CPAY_TERM_CODE;
                //}
                //else
                //{
                //    _viewModel.Data.CPAY_TERM_CODE = "";
                //}
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
            //GetPaymentTermListDTO loTemp = null;

            try
            {
                //_viewModel.Data.CPAY_TERM_CODE = poParam;
                //loTemp = _viewModel.loPaymentTermList.Where(x => x.CPAY_TERM_CODE == _viewModel.Data.CPAY_TERM_CODE).FirstOrDefault();
                //_viewModel.Data.DDUE_DATE = _viewModel.Data.DDOC_DATE.AddDays(loTemp.IPAY_TERM_DAYS);
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
                _viewModel.Data.CCURRENCY_CODE = poParam;
                var loData = (APT00310DTO)_conductorRef.R_GetCurrentData();
                await _viewModel.RefreshCurrencyRateProcess(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Button
        private async Task OnClickClose()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
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
            //APT00110PrintReportParameterDTO loParam = null;
            try
            {
                //loParam = new APT00110PrintReportParameterDTO()
                //{
                //    CLOGIN_COMPANY_ID = clientHelper.CompanyId,
                //    CLANGUAGE_ID = clientHelper.Culture.TwoLetterISOLanguageName,
                //    CREC_ID = _viewModel.loInvoiceHeader.CREC_ID
                //};

                //await _reportService.GetReport(
                //    "R_DefaultServiceUrlAP",
                //    "AP",
                //    "rpt/APT00110Print/PrintReportPost",
                //    "rpt/APT00110Print/PrintReportGet",
                //    loParam);
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
                //await _viewModel.SubmitJournalProcessAsync();
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
                //await _viewModel.RedraftJournalProcessAsync();
                //if (!loEx.HasError)
                //{
                //    await _conductorRef.R_GetEntity(new APT00110DTO { CREC_ID = _viewModel.loInvoiceHeader.CREC_ID });
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_Journal_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //eventArgs.Parameter = new GLF00100ParameterDTO()
            //{
            //    CDEPT_CODE = _viewModel.loInvoiceHeader.CDEPT_CODE,
            //    CTRANS_CODE = "110010",
            //    CREF_NO = _viewModel.loInvoiceHeader.CGL_REF_NO
            //};  
            //eventArgs.TargetPageType = typeof(GLF00100);
        }

        private void After_Open_Journal_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            //if (eventArgs.Success == false)
            //{
            //    return;
            //}
        }

        private void Before_Open_Detail_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = _conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APT00320);
        }

        private async Task After_Open_Detail_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTempParam = _conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<APT00311DTO>(loTempParam);
                var llCheckDataList = await _viewModel.CheckPurchaseDebitDTList(loParam);

                if (llCheckDataList)
                {
                    await _viewModel.GenerateWHTaxDeducation(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        private void Before_Open_Summary_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            //eventArgs.Parameter = new SummaryParameterDTO()
            //{
            //    CREC_ID = _viewModel.loInvoiceHeader.CREC_ID
            //};
            //eventArgs.TargetPageType = typeof(APT00130);
        }

        private void After_Open_Summary_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            //if (eventArgs.Success == false)
            //{
            //    return;
            //}
        }
        #endregion

        #region Tab Page
        private void R_Before_OpenInvoiceItem_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = _conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APT00311);
        }

        #endregion
    }
}
