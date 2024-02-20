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
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace APM00300FRONT
{
    public partial class APM00312 : R_Page
    {
        private APM00310ViewModel _Supplier_viewModel = new APM00310ViewModel();
        private R_Conductor _Supplier_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)poParameter;

                await _Supplier_viewModel.GetTaxTypeList();
                await _Supplier_conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Supplier_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                {
                    await _Supplier_viewModel.GetSupplier(loData);
                    eventArgs.Result = _Supplier_viewModel.Supplier;
                }
                else
                {
                    eventArgs.Result = eventArgs.Data;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Supplier_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = false;
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

        private void Supplier_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APM00310DTO)eventArgs.Data;

                bool lCancel;

                lCancel = string.IsNullOrEmpty(loData.CTAX_TYPE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "318"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_NAME);

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "319"));
                    }
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_REG_ID);

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "320"));
                    }
                }
                lCancel = loData.CTAX_REG_DATE_DISPLAY == DateTime.MinValue || string.IsNullOrWhiteSpace(loData.CTAX_REG_DATE_DISPLAY.ToString(""));

                if (loData.CTAX_TYPE == "02")
                {
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "321"));
                    }
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

        #region Btn
        private void Supplier_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(APM00340);
        }
        private async Task SupplierDetail_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
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
        #endregion
    }
}
