using BlazorClientHelper;
using GLB00600COMMON;
using GLB00600FrontResources;
using GLB00600MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLB00600FRONT
{
    public partial class GLB00600 : R_Page
    {
        private GLB00600ViewModel _CloseEntries_viewModel = new GLB00600ViewModel();
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = await _CloseEntries_viewModel.GetSystemParam();

                if (loData.CCLOSE_DEPT_CODE == null)
                {
                    await R_MessageBox.Show("", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifMustMaintainParamProperty"), clientHelper.CompanyId), R_eMessageBoxButtonType.OK);
                    await this.CloseProgram();
                }
                else
                {
                    if (loData.CSUSPENSE_ACCOUNT_NO == "")
                    {
                        await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorSetupSuspence"), R_eMessageBoxButtonType.OK);
                        await this.CloseProgram();
                    }
                    else
                    {
                        if (loData.CRETAINED_ACCOUNT_NO == "")
                        {
                            await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorSetupRetained"), R_eMessageBoxButtonType.OK);
                            await this.CloseProgram();
                        }
                        else
                        {
                            var loParam = new GLB00600InitialDTO() { CYEAR = loData.CCURRENT_PERIOD_YY };
                            await _CloseEntries_viewModel.GetInitialVar(loParam);

                            var loCurrentPeriodRight = loData.CCURRENT_PERIOD_YY;
                            int ValidationInitial = int.Parse(loCurrentPeriodRight);

                            if (ValidationInitial != _CloseEntries_viewModel.InitialVar.INO_PERIOD)
                            {
                                await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorOnlyForYearEndProcess"), R_eMessageBoxButtonType.OK);
                                await this.CloseProgram();
                            }
                            else
                            {
                                var loSuspendParam = new GLB00600SuspenseAmountDTO() { CGLACCOUNT_NO = loData.CSUSPENSE_ACCOUNT_NO, CPERIOD = loData.CCURRENT_PERIOD };
                                await _CloseEntries_viewModel.GetSsuspenAmount(loSuspendParam);

                                if (_CloseEntries_viewModel.SuspenseAmountDTO.NSUSPENSE != 0)
                                {
                                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorSuspenseAccountJrnl"), R_eMessageBoxButtonType.OK);
                                    await this.CloseProgram();
                                }
                                else
                                {
                                    await _CloseEntries_viewModel.GetGSMTransactionCode();

                                    if (loData.CSOFT_PERIOD == loData.CCURRENT_PERIOD)
                                    {
                                        await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorSoftClose"), R_eMessageBoxButtonType.OK);
                                        await this.CloseProgram();
                                    }
                                    else
                                    {
                                        _CloseEntries_viewModel.SystemParam = loData;
                                    }
                                }
                            }
                        }
                    }
                }

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
                if (!_CloseEntries_viewModel.GSMTransactionCode.LINCREMENT_FLAG)
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorClosingManualNumbering"), R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await _CloseEntries_viewModel.GetGSMValidationResult();

                    if (_CloseEntries_viewModel.ResultClose.Any(x => x.CDEPT_CODE != _CloseEntries_viewModel.SystemParam.CCLOSE_DEPT_CODE))
                    {
                        await R_MessageBox.Show("", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifErrorUserNotHaveAccessDept"), clientHelper.UserId, _CloseEntries_viewModel.SystemParam.CCLOSE_DEPT_CODE), R_eMessageBoxButtonType.OK);
                    }
                    else
                    {
                        var loConfirmation = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifAlertBeforeClosingEntries"), R_eMessageBoxButtonType.YesNo);

                        if (loConfirmation == R_eMessageBoxResult.Yes)
                        {
                            var loParam = new GLB00600DTO();
                            await _CloseEntries_viewModel.GetGSMResult(loParam);

                            await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifClosingIsSuccess"), R_eMessageBoxButtonType.OK);

                            await this.CloseProgram();
                        }
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
