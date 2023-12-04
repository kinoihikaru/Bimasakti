using BlazorClientHelper;
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
using System;

namespace LMM01000FRONT
{
    public partial class LMM01010 : R_Page, R_ITabPage
    {
        private LMM01010ViewModel _viewModel = new LMM01010ViewModel();
        private LMM01010SaveBatchViewModel _viewModelSave = new LMM01010SaveBatchViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateUC_conductorRef;
        private R_ConductorGrid _RateUCDetail_conductorRef;

        private R_Grid<LMM01011DTO> _RateUCDetail_gridRef;
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            LMM01010DTO loParam = null;

            try
            {
                //Load Radio Button
                await _Universal_viewModel.GetUsageRateModelList();
                await _Universal_viewModel.GetRateTypeList();
                await _Universal_viewModel.GetAdminFeeTypeList();

                loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(poParameter);
                await _viewModel.GetProperty(loParam);

                await RateUC_CheckData(loParam);
                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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

        private R_RadioGroup<LMM01000UniversalDTO, string> UsageRateMode_RadioGrp;
        private async Task RateUC_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await UsageRateMode_RadioGrp.FocusAsync();
            }
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
        private void RateUc_Admin_OnChange(string poParam)
        {
            _viewModel.Data.CADMIN_FEE = poParam;
            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }

        private async Task RateUC_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                EnableEditGrid = false;
                await _RateUCDetail_conductorRef.R_SaveBatch();
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
                await _viewModelSave.SaveRateEC((LMM01010DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode ,clientHelper.CompanyId, clientHelper.UserId);

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
                EnableEditGrid = false;
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
                EnableEditGrid = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(eventArgs.Data);

                await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
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
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01011DTO>(loEventParam);

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

            var loData = (List<LMM01011DTO>)eventArgs.Data;

            if (loData.Count > 0 && loData is not null)
                _viewModel.Data.CRATE_EC_LIST.AddRange(loData);
        }

        private async Task RateUCDetail_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            await _RateUCDetail_conductorRef.R_SaveBatch();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01010DTO>(poParam);
                if (string.IsNullOrWhiteSpace(loParam.CCHARGES_ID))
                {
                    await _RateUC_conductorRef.R_SetCurrentData(null);
                    _RateUCDetail_gridRef.DataSource.Clear();
                }
                else
                {

                    await _viewModel.GetProperty(loParam);

                    await RateUC_CheckData(loParam);
                    await _RateUCDetail_gridRef.R_RefreshGrid(loParam);
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
                var loParam = new LMM01010PrintParamDTO()
                {
                    CPROPERTY_ID = _viewModel.RateEC.CPROPERTY_ID,
                    CCHARGES_TYPE = _viewModel.RateEC.CCHARGES_TYPE,
                    CCHARGES_ID = _viewModel.RateEC.CCHARGES_ID,
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
                     "rpt/LMM01010Print/AllRateECReportPost",
                     "rpt/LMM01010Print/AllStreamRateECReportGet",
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
