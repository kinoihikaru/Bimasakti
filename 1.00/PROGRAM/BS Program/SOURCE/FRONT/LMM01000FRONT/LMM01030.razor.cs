using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace LMM01000FRONT
{
    public partial class LMM01030 : R_Page
    {
        private string chargesID;

        private LMM01030ViewModel _viewModel = new LMM01030ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Conductor _RatePG_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }


        [Parameter]
        public string ChargesID
        {
            get => chargesID;
            set
            {
                chargesID = value;
                if (_RatePG_conductorRef != null)
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
                await RatePG_AdminFeeType_ServiceGetListRecord(null);

                if (string.IsNullOrWhiteSpace(chargesID))
                {
                    await RatePG_CheckData(chargesID);
                }
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
                var loParam = new LMM01030DTO();
                loParam.CCHARGES_ID = poParam.ToString();


                var loCheck = await _viewModel.GetRatePGCheckData(loParam);

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

        private async Task RateWG_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01030DTO();
                loParam.CCHARGES_ID = eventArgs.Data.ToString();

                await _viewModel.GetRatePG(loParam);

                eventArgs.Result = _viewModel.RatePG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
