using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using APT00500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_APFRONT;
using APT00500COMMON;
using R_BlazorFrontEnd.Helpers;
using APT00500FrontResources;
using R_BlazorFrontEnd.Controls.DataControls;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Globalization;
using Lookup_APCOMMON.DTOs.APL00500;

namespace APT00500FRONT
{
    public partial class APT00520 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<APT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }

        private APT00520ViewModel _viewModel = new APT00520ViewModel();
        private R_Grid<APT00510DTO> _gridPurchaseAdjAllocRef;
        private R_Conductor _conRef;

        #region Private Property
        private R_ComboBox<APT00500AllocTrxTypeAPDTO, string> _TrxTypeComboBox;
        private R_NumericTextBox<decimal> _amountNumericTextBox;
        private DateTime? _headerRefDate;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();

                var loHeaderData = (APT00500DTO)poParameter;
                _viewModel.HeaderPurchaseAdju = loHeaderData;
                _headerRefDate = DateTime.ParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                await _gridPurchaseAdjAllocRef.R_RefreshGrid(null);
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
                var loData = (APT00510DTO)eventArgs.Data;

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
                        Program_Id = "APT00520",
                        Table_Name = "APT_ALLOCATION",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _viewModel.HeaderPurchaseAdju.CPROPERTY_ID, _viewModel.HeaderPurchaseAdju.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "APT00520",
                        Table_Name = "APT_ALLOCATION",
                        Key_Value = string.Join("|", clientHelper.CompanyId, _viewModel.HeaderPurchaseAdju.CPROPERTY_ID, _viewModel.HeaderPurchaseAdju.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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

        #region Allocation
        private async Task PurchAdjAlloc_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetPurchaseAdjustmentAllocList();

                eventArgs.ListEntityResult = _viewModel.PurchaseAdjustmentAllocGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PurchAdjAlloc_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (APT00510DTO)eventArgs.Data;
                await _viewModel.GetPurchaseAdjustmentAlloc(loParam);

                eventArgs.Result = _viewModel.PurchaseAdjustmentAlloc;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdjAlloc_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
            {
                await _amountNumericTextBox.FocusAsync();
            }
        }
        private async Task PurchAdjAlloc_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
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
        private async Task PurchAdjAlloc_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
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
        private async Task PurchAdjAlloc_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.VAR_ALLOCATION_TRANS_CODE.LINCREMENT_FLAG == false)
                    await R_MessageBox.Show("", _localizer["V037"], R_eMessageBoxButtonType.OK);

                eventArgs.Cancel = _viewModel.VAR_ALLOCATION_TRANS_CODE.LINCREMENT_FLAG == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PurchAdjAlloc_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await _TrxTypeComboBox.FocusAsync();
        }
        private async Task PurchAdjAlloc_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeletePurchaseAdjustmentAlloc((APT00510DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void PurchAdjAlloc_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (APT00510DTO)eventArgs.Data;

                lCancel = string.IsNullOrEmpty(loData.CTO_TRANS_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V010"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTO_REF_NO);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V011"));
                }

                lCancel = loData.NTO_AP_AMOUNT > Math.Abs(loData.NAP_REMAINING);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V012"));
                }

                lCancel = loData.NTO_TAX_AMOUNT > Math.Abs(loData.NTAX_REMAINING);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V013"));
                }

                lCancel = loData.NTO_AP_AMOUNT <= 0;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V014"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCHARGES_DEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V015"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCHARGES_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V016"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task PurchAdjAlloc_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SavePurchaseAdjustmentAlloc((APT00510DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.PurchaseAdjustmentAlloc;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PurchAdjAlloc_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loData = (APT00510DTO)eventArgs.Data;
            loData.CTRANS_NAME = loData.CTO_TRANS_NAME;
            loData.CTRANS_REF_NO = loData.CTO_REF_NO;
            loData.CCHARGES_DESC = loData.CCHARGES_NAME;

            var loGridData = loData;

            
        }
        #endregion

        #region Transaction Type
        private void TrxTypeDropdown_ValueChanged(string poParam)
        {
            _viewModel.Data.CTO_TRANS_CODE = poParam;
            _viewModel.Data.CTO_REF_NO = "";
            _viewModel.Data.CTO_REF_DATE = "";
            _viewModel.RefDate = null;
        }
        #endregion

        #region Reference no Lookup
        private void R_Before_Open_LookupReferenceNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00500ParameterDTO loParam = new APL00500ParameterDTO();

            loParam.CPROPERTY_ID = _viewModel.HeaderPurchaseAdju.CPROPERTY_ID;
            loParam.CDEPT_CODE = _viewModel.HeaderPurchaseAdju.CDEPT_CODE;
            loParam.CSUPPLIER_ID = _viewModel.HeaderPurchaseAdju.CSUPPLIER_ID;
            loParam.LHAS_REMAINING = true;
            loParam.CTRANS_CODE = _viewModel.Data.CTO_TRANS_CODE;
            loParam.CTRANS_NAME = _viewModel.VAR_ALLOC_TRX_TYPE_LIST.FirstOrDefault(x => x.CTRANS_CODE == _viewModel.Data.CTO_TRANS_CODE).CTRANSACTION_NAME;
            if (_gridPurchaseAdjAllocRef.DataSource.Count > 0)
            {
                loParam.CCURRENCY_CODE = _gridPurchaseAdjAllocRef.CurrentSelectedData.CTO_CURRENCY_CODE;
            }
            else
            {
                loParam.CCURRENCY_CODE = "";
            }

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00500);
        }

        private async Task R_After_Open_LookupReferenceNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00500DTO loTempResult = (APL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00510DTO)_conRef.R_GetCurrentData();

            loData.CTO_REC_ID = loTempResult.CREC_ID;
            loData.CTO_REF_NO = loTempResult.CREF_NO;
            loData.CTO_REF_DATE = loTempResult.CREF_DATE;
            _viewModel.RefDate = DateTime.ParseExact(loTempResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

            loData.CTO_DEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CTO_DEPT_NAME = loTempResult.CDEPT_NAME;

            loData.CTO_CURRENCY_CODE = loTempResult.CCURRENCY_CODE;

            loData.NAP_REMAINING = loTempResult.NAP_REMAINING;
            loData.NLAP_REMAINING = loTempResult.NLAP_REMAINING;
            loData.NBTARGET_REMAINING = loTempResult.NBAP_REMAINING;

            loData.NTAX_REMAINING = loTempResult.NTAX_REMAINING;
            loData.NLTAX_REMAINING = loTempResult.NLTAX_REMAINING;
            loData.NBTAX_REMAINING = loTempResult.NBTAX_REMAINING;

            loData.NTOTAL_REMAINING = loTempResult.NTOTAL_REMAINING;
            loData.NLTOTAL_REMAINING = loTempResult.NLTOTAL_REMAINING;
            loData.NBTOTAL_REMAINING = loTempResult.NBTOTAL_REMAINING;

            loData.NLBASE_RATE = loTempResult.NLBASE_RATE;
            loData.NLCURRENCY_RATE = loTempResult.NLCURRENCY_RATE;

            loData.NTAX_BASE_RATE = loTempResult.NTAX_BASE_RATE;
            loData.NLCURRENCY_RATE = loTempResult.NLCURRENCY_RATE;

            loData.NBBASE_RATE = loTempResult.NBBASE_RATE;
            loData.NBCURRENCY_RATE = loTempResult.NBCURRENCY_RATE;

            await _viewModel.CalculateAllocationProses();
        }
        #endregion

        #region Amount
        private async Task Amount_OnLostFocus()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.CalculateAllocationProses();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Tax
        private async Task Tax_OnlostFocus()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.CalculateAllocationProses();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Charges Dept Lookup
        private void R_Before_Open_LookupChargesDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO loParam = R_FrontUtility.ConvertObjectToObject<GSL00710ParameterDTO>(_viewModel.HeaderPurchaseAdju);

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupChargesDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00510DTO)_conRef.R_GetCurrentData();

            loData.CCHARGES_DEPT_CODE = loTempResult.CDEPT_CODE;
            loData.CCHARGES_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Charges Lookup
        private void R_Before_Open_LookupCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL01400ParameterDTO loParam = R_FrontUtility.ConvertObjectToObject<GSL01400ParameterDTO>(_viewModel.HeaderPurchaseAdju);
            loParam.CCHARGES_TYPE_ID = "D";

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01400);
        }

        private void R_After_Open_LookupCharges(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL01400DTO loTempResult = (GSL01400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (APT00510DTO)_conRef.R_GetCurrentData();

            loData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }
        #endregion
    }
}
