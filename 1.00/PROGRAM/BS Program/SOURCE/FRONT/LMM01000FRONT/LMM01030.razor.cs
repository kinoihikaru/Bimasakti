﻿using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace LMM01000FRONT
{
    public partial class LMM01030 : R_Page, R_ITabPage
    {
        private string chargesID;

        private LMM01030ViewModel _viewModel = new LMM01030ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RatePG_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01030DTO loParam;
                loParam = R_FrontUtility.ConvertObjectToObject<LMM01030DTO>(poParameter);
                await _viewModel.GetProperty(loParam);

                await RatePG_AdminFeeType_ServiceGetListRecord(null);

                await RatePG_CheckData(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RatePG_AdminFeeType_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetAdminFeeTypeList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task RatePG_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRatePGCheckData((LMM01030DTO)poParam);

                if (loCheck != null)
                {
                    await _RatePG_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RatePG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRatePG((LMM01030DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RatePG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private R_NumericTextBox<decimal> StandingCharges_NumericTextBox;
        private async Task RatePG_Display(R_DisplayEventArgs eventArgs)
        {
            await StandingCharges_NumericTextBox.FocusAsync();
        }
        private bool PrintBtnEnable = false;
        private void RatePG_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }

        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RatePG_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;
            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }

        private async Task RatePG_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveRatePC((LMM01030DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RatePG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RatePG_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeeAmtEnable = false;
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private void RatePG_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeeAmtEnable = false;
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01030DTO loParam;
                if (poParam is not null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<LMM01030DTO>(poParam);
                    await _viewModel.GetProperty(loParam);

                    await RatePG_AdminFeeType_ServiceGetListRecord(null);

                    await RatePG_CheckData(loParam);
                }
                else
                {
                    loParam = new LMM01030DTO();

                    await RatePG_AdminFeeType_ServiceGetListRecord(null);

                    await RatePG_CheckData(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickPrintAsync()
        {
            var loEx = new R_Exception();

            try
            {
                // set Param
                var loParam = new LMM01030PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RatePG.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RatePG.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RatePG.CCHARGES_ID,
                    CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME,
                };
                loParam.CUSER_ID = clientHelper.UserId;
                loParam.CCOMPANY_ID = clientHelper.CompanyId;

                var loValidate = await R_MessageBox.Show("", "Are you sure print this?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                     "R_DefaultServiceUrlLM",
                     "LM",
                     "rpt/LMM01030Print/AllRatePGReportPost",
                     "rpt/LMM01030Print/AllStreamRatePGReportGet",
                     loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
