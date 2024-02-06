using BlazorClientHelper;
using GLT00100COMMON;
using GLT00100MODEL;
using GLTR00100COMMON;
using GLTR00100FRONT;
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
using R_CommonFrontBackAPI;
using System;
using System.Diagnostics.Tracing;
using System.Globalization;

namespace GLT00100FRONT
{
    public partial class GLT00110 : R_Page
    {
        private GLT00110ViewModel _JournalEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_ConductorGrid _conductorDetailRef;
        private R_Grid<GLT00101DTO> _gridDetailRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _JournalEntryViewModel.GetAllUniversalData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00110DTO>(poParameter);
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    await _conductorRef.R_GetEntity(loParam);
                }
                else
                {
                    _JournalEntryViewModel.RefDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    _JournalEntryViewModel.DocDate = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                    
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Form
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _JournalEntryViewModel.GetJournal((GLT00110DTO)eventArgs.Data);
                eventArgs.Result = _JournalEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _JournalEntryViewModel.SaveJournal((GLT00110DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _JournalEntryViewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnDelete_OnClick()
        {
            var loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure want to delete this journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                await _JournalEntryViewModel.DeleteJournal(loData);
                await _conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private void JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00110DTO)eventArgs.Data;

                data.CCREATE_BY = clientHelper.UserId;
                data.CUPDATE_BY = clientHelper.UserId;
                data.DUPDATE_DATE = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                data.DCREATE_DATE = _JournalEntryViewModel.VAR_TODAY.DTODAY;
                data.CCURRENCY_CODE = _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE;

                if (string.IsNullOrWhiteSpace(data.CDEPT_CODE))
                {
                    _gridDetailRef.DataSource.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
       
        private void ValidationFormGLT00100JournalEntry(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GLT00110DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(loParam.CREF_NO))
                {
                    loEx.Add("", "Reference No. is required!");
                }


                if (_JournalEntryViewModel.RefDate < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Reference Date cannot be before Current Period!");
                }

                if (string.IsNullOrWhiteSpace(loParam.CDOC_NO))
                {
                    loEx.Add("", "Please input Document No.!");
                }

                if (_JournalEntryViewModel.DocDate > _JournalEntryViewModel.VAR_TODAY.DTODAY)
                {
                    loEx.Add("", "Document Date cannot be after today!");
                }

                if (_JournalEntryViewModel.DocDate < DateTime.ParseExact(_JournalEntryViewModel.VAR_CCURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                {
                    loEx.Add("", "Document Date cannot be before Current Period!");
                }

                if (string.IsNullOrEmpty(loParam.CTRANS_DESC))
                {
                    loEx.Add("", "Description is required!");
                }

                if ((loParam.NDEBIT_AMOUNT > 0 || loParam.NCREDIT_AMOUNT > 0) && loParam.NDEBIT_AMOUNT != loParam.NCREDIT_AMOUNT)
                {
                    loEx.Add("", "Total Debit Amount must be equal to Total Credit Amount");
                }

                if (loParam.NPRELIST_AMOUNT > 0 && loParam.NPRELIST_AMOUNT != loParam.NDEBIT_AMOUNT)
                {
                    loEx.Add("", "Journal amount is not equal to Prelist!");
                }

                if (loParam.NLBASE_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                }

                if (loParam.NLCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Local Currency Rate must be greater than 0!");
                }

                if (loParam.NBBASE_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                }

                if (loParam.NBCURRENCY_RATE <= 0)
                {
                    loEx.Add("", "Base Currency Rate must be greater than 0!");
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Private Property
        private string lcLabelSubmit = "Submit";
        private string lcLabelCommit = "Commit";
        private bool EnableEdit = false;
        private bool EnableDelete = false;
        private bool EnableSubmit = false;
        private bool EnableApprove = false;
        private bool EnableCommit = false;
        private bool EnableHaveRecId = false;
        #endregion

        private async Task JournalForm_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var data = (GLT00110DTO)eventArgs.Data;
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                if (!string.IsNullOrWhiteSpace(data.CSTATUS))
                {
                    lcLabelCommit = data.CSTATUS == "80" ? "Undo Commit" : "Commit";
                    lcLabelSubmit = data.CSTATUS == "10" ? "Undo Submit" : "Submit";

                    EnableDelete = data.CSTATUS == "00";
                    EnableDelete = data.CSTATUS != "99";
                    EnableSubmit = data.CSTATUS == "00" || data.CSTATUS == "10";
                    EnableApprove = data.CSTATUS == "10" && _JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG;
                    EnableCommit = (data.CSTATUS == "20" || (data.CSTATUS == "10" && !_JournalEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG)) ||
                                   (data.CSTATUS == "80" && _JournalEntryViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION != 1) &&
                                   int.Parse(data.CREF_PRD) >= int.Parse(_JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD);
                    EnableHaveRecId = !string.IsNullOrWhiteSpace(data.CREC_ID);

                }
                if (!string.IsNullOrWhiteSpace(data.CREC_ID))
                {
                    await _gridDetailRef.R_RefreshGrid(data);
                }
            }
        }

        private async Task JournalForm_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", "You haven’t saved your changes. Are you sure want to cancel? [Yes/No]",
                R_eMessageBoxButtonType.YesNo);
            eventArgs.Cancel = res == R_eMessageBoxResult.No;
        }

        private async Task CopyJournalEntryProcess()
        {
            var loEx = new R_Exception();
            try
            {
                //var eventArgs = new R_ServiceSaveEventArgs(_viewModel.Journal, R_eConductorMode.Edit);
                //eventArgs.Data = _viewModel.Journal;

                //var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(eventArgs.Data);
                //loParam.DetailList = new List<GLT00100JournalGridDetailDTO>(_viewModel._JournaDetailList);

                //await _viewModel.SaveJournal(loParam, eCRUDMode.EditMode);
                //eventArgs.Result = _viewModel.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _conductorDetailRef.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Onchange Value
        private async Task RefreshLastCurrency()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00110LastCurrencyRateDTO>(loData);
                var loResult = await _JournalEntryViewModel.GetLastCurrency(loParam);

                if (loResult is null)
                {
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1; 
                }
                else
                {
                    loData.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    loData.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                    loData.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                    loData.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RefDate_OnChange(DateTime poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalEntryViewModel.RefDate = poParam;
                if (_JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CLOCAL_CURRENCY_CODE 
                    || _JournalEntryViewModel.Data.CCURRENCY_CODE != _JournalEntryViewModel.VAR_GSM_COMPANY.CBASE_CURRENCY_CODE)
                {
                    await RefreshLastCurrency();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Currency_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _JournalEntryViewModel.Data.CCURRENCY_CODE = poParam;
                await RefreshLastCurrency();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail
        private async Task JournalDet_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Parameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLT00101DTO>(eventArgs.Parameter);
                    await _JournalEntryViewModel.GetJournalDetailList(loParam);
                }
                eventArgs.ListEntityResult = _JournalEntryViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_RDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00101DTO)eventArgs.Data;
                var loHeaderData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                if (data != null)
                {
                    if (data.NDEBIT > 0 && data.NCREDIT == 0)
                    {
                        data.CDBCR = 'D';
                    }
                    else if (data.NDEBIT == 0 && data.NCREDIT > 0)
                    {
                        data.CDBCR = 'C';
                    }
                    else
                    {
                        data.CDBCR = '\0';
                    }
                }

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (_gridDetailRef.DataSource.Count > 0)
                    {
                        loHeaderData.NDEBIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NDEBIT);
                        loHeaderData.NCREDIT_AMOUNT = _gridDetailRef.DataSource.Sum(x => x.NCREDIT);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void JournalDet_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void JournalDet_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00101DTO)eventArgs.Data;
                if (string.IsNullOrWhiteSpace(data.CGLACCOUNT_NO))
                {
                    loEx.Add("", "Account No. is required!");
                }

                if (string.IsNullOrWhiteSpace(data.CCENTER_CODE) && (data.CBSIS == "B" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true) || (data.CBSIS == "I" && _JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS == true))
                {
                    loEx.Add("", $"Center Code is required for Account No. {data.CGLACCOUNT_NO}!");
                }

                if (data.NDEBIT == 0 && data.NCREDIT == 0)
                {
                    loEx.Add("", "Journal amount cannot be 0!");
                }

                if (data.NDEBIT > 0 && data.NCREDIT > 0)
                {
                    loEx.Add("", "Journal amount can only be either Debit or Credit!");
                }
                if (_JournalEntryViewModel.JournalDetailGrid.Count > 0)
                {
                    if (_JournalEntryViewModel.JournalDetailGrid.Any(item => item.CGLACCOUNT_NO == data.CGLACCOUNT_NO))
                    {
                        loEx.Add("", $"Account No. {data.CGLACCOUNT_NO} already exists!");
                    }
                }
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00101DTO)eventArgs.Data;

                loData.INO = _JournalEntryViewModel.JournalDetailGrid.Count + 1;
                
                loData.CDETAIL_DESC = _JournalEntryViewModel.Data.CTRANS_DESC;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO) ? "" : _JournalEntryViewModel.Data.CDOC_NO;
                loData.CDOCUMENT_DATE = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_DATE) ? "" : _JournalEntryViewModel.DocDate.ToString("yyyyMMdd");
                loData.DDOCUMENT_DATE = _JournalEntryViewModel.DocDate;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Before_Open_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO
            {
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROGRAM_CODE = "GLM00100",
                CUSER_ID = clientHelper.UserId,
                CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName,
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

        private void After_Open_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loGetData = (GLT00101DTO)eventArgs.ColumnData;
            loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            loGetData.CBSIS = loTempResult.CBSIS_DESCR.Trim();
            if ((loTempResult.CBSIS_DESCR.Trim() == "I" && !_JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_IS) 
                || (loTempResult.CBSIS_DESCR.Trim() == "B" && !_JournalEntryViewModel.VAR_GSM_COMPANY.LENABLE_CENTER_BS))
            {
                loGetData.CCENTER_CODE = "";
                loGetData.CCENTER_NAME = "";
            }

        }

        private async Task JournalDet_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00101DTO)eventArgs.Data;

                await _JournalEntryViewModel.DeleteJournalDetailList(data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalDet_AfterDelete()
        {
            var loEx = new R_Exception();
            try
            {
                await _gridDetailRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_Saving(R_SavingEventArgs eventArgs) 
        {
            var loEx = new R_Exception();
            try
            {
                var data = (GLT00101DTO)eventArgs.Data;

                data.CDBCR = data.NDEBIT > 0 ? 'D' : data.NCREDIT > 0 ? 'C' : '\0';
                data.NAMOUNT = data.NDEBIT + data.NCREDIT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalDet_SaveBatch(R_SaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var data = (List<GLT00101DTO>)eventArgs.Data;

                await _JournalEntryViewModel.SaveJournalDetail(data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void JournalDet_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CTRANS_DESC))
                    loEx.Add("", "Journal Description is required");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void JournalDet_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CTRANS_DESC))
                    loEx.Add("", "Journal Description is required");

                var loData = (GLT00101DTO)eventArgs.Data;

                loData.CDETAIL_DESC = _JournalEntryViewModel.Data.CTRANS_DESC;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_NO) ? "" : _JournalEntryViewModel.Data.CDOC_NO;
                loData.CDOCUMENT_DATE = string.IsNullOrWhiteSpace(_JournalEntryViewModel.Data.CDOC_DATE) ? "" : _JournalEntryViewModel.DocDate.ToString("yyyyMMdd");
                loData.DDOCUMENT_DATE = _JournalEntryViewModel.DocDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Process
        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LUNDO_COMMIT = false;
                loParam.LAUTO_COMMIT = _JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.CNEW_STATUS = "20";
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CommitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                if (loData.CSTATUS == "80")
                {
                    if (_JournalEntryViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION == 3)
                    {
                        loResult = await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ",
                        R_eMessageBoxButtonType.YesNo);
                        if (loResult == R_eMessageBoxResult.No)
                        {
                            goto EndBlock;
                        }
                        else
                        {
                            lcLabelCommit = "Commit";
                        }
                    }
                }
                else
                {
                    loResult = await R_MessageBox.Show("", "Are you sure want to commit this journal? ",
                        R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                    else
                    {
                        lcLabelCommit = "Undo Commit";
                    }
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";
                loParam.LAUTO_COMMIT = false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? "20" : "80";
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);

                if (loData.CSTATUS == "80")
                {
                    await R_MessageBox.Show("", "Journal Undo Committed Successfully!",
                        R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await R_MessageBox.Show("", "Journal Committed Successfully!",
                        R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SubmitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
                if (loData.CSTATUS == "00" && int.Parse(loData.CREF_PRD) < int.Parse(_JournalEntryViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD))
                {
                    loEx.Add("", "Cannot Submit Journal with date before Soft Close Period!");
                    goto EndBlock;
                }

                if (loData.CSTATUS == "10")
                {
                    loResult = await R_MessageBox.Show("", "Are you sure want to undo submit this journal? [Yes/No] ",
                        R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                    else
                    {
                        lcLabelSubmit = "Submit";
                    }
                }
                else
                {
                    loResult = await R_MessageBox.Show("", "Are you sure want to submit this journal? [Yes/No] ",
                        R_eMessageBoxButtonType.YesNo);
                    if (loResult == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                    else
                    {
                        lcLabelSubmit = "Undo Submit";
                    }
                }
                string lcNewStatus;
                lcNewStatus = loData.CSTATUS == "00" ? "10" : "00";
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.LUNDO_COMMIT = false;
                loParam.LAUTO_COMMIT = false;
                loParam.CNEW_STATUS = lcNewStatus;
                await _JournalEntryViewModel.UpdateJournalStatus(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Print
        private R_Lookup R_LookupBtnPrint;
        private void Before_Open_lookupPrint(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
            var param = new GLTR00100DTO()
            {
                CREC_ID = loData.CREC_ID
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GLTR00100);
        }
        #endregion
        protected override Task<object> R_Set_Result_PredefinedDock()
        {
            var lcResult = _conductorRef.R_GetCurrentData();

            return Task.FromResult<object>(lcResult);
        }
        #region lookupDept

        private R_Lookup R_LookupBtnDept;
        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (GLT00110DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        #endregion
    }
}
