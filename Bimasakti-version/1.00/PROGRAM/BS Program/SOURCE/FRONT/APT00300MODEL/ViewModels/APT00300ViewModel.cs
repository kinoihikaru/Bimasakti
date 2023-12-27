using APT00300COMMON;
using APT00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace APT00300MODEL
{
    public class APT0300ViewModel : R_ViewModel<APT00300DTO>
    {
        private APT00300Model _APT00300Model = new APT00300Model();
        private APT00300UniversalModel _APT00300UniversalModel = new APT00300UniversalModel();
        public ObservableCollection<APT00300DTO> PurchaseDebitGrid { get; set; } = new ObservableCollection<APT00300DTO>();
        public APT00300ParameterDTO ParameterGridPurchase { get; set; } = new APT00300ParameterDTO();

        #region Initial Process
        public List<APT00300PropertyDTO> PropertyList { get; set; } = new List<APT00300PropertyDTO>();
        private APT00300PeriodYearRangeDTO RangePeriodYear { get; set; } = new APT00300PeriodYearRangeDTO();
        public async Task GetAllInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00300UniversalModel.GetAllInitialVarTab1Async();

                PropertyList = loResult.VAR_PROPERTY_LIST;
                RangePeriodYear = loResult.VAR_GSM_PERIOD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Combo Box or Radio Group Helper List
        public string SupplierMode { get; set; } = "A";
        public List<KeyValuePair<string, string>> SupplierOptions { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("A", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_SuppModeA")),
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_SuppModeS"))
        };
        public int PeriodYearFrom { get; set; } = DateTime.Now.Year;
        public string PeriodMonthFrom { get; set; } = DateTime.Now.Month.ToString("D2");
        public int PeriodYearTo { get; set; } = DateTime.Now.Year;
        public string PeriodMonthTo { get; set; } = DateTime.Now.Month.ToString("D2");
        public List<KeyValuePair<string, string>> Period_Month { get; set; } = GenerateMonth();
        private static List<KeyValuePair<string, string>> GenerateMonth()
        {
            return Enumerable.Range(1, 12)
                .Select(i => new KeyValuePair<string, string>($"{i:D2}", $"{i:D2}"))
                .ToList();
        }
        #endregion

        public async Task GetPurchaseDebitList()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = ParameterGridPurchase;
                loParam.CFROM_PERIOD = PeriodYearFrom.ToString() + PeriodMonthFrom;
                loParam.CTO_PERIOD = PeriodYearTo.ToString() + PeriodMonthTo;

                var loResult = await _APT00300Model.GetDebitNoteListStreamAsync(loParam);

                PurchaseDebitGrid = new ObservableCollection<APT00300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationRefresh()
        {
            var loEx = new R_Exception();
            bool llCancel = false;

            try
            {
                llCancel = string.IsNullOrWhiteSpace(ParameterGridPurchase.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err01"));
                }

                llCancel = string.IsNullOrWhiteSpace(ParameterGridPurchase.CSUPPLIER_ID) && SupplierMode == "S";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "Err02"));
                }

                llCancel = (PeriodYearFrom < RangePeriodYear.IMIN_YEAR || PeriodYearFrom > RangePeriodYear.IMAX_YEAR) || (PeriodYearTo < RangePeriodYear.IMIN_YEAR || PeriodYearTo > RangePeriodYear.IMAX_YEAR);
                if (llCancel)
                {
                    loEx.Add("Err03", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Err03"), RangePeriodYear.IMIN_YEAR, RangePeriodYear.IMAX_YEAR));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
