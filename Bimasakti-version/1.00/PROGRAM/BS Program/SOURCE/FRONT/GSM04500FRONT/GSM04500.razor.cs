using BlazorClientHelper;
using GSM04500COMMON;
using GSM04500MODEL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500FRONT
{
    public partial class GSM04500 : R_Page
    {
        private GSM04500ViewModel _viewModel = new();
        private R_ConductorGrid _conRef;
        private R_Grid<GSM04500DTO> _gridRef;

        private R_TabStrip _tabStrip;
        private R_TabPage _tabPageAccountSetting;
        
        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        private bool _comboboxEnabled = true;
        private bool _pageSupplierOnCRUDmode = true;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
      
        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.PropertyID = poParam;
                await _gridRef.R_RefreshGrid(null);

                if (_conRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_tabStrip.ActiveTab.Id == "Tab_AccountSetting")
                    {
                        var loData = _gridRef.CurrentSelectedData;
                        if (_gridRef.DataSource.Count > 0)
                        {
                            await _tabPageAccountSetting.InvokeRefreshTabPageAsync(loData);
                        }
                        else
                        {
                            await _tabPageAccountSetting.InvokeRefreshTabPageAsync(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task JournalGroupDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel.JournalGroupTypeCode = poParam;
                await _gridRef.R_RefreshGrid(null);

                if (_conRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_tabStrip.ActiveTab.Id == "Tab_AccountSetting")
                    {
                        var loData = _gridRef.CurrentSelectedData;
                        if (_gridRef.DataSource.Count > 0)
                        {
                            await _tabPageAccountSetting.InvokeRefreshTabPageAsync(loData);
                        }
                        else
                        {
                            await _tabPageAccountSetting.InvokeRefreshTabPageAsync(null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region Journal Group
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetJournalGroupList();
                eventArgs.ListEntityResult = _viewModel.JournalGroupGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetJournalGroup((GSM04500DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.JournalGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.DeleteJournalGroup((GSM04500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        public void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (GSM04500DTO)eventArgs.Data;
            loData.CJRNGRP_TYPE = _viewModel.JournalGroupTypeCode;
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                await _viewModel.SaveJournalGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.JournalGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.ValidationFieldEmpty((GSM04500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            _comboboxEnabled = eventArgs.Enable;
            _pageSupplierOnCRUDmode = eventArgs.Enable;
        }
        #endregion

        #region CHANGE TAB
        //CHANGE TAB
        private void Before_Open_AccountSetting(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loData = _gridRef.CurrentSelectedData;

            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(GSM04510);
        }
        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = !_pageSupplierOnCRUDmode;
        }
        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = (bool)poValue;
        }

        #endregion

        #region Template
        private async Task _Staff_TemplateBtn_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                string loCompanyName = clientHelper.CompanyId.ToUpper();
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _viewModel.DownloadTemplate();

                    var saveFileName = $"Journal Group - {loCompanyName}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Upload

        private void Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            GSM04500PropetyDTO loProperty = (_viewModel.PropertyList).Find(p => p.CPROPERTY_ID == _viewModel.PropertyID);
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500ParameterUploadDTO>(loProperty);
            loParam.CJRNGRP_TYPE = _viewModel.JournalGroupTypeCode;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSM04501);
        }

        private async Task After_Open_Upload(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                if ((bool)eventArgs.Result == true)
                {
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion
    }
}
