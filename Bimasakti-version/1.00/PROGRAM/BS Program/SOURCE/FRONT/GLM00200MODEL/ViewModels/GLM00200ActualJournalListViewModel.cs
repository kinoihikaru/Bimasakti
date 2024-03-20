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
    public class GLM00200ActualJournalListViewModel : R_ViewModel<JournalDTO>
    {
        private GLM00200Model _model = new GLM00200Model();
        public JournalDTO Journal { get; set; } = new JournalDTO();
        public ObservableCollection<JournalDetailActualGridDTO> JournaDetailActualGrid { get; set; } = new ObservableCollection<JournalDetailActualGridDTO>();
        public VAR_GSM_COMPANY_DTO GSM_COMPANY { get; set; } = new VAR_GSM_COMPANY_DTO();
        public VAR_GSM_TRANSACTION_CODE_DTO GSM_TRANSACTION_CODE { get; set; } = new VAR_GSM_TRANSACTION_CODE_DTO();
        public VAR_IUNDO_COMMIT_JRN_DTO IUNDO_COMMIT_JRN { get; set; } = new VAR_IUNDO_COMMIT_JRN_DTO();
        public VAR_GL_SYSTEM_PARAM_DTO GL_SYSTEM_PARAM { get; set; } = new VAR_GL_SYSTEM_PARAM_DTO();
        public VAR_PERIOD_DT_INFO_DTO CURRENT_PERIOD_START_DATE { get; set; } = new VAR_PERIOD_DT_INFO_DTO();


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

                GSM_COMPANY = loResult.COMPANY_INFO;
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
                if (!string.IsNullOrWhiteSpace(loResult.CREF_DATE))
                    RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CDOC_DATE))
                    DocDate = DateTime.ParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(loResult.CSTART_DATE))
                    StartDate = DateTime.ParseExact(loResult.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task ShowAllJournalDetail(RecurringJournalListParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetAllActualJournalDetailListAsync(poParam);

                JournaDetailActualGrid = new ObservableCollection<JournalDetailActualGridDTO>(loResult);
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
