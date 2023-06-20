using BlazorClientHelper;
using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Controls.MessageBox;
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

namespace LMM01500FRONT
{
    public partial class LMM01500 : R_Page
    {
        private LMM01500ViewModel _Genereal_viewModel = new LMM01500ViewModel();
        private LMM01510ViewModel _BankAccount_viewModel = new LMM01510ViewModel();
        private LMM01520ViewModel _InvPinalty_viewModel = new LMM01520ViewModel();
        private LMM01530ViewModel _OtherCharges_viewModel = new LMM01530ViewModel();

        private R_Grid<LMM01501DTO> _Genereal_gridRef;
        private R_Grid<LMM01510DTO> _BankAccount_gridRef;
        private R_Grid<LMM01530DTO> _OtherCharges_gridRef;

        private R_Conductor _Genereal_conductorRef;
        private R_Conductor _BankAccount_conductorRef;
        private R_Conductor _InvPinalty_conductorRef;
        private R_ConductorGrid _OtherCharges_conductorRef;

        private string _Genereal_lcLabel = "Actived";
        [Inject] IClientHelper clientHelper { get; set; }
        

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(poParameter);
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
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _Genereal_gridRef.R_RefreshGrid(null);

                switch (_TabGeneral.ActiveTabIndex)
                {
                    case 1:
                        _InvPinalty_viewModel.InvGrpCode = _Genereal_viewModel.Data.CINVGRP_CODE;
                        _InvPinalty_viewModel.PropertyValueContext = _Genereal_viewModel.PropertyValueContext;

                        var loParam = new LMM01520DTO();
                        await _InvPinalty_conductorRef.R_GetEntity(loParam);
                        break;
                    case 2:
                        _OtherCharges_viewModel.PropertyValueContext = _Genereal_viewModel.PropertyValueContext;
                        _OtherCharges_viewModel.InvGrpCode = _Genereal_viewModel.Data.CINVGRP_CODE;
                        await _OtherCharges_gridRef.R_RefreshGrid(null);
                        break;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region TabStrip

        private async Task General_OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_Genereal_viewModel.Data.CINVGRP_CODE))
                {
                    switch (eventArgs.Id)
                    {
                        case "Pinalty":
                            _InvPinalty_viewModel.InvGrpCode = _Genereal_viewModel.Data.CINVGRP_CODE;
                            _InvPinalty_viewModel.PropertyValueContext = _Genereal_viewModel.PropertyValueContext;

                            var loParam = new LMM01520DTO();
                            await _InvPinalty_conductorRef.R_GetEntity(loParam);
                            break;
                        case "OtherCharges":
                            _OtherCharges_viewModel.PropertyValueContext = _Genereal_viewModel.PropertyValueContext;
                            _OtherCharges_viewModel.InvGrpCode = _Genereal_viewModel.Data.CINVGRP_CODE;
                            await _OtherCharges_gridRef.R_RefreshGrid(null);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
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


                await _Genereal_gridRef.R_RefreshGrid(null);
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
        private void R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "LMM01501";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }
        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = _Genereal_viewModel.InvoiceGroup;
            var loParam = new LMM01500DTO()
            {
                LACTIVE = loGetData.LACTIVE,
                CINVGRP_CODE = loGetData.CINVGRP_CODE,
            };

            R_Exception loException = new R_Exception();
            try
            {
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _Genereal_viewModel.ActiveInactiveProcessAsync(loParam);
                }
                await _Genereal_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private bool TabPinaltyChargesEnable = false;
        private void R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (LMM01500DTO)eventArgs.Data;

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

                TabPinaltyChargesEnable = !string.IsNullOrWhiteSpace(loParam.CINVGRP_CODE);
            }
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

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CINVGRP_CODE); 
                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Invoice Group Code is required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CINVGRP_NAME);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Invoice Group Name is required", R_eMessageBoxButtonType.OK);
                }

                lCancel = _Genereal_viewModel.Data.IBEFORE_LIMIT_INVOICE_DATE < _Genereal_viewModel.Data.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Before Limit Invoice Date cannot be smaller than Limit Invoice Dates*", R_eMessageBoxButtonType.OK);
                }

                lCancel = _Genereal_viewModel.Data.IAFTER_LIMIT_INVOICE_DATE > _Genereal_viewModel.Data.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "After Limit Invoice Date cannot be smaller than Limit Invoice Dates*", R_eMessageBoxButtonType.OK);
                }

                if (_Genereal_viewModel.Data.LUSE_STAMP)
                {
                    lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CSTAMP_ADD_ID);

                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Additional Id and Name is required", R_eMessageBoxButtonType.OK);
                    }
                }

                if (_Genereal_viewModel.Data.LBY_DEPARTMENT)
                {
                    lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CINVOICE_TEMPLATE);

                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Invoice Template Is Required", R_eMessageBoxButtonType.OK);
                    }

                    lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CDEPT_CODE);

                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Departement Is Required", R_eMessageBoxButtonType.OK);
                    }

                    lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_CODE);

                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Bank Is Required", R_eMessageBoxButtonType.OK);
                    }

                    lCancel = string.IsNullOrEmpty(_Genereal_viewModel.Data.CBANK_ACCOUNT);

                    if (lCancel)
                    {
                        eventArgs.Cancel = lCancel;
                        await R_MessageBox.Show("", "Bank Account Is Required", R_eMessageBoxButtonType.OK);
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
        private void InvDue_OnChanged(object poParam)
        {
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
        private void InvGrp_OnChanged(object poParam)
        {
            IDueDaysEnable = poParam.ToString() == "01";
            IFixDueDateEnable = poParam.ToString() == "02";
            ILimitDueDateEnable = poParam.ToString() == "03";
            IBeforeLimitEnable = poParam.ToString() == "03";
            IAfterLimitEnable = poParam.ToString() == "03";
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
        private void BankAccount_SetAdd(R_SetEventArgs eventArgs)
        {
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);

        }
        private void BankAccount_SetEdit(R_SetEventArgs eventArgs)
        {
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private async Task BankAccount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CINVOICE_TEMPLATE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Invoice Template Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Departement Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Bank Is Required", R_eMessageBoxButtonType.OK);
                }

                lCancel = string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_ACCOUNT);

                if (lCancel)
                {
                    eventArgs.Cancel = lCancel;
                    await R_MessageBox.Show("", "Bank Account Is Required", R_eMessageBoxButtonType.OK);
                }
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

        #endregion

        #region Pinalty
        private async Task Pinalty_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01520DTO loParam = new LMM01520DTO();
                await _InvPinalty_viewModel.GetInvoicePinalty(loParam);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool MonthlyAmmountEnable;
        private bool MonthlyPercentageEnable;
        private bool DailyAmmountEnable;
        private bool DailyPercentageEnable;
        private bool OneTimeAmmountEnable;
        private bool OneTimePercentageEnable;
        private bool CalcBaseonMonthEnable;
        private bool CalcBaseonDaysEnable;

        private void Pinalty_RadioButtonOnChange(object poParam)
        {
            MonthlyAmmountEnable = poParam.ToString() == "10";
            if (poParam.ToString() == "10")
            {

            }
            MonthlyPercentageEnable = poParam.ToString() == "11";
            CalcBaseonMonthEnable = poParam.ToString() == "11";
            if (poParam.ToString() != "11")
            {
                _InvPinalty_viewModel.CalcBaseonMonthValue = "";
            }
            DailyAmmountEnable = poParam.ToString() == "20";
            if (poParam.ToString() == "20")
            {

            }
            DailyPercentageEnable = poParam.ToString() == "21";
            CalcBaseonDaysEnable = poParam.ToString() == "21";
            if (poParam.ToString() != "21")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }
            OneTimeAmmountEnable = poParam.ToString() == "30";
            if (poParam.ToString() != "30")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }
            OneTimePercentageEnable = poParam.ToString() == "31";
            if (poParam.ToString() != "30")
            {
                _InvPinalty_viewModel.CalcBaseonDaysValue = "";
            }

        }

        private async Task Pinalty_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _InvPinalty_viewModel.SaveInvoicePinalty(
                    (LMM01520DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _InvPinalty_viewModel.InvPinalty;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool MinAmmountEnable;
        private void Pinalty_OnCheckedMinAmmount(object poParam)
        {
            MinAmmountEnable = (bool)poParam;
        }

        private bool MaxAmmountEnable;
        private void Pinalty_OnCheckedMaxAmmount(object poParam)
        {
            MaxAmmountEnable = (bool)poParam;
        }

        private void Pinalty_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                switch (0)
                {
                    case var _ when 0 == _InvPinalty_viewModel.MonthlyAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.MonthlyPercentageValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.DailyAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.DailyPercentageValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.OneTimeAmmountValue:
                        break;
                    case var _ when 0 == _InvPinalty_viewModel.OneTimePercentageValue:
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Pinalty_OtherCharges_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01522);
        }

        private void Pinalty_OtherCharges_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LMM01522DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _InvPinalty_viewModel.Data.CPENALTY_ADD_ID = loTempResult.CCHARGES_ID;
            _InvPinalty_viewModel.Data.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
        }

        #endregion

        #region Other Charges
        private async Task OtherCharges_Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _OtherCharges_viewModel.GetOtherChargesList();
                eventArgs.ListEntityResult = _OtherCharges_viewModel.OtherChargesGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                LMM01530DTO loParam = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.GetOtherCharges(loParam);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _OtherCharges_viewModel.SaveOtherCharges(
                    (LMM01530DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _OtherCharges_viewModel.OtherCharges;

                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01530DTO loData = (LMM01530DTO)eventArgs.Data;
                await _OtherCharges_viewModel.DeleteOtherCharges(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }

        private void Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LMM01502);
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
            var loTempResult = (LMM01502DTO)eventArgs.Result;
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
            var loGetData = _BankAccount_viewModel.TemplateBankAccount;

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
        }

        private void OtherCharges_Before_Open_Grid_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {

            var param = new LMM01531DTO()
            {
                CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext,
                CUSER_LANGUANGE = clientHelper.CultureUI.TwoLetterISOLanguageName
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LMM01531);
        }

        private void OtherCharges_After_Open_Grid_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (LMM01531DTO)eventArgs.Result;
            var loData = (LMM01530DTO)eventArgs.ColumnData;

            loData.CPROPERTY_ID = _Genereal_viewModel.PropertyValueContext;
            loData.CINVGRP_CODE = _Genereal_viewModel.Data.CINVGRP_CODE;
            loData.CCHARGES_ID = loTempResult.CCHARGES_ID;
            loData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;
            loData.UNIT_UTILITY_CHARGE = loTempResult.UNIT_UTILITY_CHARGE;
            loData.CCHARGES_TYPE = loTempResult.CCHARGES_TYPE;
            loData.CCHARGES_TYPE_DESCR = loTempResult.CCHARGES_TYPE_DESCR;
        }

        #endregion
    }
}
