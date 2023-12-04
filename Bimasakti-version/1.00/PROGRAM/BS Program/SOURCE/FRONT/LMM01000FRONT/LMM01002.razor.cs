using BlazorClientHelper;
using LMM01000COMMON;
using LMM01000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Attributes;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace LMM01000FRONT
{
    public partial class LMM01002 : R_Page
    {
        private LMM01002ViewModel _viewModel = new LMM01002ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private R_ComboBox<LMM01000UniversalDTO, string> Charges_Type;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.Property = (LMM01000DTOPropety)poParameter;
                await PrintUtility_RateTypeFrom_ServiceGetListRecord();
                await PrintUtility_RateTypeTo_ServiceGetListRecord();

                await Charges_Type.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task PrintUtility_RateTypeFrom_ServiceGetListRecord()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _viewModel.GetRateTypeFromList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PrintUtility_RateTypeTo_ServiceGetListRecord()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _viewModel.GetRateTypeToList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000PrintParamDTO();
                loParam = _viewModel.Data;

                // set Param
                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CUSER_ID = clientHelper.UserId;
                loParam.CPROPERTY_ID = _viewModel.Property.CPROPERTY_ID;
                loParam.CPROPERTY_NAME = _viewModel.Property.CPROPERTY_NAME;

                await _reportService.GetReport(
                    "R_DefaultServiceUrlLM",
                    "LM",
                    "rpt/LMM01000Print/AllUtilityChargesPost",
                    "rpt/LMM01000Print/AllStreamUtilityChargesGet",
                    loParam);

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
