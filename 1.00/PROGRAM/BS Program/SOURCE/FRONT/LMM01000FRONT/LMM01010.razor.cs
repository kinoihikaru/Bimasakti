using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;

namespace LMM01000FRONT
{
    public partial class LMM01010 : R_Page
    {
        private LMM01010ViewModel _viewModel = new LMM01010ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateUC_conductorRef;
        private R_ConductorGrid _RateUCDetail_conductorRef;

        private R_Grid<LMM01011DTO> _RateUCDetail_gridRef;
        private string chargesID;

        [Inject] IClientHelper clientHelper { get; set; }

        [Parameter] public string ChargesID 
        { 
            get => chargesID; 
            set
            {
                chargesID = value;
                if (_RateUC_conductorRef != null)
                {
                    InitFromMasterAsync(value);
                }
            } 
        }

        [Parameter] public bool EnableAddEdit { get; set; }

        [Parameter] public string ChargesName { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await RateUC_UsageRateMode_ServiceGetListRecord(null);
                await RateUC_AdminFeeType_ServiceGetListRecord(null);
                await RateUC_RateType_ServiceGetListRecord(null);

                if (!string.IsNullOrWhiteSpace(ChargesID))
                {
                    await RateUC_CheckData(ChargesID);
                    await _RateUCDetail_gridRef.R_RefreshGrid(ChargesID);
                }

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
                var loParam = new LMM01010DTO();
                loParam.CCHARGES_ID = eventArgs.Data.ToString();

                await _viewModel.GetRateEC(loParam);

                eventArgs.Result = _viewModel.RateEC;
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
                var loParam = new LMM01011DTO();
                loParam.CCHARGES_ID = eventArgs.Parameter.ToString();

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
                var loParam = new LMM01010DTO();
                loParam.CCHARGES_ID = poParam.ToString();


                var loCheck = await _viewModel.GetRateECCheckData(loParam);

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
            _viewModel.Data.CRATE_EC_LIST = new List<LMM01011DTO>();
            List<LMM01011DTO> Lodata = (List<LMM01011DTO>)eventArgs.Data;
            _viewModel.Data.CRATE_EC_LIST = Lodata;
        }
    }
}
