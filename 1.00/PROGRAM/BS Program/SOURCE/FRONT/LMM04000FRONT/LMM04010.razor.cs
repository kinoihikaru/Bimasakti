using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.DTOs.LMM04020;
using LMM04000MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
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
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000FRONT
{
    public partial class LMM04010 : R_Page, R_ITabPage
    {
        [Inject] IJSRuntime JS { get; set; }

        private LMM04000ViewModel loContractorListViewModel = new LMM04000ViewModel();

        private LMM04010ViewModel loContractorProfileViewModel = new LMM04010ViewModel();

        private LMM04020ViewModel loTaxInfoViewModel = new LMM04020ViewModel();

        private R_Conductor _conductorProfileTaxRef;

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private R_TabStrip tabStripRef;

        private bool IsSuccess = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loContractorProfileViewModel.loTabParameter = (TabParameterDTO)poParameter;
                loTaxInfoViewModel.loTabParameter = loContractorProfileViewModel.loTabParameter;

                //await loContractorProfileViewModel.GetContractorGroupListAsync();
                //await loContractorProfileViewModel.GetContractorCategoryListAsync();

                await loTaxInfoViewModel.GetTaxCodeListStreamAsync();
                await loTaxInfoViewModel.GetIdTypeListStreamAsync();
                await loTaxInfoViewModel.GetTaxTypeListStreamAsync();

                if (loContractorProfileViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
                {
                    await _conductorProfileTaxRef.R_GetEntity(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Contractor_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loContractorProfileViewModel.loTabParameter = (TabParameterDTO)poParam;
            loTaxInfoViewModel.loTabParameter = loContractorProfileViewModel.loTabParameter;

            if (loContractorProfileViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
            {
                await _conductorProfileTaxRef.R_GetEntity(null);
            }
            else
            {
                loContractorProfileViewModel.Data.Profile.Clear();
                loContractorProfileViewModel.Data.TaxInfo.Clear();
            }
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateContractorDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loContractorListViewModel.DownloadTemplateContractorAsync();

                    var saveFileName = $"Contractor.xlsx";
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
                CPROPERTY_ID = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                CPROPERTY_NAME = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_NAME
            };
            eventArgs.Parameter = new UploadContractorParameterDTO()
            {
                PropertyData = loParam
            };
            eventArgs.TargetPageType = typeof(UploadContractor);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        private async Task OnClickNextButton()
        {
            R_Exception loException = new R_Exception();
            try
            {
                ProfileTaxDTO loParam = new ProfileTaxDTO();
                loParam = (ProfileTaxDTO)_conductorProfileTaxRef.R_GetCurrentData();
                loContractorProfileViewModel.ContractorProfileValidation(loParam.Profile);
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

        private void Contractor_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsSuccess = false;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
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
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (_conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Add || _conductorProfileTaxRef.R_ConductorMode == R_eConductorMode.Edit)
                {
                    if (eventArgs.Id == "TaxInfo")
                    {
                        loContractorProfileViewModel.Data.TaxInfo.CTENANT_ID = loContractorProfileViewModel.Data.Profile.CTENANT_ID;
                        loContractorProfileViewModel.Data.TaxInfo.CTENANT_NAME = loContractorProfileViewModel.Data.Profile.CTENANT_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ProfileAndTaxInfo_GetContractorProfile(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loContractorProfileViewModel.GetContractorProfileAsync();
                await loTaxInfoViewModel.GetTaxInfoAsync();
                ProfileTaxDTO loResult = new ProfileTaxDTO()
                {
                    Profile = loContractorProfileViewModel.loContractorProfile,
                    TaxInfo = loTaxInfoViewModel.loTaxInfo
                };
                eventArgs.Result = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

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
            //loContractorProfileViewModel.Data.Profile.CCITY_CODE = loTempResult.CCODE;
            //loContractorProfileViewModel.Data.Profile.CCITY_NAME = loTempResult.CNAME;
            //loTenantProfileViewModel.Data.Profile.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }

        private void BeforeOpenContractorGroupLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookupContractorGroup);
        }

        private void AfterOpenContractorGroupLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetContractorGroupDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loContractorProfileViewModel.Data.Profile.CTENANT_GROUP_ID = loTempResult.CTENANT_GROUP_ID;
            loContractorProfileViewModel.Data.Profile.CTENANT_GROUP_NAME = loTempResult.CTENANT_GROUP_NAME;
        }

        private void BeforeOpenContractorCategoryLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookupContractorCategory);
        }

        private void AfterOpenContractorCategoryLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetContractorCategoryDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loContractorProfileViewModel.Data.Profile.CTENANT_CATEGORY_ID = loTempResult.CCATEGORY_ID;
            loContractorProfileViewModel.Data.Profile.CTENANT_CATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }

        private void BeforeOpenJournalGroupLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpJournalGroup);
        }

        private void AfterOpenJournalGroupLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetJournalGroupDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loContractorProfileViewModel.Data.Profile.CJRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loContractorProfileViewModel.Data.Profile.CJRNGRP_NAME = loTempResult.CJRNGRP_NAME;
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
            loContractorProfileViewModel.Data.Profile.CLOB_CODE = loTempResult.CLOB_CODE;
            loContractorProfileViewModel.Data.Profile.CLOB_DESCRIPTION = loTempResult.CLOB_NAME;
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
            loContractorProfileViewModel.Data.Profile.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            loContractorProfileViewModel.Data.Profile.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }

        private void BeforeOpenPaymentTermLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = loContractorProfileViewModel.loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(LookUpPaymentTerm);
        }

        private void AfterOpenPaymentTermLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GetPaymentTermDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loContractorProfileViewModel.Data.Profile.CPAYMENT_TERM_CODE = loTempResult.CPAY_TERM_CODE;
            loContractorProfileViewModel.Data.Profile.CPAYMENT_TERM_NAME = loTempResult.CPAY_TERM_NAME;
        }

        private async Task ProfileAndTaxInfo_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                ProfileTaxDTO loData = (ProfileTaxDTO)eventArgs.Data;
                await loContractorProfileViewModel.DeleteContractorProfileAndTaxInfoAsync(loData);
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
                ProfileTaxDTO loSavingParam = new ProfileTaxDTO();
                loSavingParam = (ProfileTaxDTO)eventArgs.Data;

                loContractorProfileViewModel.ContractorProfileAndTaxInfoValidation(loSavingParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task ProfileAndTaxInfo_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                ProfileTaxDTO loSaveParam = new ProfileTaxDTO();
                loSaveParam = (ProfileTaxDTO)eventArgs.Data;

                await loContractorProfileViewModel.SaveContractorProfileAndTaxInfoAsync(
                    loSaveParam,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loContractorProfileViewModel.loProfileAndTaxInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
