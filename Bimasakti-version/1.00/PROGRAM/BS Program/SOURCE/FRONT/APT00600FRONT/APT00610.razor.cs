using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using APT00600MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using APT00600COMMON;
using R_BlazorFrontEnd.Helpers;
using APT00600FrontResources;
using R_BlazorFrontEnd.Controls.DataControls;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;
using Lookup_APCOMMON.DTOs.APL00110;
using GLF00100COMMON;
using GLF00100FRONT;

namespace APT00600FRONT
{
    public partial class APT00610 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<APT00600FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        private APT00610ViewModel _viewModel = new APT00610ViewModel();
        private R_Grid<APT00610DTO> _gridPurchaseAdjAllocRef;
        private R_Conductor _conRef;

        #region Private Property
        private R_ComboBox<APT00600PropertyDTO, string> _propertyComboBox;
        private R_TextBox _supplierIdTextBox;
        private int _TransStatusInt = 0;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();

                if (poParameter != null)
                {
                    await _conRef.R_GetEntity(poParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_MODULE_NAME = "AP";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (APT00600DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APT00610",
                        Table_Name = "APT_TRANS_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APT00610",
                        Table_Name = "APT_TRANS_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion

        #region Property
        private void PropertyDropdown_ValueChanged(string poParam)
        {
            _viewModel.Data.CPROPERTY_ID = poParam;
            _viewModel.Data.CSUPPLIER_ID = "";
            _viewModel.Data.CSUPPLIER_NAME = "";
            _viewModel.Data.CDEPT_CODE = "";
            _viewModel.Data.CDEPT_NAME = "";
        }
        #endregion

        #region Form Header
        private async Task PurchAdj_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<APT00600DTO>(eventArgs.Data);
                await _viewModel.GetPurchaseAdjustment(loData);

                eventArgs.Result = _viewModel.PurchaseAdjusment;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void PurchAdj_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.VAR_ADJUSTMENT_TRANS_CODE.LINCREMENT_FLAG == false)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V036"));
                }
                eventArgs.Cancel = _viewModel.VAR_ADJUSTMENT_TRANS_CODE.LINCREMENT_FLAG == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdj_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (APT00600DTO)eventArgs.Data;
            if (_viewModel.PropertyList.Count > 0)
                loData.CPROPERTY_ID = _viewModel.PropertyList.FirstOrDefault().CPROPERTY_ID;

            _viewModel.RefDate = _viewModel.VAR_TODAY.DTODAY;

            await _propertyComboBox.FocusAsync();
        }

        private void PurchAdj_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (APT00600DTO)eventArgs.Data;

                lCancel = string.IsNullOrEmpty(loData.CPROPERTY_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V003"));
                }

                lCancel = string.IsNullOrEmpty(loData.CDEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V004"));
                }

                lCancel = _viewModel.RefDate == null;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V005"));
                }
                else
                {
                    lCancel = int.Parse(_viewModel.RefDate.Value.ToString("yyyyMMdd")) < int.Parse(_viewModel.VAR_SOFT_PERIOD_START_DATE.CSTART_DATE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V006"));
                    }
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V007"));
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_SEQ_NO) && loData.LONETIME == true;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V008"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTRANS_DESC);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V009"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PurchAdj_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SavePurchaseAdjustment((APT00600DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.PurchaseAdjusment;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdj_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N002"], R_eMessageBoxButtonType.YesNo);
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdj_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N001"], R_eMessageBoxButtonType.YesNo);
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdj_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeletePurchaseAdjustment((APT00600DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdj_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
            {
                var loData = (APT00600DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                {
                    await _gridPurchaseAdjAllocRef.R_RefreshGrid(loData);
                }

                int.TryParse(loData.CTRANS_STATUS, out  _TransStatusInt);
            }

            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await _supplierIdTextBox.FocusAsync();
            }
        }
        #endregion

        #region Grid Detail
        private async Task PurchAdjAlloc_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<APT00610DTO>(eventArgs.Parameter);
                await _viewModel.GetPurchaseAdjustmentAllocList(loData);

                eventArgs.ListEntityResult = _viewModel.PurchaseAdjustmentAllocGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Dept Lookup
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CUSER_LOGIN_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00600DTO)_conRef.R_GetCurrentData();
            loData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Supplier Lookup
        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CLANGUAGE_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00100);
        }

        private void R_After_Open_LookupSupplier(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00100DTO loTempResult = (APL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00600DTO)_conRef.R_GetCurrentData();
            loData.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            loData.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            loData.LONETIME = loTempResult.LONETIME;
            loData.CSUPPLIER_SEQ_NO = "";
        }
        #endregion

        #region Supplier SEQ Lookup
        private void R_Before_Open_LookupSupplierSEQ(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00110ParameterDTO loParam = new APL00110ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSUPPLIER_ID = _viewModel.Data.CSUPPLIER_ID,
                CLANGUAGE_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00110);
        }

        private void R_After_Open_LookupSupplierSEQ(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00110DTO loTempResult = (APL00110DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00600DTO)_conRef.R_GetCurrentData();
            loData.CSUPPLIER_SEQ_NO = loTempResult.CSEQ_NO;
            loData.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
            
        }
        #endregion

        #region Submit
        private async Task SubmitPurchaseAdjProcess()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N003"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loData = _conRef.R_GetCurrentData();
                    var loParam = R_FrontUtility.ConvertObjectToObject<APT00600SubmitRedraftDTO>(loData);

                    await _viewModel.SubmitPurchaseAdjustment(loParam);
                    await _conRef.R_GetEntity(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Redraft
        private async Task RedraftPurchaseAdjProcess()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_N004"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loData = _conRef.R_GetCurrentData();
                    var loParam = R_FrontUtility.ConvertObjectToObject<APT00600SubmitRedraftDTO>(loData);
                    loParam.CNEW_STATUS = "00";

                    await _viewModel.RedraftPurchaseAdjustment(loParam);
                    await _conRef.R_GetEntity(loData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Allocate
        private void R_Before_Open_PopupAllocate(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = _conRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APT00620);
        }
        private async Task R_After_Open_PopupAllocate(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = _conRef.R_GetCurrentData();
                await _conRef.R_GetEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Journal
        private void R_Before_Open_PopupJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = _conRef.R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<GLF00100ParameterDTO>(loData);
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLF00100);
        }
        #endregion
    }
}
