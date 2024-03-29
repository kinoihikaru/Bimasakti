﻿using APM00300COMMON;
using APM00300FrontResources;
using APM00300MODEL;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace APM00300FRONT
{
    public partial class APM00300 : R_Page
    {
        private APM00300ViewModel _Supplier_viewModel = new APM00300ViewModel();
        private R_Grid<APM00300DTO> _Supllier_gridRef;
        private R_Conductor _Supplier_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }
        R_PredefinedDock tesPredif;

        private bool PageOnCrudMode = true;
        private R_ComboBox<APM00300PropertyDTO, string> Property_ComboBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.GetInitialVar();
                await _Supplier_viewModel.GetPropertyList();
                if (_Supplier_viewModel.PropertyList.Count > 0)
                {
                    _Supplier_viewModel.PropertyValueContext = _Supplier_viewModel.PropertyList.FirstOrDefault().CPROPERTY_ID;
                }

                await _Supplier_viewModel.GetLOBList();

                await Property_ComboBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void LOBComboBox_OnChange(string poParam)
        {
            _Supplier_viewModel.LOBValueContext = poParam is null ? "" : poParam;
        }
        private async Task SearchBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.ValidationSearch();

                await _Supllier_gridRef.R_RefreshGrid(null);   
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ShowAllBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_Supplier_viewModel.PropertyValueContext))
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "301"), R_eMessageBoxButtonType.OK);
                }
                else
                {
                    await _Supllier_gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task SupplierList_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.SearchSupplierList();

                eventArgs.ListEntityResult = _Supplier_viewModel.SupplierGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Supplier_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private bool _isCopySource = false;
        private void SupplierDetail_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            APM00300DTO loData = (APM00300DTO)_Supplier_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loData;

            eventArgs.TargetPageType = typeof(APM00310);
        }

        private async Task SupplierDetail_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await Property_ComboBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
       
    }
}
