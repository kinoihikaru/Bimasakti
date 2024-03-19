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
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using APT00500COMMON;
using R_BlazorFrontEnd.Helpers;
using APT00500FrontResources;
using Lookup_GSModel.ViewModel;

namespace APT00500FRONT
{
    public partial class APT00500 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<APT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private APT00500ViewModel _viewModel = new APT00500ViewModel();
        private R_Grid<APT00500DTO> _gridInvoiceRef;

        private string DeptName = "";
        private string SupplierName = "";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVar();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();
            bool llValidate = false;
            try
            {
                if (_viewModel.CSUPPLIER_OPTIONS == "S")
                {
                    if (string.IsNullOrEmpty(_viewModel.PurchaseAdjuParam.CSUPPLIER_ID))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "V002"));
                        llValidate = true;
                    }
                }

                if (llValidate)
                {
                    goto EndBlock;
                }
                else
                {
                    await _gridInvoiceRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        private void PropertyDropdown_ValueChanged(string poParam)
        {
            _viewModel.PurchaseAdjuParam.CPROPERTY_ID = poParam;
            _viewModel.PurchaseAdjuParam.CSUPPLIER_ID = "";
            SupplierName = "";
            _viewModel.PurchaseAdjuParam.CDEPT_CODE = "";
            DeptName = "";
        }
       
        private async Task Grid_Invoice_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetPurchaseAdjustmentList();

                eventArgs.ListEntityResult = _viewModel.PurchaseAdjusmentGrid;
                if (_viewModel.PurchaseAdjusmentGrid.Count() == 0)
                {
                    await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loParam = _gridInvoiceRef.CurrentSelectedData;

                if (loParam != null)
                {
                    loParam.CPROPERTY_ID = _viewModel.PurchaseAdjuParam.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.PurchaseAdjuParam.CDEPT_CODE;
                    loParam.CSUPPLIER_ID = _viewModel.PurchaseAdjuParam.CSUPPLIER_ID;
                }
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(APT00510);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #region Dept Lookup
        private async Task DeptCode_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PurchaseAdjuParam.CPROPERTY_ID,
                    CSEARCH_TEXT = _viewModel.PurchaseAdjuParam.CDEPT_CODE
                };

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    DeptName = "";
                    goto EndBlock;
                }

                DeptName = loResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.PurchaseAdjuParam.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.PurchaseAdjuParam.CPROPERTY_ID,
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
            _viewModel.PurchaseAdjuParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            DeptName = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Supplier Lookup
        private async Task SupplierId_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                APL00100ParameterDTO loParam = new APL00100ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.PurchaseAdjuParam.CPROPERTY_ID,
                    CSEARCH_TEXT = ""
                };

                //LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();

                //var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

                //if (loResult == null)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                //            "_ErrLookup01"));
                //    DeptName = "";
                //    goto EndBlock;
                //}

                //DeptName = loResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.PurchaseAdjuParam.CPROPERTY_ID,
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
            _viewModel.PurchaseAdjuParam.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            SupplierName = loTempResult.CSUPPLIER_NAME;
        }
        #endregion
    }
}
