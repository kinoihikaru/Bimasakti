using APM00200COMMON.DTOs.APM00200;
using APM00200MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace APM00200FRONT
{
    public partial class APM00200 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        private APM00200ViewModel loExpenditureViewModel = new APM00200ViewModel();

        private R_Conductor _conGridExpenditureRef;

        private R_Grid<APM00200DTO> _gridExpenditureRef;

        private string loLabel = "Deactivate";

        private bool _comboboxEnabled = true;

        private bool IsWitholdingTaxEnabled = false;

        private bool IsWitholdingTaxIdEnabled = false;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureViewModel.GetPropertyListStreamAsync();

                if (loExpenditureViewModel.loPropertyList.Count() > 0)
                {
                    loExpenditureViewModel.loProperty = loExpenditureViewModel.loPropertyList.FirstOrDefault();
                    await PropertyDropdown_ValueChanged(loExpenditureViewModel.loProperty.CPROPERTY_ID);
                }
                await loExpenditureViewModel.GetWithholdingTaxTypeListStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _gridExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ActivateDeactivateOnClick()
        {
            R_Exception loEx = new R_Exception();
            string lcOldLabel;
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", $"Are you sure want to {loLabel} this Expenditure?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await loExpenditureViewModel.UpdateExpenditureActiveFlagAsync();
                }

                if (loEx.HasError == false)
                {
                    lcOldLabel = loLabel;
                    await _gridExpenditureRef.R_GetRecordAsync(loExpenditureViewModel.loExpenditureDetail);
                    await _conGridExpenditureRef.R_SetCurrentData(loExpenditureViewModel.loExpenditureDetail);
                    loValidate = await R_MessageBox.Show("", $"{lcOldLabel} Expenditure Successful!", R_eMessageBoxButtonType.OK);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loExpenditureViewModel.loProperty.CPROPERTY_ID = poParam;
                await _gridExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void WithholdingTaxType_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loExpenditureViewModel.Data.CWITHHOLDING_TAX_TYPE = poParam;
                if (_conGridExpenditureRef.R_ConductorMode == R_eConductorMode.Add || _conGridExpenditureRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    loExpenditureViewModel.Data.CWITHHOLDING_TAX_ID = "";
                    loExpenditureViewModel.Data.CWITHHOLDING_TAX_NAME = "";
                    if (string.IsNullOrWhiteSpace(loExpenditureViewModel.Data.CWITHHOLDING_TAX_TYPE))
                    {
                        IsWitholdingTaxIdEnabled = false;
                    }
                    else
                    {
                        IsWitholdingTaxIdEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void WithholdingTaxCheckBox_ValueChanged(bool poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loExpenditureViewModel.Data.LWITHHOLDING_TAX = poParam;
                if (_conGridExpenditureRef.R_ConductorMode == R_eConductorMode.Add || _conGridExpenditureRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    IsWitholdingTaxEnabled = poParam;
                    loExpenditureViewModel.Data.CWITHHOLDING_TAX_ID = "";
                    loExpenditureViewModel.Data.CWITHHOLDING_TAX_NAME = "";
                    loExpenditureViewModel.Data.CWITHHOLDING_TAX_TYPE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loExpenditureViewModel.DownloadTemplateExpenditureAsync();

                    var saveFileName = $"Expenditure.xlsx";
                    /*
                                        var saveFileName = $"Staff {loExpenditureViewModel.PropertyValueContext}.xlsx";*/

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(UploadExpenditure);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridExpenditureRef.R_RefreshGrid(null);
            }
        }

        private void Grid_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsWitholdingTaxEnabled = false;
            IsWitholdingTaxIdEnabled = false;
        }

        private void Grid_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            if (loExpenditureViewModel.Data.LWITHHOLDING_TAX)
            {
                IsWitholdingTaxEnabled = true;
            }
            if (string.IsNullOrWhiteSpace(loExpenditureViewModel.Data.CWITHHOLDING_TAX_TYPE) == false)
            {
                IsWitholdingTaxIdEnabled = true;
            }
        }

        private void Grid_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loExpenditureViewModel.ExpenditureValidation((APM00200DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (APM00200DetailDTO)eventArgs.Data;
                loExpenditureViewModel.loExpenditure = _gridExpenditureRef.CurrentSelectedData;
                loExpenditureViewModel.loExpenditureDetail = loParam;

                if (loParam.LACTIVE)
                {
                    loLabel = "Deactivate";
                }
                else
                {
                    loLabel = "Activate";
                }
            }
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureViewModel.GetExpenditureListStreamAsync();
                eventArgs.ListEntityResult = loExpenditureViewModel.loExpenditureList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {

                APM00200DetailDTO loParam = R_FrontUtility.ConvertObjectToObject<APM00200DetailDTO>(eventArgs.Data);
                await loExpenditureViewModel.GetExpenditureAsync(loParam);

                eventArgs.Result = loExpenditureViewModel.loExpenditureDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureViewModel.SaveExpenditureAsync(
                    (APM00200DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                IsWitholdingTaxEnabled = false;
                IsWitholdingTaxIdEnabled = false;
                eventArgs.Result = loExpenditureViewModel.loExpenditureDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APM00200DetailDTO loData = (APM00200DetailDTO)eventArgs.Data;
                await loExpenditureViewModel.DeleteExpenditureAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            APM00200DetailDTO loData = (APM00200DetailDTO)eventArgs.Data;
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<APM00200DTO>(loData);
        }

        private void Grid_SetOther(R_SetEventArgs eventArgs)
        {
            _comboboxEnabled = eventArgs.Enable;
        }

        #region Lookup
        private void R_Before_Open_JournalGroup_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00400ParameterDTO loParam = new GSL00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loExpenditureViewModel.loProperty.CPROPERTY_ID,
                CJRNGRP_TYPE = "40"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void R_After_Open_JournalGroup_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00400DTO loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00200DetailDTO)_conGridExpenditureRef.R_GetCurrentData();
            loGetData.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loGetData.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }

        private void R_Before_Open_Category_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL01800DTOParameter loParam = new GSL01800DTOParameter()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loExpenditureViewModel.loProperty.CPROPERTY_ID,
                CCATEGORY_TYPE = "40"
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void R_After_Open_Category_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL01800DTO loTempResult = (GSL01800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00200DetailDTO)_conGridExpenditureRef.R_GetCurrentData();
            loGetData.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
            loGetData.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }


        private void R_Before_Open_OtherTax_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00100ParameterDTO loParam = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void R_After_Open_OtherTax_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00100DTO loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00200DetailDTO)_conGridExpenditureRef.R_GetCurrentData();
            loGetData.COTHER_TAX_ID = loTempResult.CTAX_ID;
            loGetData.COTHER_TAX_NAME = loTempResult.CTAX_NAME;
        }

        private void R_Before_Open_WithholdingTax_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00200ParameterDTO loParam = new GSL00200ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loExpenditureViewModel.loProperty.CPROPERTY_ID,
                CTAX_TYPE_LIST = loExpenditureViewModel.Data.CWITHHOLDING_TAX_TYPE
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00200);
        }

        private void R_After_Open_WithholdingTax_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00200DTO loTempResult = (GSL00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (APM00200DetailDTO)_conGridExpenditureRef.R_GetCurrentData();
            loGetData.CWITHHOLDING_TAX_ID = loTempResult.CTAX_ID;
            loGetData.CWITHHOLDING_TAX_NAME = loTempResult.CTAX_NAME;
        }


        #endregion

        private void Before_Open_CopyFrom_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(APM00200CopyFrom);
        }

        private async Task After_Open_CopyFrom_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
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
                    await _gridExpenditureRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            await _gridExpenditureRef.R_RefreshGrid(null);
        }
    }
}
