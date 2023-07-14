using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Diagnostics.Tracing;

namespace LMM01000FRONT
{
    public partial class LMM01010 : R_Page, R_ITabPage
    {
        private LMM01010ViewModel _viewModel = new LMM01010ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateUC_conductorRef;
        private R_ConductorGrid _RateUCDetail_conductorRef;

        private R_Grid<LMM01011DTO> _RateUCDetail_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(poParameter);

                await RateUC_UsageRateMode_ServiceGetListRecord(null);
                await RateUC_AdminFeeType_ServiceGetListRecord(null);
                await RateUC_RateType_ServiceGetListRecord(null);

                await RateUC_CheckData(loParam);
                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateUC_UsageRateMode_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetUsageRateModelList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task RateUC_RateType_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetRateTypeList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task RateUC_AdminFeeType_ServiceGetListRecord(object eventArgs)
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

        private async Task RateUC_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateEC((LMM01010DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateEC;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool PrintBtnEnable = false;
        private void RateUC_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }

        private bool EnableEditGrid = false;
        private void RateUC_SetEdit(R_SetEventArgs eventArgs)
        {
            EnableEditGrid = eventArgs.Enable;
        }

        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RateUc_Admin_OnChange(object poParam)
        {
            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }

        private void RateUC_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
               
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateUC_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveRateEC((LMM01010DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RateEC;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateUC_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(eventArgs.Data);

                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateUC_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(eventArgs.Data);

                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateUC_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.RateUCDetailList.ToList().Count > 0)
                    _viewModel.Data.CRATE_EC_LIST = _viewModel.RateUCDetailList.ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateUC_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (LMM01010DTO)eventArgs.Parameter;
                var loParam = new LMM01011DTO();
                loParam.CCHARGES_ID = loEventParam.CCHARGES_ID;

                await _viewModel.GetRateUCDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateUCDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RateUC_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateECCheckData((LMM01010DTO)poParam);

                if (loCheck != null)
                {
                    await _RateUC_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateUCDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private void RateUCDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01010DTO)_RateUC_conductorRef.R_GetCurrentData();
            var loData = (LMM01011DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
        }

        private async Task RateUCDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel = false;
            var loData = (LMM01011DTO)eventArgs.Data;
            try
            {
                foreach (var item in _viewModel.Data.CRATE_EC_LIST)
                {
                    lCancel = item.IUP_TO_USAGE == loData.IUP_TO_USAGE;
                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Duplicate Usage (Kwh)", R_eMessageBoxButtonType.OK);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private void RateUCDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            _viewModel.Data.CRATE_EC_LIST = new List<LMM01011DTO>();
            _viewModel.Data.CRATE_EC_LIST = (List<LMM01011DTO>)eventArgs.Data;
        }

        private void RateUCDetail_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            _RateUCDetail_conductorRef.R_SaveBatch();
        }

        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }
    }
}
