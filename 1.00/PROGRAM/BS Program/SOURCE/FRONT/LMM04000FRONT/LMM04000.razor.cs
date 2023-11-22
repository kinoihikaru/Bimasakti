using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs;
using LMM04000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMM04000FRONT
{
    public partial class LMM04000 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }

        private LMM04000ViewModel loContractorViewModel = new LMM04000ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        private R_TabStrip _TabStripRef;

        private R_ConductorGrid _conductorContractorRef;

        private R_Grid<LMM04000DTO> _gridContractorRef;

        private List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        private bool IsContractorListExist = false;

        private R_TabPage _tabFittingOutListRef;

        private R_TabPage _tabProfileRef;

        private R_TabPage _tabBankRef;

        private bool _comboboxEnabled = true;

        private bool _pageContractorOnCRUDmode = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loContractorViewModel.GetPropertyListStreamAsync();
                loPropertyList = loContractorViewModel.loPropertyList.Select(x => new GetPropertyListDTO()
                {
                    CPROPERTY_ID = x.CPROPERTY_ID,
                    CPROPERTY_NAME = x.CPROPERTY_NAME
                }).ToList();

                if (loContractorViewModel.loPropertyList.Count > 0)
                {
                    loContractorViewModel.loProperty = loContractorViewModel.loPropertyList.FirstOrDefault();
                    await PropertyDropdown_ValueChanged(loContractorViewModel.loProperty.CPROPERTY_ID);
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
                _pageContractorOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
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
                    var loByteFile = await loContractorViewModel.DownloadTemplateContractorAsync();

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
            R_Exception loException = new R_Exception();
            try
            {
                loContractorViewModel.SelectedProperty.CPROPERTY_ID = loContractorViewModel.loProperty.CPROPERTY_ID;
                loContractorViewModel.SelectedProperty.CPROPERTY_NAME = loContractorViewModel.loProperty.CPROPERTY_NAME;
                eventArgs.Parameter = new UploadContractorParameterDTO()
                {
                    PropertyData = loContractorViewModel.SelectedProperty
                };
                eventArgs.TargetPageType = typeof(UploadContractor);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridContractorRef.R_RefreshGrid(null);
            }
        }

        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "List")
            {
                await _gridContractorRef.R_RefreshGrid(null);
            }
        }
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageContractorOnCRUDmode;
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loContractorViewModel.loProperty.CPROPERTY_ID = poParam;
                loTabParameter.CSELECTED_PROPERTY_ID = poParam;
                loTabParameter.CSELECTED_PROPERTY_NAME = loPropertyList.Where(x => loTabParameter.CSELECTED_PROPERTY_ID == x.CPROPERTY_ID).FirstOrDefault().CPROPERTY_NAME;

                loContractorViewModel.loProperty.CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                loContractorViewModel.loProperty.CPROPERTY_NAME = loTabParameter.CSELECTED_PROPERTY_NAME;

                await _gridContractorRef.R_RefreshGrid(null);

                if (_TabStripRef.ActiveTab.Id == "Profile")
                {
                    await _tabProfileRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
                else if (_TabStripRef.ActiveTab.Id == "Bank")
                {
                    await _tabBankRef.InvokeRefreshTabPageAsync(loTabParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Contractor_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loContractorViewModel.GetContractorListStreamAsync();
                eventArgs.ListEntityResult = loContractorViewModel.loContractorList;
                if (loContractorViewModel.loContractorList.Count() == 0)
                {
                    loTabParameter.CSELECTED_TENANT_ID = null;
                    IsContractorListExist = false;
                }
                else if (loContractorViewModel.loContractorList.Count() > 0)
                {
                    loTabParameter.CSELECTED_TENANT_ID = loContractorViewModel.loContractorList.FirstOrDefault().CTENANT_ID;
                    IsContractorListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Contractor_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loContractorViewModel.loContractor = (LMM04000DTO)eventArgs.Data;
                loTabParameter.CSELECTED_TENANT_ID = loContractorViewModel.loContractor.CTENANT_ID;
            }
        }

        private void _General_Before_Open_FittingOutList_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {/*
            eventArgs.Parameter = loTabParameter;*/
            eventArgs.TargetPageType = typeof(LMM04040);
        }

        private void _General_Before_Open_Profile_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM04010);
        }
        
        private void _General_Before_Open_Bank_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter;
            eventArgs.TargetPageType = typeof(LMM04030);
        }
    }
}
