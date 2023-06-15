using BlazorClientHelper;
using LMM01000MODEL;
using LMM01000COMMON;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01000FRONT
{
    public partial class LMM01000 : R_Page
    {
        private LMM01000ViewModel _General_viewModel = new LMM01000ViewModel();
        private LMM01000UniversalViewModel _Universal_viewModel = new LMM01000UniversalViewModel();

        private R_Grid<LMM01000UniversalDTO> _General_gridRef;
        private R_Grid<LMM01002DTO> _UtilityCharges_gridRef;

        private R_Conductor _General_conductorRef;
        private R_Conductor _UtilityCharges_conductorRef;

        private string _Genereal_lcLabel = "Activate";
        
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(poParameter);
                await UtilityCharges_Status_ServiceGetListRecord(null);
                await UtilityCharges_TaxExemption_ServiceGetListRecord(null);
                await UtilityCharges_WithholdingTax_ServiceGetListRecord(null);
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
                await _General_viewModel.GetPropertyList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task ChargesTypeGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _General_viewModel.GetChargesTypeList(loParam);

                eventArgs.ListEntityResult = _General_viewModel.ChargesTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ChargesTypeGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01000UniversalDTO>(eventArgs.Data);
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                await _General_viewModel.GetChargesType(loParam);

                eventArgs.Result = _General_viewModel.UtilityCharges;
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
            var loTempParam = _General_gridRef.CurrentSelectedData;

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Utility 
        private R_TabStrip _TabGeneral;
        private void UtilityCharges_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (LMM01000DTO)eventArgs.Data;

                    if (loParam.CSTATUS != "80")
                    {
                        _Genereal_lcLabel = "Activate";
                        _General_viewModel.StatusChange = "80";
                    }
                    else
                    {
                        _Genereal_lcLabel = "Inactive";
                        _General_viewModel.StatusChange = "90";
                    }

                    var tes = _TabGeneral.ActiveTabIndex;
                    tes = 1;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task UtilityCharges_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = R_FrontUtility.ConvertObjectToObject<LMM01000UniversalDTO>(eventArgs.Parameter);
                var loParam = new LMM01002DTO()
                {
                    CCHARGES_TYPE = loTempParam.CCODE
                };
                await _General_viewModel.GetChargesUtilityList(loParam);

                eventArgs.ListEntityResult = _General_viewModel.ChargesUtilityGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01000DTO>(eventArgs.Data);
                await _General_viewModel.GetChargesUtility(loParam);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_Status_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetStatusList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task UtilityCharges_TaxExemption_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetTaxExemptionList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task UtilityCharges_WithholdingTax_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new LMM01000UniversalDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };

                await _Universal_viewModel.GetWithHoldingTaxList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void UtilityCharges_OtherTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO();

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void UtilityCharges_OtherTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.COTHER_TAX_ID = loTempResult.CTAX_ID;
            loData.CTAX_OTHER_NAME = loTempResult.CTAX_NAME;
        }

        private void UtilityCharges_WithholdingTax_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00200ParameterDTO();
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

           param.CTAX_TYPE = loData.CWITHHOLDING_TAX_TYPE;

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00200);
        }

        private void UtilityCharges_WithholdingTax_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            loData.CWITHHOLDING_TAX_ID = loTempResult.CTAX_ID;
            loData.CWITHHOLDING_TAX_NAME = loTempResult.CTAX_NAME;
        }

        private void UtilityCharges_JournalGrp_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00400ParameterDTO()
            {
                CPROPERTY_ID = _General_viewModel.PropertyValueContext,
                CJRNGRP_TYPE = "11"
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void UtilityCharges_JournalGrp_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();
            loData.CUTILITY_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loData.CUTILITY_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }

        private bool TaxExemptionEnable = false;
        private void Taxable_OnChnaged(object poParam)
        {
            TaxExemptionEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.LTAX_EXEMPTION = false;
                loData.CTAX_EXEMPTION_CODE = "";
                loData.ITAX_EXEMPTION_PCT = 0;
            }
        }

        private bool TaxExemptionCodeEnable = false;
        private void TaxExemption_OnChnaged(object poParam)
        {
            TaxExemptionCodeEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.CTAX_EXEMPTION_CODE = "";
                loData.ITAX_EXEMPTION_PCT = 0;
            }
        }

        private bool OtherTaxLookupEnable = false;
        private void OtherTax_OnChnaged(object poParam)
        {
            OtherTaxLookupEnable = (bool)poParam;
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.COTHER_TAX_ID = "";
                loData.CTAX_OTHER_NAME = "";
            }
        }

        private void WithholdingTax_OnChange(object poParam)
        {
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.CWITHHOLDING_TAX_TYPE = "";
                loData.CWITHHOLDING_TAX_ID = "";
                loData.CWITHHOLDING_TAX_NAME = "";
            }
        }

        private void AccuralJournal_OnChanged(object poParam)
        {
            var loData = (LMM01000DTO)_UtilityCharges_conductorRef.R_GetCurrentData();

            if (!(bool)poParam)
            {
                loData.CUTILITY_JRNGRP_CODE = "";
                loData.CUTILITY_JRNGRP_NAME = "";
            }
        }

        private bool _EnableAddEdit = false;

        private void UtilityCharges_SetAdd(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_SetEdit(R_SetEventArgs eventArgs)
        {
            _EnableAddEdit = eventArgs.Enable;
        }

        private void UtilityCharges_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            eventArgs.Allow = !string.IsNullOrEmpty(_General_viewModel.PropertyValueContext);
        }

        private void UtilityCharges_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.SaveChargesUtility((LMM01000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _General_viewModel.UtilityCharges;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UtilityCharges_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _General_viewModel.DeleteChargesUtility((LMM01000DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private bool CopyNewBtnEnable = false;
        private bool ActiveInactiveBtnEnable = false;
        private bool PrintBtnEnable = false;

        private void UtilityCharges_SetHasData(R_SetEventArgs eventArgs)
        {
            CopyNewBtnEnable = eventArgs.Enable;
            ActiveInactiveBtnEnable = eventArgs.Enable;
            PrintBtnEnable = eventArgs.Enable;
        }

        private void UtilityCharges_R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = "LMM01001";
            eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
        }

        private async Task UtilityCharges_R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loGetData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();
            
            R_Exception loException = new R_Exception();
            try
            {
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await _General_viewModel.ActiveInactiveProcessAsync(loGetData);
                }

                var loTempParam = _General_gridRef.CurrentSelectedData;
                await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void UtilityCharges_CopyFromBtn_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (LMM01000DTO)_General_viewModel.R_GetCurrentData();
            var loParam = new LMM01003DTO() 
            {
                CPROPERTY_ID = loData.CPROPERTY_ID,
                CCHARGES_TYPE = loData.CCHARGES_TYPE,
                CCURRENT_CHARGES_ID = loData.CCHARGES_ID,
                CCURRENT_CHARGES_NAME = loData.CCHARGES_NAME,

            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LMM01003);
        }

        private async Task UtilityCharges_CopyFromBtn_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loTempData = (LMM01003DTO)eventArgs.Result;
            R_Exception loException = new R_Exception();
            try
            {
                var loData = new LMM01000DTO()
                {
                    CCHARGES_ID = loTempData.CNEW_CHARGES_ID,
                    CCHARGES_TYPE = loTempData.CCHARGES_TYPE,
                    CPROPERTY_ID = loTempData.CPROPERTY_ID,
                };

                var loTempParam = _General_gridRef.CurrentSelectedData;
                await _UtilityCharges_gridRef.R_RefreshGrid(loTempParam);

                await _UtilityCharges_conductorRef.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion
        private string _ChargesTypeVal = "";
        private string _ChargesIDVall = "";

        private void _General_OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loTempParam = _General_gridRef.CurrentSelectedData;
            var loTempParamUtility = _UtilityCharges_gridRef.CurrentSelectedData;

            if (eventArgs.Id == "Rate")
            {
                _ChargesTypeVal = loTempParam.CCODE;
            }

        }

    }
}
