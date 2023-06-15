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
    public partial class LMM01040 : R_Page
    {
        private string chargesID;

        private LMM01040ViewModel _viewModel = new LMM01040ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RateGU_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }


        [Parameter]
        public string ChargesID
        {
            get => chargesID;
            set
            {
                chargesID = value;
                if (_RateGU_conductorRef != null)
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
                await RateGU_AdminFeeType_ServiceGetListRecord(null);

                if (string.IsNullOrWhiteSpace(chargesID))
                {
                    await RateGU_CheckData(chargesID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task RateGU_AdminFeeType_ServiceGetListRecord(object eventArgs)
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
                var loParam = new LMM01040DTO();
                loParam.CCHARGES_ID = poParam.ToString();


                var loCheck = await _viewModel.GetRateGUCheckData(loParam);

                if (loCheck != null)
                {
                    await _RateGU_conductorRef.R_GetEntity(poParam);
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
                var loParam = new LMM01040DTO();
                loParam.CCHARGES_ID = eventArgs.Data.ToString();

                await _viewModel.GetRateUG(loParam);

                eventArgs.Result = _viewModel.RateGU;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
