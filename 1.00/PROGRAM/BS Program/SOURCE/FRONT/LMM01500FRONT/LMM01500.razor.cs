using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Security.Principal;
using System.Xml.Linq;

namespace LMM01500FRONT
{
    public partial class LMM01500 : R_Page
    {
        private LMM01500ViewModel _Genereal_viewModel = new LMM01500ViewModel();
        private LMM01510ViewModel _BankAccount_viewModel = new LMM01510ViewModel();

        private R_Grid<LMM01501DTO> _Genereal_gridRef;
        private R_Grid<LMM01510DTO> _BankAccount_gridRef;

        private R_Conductor _Genereal_conductorRef;
        private R_Conductor _BankAccount_conductorRef;

        private string _Genereal_lcLabel = "Actived";
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(poParameter);

                if (_Genereal_viewModel.PropertyList.Count > 0)
                {
                    LMM01500DTOPropety loParam = _Genereal_viewModel.PropertyList.FirstOrDefault();
                    _Genereal_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task PropertyDropdown_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.GetPropertyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabStrip _TabGeneral;
        private R_TabPage _tabPagePinalty;
        private R_TabPage _tabPageOtherCharges;
        private R_TabPage _tabPageBankAccount;

        private async Task PropertyDropdown_OnChange(string poParam)
        {
            _Genereal_viewModel.TabEnable = false;
            _Genereal_viewModel.PropertyValueContext = poParam;
            var loEx = new R_Exception();

            try
            {
                var loParam = _Genereal_gridRef.CurrentSelectedData;
                var loData = (LMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();

                await _Genereal_gridRef.R_RefreshGrid(null);

                if (_Genereal_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_TabGeneral.ActiveTab.Id == "Pinalty")
                    {
                        await _tabPagePinalty.InvokeRefreshTabPageAsync(loData);
                    }
                    else if (_TabGeneral.ActiveTab.Id == "Pinalty")
                    {
                        await _tabPageOtherCharges.InvokeRefreshTabPageAsync(loData);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region TabStrip
        private async Task BankAccount_OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CINVGRP_CODE))
                {
                    _BankAccount_viewModel.PropertyValueContext = _Genereal_viewModel.PropertyValueContext;
                    _BankAccount_viewModel.InvGrpCode = _Genereal_viewModel.Data.CINVGRP_CODE;

                    await _BankAccount_gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region General
        public bool _enableComboCategory = true;
        public bool _pageSupplierOnCRUDmode = false;
        private void General_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private void Tempalte_OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

        private async Task GroupGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.GetInvoiceGroupList();

                eventArgs.ListEntityResult = _Genereal_viewModel.InvoinceGroupGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01500DTO>(eventArgs.Data);
                await _Genereal_viewModel.GetInvoiceGroup(loParam);


                eventArgs.Result = _Genereal_viewModel.InvoiceGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_viewModel.SaveInvoiceGroup((LMM01500DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _Genereal_viewModel.InvoiceGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01500DTO)eventArgs.Data;
                await _Genereal_viewModel.DeleteInvoiceGroup(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loGetData = (LMM01500DTO)_Genereal_viewModel.R_GetCurrentData();

                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM01501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Genereal_conductorRef.R_SetCurrentData(loGetData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "LMM01501" //Uabh Approval Code sesuai Spec masing masing
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
        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM01500DTO)_Genereal_viewModel.R_GetCurrentData();

            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }

                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                    await _Genereal_conductorRef.R_SetCurrentData(loGetData);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private bool TabPinaltyChargesEnable;
        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM01500DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {

                    if (loParam.LACTIVE != true)
                    {

                        _Genereal_lcLabel = "Activate";
                        _Genereal_viewModel.StatusChange = true;
                    }
                    else
                    {
                        _Genereal_lcLabel = "Inactive";
                        _Genereal_viewModel.StatusChange = false;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        private void R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01500DTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            
        }
        private void R_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(_Genereal_viewModel.PropertyValueContext))
            {
                eventArgs.Allow = false;
            }

        }

        private bool GeneralButtonEnable = false;

        private void R_SetAdd(R_SetEventArgs eventArgs)
        {
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }
        private void R_SetEdit(R_SetEventArgs eventArgs)
        {
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }

        private async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loData = (LMM01500DTO)eventArgs.Data;
                await _Genereal_viewModel.ValidationInvoiceGrp(loData);

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    if (loData.LACTIVE)
                    {
                        var loGetData = loData;

                        var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                        loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "LMM01501"; //Uabh Approval Code sesuai Spec masing masing
                        await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                        //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                        if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                        {
                            await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                            await _Genereal_conductorRef.R_SetCurrentData(loGetData);
                            return;
                        }
                        else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                        {
                            var loPopupParam = new GFF00900ParameterDTO()
                            {
                                Data = loValidateViewModel.loRspActivityValidityList,
                                IAPPROVAL_CODE = "LMM01501" //Uabh Approval Code sesuai Spec masing masing
                            };
                            loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loPopupParam);

                            if (loResult.Success == false)
                            {
                                eventArgs.Cancel = true;
                            }

                            bool result = (bool)loResult.Result;
                            if (result == true)
                            {
                                await _Genereal_viewModel.ActiveInactiveProcessAsync(loGetData);
                            }
                            else
                            {
                                eventArgs.Cancel = true;
                            }
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

        private bool R_ActiveInActiveBtnEnable;
        private bool PinaltyTabStripEnabled;
        private bool OtherChargesTabStripEnabled;
        private void R_SetHasData(R_SetEventArgs eventArgs)
        {
            R_ActiveInActiveBtnEnable = eventArgs.Enable;
            PinaltyTabStripEnabled = eventArgs.Enable;
            OtherChargesTabStripEnabled = eventArgs.Enable;
        }

        private bool InvGrpModeEnable = false;
        private void InvDue_OnChanged(string poParam)
        {
            _Genereal_viewModel.Data.CINVOICE_DUE_MODE = poParam;

            InvGrpModeEnable = poParam.ToString() == "02";
            if (InvGrpModeEnable == false)
            {
                _Genereal_viewModel.Data.CINVOICE_GROUP_MODE = "";
            }
        }

        private bool IDueDaysEnable = false;
        private bool IFixDueDateEnable = false;
        private bool ILimitDueDateEnable = false;
        private bool IBeforeLimitEnable = false;
        private bool IAfterLimitEnable = false;
        private void InvGrp_OnChanged(string poParam)
        {
            _Genereal_viewModel.Data.CINVOICE_GROUP_MODE = poParam;

            IDueDaysEnable = poParam.ToString() == "01";
            IFixDueDateEnable = poParam.ToString() == "02";
            ILimitDueDateEnable = poParam.ToString() == "03";
            IBeforeLimitEnable = poParam.ToString() == "03";
            IAfterLimitEnable = poParam.ToString() == "03";
        }

        private void General_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            InvGrpModeEnable = false;
            IDueDaysEnable = false;
            IFixDueDateEnable = false;
            ILimitDueDateEnable = false;
            IBeforeLimitEnable = false;
            IAfterLimitEnable = false;

            _Genereal_gridRef.CurrentSelectedData.CINVGRP_CODE_NAME = string.Format("{0} - {1}", _Genereal_gridRef.CurrentSelectedData.CINVGRP_CODE, _Genereal_gridRef.CurrentSelectedData.CINVGRP_NAME);

        }

        private void General_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            InvGrpModeEnable = false;
            IDueDaysEnable = false;
            IFixDueDateEnable = false;
            ILimitDueDateEnable = false;
            IBeforeLimitEnable = false;
            IAfterLimitEnable = false;
        }

        private void General_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
        }

        private async Task _General_InvTemplateUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01500DTO)_Genereal_conductorRef.R_GetCurrentData();
                loData.CINVOICE_TEMPLATE = eventArgs.File.Name;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Bank Account
        private async Task BankAccount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccount_viewModel.GetListTemplateBankAccount();

