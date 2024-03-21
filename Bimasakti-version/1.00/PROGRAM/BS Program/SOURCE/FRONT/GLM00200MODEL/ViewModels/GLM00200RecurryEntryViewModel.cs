﻿
using GLM00200Common;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLM00200Model
{
    public class GLM00200RecurryEntryViewModel : R_ViewModel<JournalDTO>
    {
        private GLM00200Model _model = new GLM00200Model();
        private PublicLookupModel _lookupGSModel = new PublicLookupModel();
        public RecurringJournalListParamDTO Parameter { get; set; } = new RecurringJournalListParamDTO();
        public JournalDTO Journal { get; set; } = new JournalDTO();
        public ObservableCollection<JournalDetailGridDTO> JournaDetailGrid { get; set; } = new ObservableCollection<JournalDetailGridDTO>();
        public VAR_GSM_COMPANY_DTO GSM_COMPANY { get; set; } = new VAR_GSM_COMPANY_DTO();
        public VAR_GSM_TRANSACTION_CODE_DTO GSM_TRANSACTION_CODE { get; set; } = new VAR_GSM_TRANSACTION_CODE_DTO();
        public VAR_IUNDO_COMMIT_JRN_DTO IUNDO_COMMIT_JRN { get; set; } = new VAR_IUNDO_COMMIT_JRN_DTO();
        public VAR_GL_SYSTEM_PARAM_DTO GL_SYSTEM_PARAM { get; set; } = new VAR_GL_SYSTEM_PARAM_DTO();
        public VAR_PERIOD_DT_INFO_DTO CURRENT_PERIOD_START_DATE { get; set; } = new VAR_PERIOD_DT_INFO_DTO();
        public List<VAR_CURRENCY_DTO> CURRENCY_LIST { get; set; } = new List<VAR_CURRENCY_DTO>();
        public List<GSL00900DTO> CENTER_LIST { get; set; } = new List<GSL00900DTO>();


        #region Property ViewModel
        public DateTime? RefDate { get; set; } = DateTime.Now;
        public DateTime? DocDate { get; set; } 
        public DateTime? StartDate { get; set; } = DateTime.Now;
        #endregion

        #region Init
        public async Task GetInitData()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult =  await _model.GetInitDataAsync();
                var loCenterResult = await _lookupGSModel.GSL00900GetCenterListAsync();

                GSM_COMPANY = loResult.COMPANY_INFO;
                CURRENCY_LIST = loResult.CURRENCY_LIST;
                CENTER_LIST = loCenterResult;
                GSM_TRANSACTION_CODE = loResult.GSM_TRANSACTION_CODE;
                IUNDO_COMMIT_JRN = loResult.IUNDO_COMMIT_JRN;
                GL_SYSTEM_PARAM = loResult.GL_SYSTEM_PARAM;
                CURRENT_PERIOD_START_DATE = loResult.CURRENT_PERIOD_START_DATE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region Journal
        public async Task GetlJournal(JournalDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetJournalDataAsync(poEntity);
                //if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                //{
                //    RefDate = ldRefDate;
                //}
                //else
                //{
                //    RefDate = null;
                //}
                //if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                //{
                //    DocDate = ldDocDate;
                //}
                //else
                //{
                //    RefDate = null;
                //}
                //if (DateTime.TryParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                //{
                //    StartDate = ldStartDate;
                //}
                //else
                //{
                //    StartDate = null;
                //}

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ShowAllJournalDetail(JournalDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetAllJournalDetailListAsync(poParam);

                JournaDetailGrid = new ObservableCollection<JournalDetailGridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournal(JournalParamDTO poEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poEntity.CDOC_DATE = DocDate.HasValue == true ? DocDate.Value.ToString("yyyyMMdd") : "";
                poEntity.CSTART_DATE = StartDate.HasValue == true ? StartDate.Value.ToString("yyyyMMdd") : "";
                poEntity.CREF_DATE = RefDate.HasValue == true ? RefDate.Value.ToString("yyyyMMdd") : "";

                var loParam = new ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO>();
                loParam.data = poEntity;
                loParam.eCRUDMode = poCRUDMode;
                var loResult = await _model.SaveJournalDataAsync(loParam);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
       
        #region RefreshCurrencyRate
        public async Task RefreshCurrencyRate()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<CurrencyRateResult>(loData);
                loParam.CRATE_DATE = StartDate.Value.ToString("yyyyMMdd");
                loParam.CRATETYPE_CODE = GL_SYSTEM_PARAM.CRATETYPE_CODE;
                var _CURRENCY_RATE_RESULT = await _model.GetLastCurrencyAsync(loParam);

                if (_CURRENCY_RATE_RESULT != null)
                {
                    loData.NLBASE_RATE = _CURRENCY_RATE_RESULT.NLBASE_RATE_AMOUNT;
                    loData.NLCURRENCY_RATE = _CURRENCY_RATE_RESULT.NLCURRENCY_RATE_AMOUNT;
                    loData.NBBASE_RATE = _CURRENCY_RATE_RESULT.NBBASE_RATE_AMOUNT;
                    loData.NBCURRENCY_RATE = _CURRENCY_RATE_RESULT.NBCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Commit/Approval
        public async Task CommitJournal()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //string lcNewStatus = "";
                //bool llAutoCommit = false;
                //bool llUndoCommit = false;
                //EModeCmmtAprvJRN loMode = EModeCmmtAprvJRN.Commit;
                //if (_Journal.CSTATUS == "80")
                //{
                //    lcNewStatus = "20";
                //    llUndoCommit = true;
                //}
                //else
                //{
                //    lcNewStatus = "80";
                //    llUndoCommit = false;
                //}
                
                //await _model.JournalCommitApprovalAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ApproveJournal()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //string lcNewStatus = "20";
                //bool llAutoCommit = _InitData.SYSTEM_PARAM.LCOMMIT_APVJRN;
                //bool llUndoCommit = false;
                //EModeCmmtAprvJRN loMode = EModeCmmtAprvJRN.Approval;

                //R_FrontContext.R_SetContext(RecurringJournalContext.CJRN_ID_LIST, _CREC_ID);
                //R_FrontContext.R_SetContext(RecurringJournalContext.CNEW_STATUS, lcNewStatus);
                //R_FrontContext.R_SetContext(RecurringJournalContext.LAUTO_COMMIT, llAutoCommit);
                //R_FrontContext.R_SetContext(RecurringJournalContext.LUNDO_COMMIT, llUndoCommit);
                //R_FrontContext.R_SetContext(RecurringJournalContext.EMODE, loMode);

                //await _model.JournalCommitApprovalAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region BatchJournalDetail

        #endregion


    }
}
