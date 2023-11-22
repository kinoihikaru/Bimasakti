using APM00300COMMON;
using APM00300FrontResources;
using APM00300MODEL;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace APM00300FRONT
{
    public partial class APM00310 : R_Page
    {
        private APM00310ViewModel _Supplier_viewModel = new APM00310ViewModel();
        private R_Conductor _Supplier_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // Convert Param
                var loData = R_FrontUtility.ConvertObjectToObject<APM00310DTO>(poParameter);

                // Combo box Property
                await _Supplier_viewModel.GetPropertyList();

                if (!string.IsNullOrWhiteSpace(loData.CPROPERTY_ID))
                {
                    _Supplier_viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                        await _Supplier_conductorRef.R_GetEntity(loData);
                }
                else
                {
                   var loTempData = _Supplier_viewModel.PropertyList.FirstOrDefault();
                    _Supplier_viewModel.PropertyValueContext = loTempData.CPROPERTY_ID;
                }

                // Combo box
                await _Supplier_viewModel.GetLOBList();
                await _Supplier_viewModel.GetPayTermList();
                await _Supplier_viewModel.GetCurrencyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyComboBox_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _Supplier_viewModel.PropertyValueContext = poParam is null ? "" : poParam;

                // load Term property onChange
                await _Supplier_viewModel.GetPayTermList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Supplier
        private async Task Supplier_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    await _Supplier_viewModel.GetSupplier(loData);

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_Journal_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL00400ParameterDTO() { CPROPERTY_ID = _Supplier_viewModel.PropertyValueContext, CJRNGRP_TYPE = "50" };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void Supplier_Journal_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00400DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
                loData.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
                loData.CJRNGRP_TYPE = loTempResult.CJRNGRP_TYPE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_Category_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01800DTOParameter() { CPROPERTY_ID = _Supplier_viewModel.PropertyValueContext, CCATEGORY_TYPE = "50" };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void Supplier_Category_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL01800DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                var loData = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

                loData.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
                loData.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;
                loData.CCATEGORY_TYPE = loTempResult.CCATEGORY_TYPE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Supplier_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.DeleteSupplier((APM00310DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (APM00310DTO)eventArgs.Data;
            loData.DUPDATE_DATE = DateTime.Now;
            loData.DCREATE_DATE = DateTime.Now;
        }
        private void Supplier_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;

                bool lCancel;

                lCancel = string.IsNullOrEmpty(_Supplier_viewModel.PropertyValueContext);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "301"));
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "304"));
                }

                lCancel = string.IsNullOrEmpty(loData.CSUPPLIER_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "305"));
                }

                lCancel = string.IsNullOrEmpty(loData.CJRNGRP_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "306"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCATEGORY_ID);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "307"));
                }

                lCancel = string.IsNullOrEmpty(loData.CPAY_TERM_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "308"));
                }

                lCancel = string.IsNullOrEmpty(loData.CCURRENCY_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "309"));
                }

                lCancel = string.IsNullOrEmpty(loData.CLOB_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "310"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Supplier_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Supplier_viewModel.SaveSupplier((APM00310DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Supplier_viewModel.Supplier;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        private void Supplier_OneTime_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00320);
        }

        private void Supplier_OneTime_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
               
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Supplier_Bank_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00330);
        }

        private void Supplier_Bank_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabStrip _tabSupplierDetailChill;
        private R_TabPage _AddressTabPage;
        private R_TabPage _TaxBalanceTabPage;

        private void Supplier_Address_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00311);
        }

        private void Supplier_TaxBalance_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = (APM00310DTO)_Supplier_conductorRef.R_GetCurrentData();

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APM00312);
        }
    }
}
