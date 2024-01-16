using GLT00100COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00100ViewModel : R_ViewModel<GLT00100DTO>
    {
        #region Model
        private GLT00100UniversalModel _GLT00100UniversalModel = new GLT00100UniversalModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private GLT00100Model _GLT00100Model = new GLT00100Model();
        #endregion

        #region Initial Data
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new GLT00100GLSystemParamDTO();
        public GLT00100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new GLT00100GSTransInfoDTO();
        public GLT00100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new GLT00100GLSystemEnableOptionInfoDTO();
        public GLT00100GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new GLT00100GSPeriodYearRangeDTO();
        public List<GLT00100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<GLT00100GSGSBCodeDTO>();
        public List<GSL00700DTO> VAR_DEPARTEMENT_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion

        #region Public Property ViewModel
        public int JournalPeriodYear { get; set; }
        public string JournalPeriodMonth { get; set; }
        public GLT00100ParamDTO JornalParam { get; set; } = new GLT00100ParamDTO();
        public ObservableCollection<GLT00100DTO> JournalGrid { get; set; } = new ObservableCollection<GLT00100DTO>();
        public ObservableCollection<GLT00101DTO> JournalDetailGrid { get; set; } = new ObservableCollection<GLT00101DTO>();
        #endregion

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> PeriodMonthList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", "01"),
            new KeyValuePair<string, string>("02", "02"),
            new KeyValuePair<string, string>("03", "03"),
            new KeyValuePair<string, string>("04", "04"),
            new KeyValuePair<string, string>("05", "05"),
            new KeyValuePair<string, string>("06", "06"),
            new KeyValuePair<string, string>("07", "07"),
            new KeyValuePair<string, string>("08", "08"),
            new KeyValuePair<string, string>("09", "09"),
            new KeyValuePair<string, string>("10", "10"),
            new KeyValuePair<string, string>("11", "11"),
            new KeyValuePair<string, string>("12", "12")
        };
        #endregion
        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _GLT00100UniversalModel.GetTabJournalListUniversalVarAsync();

                //Set Universal Data
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                VAR_IUNDO_COMMIT_JRN = loResult.VAR_IUNDO_COMMIT_JRN;
                VAR_GSM_PERIOD = loResult.VAR_GSM_PERIOD;
                VAR_GSB_CODE_LIST = loResult.VAR_GSB_CODE_LIST;
                VAR_GSB_CODE_LIST.Add(new GLT00100GSGSBCodeDTO { CCODE="", CNAME="ALL" });

                //Get And Set List Dept Code
                var loDeptResult = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
                VAR_DEPARTEMENT_LIST = loDeptResult;

                //Set Dept Code
                var loDeptRecord = loDeptResult.FirstOrDefault();
                JornalParam.CDEPT_CODE = loDeptRecord.CDEPT_CODE;
                JornalParam.CDEPT_NAME = loDeptRecord.CDEPT_NAME;

                //Set Journal Period
                JournalPeriodYear = int.Parse(loResult.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_YY);
                JournalPeriodMonth = loResult.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_MM;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalList()
        {
            var loEx = new R_Exception();

            try
            {
                JornalParam.CPERIOD = JournalPeriodYear + JournalPeriodMonth;
                var loResult = await _GLT00100Model.GetJournalListAsync(JornalParam);

                JournalGrid = new ObservableCollection<GLT00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalDetailList(GLT00101DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00100Model.GetJournalDetailListAsync(poEntity);

                JournalDetailGrid = new ObservableCollection<GLT00101DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                 await _GLT00100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity)
        {
            var loEx = new R_Exception();
            bool loRtn = false;

            try
            {
                var loResult = await _GLT00100Model.ValidationRapidApprovalAsync(poEntity);

                loRtn = string.IsNullOrWhiteSpace(loResult.CRESULT);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
