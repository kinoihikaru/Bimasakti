using BlazorClientHelper;
using GLM00200Common;
using GLM00200Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace GLM00200Front
{
    public partial class RecurringEntry : R_Page
    {
        private GLM00200RecurryEntryViewModel _journalVM = new GLM00200RecurryEntryViewModel();
        private R_Grid<JournalDetailGridDTO> _gridJournalDet;
        private R_Conductor _conJournalNavigator;
        private R_ConductorGrid _conJournalDetail;
        private bool _enableCrudJournalDetail = false;
        [Inject] IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<JournalDTO>(poParameter);

                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                   loParam.CJRN_ID = loParam.CREC_ID;
                   await  _conJournalNavigator.R_GetEntity(loParam);
                }

                await _journalVM.GetInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region JournalForm
        private void JournalForm_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)eventArgs.Data;
                if (eventArgs.ConductorMode != R_eConductorMode.Normal)
                {
                    if (string.IsNullOrEmpty(loData.CREF_NO) || string.IsNullOrWhiteSpace(loData.CREF_NO))
                    {
                        loEx.Add("", "Account No. is required!");
                    }
                    if (_journalVM.RefDate == null)
                    {
                        loEx.Add("", "Reference Date is required!");
                    }
                    if (_journalVM.RefDate < DateTime.ParseExact(_journalVM.CURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Reference Date cannot be before Current Period!");
                    }
                    if (_journalVM.RefDate > _journalVM.StartDate)
                    {
                        loEx.Add("", "Reference Date cannot be after Start Date!");
                    }
                    if (_journalVM.StartDate < DateTime.ParseExact(_journalVM.CURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Start Date cannot be before Current Period!");
                    }
                    if (string.IsNullOrEmpty(loData.CDOC_NO) && _journalVM.DocDate.HasValue)
                    {
                        loEx.Add("", "Please input Document No.!");
                    }
                    if (_journalVM.DocDate == null && _journalVM.DocDate > DateTime.Now)
                    {
                        loEx.Add("", "Document Date cannot be after today");
                    }
                    if (_journalVM.DocDate == null && _journalVM.DocDate < DateTime.ParseExact(_journalVM.CURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Document Date cannot be before Current Period!");
                    }
                    if (!string.IsNullOrEmpty(loData.CDOC_NO) && _journalVM.DocDate == null)
                    {
                        loEx.Add("", "Please input Document Date!");
                    }
                    if (_journalVM.DocDate > _journalVM.StartDate)
                    {
                        loEx.Add("", "Document Date cannot be after Start Date!");
                    }
                    if (string.IsNullOrEmpty(loData.CTRANS_DESC) || string.IsNullOrWhiteSpace(loData.CTRANS_DESC))
                    {
                        loEx.Add("", "Description is required!");
                    }

                    if (loData.NPRELIST_AMOUNT > 0 && loData.NPRELIST_AMOUNT != loData.NNTRANS_AMOUNT_D)
                    {
                        loEx.Add("", "Journal amount is not equal to Prelist!");
                    }

                    if (loData.NLBASE_RATE <= 0)
                    {
                        loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                    }

                    if (loData.NLCURRENCY_RATE <= 0)
                    {
                        loEx.Add("", "Local Currency Rate must be greater than 0!");
                    }

                    if (loData.NBBASE_RATE <= 0)
                    {
                        loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                    }

                    if (loData.NBCURRENCY_RATE <= 0)
                    {
                        loEx.Add("", "Base Currency Rate must be greater than 0!");
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                var loData = R_FrontUtility.ConvertObjectToObject<JournalParamDTO>(loParam);
                loData.ListJournalDetail = _gridJournalDet.DataSource.ToList();
                await _journalVM.SaveJournal(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                await _journalVM.GetlJournal(loParam);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private string lcSubmit = "Submit";
        private string lcCommit = "Commit";
        private async Task JournalForm_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (eventArgs.Data != null)
                    {
                        var loData = (JournalDTO)eventArgs.Data;
                        if (!string.IsNullOrWhiteSpace(loData.CREC_ID) || !string.IsNullOrWhiteSpace(loData.CJRN_ID))
                        {
                            await _gridJournalDet.R_RefreshGrid(loData);
                        }
                        lcSubmit = loData.CSTATUS == "10" ? "Undo Submit" : "Submit";
                        lcCommit = loData.CSTATUS == "80" ? "Undo Commit" : "Commit";

                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        { 
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)eventArgs.Data;
                _gridJournalDet.DataSource.Clear();

                loData.CCURRENCY_CODE = _journalVM.GSM_COMPANY.CLOCAL_CURRENCY_CODE;//set default ccurrency data when addmode
                loData.NLBASE_RATE = 1;
                loData.NLCURRENCY_RATE = 1;
                loData.NBBASE_RATE = 1;
                loData.NBCURRENCY_RATE = 1;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void JournalForm_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            //_enableCrudJournalDetail = false;
            //if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add)
            //{ _journalVM._JournaDetailList = _journalVM._JournaDetailListTemp; }
        }
        private void JournalForm_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            //_enableCrudJournalDetail = true;
        }
        #endregion

        #region DepartmentLookup
        private void Dept_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Dept_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                if (loTempResult == null)
                {
                    return;
                }
                _journalVM.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _journalVM.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region JournalDetailGrid
        private void JurnalDetail_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Parameter;
                await _journalVM.ShowAllJournalDetail(loParam);
                eventArgs.ListEntityResult = _journalVM.JournaDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void JournalDetailBeforeOpenLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO
            {
                CPROGRAM_CODE = "GLM00200",
                CBSIS = "",
                CDBCR = "",
                CCENTER_CODE = "",
                CPROPERTY_ID = "",
                LCENTER_RESTR = false,
                LUSER_RESTR = false
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00500);
        }
        private void JournalDetailAfterOpenLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loGetData = (JournalDetailGridDTO)eventArgs.ColumnData;
            loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            loGetData.CBSIS = loTempResult.CBSIS;
            //loGetData.CBSIS = loTempResult.CBSIS_DESCR.Contains("B") ? 'B' : (loTempResult.CBSIS_DESCR.Contains("I") ? 'I' :default(char));
        }
        private void JurnalDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDetailGridDTO)eventArgs.Data;
                if (eventArgs.ConductorMode != R_eConductorMode.Normal)
                {
                    if (string.IsNullOrWhiteSpace(loData.CGLACCOUNT_NO))
                    {
                        loEx.Add("", "Account No. is required!");
                    }

                    if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE) && (loData.CBSIS == "B" && _journalVM.GSM_COMPANY.LENABLE_CENTER_BS == true) || (loData.CBSIS == "I" && _journalVM.GSM_COMPANY.LENABLE_CENTER_IS == true))
                    {
                        loEx.Add("", $"Center Code is required for Account No. {loData.CGLACCOUNT_NO}!");
                    }

                    if (loData.NDEBIT == 0 && loData.NCREDIT == 0)
                    {
                        loEx.Add("", "Journal amount cannot be 0!");
                    }

                    if (loData.NDEBIT > 0 && loData.NCREDIT > 0)
                    {
                        loEx.Add("", "Journal amount can only be either Debit or Credit!");
                    }

                    if (eventArgs.ConductorMode == R_eConductorMode.Add)
                    {
                        if (_journalVM.JournaDetailGrid.Any(item => item.CGLACCOUNT_NO == loData.CGLACCOUNT_NO))
                        {
                            loEx.Add("", $"Account No. {loData.CGLACCOUNT_NO} already exists!");
                        }
                    }

                    if (_journalVM.JournaDetailGrid.Count(item => item.CGLACCOUNT_NO == loData.CGLACCOUNT_NO) > 1 ||
                        (_journalVM.JournaDetailGrid.Any(item => item.CGLACCOUNT_NO == loData.CGLACCOUNT_NO) && eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit))
                    {
                        loEx.Add("", $"Account No. {loData.CGLACCOUNT_NO} already exists!");
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void JurnalDetail_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var lodata = (JournalDetailGridDTO)eventArgs.Data;

                //findout credit or debit
                lodata.CDBCR = lodata.NDEBIT > 0 ? "D" : lodata.NCREDIT > 0 ? "C" : "";

                //fill ccentercode if null based on ccentername
                //if (string.IsNullOrWhiteSpace(lodata.CCENTER_CODE) || string.IsNullOrWhiteSpace(lodata.CCENTER_CODE))
                //{
                //    foreach (var loitem in _journalVM.CENTER_LIST)
                //    {
                //        if (lodata.CCENTER_CODE == loitem.CCENTER_CODE)
                //            lodata.CCENTER_NAME = loitem.CCENTER_NAME;
                //    }
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void JurnalDetail_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (JournalDetailGridDTO)eventArgs.Data;

            //create increment data grid
            loData.INO = _gridJournalDet.DataSource.Count + 1;
            loData.CCENTER_CODE = _journalVM.CENTER_LIST.FirstOrDefault().CCENTER_CODE;
            loData.CDETAIL_DESC = _journalVM.Data.CTRANS_DESC;
        }
        private void JurnalDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (JournalDetailGridDTO)eventArgs.Data;

                data.CDBCR = data.NDEBIT > 0 ? "D" : data.NCREDIT > 0 ? "C" : "";
                data.NAMOUNT = data.NDEBIT + data.NCREDIT;
                data.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_journalVM.Data.CDOC_NO) ? "" : _journalVM.Data.CDOC_NO;
                data.CDOCUMENT_DATE = _journalVM.DocDate.HasValue == true ? _journalVM.DocDate.Value.ToString("yyyyMMdd") : "";

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Form Control
        private bool _enable_NLBASE_RATE = false;
        private bool _enable_NLCURRENCY_RATE = false;
        private bool _enable_NBBASE_RATE = false;
        private bool _enable_NBCURRENCY_RATE = false;
        private void OnChangedStartDate()
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.StartDate = DateTime.Now.AddDays(1);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void OnChanged_LFIX_RATE(bool plParam)
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.Data.LFIX_RATE = plParam;
                //if (_journalVM.Data.LFIX_RATE)
                //{
                //    _enable_NLBASE_RATE = false;
                //    _enable_NBBASE_RATE = false;
                //    _enable_NLCURRENCY_RATE = false;
                //    _enable_NBCURRENCY_RATE = false;
                //}
                //else
                //{
                //    _enable_NLBASE_RATE = true;
                //    _enable_NBBASE_RATE = true;
                //    _enable_NLCURRENCY_RATE = true;
                //    _enable_NBCURRENCY_RATE = true;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task OnChanged_CurrencyCodeAsync(string pcParam)
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.Data.CCURRENCY_CODE = pcParam;
                await _journalVM.RefreshCurrencyRate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Print
        private void PrintBtn_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)_conJournalNavigator.R_GetCurrentData();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GLM00200PrintPopup);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
