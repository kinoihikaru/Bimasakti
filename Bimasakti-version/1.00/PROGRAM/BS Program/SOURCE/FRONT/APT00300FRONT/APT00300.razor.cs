using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using APT00300MODEL;
using APT00300COMMON;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APFRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using APT00300FrontResources;
using R_BlazorFrontEnd.Interfaces;

namespace APT00300FRONT
{
    public partial class APT00300 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] R_ILocalizer<APT00300FrontResources.Resources_Dummy_Class> _localizer { get; set;}
        public string _LabelTitleTab;

        private APT0300ViewModel _viewModel = new APT0300ViewModel();
        private R_Grid<APT00300DTO> _gridInvoiceRef;
        private R_ConductorGrid _conductorInvoiceRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetAllInitialVar();
                if (_viewModel.PropertyList.Count() > 0)
                {
                    _viewModel.ParameterGridPurchase.CPROPERTY_ID = _viewModel.PropertyList.FirstOrDefault().CPROPERTY_ID;
                }
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

            try
            {
                await _viewModel.ValidationRefresh();
                await _gridInvoiceRef.R_RefreshGrid(null);
                if (_viewModel.PurchaseDebitGrid.Count == 0 )
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

        private async Task SupplierOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.SupplierMode = poParam;
                //if (poParam == "A")
                //{
                //    loInvoiceViewModel.loInvoice.CSUPPLIER_ID = "";
                //    loInvoiceViewModel.loInvoice.CSUPPLIER_NAME = "";
                //    IsSupplierEnabled = false;
                //}
                //else if (poParam == "S")
                //{
                //    IsSupplierEnabled = true;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ParameterGridPurchase.CPROPERTY_ID = poParam;
                //loInvoiceViewModel.loInvoice.CPROPERTY_ID = poParam;
                //loInvoiceViewModel.loProperty.CPROPERTY_ID = poParam;
                //loInvoiceViewModel.loInvoice.CSUPPLIER_ID = "";
                //loInvoiceViewModel.loInvoice.CSUPPLIER_NAME = "";
                //loInvoiceViewModel.loInvoice.CDEPARTMENT_CODE = "";
                //loInvoiceViewModel.loInvoice.CDEPARTMENT_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DeptCodeTextBox_OnLostFocus(object poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void SupplierIdTextBox_OnLostFocus(object poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_Invoice_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetPurchaseDebitList();
                eventArgs.ListEntityResult = _viewModel.PurchaseDebitGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_Invoice_Display(R_DisplayEventArgs eventArgs)
        {
        }

        private void R_Before_Open_LookupDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ParameterGridPurchase.CPROPERTY_ID
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
            var loGetData = _viewModel.ParameterGridPurchase;
            _viewModel.ParameterGridPurchase.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.ParameterGridPurchase.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private void R_Before_Open_LookupSupplier(R_BeforeOpenLookupEventArgs eventArgs)
        {
            APL00100ParameterDTO loParam = new APL00100ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.ParameterGridPurchase.CPROPERTY_ID
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
            _viewModel.ParameterGridPurchase.CSUPPLIER_ID = loTempResult.CSUPPLIER_ID;
            _viewModel.ParameterGridPurchase.CSUPPLIER_NAME = loTempResult.CSUPPLIER_NAME;
        }

        private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loParam = _gridInvoiceRef.CurrentSelectedData;
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(APT00310);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private async Task R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            //await _gridInvoiceRef.R_RefreshGrid(null);
        }

    }

}