                eventArgs.ListEntityResult = _BankAccount_viewModel.TemplateBankAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01511DTO>(eventArgs.Data);
                await _BankAccount_viewModel.GetTemplateBankAccount(loParam);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool BankAccountButtonEnable = false;
       
        private void BankAccount_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            BankAccountButtonEnable = false;
        }
        private void BankAccount_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            BankAccountButtonEnable = false;
        }
        private void BankAccount_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            BankAccountButtonEnable = false;
        }
        private async Task BankAccount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccount_viewModel.ValidationTemplateBankAccount((LMM01511DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;

                await _BankAccount_viewModel.SaveTemplateBankAccount(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;
                await _BankAccount_viewModel.DeleteTemplateBankAccount(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task _BankAccount_InvTemplateUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();
                loData.CINVOICE_TEMPLATE = eventArgs.File.Name;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private bool _templateBtn;
        private void BankAccount_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
            _templateBtn = eventArgs.Enable;
        }
        #endregion

        #region Lookup
        private void OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01400ParameterDTO()
            {
                CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext,
                CCHARGES_TYPE_ID = "A",
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01400);
        }

        private void OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CSTAMP_ADD_ID = loTempResult.CCHARGES_ID;
            _Genereal_viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void General_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _Genereal_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }

        private void BankAccount_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _BankAccount_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();
            BankAccountButtonEnable = !string.IsNullOrWhiteSpace(loGetData.CBANK_ACCOUNT) && !string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE);
        }

        private void Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01200ParameterDTO
            {
                CCB_TYPE = "B"
            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void BankAccount_Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01200ParameterDTO()
            {
                CCB_TYPE = "B"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void General_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _Genereal_viewModel.Data.CCB_NAME = loTempResult.CCB_NAME;
            GeneralButtonEnable = !string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);
        }

        private void BankAccount_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _BankAccount_viewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private void BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = _Genereal_viewModel.InvoiceGroup;

            var param = new GSL01300ParameterDTO()
            {
                CBANK_TYPE = "B",
                CCB_CODE = loGetData.CBANK_CODE,
                CDEPT_CODE = loGetData.CDEPT_CODE,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }

        private void BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _Genereal_viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;
        }

        private void BankAccount_BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();

            var param = new GSL01300ParameterDTO()
            {
                CBANK_TYPE = "B",
                CCB_CODE = loGetData.CBANK_CODE,
                CDEPT_CODE = loGetData.CDEPT_CODE,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }

        private void BankAccount_BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _BankAccount_viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;

            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();
            BankAccountButtonEnable = !string.IsNullOrWhiteSpace(loGetData.CBANK_ACCOUNT) && !string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE);
        }

        #endregion

        private bool _comboboxEnabled = true;
        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = !(bool)poValue;
        }
        private void _PinaltyTab_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = _Genereal_gridRef.CurrentSelectedData;
            loParam.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            eventArgs.TargetPageType = typeof(LMM01520);
            eventArgs.Parameter = loParam;
        }
        private void _OtherCharges_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loParam = _Genereal_gridRef.CurrentSelectedData;
            loParam.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            eventArgs.TargetPageType = typeof(LMM01530);
            eventArgs.Parameter = loParam;
        }

    }
}
