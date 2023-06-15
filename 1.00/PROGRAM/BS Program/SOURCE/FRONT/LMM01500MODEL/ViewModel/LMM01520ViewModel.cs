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


        public int MonthlyAmmountValue;
        public int MonthlyPercentageValue;
        public int DailyAmmountValue;
        public int DailyPercentageValue;
        public int OneTimeAmmountValue;
        public int OneTimePercentageValue;
        public string CalcBaseonMonthValue = "";
        public string CalcBaseonDaysValue = "";

        public async Task GetInvoicePinalty(LMM01520DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                var loResult = await _LMM01520Model.R_ServiceGetRecordAsync(poParam);

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
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                //if (string.IsNullOrEmpty(PinaltyDays))
                //{
                //    poNewEntity.CPENALTY_TYPE_CALC_BASEON = PinaltyDays;
                //}
                //else if(string.IsNullOrEmpty(PinaltyMonth))
                //{
                //    poNewEntity.CPENALTY_TYPE_CALC_BASEON = PinaltyMonth;
                //}



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
