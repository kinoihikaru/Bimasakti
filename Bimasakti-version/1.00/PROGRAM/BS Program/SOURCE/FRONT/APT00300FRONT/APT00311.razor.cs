using APT00300COMMON;
using APT00300MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00300FRONT
{
    public partial class APT00311 : R_Page
    {
        private APT00311ViewModel _viewModel = new APT00311ViewModel();

        private R_Conductor _conductorInvoiceItemRef;

        private R_Grid<APT00311DTO> _gridInvoiceItemRef;
        [Inject] R_ILocalizer<APT00300FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loHeaderData = (APT00310DTO)poParameter;
                _viewModel.HeaderData = loHeaderData;
                _viewModel.RefDate = DateTime.ParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                await _viewModel.GetAllInitialVar();
                await _gridInvoiceItemRef.R_RefreshGrid(loHeaderData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_InvoiceItem_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<APT00311DTO>(eventArgs.Parameter);
                await _viewModel.GetPurchaseDebitDTList(loParam);
                eventArgs.ListEntityResult = _viewModel.PurchaseDebitDTGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_InvoiceItem_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetPurchaseDebitDT((APT00311DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.PurchaseDebitDT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
