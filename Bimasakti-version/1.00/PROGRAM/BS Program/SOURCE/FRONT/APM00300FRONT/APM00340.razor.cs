using APM00300COMMON;
using APM00300MODEL;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace APM00300FRONT
{
    public partial class APM00340 : R_Page
    {
        private APM00300ViewModel _Supplier_viewModel = new APM00300ViewModel();
        private R_Grid<APM00300DTO> _Supllier_gridRef;
        private R_Conductor _Supplier_conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.GetInitialVar();
                await _Supplier_viewModel.GetPropertyList();
                await _Supplier_viewModel.GetLOBList();
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

        R_PredefinedDock tesPredif;
        private async Task ShowAllBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_Supplier_viewModel.PropertyValueContext))
                {
                    await R_MessageBox.Show("", "Please Select Property!", R_eMessageBoxButtonType.OK);
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


        private async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                var loParamData = (APM00300DTO)_Supplier_conductorRef.R_GetCurrentData();
                await this.Close(true, loParamData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Button_OnClickCancelAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await this.Close(true, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
