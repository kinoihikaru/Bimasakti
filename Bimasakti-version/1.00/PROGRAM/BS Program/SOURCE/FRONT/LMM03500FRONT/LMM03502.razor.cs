using BlazorClientHelper;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
using LMM03500MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500FRONT
{
    public partial class LMM03502 : R_Page, R_ITabPage
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        private LMM03501ViewModel loTenantListViewModel = new LMM03501ViewModel();

        private LMM03502ViewModel loTenantProfileViewModel = new LMM03502ViewModel();

        private R_Conductor _conductorProfileTaxRef;

        private LMM03503ViewModel loTaxInfoViewModel = new LMM03503ViewModel();

        private R_TabStrip tabStripRef;

        private R_TextBox tenantIdRef;

        private bool llResult = false;

        private bool IsCRUDMode = true;

        private bool IsSuccess = false;

        private bool IsSelectedTenantExist = false;

        //Billing
        private LMM03504ViewModel loBillingViewModel = new();

        private R_Conductor _conductorBillingRef;

        private bool IsEnabled = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loTenantProfileViewModel.loTabParameter = (TabParameterDTO)poParameter;
                loTaxInfoViewModel.loTabParameter = loTenantProfileViewModel.loTabParameter;

                await loTenantProfileViewModel.GetTenantTypeListAsync();

                await loTaxInfoViewModel.GetTaxCodeListStreamAsync();
                await loTaxInfoViewModel.GetIdTypeListStreamAsync();
                await loTaxInfoViewModel.GetTaxTypeListStreamAsync();

                if (loTenantProfileViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
                {
                    IsSelectedTenantExist = true;
                    await _conductorProfileTaxRef.R_GetEntity(null);
                }
                else
                {
                    IsSelectedTenantExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ProfileAndTaxInfo_SetOther(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = !eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private async Task R_TabEventCallback(object poValue)
        {
            IsCRUDMode = !(bool)poValue;
            await InvokeTabEventCallbackAsync(poValue);
        }

        private async Task ProfileAndTaxInfo_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await tenantIdRef.FocusAsync();
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            loTenantProfileViewModel.loTabParameter = (TabParameterDTO)poParam;
            loTaxInfoViewModel.loTabParameter = loTenantProfileViewModel.loTabParameter;
            if (tabStripRef.ActiveTab.Id == "Profile" || tabStripRef.ActiveTab.Id == "TaxInfo")
            {

                if (loTenantProfileViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
                {
                    IsSelectedTenantExist = true;
                    await _conductorProfileTaxRef.R_GetEntity(null);
                }
                else
                {
                    loTenantProfileViewModel.Data.Profile.Clear();
                    loTenantProfileViewModel.Data.TaxInfo.Clear();
                    IsSelectedTenantExist = false;
                }
            }
            else if (tabStripRef.ActiveTab.Id == "Billing")
            {
                loBillingViewModel.loTabParameter = loTenantProfileViewModel.loTabParameter;

                if (loBillingViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
                {
                    IsEnabled = true;
                    await _conductorBillingRef.R_GetEntity(null);
                }
                else
                {
                    IsEnabled = false;
                    loBillingViewModel.Data.Clear();
                }
            }
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateTenantDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loTenantListViewModel.DownloadTemplateTenantAsync();

                    var saveFileName = $"Tenant.xlsx";
                    /*
                                        var saveFileName = $"Staff {CenterViewModel.PropertyValueContext}.xlsx";*/

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
            SelectedPropertyDTO loParam = new SelectedPropertyDTO()
            {
                CPROPERTY_ID = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                CPROPERTY_NAME = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_NAME
            };
            eventArgs.Parameter = new UploadTenantParameterDTO()
            {
                PropertyData = loParam
            };
            eventArgs.TargetPageType = typeof(UploadTenant);
        }

        private void After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }/*
            if ((bool)eventArgs.Result == true)
            {
                await _gridTenantRef.R_RefreshGrid(null);
            }*/
        }

        protected override Task<object> R_Set_Result_TabPage()
        {
            return Task.FromResult<object>(llResult);
        }

        protected override Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            eventArgs.Cancel = _conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Add || _conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Edit;
            return Task.CompletedTask;
        }

        private async Task OnClickNextButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                ProfileTaxDTO loParam = new ProfileTaxDTO();
                loParam = (ProfileTaxDTO)_conductorProfileTaxRef.R_GetCurrentData();
                loTenantProfileViewModel.TenantProfileValidation(loParam.Profile);
                if (!loException.HasError)
                {
                    IsSuccess = true;
                    await tabStripRef.SetActiveTabAsync("TaxInfo");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void ProfileAndTaxInfo_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsSuccess = false;
        }

        private async void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (_conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Add || _conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    if (eventArgs.TabStripTab.Id == "TaxInfo" && IsSuccess)
                    {
                        IsSuccess = false;
                    }
                    else
                    {
                        eventArgs.Cancel = true;
                    }
                }
                else
                {
                    if (eventArgs.TabStripTab.Id == "TaxInfo" || eventArgs.TabStripTab.Id == "Profile")
                    {
                        if (IsCRUDMode)
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                eventArgs.Cancel = true;
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (_conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Add || _conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                if (eventArgs.Id == "TaxInfo")
                {
                    loTenantProfileViewModel.Data.TaxInfo.CTENANT_ID = loTenantProfileViewModel.Data.Profile.CTENANT_ID;
                    loTenantProfileViewModel.Data.TaxInfo.CTENANT_NAME = loTenantProfileViewModel.Data.Profile.CTENANT_NAME;
                }
            }
            if (eventArgs.Id == "Billing")
            {
                loBillingViewModel.loTabParameter = (TabParameterDTO)loTenantProfileViewModel.loTabParameter;

                await _conductorBillingRef.R_GetEntity(null);
            }
        }

        private void _General_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTenantProfileViewModel.loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03504);
        }

        private async Task ProfileAndTaxInfo_GetTenantProfile(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loTenantProfileViewModel.GetTenantProfileAsync();
                await loTaxInfoViewModel.GetTaxInfoAsync();

                ProfileTaxDTO loParam = new ProfileTaxDTO()
                {
                    Profile = loTenantProfileViewModel.loTenantProfile,
                    TaxInfo = loTaxInfoViewModel.loTaxInfo
                };
                eventArgs.Result = loParam;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void BeforeOpenTenantGroupLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpTenantGroup);
        }

        private void AfterOpenTenantGroupLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetTenantGroupDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CTENANT_GROUP_ID = loTempResult.CTENANT_GROUP_ID;
            loTenantProfileViewModel.Data.Profile.CTENANT_GROUP_NAME = loTempResult.CTENANT_GROUP_NAME;
        }

        private void BeforeOpenTenantCategoryLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpTenantCategory);
        }

        private void AfterOpenTenantCategoryLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetTenantCategoryDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CTENANT_CATEGORY_ID = loTempResult.CCATEGORY_ID;
            loTenantProfileViewModel.Data.Profile.CTENANT_CATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }
        /*
                private void BeforeOpenJournalGroupLookUp(R_BeforeOpenLookupEventArgs eventArgs)
                {
                    eventArgs.Parameter = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
                    eventArgs.TargetPageType = typeof(LookUpJournalGroup);
                }

                private void AfterOpenJournalGroupLookUp(R_AfterOpenLookupEventArgs eventArgs)
                {
                    var loTempResult = (GetJournalGroupDTO)eventArgs.Result;
                    if (loTempResult == null)
                    {
                        return;
                    }
                    loTenantProfileViewModel.Data.Profile.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
                    loTenantProfileViewModel.Data.Profile.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
                }
        */

        private void BeforeOpenCityLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL02000);
        }

        private void AfterOpenCityLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02000DTO loTempResult = (GSL02000DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            //loTenantProfileViewModel.Data.Profile.CCITY_CODE = loTempResult.CCODE;
            //loTenantProfileViewModel.Data.Profile.CCITY_NAME = loTempResult.CNAME;
            //loTenantProfileViewModel.Data.Profile.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }

        private void BeforeOpenJournalGroupLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                CJRNGRP_TYPE = "20"
            };
            eventArgs.TargetPageType = typeof(GSL00400);
        }

        private void AfterOpenJournalGroupLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00400DTO loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loTenantProfileViewModel.Data.Profile.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }
        private void BeforeOpenLineOfBusinessLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LookUpLineOfBusiness);
        }

        private void AfterOpenLineOfBusinessLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetLineOfBusinessDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CLOB_CODE = loTempResult.CLOB_CODE;
            loTenantProfileViewModel.Data.Profile.CLOB_DESCRIPTION = loTempResult.CLOB_NAME;
        }

        private void BeforeOpenCurrencyLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(LookUpCurrency);
        }

        private void AfterOpenCurrencyLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetCurrencyDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            loTenantProfileViewModel.Data.Profile.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }
