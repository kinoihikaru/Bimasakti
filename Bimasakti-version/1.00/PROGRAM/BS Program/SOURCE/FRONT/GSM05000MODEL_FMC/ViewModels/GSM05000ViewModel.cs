using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GSM05000COMMON_FMC;
using GSM05000FrontResources_FMC;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05000ViewModel : R_ViewModel<GSM05000DTO>
    {
        private GSM05000UniversalModel _GSM05000UniversalModel = new GSM05000UniversalModel();
        private GSM05000Model _GSM05000Model = new GSM05000Model();
        public ObservableCollection<GSM05000DTO> GridList = new ObservableCollection<GSM05000DTO>();
        public GSM05000DTO Entity = new GSM05000DTO();
        public GSM05000DTO TempEntity = new GSM05000DTO();


        #region GetDelimiterList
        // Delimiter List
        public List<GSM05000UniversalDTO> DelimiterListPrefix = new List<GSM05000UniversalDTO>();
        public List<GSM05000UniversalDTO> DelimiterListNumber = new List<GSM05000UniversalDTO>();
        public List<GSM05000UniversalDTO> DelimiterListDepartment = new List<GSM05000UniversalDTO>();
        public List<GSM05000UniversalDTO> DelimiterListTransCode = new List<GSM05000UniversalDTO>();
        public List<GSM05000UniversalDTO> DelimiterListPeriodMode = new List<GSM05000UniversalDTO>();
        public async Task GetDelimiterList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05000UniversalModel.GetRefNoDelimiterListAsync();

                DelimiterListPrefix = loReturn;
                DelimiterListNumber = loReturn;
                DelimiterListDepartment = loReturn;
                DelimiterListTransCode = loReturn;
                DelimiterListPeriodMode = loReturn;
                DelimiterListNumber = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region Combo Box Helper List
        public List<KeyValuePair<string, string>> CPERIOD_MODE { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("N", "None"),
            new KeyValuePair<string, string>("P", "Periodically"),
            new KeyValuePair<string, string>("Y", "Yearly")
        };

        public List<KeyValuePair<string, string>> CAPPROVAL_MODE { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("1", "Hierarchy"),
            new KeyValuePair<string, string>("2", "Flat And"),
            new KeyValuePair<string, string>("3", "Flat Or"),
            new KeyValuePair<string, string>("", ""),
        };

        public List<KeyValuePair<string, string>> CYEAR_FORMAT { get; set; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("1", "YY"),
            new KeyValuePair<string, string>("2", "YYYY"),
        };
        #endregion

        public async Task GetGridList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05000Model.GetTransactionCodeListStreamAsync();
                GridList = new ObservableCollection<GSM05000DTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntity(GSM05000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM05000Model.R_ServiceGetRecordAsync(poEntity);
                Entity = loResult;
                TempEntity = loResult;
                GetSequence();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveEntity(GSM05000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {

                Entity = await _GSM05000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Validation Transaction Code
        public async Task ValidationEntity(GSM05000DTO poEntity)
        {
            var loEx = new R_Exception();
            bool llCancel;
            try
            {
                var loCheckExistData = await CheckExistData(poEntity, GSM05000eTabName.Numbering);
                llCancel = poEntity is { LAPPROVAL_FLAG: true, LUSE_THIRD_PARTY: false } && string.IsNullOrWhiteSpace(poEntity.CAPPROVAL_MODE);

                if (loCheckExistData)
                {
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "Err01"));
                    }
                }

                llCancel = DeptSequence == 0 && poEntity.LDEPT_MODE;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err02"));
                }

                llCancel = TrxSequence == 0 && poEntity.LTRANSACTION_MODE;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err03"));
                }

                llCancel = PeriodSequence == 0 && poEntity.LINCREMENT_FLAG;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err04"));
                }

                llCancel = poEntity.INUMBER_LENGTH == 0 && poEntity.LINCREMENT_FLAG;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err05"));
                }

                var loList = GetSeqDataInList(poEntity);
                llCancel = IsDuplicateSequence(loList); ;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err06"));
                }

                llCancel = IsValidSequence(loList);
                if (llCancel)
                {
                    loEx.Add("Err07", 
                        string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Err07"), 
                        loList.Count));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private List<int> GetSeqDataInList(GSM05000DTO poEntity)
        {
            List<int> list = new List<int>
            {
                PeriodSequence, LenSequence
            };

            if (poEntity.LDEPT_MODE)
            {
                list.Insert(0, DeptSequence);
            }

            if (poEntity.LTRANSACTION_MODE)
            {
                list.Insert(0, TrxSequence);
            }

            return list;
        }
        private bool IsDuplicateSequence(List<int> poList)
        {
            bool llReturn = false;
            var isDuplicate = poList.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            if (isDuplicate.Count > 0)
            {
                llReturn = true;
            }

            return llReturn;
        }
        private bool IsValidSequence(List<int> poList)
        {
            bool llReturn = true;
            bool llValid;
            for (var i = 1; i <= poList.Count; i++)
            {
                llValid = poList.Any(x => x == i);
                if (!llValid)
                {
                    llReturn = false;
                    break;
                }
            }

            return llReturn;
        }
        public async Task<bool> CheckExistData(GSM05000DTO poEntity, GSM05000eTabName pcEtabName)
        {
            var loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                var loParams = R_FrontUtility.ConvertObjectToObject<GSM05000ValidateDTO>(poEntity);
                loParams.ETAB_INDEX = pcEtabName;

                var loResult = await _GSM05000Model.ValidateTransactionCodeAsync(loParams);
                llReturn = loResult is null ? false : true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llReturn;
        }
        #endregion

        #region Parse sequence
        // For get Sequence
        public int DeptSequence { get; set; }
        public int TrxSequence { get; set; }
        public int PeriodSequence { get; set; }
        public int LenSequence { get; set; }
        public int PrefixSequence { get; set; }
        public void GetSequence()
        {
            DeptSequence = GetSequenceIndex("01");
            TrxSequence = GetSequenceIndex("02");
            PeriodSequence = GetSequenceIndex("03");
            LenSequence = GetSequenceIndex("04");
            PrefixSequence = GetSequenceIndex("05");
        }
        private int GetSequenceIndex(string pcIndex)
        {
            if (Entity.CSEQUENCE01 == pcIndex) return 1;
            if (Entity.CSEQUENCE02 == pcIndex) return 2;
            if (Entity.CSEQUENCE03 == pcIndex) return 3;
            if (Entity.CSEQUENCE04 == pcIndex) return 4;

            return 0;
        }
        #endregion

        #region REF NO / Sample Code
        public string REFNO { get; set; } = "";
        public string REFNO_LENGTH { get; set; } = "";
        public async Task GetSeqValue()
        {
            var loEx = new R_Exception();

            try
            {
                Data.CSEQUENCE01 = (DeptSequence == 1) ? "01" : (TrxSequence == 1) ? "02" : (PeriodSequence == 1) ? "03" : (LenSequence == 1) ? "04" : "00";
                Data.CSEQUENCE02 = (DeptSequence == 2) ? "01" : (TrxSequence == 2) ? "02" : (PeriodSequence == 2) ? "03" : (LenSequence == 2) ? "04" : "00";
                Data.CSEQUENCE03 = (DeptSequence == 3) ? "01" : (TrxSequence == 3) ? "02" : (PeriodSequence == 3) ? "03" : (LenSequence == 3) ? "04" : "00";
                Data.CSEQUENCE04 = (DeptSequence == 4) ? "01" : (TrxSequence == 4) ? "02" : (PeriodSequence == 4) ? "03" : (LenSequence == 4) ? "04" : "00";
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task getFormatDocRefNo()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = string.Concat(
                    Data.CPREFIX.Trim() +
                    (string.IsNullOrEmpty(Data.CPREFIX) ? "" : Data.CPREFIX_DELIMITER) +
                    (
                        Data.CSEQUENCE01 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE01 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE01 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE01 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE01)
                        ? ""
                        : (
                            Data.CSEQUENCE01 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE01 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE01 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE01 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE02 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE02 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE02 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE02 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE02)
                        ? ""
                        : (
                            Data.CSEQUENCE02 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE02 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE02 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE02 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE03 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE03 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE03 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE03 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE03)
                        ? ""
                        : (
                            Data.CSEQUENCE03 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE03 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE03 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE03 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    (
                        Data.CSEQUENCE04 == "01" ? "DDDDDDDD" :
                        Data.CSEQUENCE04 == "02" ? "TTTTTT" :
                        Data.CSEQUENCE04 == "03" ? (Data.CPERIOD_MODE == "P" ? "YYYYMM" : "YYYY") :
                        Data.CSEQUENCE04 == "04" ? new string('9', Data.INUMBER_LENGTH) :
                        ""
                    ) +
                    (string.IsNullOrEmpty(Data.CSEQUENCE04)
                        ? ""
                        : (
                            Data.CSEQUENCE04 == "01" ? Data.CDEPT_DELIMITER :
                            Data.CSEQUENCE04 == "02" ? Data.CTRANSACTION_DELIMITER :
                            Data.CSEQUENCE04 == "03" ? Data.CPERIOD_DELIMITER :
                            Data.CSEQUENCE04 == "04" ? Data.CNUMBER_DELIMITER :
                            ""
                        )) +
                    Data.CSUFFIX.Trim()
                );

                REFNO = loResult;
                REFNO_LENGTH = REFNO.Length.ToString();
                //REFNO_LENGTH = Regex.Replace(REFNO, "[^a-zA-Z0-9]", "").Length.ToString();
                await Task.CompletedTask;
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