using LMM07000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;
using R_CommonFrontBackAPI;
using System.Collections;
using R_BlazorFrontEnd.Helpers;
using LMM07000FrontResources;

namespace LMM07000MODEL
{
    public class LMM07000ViewModel : R_ViewModel<LMM07000DTO>
    {
        private LMM07000Model _LMM07000Model = new LMM07000Model();
        private LMM07000UniversalModel _LMM07000UniversalModel = new LMM07000UniversalModel();

        public ObservableCollection<LMM07000DTO> ChargesDiscountGrid { get; set; } = new ObservableCollection<LMM07000DTO>();
        public LMM07000DTO ChargesDiscount = new LMM07000DTO();

        public ObservableCollection<LMM07000DTOUniversal> ChargesTypeGrid { get; set; } = new ObservableCollection<LMM07000DTOUniversal>();

        public List<LMM07000DTOInitial> PropertyList { get; set; } = new List<LMM07000DTOInitial>();
        public List<LMM07000PeriodDTO> InvoicePeriodList { get; set; } = new List<LMM07000PeriodDTO>();
        public List<LMM07000DTOUniversal> DiscountTypeList { get; set; } = new List<LMM07000DTOUniversal>();

        public int FromPeriodYear { get; set; } = 1;
        public int ToPeriodYear { get; set; } = 12;
        public bool StatusChange = false;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000Model.GetPropertyAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000UniversalModel.GetChargesTypeListAsync();
                ChargesTypeGrid = new ObservableCollection<LMM07000DTOUniversal>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesDiscountList(LMM07000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000Model.GetChargesDiscountListAsync(poParam);

                ChargesDiscountGrid = new ObservableCollection<LMM07000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoicePeriodList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000UniversalModel.GetInvPeriodListAsync();
                InvoicePeriodList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDiscountTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000UniversalModel.GetDiscountTypeListAsync();
                DiscountTypeList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesDiscount(LMM07000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000Model.R_ServiceGetRecordAsync(poParam);

                //set charges type
                loResult.CCHARGES_TYPE = poParam.CCHARGES_TYPE;

                ChargesDiscount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ChargesDiscountValidation(LMM07000DTO poParam)
        {
            var loEx = new R_Exception();
            bool llCancel;
            LMM07000PeriodDTO MaxPeriod = null;
            LMM07000PeriodDTO MaxPeriodParam = new LMM07000PeriodDTO();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CDISCOUNT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "7010"));
                }
                else
                {
                    llCancel = poParam.CDISCOUNT_CODE.Length > 20;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "7011"));
                    }
                }


                llCancel = string.IsNullOrWhiteSpace(poParam.CDISCOUNT_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "7012"));
                }
                else
                {
                    llCancel = poParam.CDISCOUNT_NAME.Length > 100;
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "7013"));
                    }
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDISCOUNT_TYPE);

                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "7014"));
                }

                llCancel = poParam.NDISCOUNT_VALUE <= 0 || poParam.NDISCOUNT_VALUE > 100;

                if (poParam.CDISCOUNT_TYPE == "02" || poParam.CDISCOUNT_TYPE == "03")
                {
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "7016"));
                    }
                }

                llCancel = poParam.NDISCOUNT_VALUE <= 0;

                if (poParam.CDISCOUNT_TYPE == "01" )
                {
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "7015"));
                    }
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CAPPLY_PERIOD_YEAR_FROM) || string.IsNullOrWhiteSpace(poParam.CAPPLY_PERIOD_YEAR_TO);

                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "7017"));
                }
                else
                {
                    if (FromPeriodYear  > ToPeriodYear)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "7018"));
                    }

                    MaxPeriodParam.CYEAR = poParam.CAPPLY_PERIOD_YEAR_FROM;
                    MaxPeriod = await _LMM07000Model.GetMaxInvoicePeriodValueAsync(MaxPeriodParam);

                    if (FromPeriodYear > MaxPeriod.INO_PERIOD || FromPeriodYear < 1)
                    {
                        loEx.Add("7019", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "7019"), MaxPeriod.INO_PERIOD));
                    }

                    MaxPeriodParam.CYEAR = poParam.CAPPLY_PERIOD_YEAR_TO;
                    MaxPeriod = await _LMM07000Model.GetMaxInvoicePeriodValueAsync(MaxPeriodParam);

                    if (ToPeriodYear > MaxPeriod.INO_PERIOD || ToPeriodYear < 1)
                    {
                        loEx.Add("7020", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "7020"), MaxPeriod.INO_PERIOD));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChargesDiscount(LMM07000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM07000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                ChargesDiscount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteChargesDiscount(LMM07000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM07000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LMM07000DTO> ActiveInactiveProcessAsync(LMM07000DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            LMM07000DTO loRtn = null;

            try
            {
                // set Status
                poParameter.LACTIVE = StatusChange;

                await _LMM07000Model.LMM07000ActiveInactiveAsync(poParameter);
                var loResult = await _LMM07000Model.R_ServiceGetRecordAsync(poParameter);

                //set charges type
                loResult.CCHARGES_TYPE = poParameter.CCHARGES_TYPE;

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

    }
}
