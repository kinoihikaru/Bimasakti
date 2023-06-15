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

namespace LMM01000FRONT
{
    public partial class LMM01050 : R_Page
    {
        private string chargesID;

        private LMM01050ViewModel _viewModel = new LMM01050ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateOT_conductorRef;
        private R_ConductorGrid _RateOTDetailWD_conductorRef;
        private R_ConductorGrid _RateOTDetailWK_conductorRef;

        private R_Grid<LMM01051DTO> _RateOTDetailWD_gridRef;
        private R_Grid<LMM01051DTO> _RateOTDetailWK_gridRef;

        [Inject] IClientHelper clientHelper { get; set; }


        [Parameter]
        public string ChargesID
        {
            get => chargesID;
            set
            {
                chargesID = value;
                if (_RateOT_conductorRef != null)
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
                await RateOT_AdminFeeType_ServiceGetListRecord(null);

                if (string.IsNullOrWhiteSpace(chargesID))
                {
                    await RateGU_CheckData(chargesID);
                    await _RateOTDetailWD_gridRef.R_RefreshGrid(chargesID);
                    await _RateOTDetailWK_gridRef.R_RefreshGrid(chargesID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateOT_AdminFeeType_ServiceGetListRecord(object eventArgs)
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

        private async Task RateGU_CheckData(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01050DTO();
                loParam.CCHARGES_ID = poParam.ToString();


                var loCheck = await _viewModel.GetRateOTCheckData(loParam);

                if (loCheck != null)
                {
                    await _RateOT_conductorRef.R_GetEntity(poParam);
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
                var loParam = new LMM01050DTO();
                loParam.CCHARGES_ID = eventArgs.Data.ToString();

                await _viewModel.GetRateOT(loParam);

                eventArgs.Result = _viewModel.RateOT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateOTDetailWK_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01051DTO();
                loParam.CCHARGES_ID = eventArgs.Parameter.ToString();

                await _viewModel.GetRateOTWDDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWDDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateOTDetailWD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01051DTO();
                loParam.CCHARGES_ID = eventArgs.Parameter.ToString();

                await _viewModel.GetRateOTWKDetailList(loParam);

                eventArgs.ListEntityResult = _viewModel.RateOTWKDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void RateOTDetailWD_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private void RateWGDetailWK_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

    }
}
