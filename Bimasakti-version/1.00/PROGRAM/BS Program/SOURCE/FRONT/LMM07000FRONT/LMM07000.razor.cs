using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM07000COMMON;
using LMM07000FrontResources;
using LMM07000MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_MODULE_NAME = "LM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (LMM07000DTO)eventArgs.Data;

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
                        Program_Id = "LMM07000",
                        Table_Name = "LMM_DISCOUNT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CDISCOUNT_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "LMM07000",
                        Table_Name = "LMM_DISCOUNT",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CDISCOUNT_CODE)
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
        private bool IsTes = false;
        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                PropertyId = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                await _ChargesType_gridRef.R_RefreshGrid(null);
                //IsTes = poParam is "JBMPC" or "LMPNG" or "TAR";
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

        private R_TextBox DiscountName_TextBox;
        private async Task ChargesDiscount_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM07000DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {

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

                    _LMM07000ViewModel.FromPeriodYear = int.Parse(loParam.CAPPLY_PERIOD_NO_FROM);
                    _LMM07000ViewModel.ToPeriodYear = int.Parse(loParam.CAPPLY_PERIOD_NO_TO);
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    _LMM07000ViewModel.FromPeriodYear = DateTime.Now.Month;
                    _LMM07000ViewModel.ToPeriodYear = DateTime.Now.Month;
                }
                else if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await DiscountName_TextBox.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox DiscountCode_TextBox;
        private async Task ChargesDiscount_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await DiscountCode_TextBox.FocusAsync();
        }
        private async Task ChargesDiscount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loData = (LMM07000DTO)eventArgs.Data;
            GFF00900ParameterDTO loPopupParam = null;
            R_PopupResult loResult = null;
            var loTempParam = _ChargesType_gridRef.CurrentSelectedData;

            var loEx = new R_Exception();
            try
            {
                await _LMM07000ViewModel.ChargesDiscountValidation(loData);

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    //set Data
                    loData.CPROPERTY_ID = PropertyId;
                    loData.CCHARGES_TYPE = loTempParam.CCODE;

                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM07001"; //Uabh Approval Code sesuai Spec masing masing
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                    //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                    }
                    else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                    {
                        loPopupParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "LMM07001" //Uabh Approval Code sesuai Spec masing masing
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                        if (loResult.Success == false)
                        {
                            eventArgs.Cancel = true;
                            return;
                        }

                        bool result = (bool)loResult.Result;
                        if (result == true)
                        {
                        }
                        else
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
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

     
        private async Task ChargesDiscount_Activate_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (LMM07000DTO)_ChargesDiscount_conductorRef.R_GetCurrentData();
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM07001"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _LMM07000ViewModel.ActiveInactiveProcessAsync(loGetData);
                    await _ChargesDiscount_conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM07001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ChargesDiscount_Activate_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {

            R_Exception loException = new R_Exception();
            try
            {
                 var loGetData = (LMM07000DTO)_ChargesDiscount_conductorRef.R_GetCurrentData();

                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    var loActiveData = await _LMM07000ViewModel.ActiveInactiveProcessAsync(loGetData);
                    await _ChargesDiscount_conductorRef.R_SetCurrentData(loActiveData);
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
                _LMM07000ViewModel.Data.NDISCOUNT_VALUE = 1;
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
