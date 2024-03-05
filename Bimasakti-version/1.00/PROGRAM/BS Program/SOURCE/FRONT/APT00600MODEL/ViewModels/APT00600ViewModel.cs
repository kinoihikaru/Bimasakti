using APT00600COMMON;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APT00600MODEL
{
    public class APT00600ViewModel
    {
        private APT00600Model _APT00600Model = new APT00600Model();
        private APT00600InitModel _APT00600InitModel = new APT00600InitModel();

        #region Property Class
        public ObservableCollection<APT00600DTO> PurchaseAdjusmentGrid { get; set; } = new ObservableCollection<APT00600DTO>();
        public List<APT00600PropertyDTO> PropertyList { get; set; } = new List<APT00600PropertyDTO>();
        public APT00600PeriodYearRangeGSDTO VAR_GSM_PERIOD { get; set; } = new APT00600PeriodYearRangeGSDTO();
        public APT00600ParamDTO PurchaseAdjuParam { get; set; } = new APT00600ParamDTO();
        public APT00600TodayDateDBDTO VAR_TODAY { get; set; } = new APT00600TodayDateDBDTO();
        #endregion

        #region Property ViewModel
        public string CSUPPLIER_OPTIONS { get; set; } = "A";
        public int PERIOD_FROM_YEAR { get; set; } 
        public string PERIOD_FROM_MONTH { get; set; }
        public int PERIOD_TO_YEAR { get; set; } 
        public string PERIOD_TO_MONTH { get; set; }
        #endregion

        #region Combo Box Helper List
        public List<KeyValuePair<string, string>> CSUPPLIER_OPTIONS_LIST { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("A", "All Suppliers"),
            new KeyValuePair<string, string>("S", "Selected Supplier")
        };
        public List<KeyValuePair<string, string>> CFROM_PERIOD_LIST { get; } = new List<KeyValuePair<string, string>>()
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
        public List<KeyValuePair<string, string>> CTO_PERIOD_LIST { get; } = new List<KeyValuePair<string, string>>()
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

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00600InitModel.GetAllInitialProcessTabListAsync();

                VAR_TODAY = loResult.VAR_TODAY;
                VAR_GSM_PERIOD = loResult.VAR_GSM_PERIOD;
                PropertyList = loResult.VAR_PROPERTY_LIST;
                if (PropertyList.Count > 0)
                    PurchaseAdjuParam.CPROPERTY_ID = loResult.VAR_PROPERTY_LIST.FirstOrDefault().CPROPERTY_ID;

                PERIOD_TO_YEAR = loResult.VAR_TODAY.DTODAY.Year;
                PERIOD_FROM_YEAR = loResult.VAR_TODAY.DTODAY.Year;
                PERIOD_FROM_MONTH = loResult.VAR_TODAY.DTODAY.ToString("MM");
                PERIOD_TO_MONTH = loResult.VAR_TODAY.DTODAY.ToString("MM");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPurchaseAdjustmentList()
        {
            var loEx = new R_Exception();

            try
            {
                PurchaseAdjuParam.CSUPPLIER_ID = string.IsNullOrWhiteSpace(PurchaseAdjuParam.CSUPPLIER_ID) ? "" : PurchaseAdjuParam.CSUPPLIER_ID;
                PurchaseAdjuParam.CFROM_PERIOD = PERIOD_FROM_YEAR + PERIOD_FROM_MONTH;
                PurchaseAdjuParam.CTO_PERIOD = PERIOD_TO_YEAR + PERIOD_TO_MONTH;

                var loResult = await _APT00600Model.GetPurhcaseAdjustmentStreamAsync(PurchaseAdjuParam);
                loResult.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                });

                PurchaseAdjusmentGrid = new ObservableCollection<APT00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
