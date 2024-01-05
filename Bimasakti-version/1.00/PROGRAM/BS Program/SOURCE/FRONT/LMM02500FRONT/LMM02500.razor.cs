using BlazorClientHelper;
using LMM02500COMMON;
using LMM02500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace LMM02500FRONT
{
    public partial class LMM02500 : R_Page
    {
        private LMM02500ViewModel _viewModel = new LMM02500ViewModel();
        private R_Grid<LMM02500DTO> _gridRef;
        private R_Conductor _conductorRef;

        #region Private Property
        private bool _isDataExist { get; set; } = false;
        private bool _pageContractorOnCRUDmode { get; set; } = false;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetPropertyList();

                if (_viewModel.PropertyList.Count > 0)
                {
                    var loParam = _viewModel.PropertyList.FirstOrDefault();
                    _viewModel.PropertyId = loParam.CPROPERTY_ID;
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
                _viewModel.PropertyId = poParam;

                await _gridRef.R_RefreshGrid(null);

                if (_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    var loData = (LMM02500DTO)_conductorRef.R_GetCurrentData();
                    loData.CPROPERTY_ID = _viewModel.PropertyId;
                    if (_tabStripRef.ActiveTab.Id == "Profile")
                    {
                        await _tabProfileRef.InvokeRefreshTabPageAsync(loData);
                    }
                    else if (_tabStripRef.ActiveTab.Id == "TenantList")
                    {
                        await _tabTenantRef.InvokeRefreshTabPageAsync(loData);
                    }
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Tenant Group

        private async Task TenantGrp_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetTenantGrpList();

                eventArgs.ListEntityResult = _viewModel.TenantGrpGrid;
                _isDataExist = _viewModel.TenantGrpGrid.Count > 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void TenantGrp_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }


        #endregion

        #region Master Tab
        private R_TabStrip _tabStripRef;
        private R_TabPage _tabProfileRef;
        private R_TabPage _tabTenantRef;
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "List")
            {
                await _gridRef.R_RefreshGrid(null);
            }
        }
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageContractorOnCRUDmode;
        }

        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageContractorOnCRUDmode = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private void General_Before_Open_Profile_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loData = (LMM02500DTO)_conductorRef.R_GetCurrentData();
            loData.CPROPERTY_ID = _viewModel.PropertyId;
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(LMM02510);
        }
        private void General_Before_Open_Tenant_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loData = (LMM02500DTO)_conductorRef.R_GetCurrentData();
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(LMM02520);
        }
        #endregion
    }
}
