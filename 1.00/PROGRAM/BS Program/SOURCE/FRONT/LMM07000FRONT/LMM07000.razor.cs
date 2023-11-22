using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM07000COMMON;
using LMM07000FrontResources;
using LMM07000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM07000FRONT
{
    public partial class LMM07000 : R_Page
    {
        private LMM07000ViewModel _LMM07000ViewModel = new LMM07000ViewModel();

        private R_Grid<LMM07000DTO> _ChargesDiscountgridRef;
        private R_Grid<LMM07000DTOUniversal> _ChargesType_gridRef;

        private R_Conductor _ChargesDiscount_conductorRef;
        private R_ConductorGrid _ChargesType_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] public R_PopupService PopupService { get; set; }

        private string PropertyId;
        private string _Genereal_lcLabel = "Activate";
        private bool _DiscValEnable = false;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM07000ViewModel.GetPropertyList();
                await _LMM07000ViewModel.GetDiscountTypeList();
                await _LMM07000ViewModel.GetInvoicePeriodList();

                // set first property value
                if (_LMM07000ViewModel.PropertyList.Count > 0)
                {
                    LMM07000DTOInitial loParam = _LMM07000ViewModel.PropertyList.FirstOrDefault();
                    PropertyId = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                PropertyId = poParam;
                await _ChargesType_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Charges Type
        private async Task ChargesTypeGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM07000ViewModel.GetChargesTypeList();

                eventArgs.ListEntityResult = _LMM07000ViewModel.ChargesTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ChargesTypeGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesTypeGrid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var loTempParam = _ChargesType_gridRef.CurrentSelectedData;

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (!string.IsNullOrWhiteSpace(PropertyId))
                    {
                        await _ChargesDiscountgridRef.R_RefreshGrid(loTempParam);
                    }
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Charges Discount
        private async Task ChargesDiscount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (LMM07000DTOUniversal)eventArgs.Parameter;
                var loParam = new LMM07000DTO()
                {
                    CCHARGES_TYPE = loTempParam.CCODE,
                    CPROPERTY_ID = PropertyId
                };

                await _LMM07000ViewModel.GetChargesDiscountList(loParam);

                eventArgs.ListEntityResult = _LMM07000ViewModel.ChargesDiscountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesDiscount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                // set param
                var loTempParam = _ChargesType_gridRef.CurrentSelectedData;
                var loParam = (LMM07000DTO)eventArgs.Data;
                loParam.CCHARGES_TYPE = loTempParam.CCODE;

                await _LMM07000ViewModel.GetChargesDiscount(loParam);

                eventArgs.Result = _LMM07000ViewModel.ChargesDiscount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ChargesDiscount_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (LMM07000DTO)eventArgs.Data;

                    if (loParam.LACTIVE)
                    {
                        _Genereal_lcLabel = "Inactive";
                       _LMM07000ViewModel.StatusChange = false;
                    }
                    else
                    {
                        _Genereal_lcLabel = "Activate";
                        _LMM07000ViewModel.StatusChange = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ChargesDiscount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loData = (LMM07000DTO)eventArgs.Data;
            GFF00900ParameterDTO loPopupParam = null;
            R_PopupResult loResult = null;

            var loEx = new R_Exception();
            try
            {
                await _LMM07000ViewModel.ChargesDiscountValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ChargesDiscount_Saving(R_SavingEventArgs eventArgs)
        {
            var loData = (LMM07000DTO)eventArgs.Data;
            var loTempParam = _ChargesType_gridRef.CurrentSelectedData;

            R_Exception loException = new R_Exception();
            try
            {
                loData.CAPPLY_PERIOD_NO_FROM = _LMM07000ViewModel.FromPeriodYear.ToString();
                loData.CAPPLY_PERIOD_NO_TO = _LMM07000ViewModel.ToPeriodYear.ToString();

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CPROPERTY_ID = PropertyId;
                    loData.CCHARGES_TYPE = loTempParam.CCODE;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ChargesDiscount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM07000ViewModel.SaveChargesDiscount((LMM07000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _LMM07000ViewModel.ChargesDiscount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesDiscount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM07000ViewModel.DeleteChargesDiscount((LMM07000DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

     
        private async Task ChargesDiscount_ActivateInactive_OnClick()
        {
            var loGetData = (LMM07000DTO)_LMM07000ViewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", string.Format(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifInactive"), _Genereal_lcLabel, loGetData.CDISCOUNT_CODE), R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _LMM07000ViewModel.ActiveInactiveProcessAsync(loGetData);
                    await _ChargesDiscount_conductorRef.R_SetCurrentData(loGetData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void DiscountType_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _LMM07000ViewModel.Data.CDISCOUNT_TYPE = poParam;

                if (poParam == "03")
                {
                    _DiscValEnable = false;
                    _LMM07000ViewModel.Data.NDISCOUNT_VALUE = 100;
                }
                else
                {
                    _DiscValEnable = true;
                    _LMM07000ViewModel.Data.NDISCOUNT_VALUE = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public bool _pageSupplierOnCRUDmode = true;
        public void ChargesDiscount_SetOther(R_SetEventArgs e)
        {
            _pageSupplierOnCRUDmode = e.Enable;
            _DiscValEnable = !e.Enable;
        }
        #endregion
    }
}
