using BlazorClientHelper;
using GLB09900COMMON;
using GLB09900FrontResources;
using GLB09900MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLB09900FRONT
{
    public partial class GLB09900 : R_Page
    {
        private GLB09900ViewModel _CloseGLPeriod_viewModel = new GLB09900ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await ClosePeriod_InitialVar_ServiceGetRecord(null);
                await ClosePeriod_SystemParam_ServiceGetRecord(null);

                if (_CloseGLPeriod_viewModel.SystemParam.CSOFT_PERIOD == _CloseGLPeriod_viewModel.SystemParam.CCURRENT_PERIOD)
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorSoftClose"), R_eMessageBoxButtonType.OK);
                    await this.CloseProgram();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosePeriod_InitialVar_ServiceGetRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _CloseGLPeriod_viewModel.GetInitialVar();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosePeriod_SystemParam_ServiceGetRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _CloseGLPeriod_viewModel.GetSystemParam();
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ClosePeriod_ServiceGetRecord()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB09900DTO() { CPERIOD = _CloseGLPeriod_viewModel.SystemParam.CCURRENT_PERIOD };
                await _CloseGLPeriod_viewModel.GetResultClosing(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task Button_OnClickPeriodEndAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLB09900ValidateDTO() { CPERIOD = _CloseGLPeriod_viewModel.SystemParam.CCURRENT_PERIOD };
                bool loValidate = await _CloseGLPeriod_viewModel.GetValidateResultClosing(loParam);

                if (loValidate)
                {
                    var loResult = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifAlertClosePeiod"), R_eMessageBoxButtonType.YesNo);

                    if (loResult == R_eMessageBoxResult.Yes)
                    {
                        await ClosePeriod_ServiceGetRecord();
                        await this.CloseProgram();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.CloseProgram();
        }
    }
}
