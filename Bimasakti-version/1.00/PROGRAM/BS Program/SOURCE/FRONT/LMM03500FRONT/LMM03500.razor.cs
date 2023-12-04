using LMM03500COMMON.DTOs.LMM03501;
using LMM03500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMM03500FRONT
{
    public partial class LMM03500 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }

        private LMM03500ViewModel loViewModel = new LMM03500ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        private List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        private R_TabStrip _TabStripRef;

        private R_TabPage _tabProfileRef;

        private R_TabPage _tabBankRef;

        private R_TabPage _tabMembersRef;

        private R_TabPage _tabFixVARef;

        private R_Conductor _conductorRef;

        private LMM03501ViewModel loTenantViewModel = new LMM03501ViewModel();

        private R_ConductorGrid _conductorTenantRef;

        private R_Grid<LMM03501DTO> _gridTenantRef;

        private bool IsTenantListExist = false;

        private bool _comboboxEnabled = true;

        private bool _pageTenantOnCRUDmode = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetPropertyListStreamAsync();
                loPropertyList = loViewModel.loPropertyList.Select(x => new GetPropertyListDTO()
                { 
                    CPROPERTY_ID = x.CPROPERTY_ID,
                    CPROPERTY_NAME = x.CPROPERTY_NAME
                }).ToList();

                if (loViewModel.loPropertyList.Count() > 0)
                {
                    loViewModel.loProperty = loViewModel.loPropertyList.FirstOrDefault();
                    await PropertyDropdown_ValueChanged(loViewModel.loProperty.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _comboboxEnabled = (bool)poValue;
                _pageTenantOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "List")
            {
                await _gridTenantRef.R_RefreshGrid(null);
            }
        }
        
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageTenantOnCRUDmode;
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
                    var loByteFile = await loTenantViewModel.DownloadTemplateTenantAsync();

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
            loTenantViewModel.SelectedProperty.CPROPERTY_ID = loTenantViewModel.loProperty.CPROPERTY_ID;
            loTenantViewModel.SelectedProperty.CPROPERTY_NAME = loTenantViewModel.loProperty.CPROPERTY_NAME;
            eventArgs.Parameter = new UploadTenantParameterDTO()
            {
                PropertyData = loTenantViewModel.SelectedProperty
            };
            eventArgs.TargetPageType = typeof(UploadTenant);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridTenantRef.R_RefreshGrid(null);
            }
        }


        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loTabParameter.CSELECTED_PROPERTY_ID = poParam;

                loTabParameter.CSELECTED_PROPERTY_NAME = loPropertyList.Where(x => loTabParameter.CSELECTED_PROPERTY_ID == x.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;

                loTenantViewModel.loProperty.CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                loTenantViewModel.loProperty.CPROPERTY_NAME = loTabParameter.CSELECTED_PROPERTY_NAME;

                loTabParameter.CSELECTED_PROPERTY_NAME = loTenantViewModel.loProperty.CPROPERTY_NAME;

                loViewModel.loProperty.CPROPERTY_ID = poParam;
                await _gridTenantRef.R_RefreshGrid(null);
                
                if (_TabStripRef.ActiveTab.Id == "Profile")
                {
                    await _tabProfileRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "Bank")
                {
                    await _tabBankRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "Members")
                {
                    await _tabMembersRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "FixVA")
                {
                    await _tabFixVARef.InvokeRefreshTabPageAsync(loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Tenant_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantViewModel.GetTenantListStreamAsync();
                eventArgs.ListEntityResult = loTenantViewModel.loTenantList;
                if (loTenantViewModel.loTenantList.Count() == 0)
                {
                    loTabParameter.CSELECTED_TENANT_ID = null;
                    IsTenantListExist = false;
                }
                else if (loTenantViewModel.loTenantList.Count() > 0)
                {
                    loTabParameter.CSELECTED_TENANT_ID = loTenantViewModel.loTenantList.FirstOrDefault().CTENANT_ID;
                    IsTenantListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Tenant_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loTenantViewModel.loTenant = (LMM03501DTO)eventArgs.Data;
                loTabParameter.CSELECTED_TENANT_ID = loTenantViewModel.loTenant.CTENANT_ID;
            }
        }

        private void _General_Before_Open_Profile_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            loTabParameter.CSELECTED_PROPERTY_NAME = loTenantViewModel.loProperty.CPROPERTY_NAME;
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03502);
        }

        private void _General_Before_Open_Bank_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03505);
        }
        private void _General_Before_Open_AgreementList_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03506);
        }
        private void _General_Before_Open_Members_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03507);
        }
        private void _General_Before_Open_FixVA_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM03508);
        }
        /*
                private void _General_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
                {
                    switch (_TabStripRef.ActiveTab.Id)
                    {
                        case "Profile":
                            eventArgs.Parameter = loTabParameter;
                            eventArgs.TargetPageType = typeof(LMM03502);
                            break;
                        case "Bank":
                            eventArgs.Parameter = loTabParameter;
                            eventArgs.TargetPageType = typeof(LMM03505);
                            break;
                        case "AgreementList":
                            eventArgs.Parameter = loTabParameter;
                            eventArgs.TargetPageType = typeof(LMM03506);
                            break;
                        case "Members":
                            eventArgs.Parameter = loTabParameter;
                            eventArgs.TargetPageType = typeof(LMM03507);
                            break;
                        case "FixVA":
                            eventArgs.Parameter = loTabParameter;
                            eventArgs.TargetPageType = typeof(LMM03508);
                            break;
                    }
                }*/
        private async Task _General_After_Open_TabPage(R_AfterOpenTabPageEventArgs eventArgs)
        {
            if (eventArgs.Result == null)
            {
                return;
            }
            if ((bool)eventArgs.Result)
            {
                await _gridTenantRef.R_RefreshGrid(null);
            }
        }
    }
}
