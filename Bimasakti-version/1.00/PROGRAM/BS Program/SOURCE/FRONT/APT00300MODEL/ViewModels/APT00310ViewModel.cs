using APT00300COMMON;
using APT00300FrontResources;
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

namespace APT00300MODEL
{
    public class APT00310ViewModel : R_ViewModel<APT00310DTO>
    {
        private APT00310Model _APT00310Model = new APT00310Model();
        private APT00300UniversalModel _APT00300UniversalModel = new APT00300UniversalModel();

        #region Property View Model
        public APT00310DTO PurchaseDebit { get; set; } = new APT00310DTO();
        public DateTime RefDate { get; set; } = DateTime.Now;
        public DateTime DocDate { get; set; } = DateTime.Now;
        #endregion

        #region Initial Process
        public List<APT00300CurrencyDTO> CurrencyList { get; set; } = new List<APT00300CurrencyDTO>();
        public List<APT00300PropertyDTO> PropertyList { get; set; } = new List<APT00300PropertyDTO>();
        public APT00300APSystemParamDTO APSystemParam { get; set; } = new APT00300APSystemParamDTO();
        public APT00300GSCompanyInfoDTO GSCompanyInfo { get; set; } = new APT00300GSCompanyInfoDTO();
        public APT00300GSTransCodeInfoDTO GSTransCode { get; set; } = new APT00300GSTransCodeInfoDTO();
        public async Task GetAllInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00300UniversalModel.GetAllInitialVarTab2Async();

                PropertyList = loResult.VAR_PROPERTY_LIST;
                APSystemParam = loResult.VAR_AP_SYSTEM_PARAM;
                GSCompanyInfo = loResult.VAR_GSM_COMPANY;
                GSTransCode = loResult.VAR_GSM_TRANSACTION_CODE;
                CurrencyList = loResult.VAR_CURRENCY_LIST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Combo Box or Radio Group Helper List
     

        #endregion
        public async Task GetPurchaseDebit(APT00310DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APT00310Model.R_ServiceGetRecordAsync(poEntity);

                RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                DocDate = DateTime.ParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                PurchaseDebit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task RefreshCurrencyRateProcess(APT00310DTO poEntity)
        {
            var loEx = new R_Exception();
            APT00310LastCurrencyDTO loResult = null;

            try
            {
                //get Param
                var loParam = R_FrontUtility.ConvertObjectToObject<APT00310LastCurrencyParameterDTO>(poEntity);
                loParam.CRATE_DATE = RefDate.ToString("yyyyMMdd");

                loParam.CRATETYPE_CODE = APSystemParam.CCUR_RATETYPE_CODE;
                loResult = await _APT00310Model.GetLastCurrencyRateAsync(loParam);
                if (loResult != null)
                {
                    Data.NLBASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    Data.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                    Data.NBBASE_RATE = loResult.NBBASE_RATE_AMOUNT;
                    Data.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    Data.NLBASE_RATE = 1;
                    Data.NLCURRENCY_RATE = 1;
                    Data.NBBASE_RATE = 1;
                    Data.NBCURRENCY_RATE = 1;
                }

                loParam.CRATETYPE_CODE = APSystemParam.CTAX_RATETYPE_CODE;
                loResult = await _APT00310Model.GetLastCurrencyRateAsync(loParam);
                if (loResult != null)
                {
                    Data.NTAX_BASE_RATE = loResult.NLBASE_RATE_AMOUNT;
                    Data.NTAX_CURRENCY_RATE = loResult.NLCURRENCY_RATE_AMOUNT;
                }
                else
                {
                    Data.NTAX_BASE_RATE = 1;
                    Data.NTAX_CURRENCY_RATE = 1;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationPurchaseDebit(APT00310DTO poEntity)
        {
            var loEx = new R_Exception();
            bool llCancel = false;

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err04"));
                }

                llCancel = string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err01"));
                }

                llCancel = string.IsNullOrWhiteSpace(poEntity.CSUPPLIER_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err02"));
                }

                llCancel = string.IsNullOrWhiteSpace(poEntity.CSUPPLIER_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err05"));
                }

                llCancel = RefDate == DateTime.MinValue;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err18"));
                }

                llCancel = string.IsNullOrWhiteSpace(poEntity.CDOC_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err06"));
                }

                llCancel = DocDate == DateTime.MinValue;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err07"));
                }

                //llCancel = string.IsNullOrWhiteSpace(poEntity.CPAY_TERM_CODE);
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "Err08"));
                //}

                llCancel = string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err09"));
                }

                llCancel = poEntity.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err10"));
                }

                llCancel = poEntity.NLCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err11"));
                }

                llCancel = poEntity.NBBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err12"));
                }

                llCancel = poEntity.NBCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err13"));
                }

                if (poEntity.LTAXABLE)
                {
                    llCancel = string.IsNullOrWhiteSpace(poEntity.CTAX_ID);
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "Err14"));
                    }

                    llCancel = poEntity.NTAX_BASE_RATE <= 0;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "Err15"));
                    }

                    llCancel = poEntity.NTAX_CURRENCY_RATE <= 0;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "Err16"));
                    }
                }

                llCancel = string.IsNullOrWhiteSpace(poEntity.CTRANS_DESC);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err17"));
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SavePurchaseDebit(APT00310DTO poEntity, eCRUDMode poCRUDMODE)
        {
            var loEx = new R_Exception();
            try
            {
                //Set Param
                poEntity.CREF_DATE = RefDate.ToString("yyyyMMdd");
                poEntity.CDOC_DATE = DocDate.ToString("yyyyMMdd");

                var loResult = await _APT00310Model.R_ServiceSaveAsync(poEntity, poCRUDMODE);

                PurchaseDebit = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeletePurchaseDebit(APT00310DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _APT00310Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Popup Detail 
        public async Task<bool> CheckPurchaseDebitDTList(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();
            bool llRtn = false;

            try
            {
                var loResult = await _APT00310Model.GetDebitNoteDTListStreamAsync(poEntity);
                llRtn = loResult.Count > 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }
        public async Task GenerateWHTaxDeducation(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00310Model.GenerateWHTaxDeducationDTAsync(poEntity);
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
