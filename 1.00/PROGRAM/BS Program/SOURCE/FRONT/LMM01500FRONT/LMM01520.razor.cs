using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500FRONT
{
    public partial class LMM01520 : R_Page
    {
        private LMM01520ViewModel _InvPinalty_viewModel = new LMM01520ViewModel();
        private R_Conductor _InvPinalty_conductorRef;

        [Parameter] public string PropertyIdVal { get; set; }
        [Parameter] public string InvGrpIdVal { get; set; }
        [Parameter] public string InvGrpNameVal { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _InvPinalty_viewModel.InvGrpCode = InvGrpIdVal ;
                _InvPinalty_viewModel.PropertyValueContext = PropertyIdVal;
                _InvPinalty_viewModel.InvGrpName = InvGrpNameVal ;

                LMM01520DTO loData = new LMM01520DTO() 
                {
                    CINVGRP_CODE = InvGrpIdVal,
                    CINVGRP_NAME = InvGrpNameVal,
                };

                LMM01520DTO loResult = new LMM01520DTO();

                var loParam = new R_ServiceGetRecordEventArgs(loData, loResult);
                await Pinalty_ServiceGetRecord(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task Pinalty_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01520DTO loParam = new LMM01520DTO();
                await _InvPinalty_viewModel.GetInvoicePinalty(loParam);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool MonthlyAmmountEnable;
        private bool MonthlyPercentageEnable;
        private bool DailyAmmountEnable;
        private bool DailyPercentageEnable;
        private bool OneTimeAmmountEnable;
        private bool OneTimePercentageEnable;
        private bool CalcBaseonMonthEnable;
        private bool CalcBaseonDaysEnable;

        private void Pinalty_RadioButtonOnChange(object poParam)
        {
            MonthlyAmmountEnable = poParam.ToString() == "10";
            if (poParam.ToString() == "10")
            {

            }
            MonthlyPercentageEnable = poParam.ToString() == "11";
            CalcBaseonMonthEnable = poParam.ToString() == "11";
            if (poParam.ToString() != "11")
            {
                _InvPinalty_viewModel.CalcBaseonMonthValue = "";
            }
            DailyAmmountEnable = poParam.ToString() == "20";
            if (poParam.ToString() == "20")
            {

            }
            DailyPercentageEnable = poParam.ToString() == "21";
            CalcBaseonDaysEnable = poParam.ToString() == "21";
            if (poParam.ToString() != "21")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }
            OneTimeAmmountEnable = poParam.ToString() == "30";
            if (poParam.ToString() != "30")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }
            OneTimePercentageEnable = poParam.ToString() == "31";
            if (poParam.ToString() != "30")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }

        }

        private async Task Pinalty_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinalty_viewModel.SaveInvoicePinalty(
                    (LMM01520DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool MinAmmountEnable;
        private void Pinalty_OnCheckedMinAmmount(object poParam)
        {
            MinAmmountEnable = (bool)poParam;
        }

        private bool MaxAmmountEnable;
        private void Pinalty_OnCheckedMaxAmmount(object poParam)
        {
            MaxAmmountEnable = (bool)poParam;
        }

        private void Pinalty_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                switch (0)
                {
                    case var _ when 0 == _InvPinalty_viewModel.MonthlyAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.MonthlyPercentageValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.DailyAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.DailyPercentageValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.OneTimeAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.OneTimePercentageValue:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Pinalty_OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01522);
        }

        private void Pinalty_OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LMM01522DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _InvPinalty_viewModel.Data.CPENALTY_ADD_ID = loTempResult.CCHARGES_ID;
            _InvPinalty_viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

    }
}