/*
        private void BeforeOpenPaymentTermLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpPaymentTerm);
        }

        private void AfterOpenPaymentTermLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetPaymentTermDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CPAYMENT_TERM_CODE = loTempResult.CPAY_TERM_CODE;
            loTenantProfileViewModel.Data.Profile.CPAYMENT_TERM_NAME = loTempResult.CPAY_TERM_NAME;
        }
*/

        private void BeforeOpenPaymentTermLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02100ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.TargetPageType = typeof(GSL02100);
        }

        private void AfterOpenPaymentTermLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL02100DTO loTempResult = (GSL02100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CPAYMENT_TERM_CODE = loTempResult.CPAY_TERM_CODE;
            loTenantProfileViewModel.Data.Profile.CPAYMENT_TERM_NAME = loTempResult.CPAY_TERM_NAME;
        }
/*
        private void BeforeOpenSalesmanLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpSalesman);
        }

        private void AfterOpenSalesmanLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetSalesmanDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
            loTenantProfileViewModel.Data.Profile.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
        }
*/
        private void BeforeOpenSalesmanLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00500ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = loTenantProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.TargetPageType = typeof(LML00500);
        }

        private void AfterOpenSalesmanLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            LML00500DTO loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loTenantProfileViewModel.Data.Profile.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
            loTenantProfileViewModel.Data.Profile.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
        }

        private async Task ProfileAndTaxInfo_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                ProfileTaxDTO loData = (ProfileTaxDTO)eventArgs.Data;
                await loTenantProfileViewModel.DeleteTenantProfileAndTaxInfoAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ProfileAndTaxInfo_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantProfileViewModel.SaveTenantProfileAndTaxInfoAsync(
                    (ProfileTaxDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loTenantProfileViewModel.loProfileAndTaxInfo;
                if (eventArgs.Result != null)
                {
                    llResult = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void ProfileAndTaxInfo_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                ProfileTaxDTO loParam = (ProfileTaxDTO)eventArgs.Data;
                loTenantProfileViewModel.TenantProfileAndTaxInfoValidation(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Billing 

        private void BeforeCancelBilling(R_BeforeCancelEventArgs eventArgs)
        {
            IsEnabled = true;
        }

        private void BeforeEditBilling(R_BeforeEditEventArgs eventArgs)
        {
            IsEnabled = false;
        }

        private void AfterSaveBilling(R_AfterSaveEventArgs eventArgs)
        {
            IsEnabled = true;
        }


        private async Task GetBillingAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loBillingViewModel.GetBillingAsync();
                eventArgs.Result = loBillingViewModel.loBilling;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void SavingBilling(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loBillingViewModel.BillingValidation((LMM03504DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceSaveBilling(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loBillingViewModel.SaveBillingAsync(
                    (LMM03504DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loBillingViewModel.loBilling;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Billing_SetOther(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = !eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        #endregion
    }
}
