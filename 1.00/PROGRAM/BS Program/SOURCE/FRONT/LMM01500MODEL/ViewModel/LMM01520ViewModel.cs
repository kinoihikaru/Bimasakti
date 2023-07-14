using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;

namespace LMM01500MODEL
{
    public class LMM01520ViewModel : R_ViewModel<LMM01520DTO>
    {
        private LMM01520Model _LMM01520Model = new LMM01520Model();

        public LMM01520DTO InvPinalty = new LMM01520DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode = "";
        public string InvGrpName = "";

        public bool MinPinaltyAmount { get; set; } = false;
        public bool MaxPinaltyAmount { get; set; } = false;

        public async Task GetInvoicePinalty()
        {
            var loEx = new R_Exception();
            try
            {
                if(!string.IsNullOrWhiteSpace(PropertyValueContext))
                    R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                if (!string.IsNullOrWhiteSpace(InvGrpCode))
                    R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                var loParam = new LMM01520DTO();
                var loResult = await _LMM01520Model.R_ServiceGetRecordAsync(loParam);

                switch (loResult.CPENALTY_TYPE)
                {
                    case "10":
                        loResult.NPENALTY_TYPE_VALUE_MonthlyAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "11":
                        loResult.NPENALTY_TYPE_VALUE_MonthlyPercentage = loResult.NPENALTY_TYPE_VALUE;
                        loResult.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = loResult.CPENALTY_TYPE_CALC_BASEON;
                        break;
                    case "20":
                        loResult.NPENALTY_TYPE_VALUE_DailyAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "21":
                        loResult.NPENALTY_TYPE_VALUE_DailyPercentage = loResult.NPENALTY_TYPE_VALUE;
                        loResult.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = loResult.CPENALTY_TYPE_CALC_BASEON;
                        break;
                    case "30":
                        loResult.NPENALTY_TYPE_VALUE_OneTimeAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "31":
                        loResult.NPENALTY_TYPE_VALUE_OneTimePercentage = loResult.NPENALTY_TYPE_VALUE;
                        break;
                }

                InvPinalty = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoicePinalty(LMM01520DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrEmpty(poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth))
                {
                    poNewEntity.CPENALTY_TYPE_CALC_BASEON = poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth;
                }
                else if (!string.IsNullOrEmpty(poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays))
                {
                    poNewEntity.CPENALTY_TYPE_CALC_BASEON = poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays;
                }

                if (poNewEntity.NPENALTY_TYPE_VALUE_MonthlyAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_MonthlyAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_MonthlyPercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_MonthlyPercentage;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_DailyAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_DailyAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_DailyPercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_DailyPercentage;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_OneTimeAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_OneTimeAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_OneTimePercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_OneTimePercentage;
                }

                var loResult = await _LMM01520Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                InvPinalty = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public List<RadioButton> RadioPinaltyType { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "10", Text = "Monthly Amount"},
            new RadioButton { Id = "11", Text = "Monthly Percentage"},
            new RadioButton { Id = "20", Text = "Daily Amount"},
            new RadioButton { Id = "21", Text = "Daily Percentage"},
            new RadioButton { Id = "30", Text = "One Time Amount"},
            new RadioButton { Id = "31", Text = "One Time Percentage"},
        };

        public List<RadioButton> RadioPinaltyTypeCalcBaseonMonth { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Principal"},
            new RadioButton { Id = "02", Text = "Principal and Penalty"},

        };

        public List<RadioButton> RadioPinaltyTypeCalcBaseonDays { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Principal"},
            new RadioButton { Id = "02", Text = "Principal and Penalty"},

        };

        public List<RadioButtonInt> RadioRounded { get; set; } = new List<RadioButtonInt>
        {
            new RadioButtonInt { Id = -2, Text = "-2"},
            new RadioButtonInt { Id = -1, Text = "-1"},
            new RadioButtonInt { Id = 0, Text = "0"},
            new RadioButtonInt { Id = 1, Text = "1"},
            new RadioButtonInt { Id = 2, Text = "2"},
        };

        public List<RadioButton> RadioCutOfDate { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Due Date"},
            new RadioButton { Id = "02", Text = "1st Day of the Next Month"},

        };

        public List<RadioButton> RadioPinaltyFeeStartFrom { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Grace Period "},
            new RadioButton { Id = "02", Text = "Due Date"},

        };
    }
}
