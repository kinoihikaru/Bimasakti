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
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM01000FRONT
{
    public partial class LMM01020 : R_Page, R_ITabPage
    {
        private LMM01020ViewModel _viewModel = new LMM01020ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateWG_conductorRef;
        private R_ConductorGrid _RateWGDetail_conductorRef;
        private R_Grid<LMM01021DTO> _RateWGDetail_gridRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(poParameter);

                await RateWG_UsageRateMode_ServiceGetListRecord(null);
                await RateWG_AdminFeeType_ServiceGetListRecord(null);

                await RateWG_CheckData(loParam);
                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_UsageRateMode_ServiceGetListRecord(object eventArgs)
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

        private async Task RateWG_AdminFeeType_ServiceGetListRecord(object eventArgs)
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

        private async Task RateWG_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loCheck = await _viewModel.GetRateWGCheckData((LMM01020DTO)poParam);

                if (loCheck != null)
                {
                    await _RateWG_conductorRef.R_GetEntity(poParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetRateWG((LMM01020DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.RateWG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool PrintBtnEnable = false;
        private void RateWG_SetHasData(R_SetEventArgs eventArgs)
        {
            PrintBtnEnable = eventArgs.Enable;
        }

        private bool EnableEdit = false;
        private void RateWG_SetEdit(R_SetEventArgs eventArgs)
        {
            EnableEdit = eventArgs.Enable;
        }

        private bool AdminFeePctEnable = false;
        private bool AdminFeeAmtEnable = false;
        private void RateWG_Admin_OnChange(object poParam)
        {
            AdminFeePctEnable = (string)poParam == "01";
            if ((string)poParam == "01")
                _viewModel.Data.NADMIN_FEE_AMT = 0;

            AdminFeeAmtEnable = (string)poParam == "02";
            if ((string)poParam == "02")
                _viewModel.Data.NADMIN_FEE_PCT = 0;
        }

        private async Task RateWG_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _RateWGDetail_conductorRef.R_SaveBatch();

                await _viewModel.SaveRateEC((LMM01020DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RateWG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task RateWG_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(eventArgs.Data);

                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateWG_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                AdminFeePctEnable = false;
                AdminFeeAmtEnable = false;
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01020DTO>(eventArgs.Data);

                await _RateWGDetail_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RateWG_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.RateWGDetailList.ToList().Count > 0)
                    _viewModel.Data.CRATE_WG_LIST = _viewModel.RateWGDetailList.ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task RateWGDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loEventParam = (LMM01020DTO)eventArgs.Parameter;

                var loParam = new LMM01021DTO();
                loParam.CCHARGES_ID = loEventParam.CCHARGES_ID;

                await _viewModel.GetRateWGDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateWGDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateWGDetail_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void RateWGDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loParentData = (LMM01020DTO)_RateWG_conductorRef.R_GetCurrentData();
            var loData = (LMM01021DTO)eventArgs.Data;
            loData.CCOMPANY_ID = loParentData.CCOMPANY_ID;
            loData.CPROPERTY_ID = loParentData.CPROPERTY_ID;
            loData.CCHARGES_TYPE = loParentData.CCHARGES_TYPE;
            loData.CCHARGES_ID = loParentData.CCHARGES_ID;
        }
        private void RateWGDetail_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeePctEnable = false;
            _RateWGDetail_conductorRef.R_SaveBatch();
        }
        private void RateUCDetail_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            AdminFeePctEnable = false;
            AdminFeePctEnable = false;
        }
        private void RateWGDetail_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            _viewModel.Data.CRATE_WG_LIST = new List<LMM01021DTO>();
            _viewModel.Data.CRATE_WG_LIST = (List<LMM01021DTO>)eventArgs.Data;
        }

        private async Task RateWGDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            bool lCancel = false;
            var loData = (LMM01021DTO)eventArgs.Data;
            try
            {
                foreach (var item in _viewModel.Data.CRATE_WG_LIST)
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

        public Task RefreshTabPageAsync(object poParam)
        {
            throw new NotImplementedException();
        }
    }
}
