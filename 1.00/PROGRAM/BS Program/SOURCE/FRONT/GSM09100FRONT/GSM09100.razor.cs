using BlazorClientHelper;
using GSM09100COMMON;
using GSM09100FrontResources;
using GSM09100MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Xml.Linq;

namespace GSM09100FRONT
{
    public partial class GSM09100 : R_Page
    {
        private GSM09100ViewModel _viewModel = new GSM09100ViewModel();
        private R_TreeView<GSM09100DTO> _treeRef = new();
        private R_Conductor _conductorRef;


        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await PropertyDropdown_ServiceGetListRecord(null);
                if (_viewModel.PropertyList.Count > 0)
                {
                    GSM09100PropertyDTO loParam = _viewModel.PropertyList.FirstOrDefault();
                    _viewModel.PropertyValueContext = loParam.CPROPERTY_ID;
                    await PropertyDropdown_OnChange(loParam.CPROPERTY_ID);
                    await _viewModel.GetInitialDTO();
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
                await _viewModel.GetPropertyList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TabStrip _TabParent;
        private async Task PropertyDropdown_OnChange(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.PropertyValueContext = poParam; 

                await _treeRef.R_RefreshTree(null);
                await _viewModel.GetProductCategoryComboBoxList();
                await _viewModel.GetInitialDTO();

                if (_conductorRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    if (_TabParent.ActiveTab.Id == "Product")
                    {
                        var loData = (GSM09100DTO)_conductorRef.R_GetCurrentData();
                        await _tabPageList.InvokeRefreshTabPageAsync(loData);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Product Category
        private async Task ProductCategory_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProductCategoryList();

                eventArgs.ListEntityResult = _viewModel.ProductCategoryGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void ProductCategory_RefreshTreeViewState(R_RefreshTreeViewStateEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTreeList = (List<GSM09100DTO>)eventArgs.TreeViewList;

                loTreeList.ForEach(x => x.LHAS_CHILD = loTreeList.Where(y => y.CPARENT == x.CCATEGORY_ID).Count() > 0 ? true :
                loTreeList.Where(y => y.CPARENT == x.CCATEGORY_ID).Count() > 0);

                eventArgs.ExpandedList = loTreeList.Where(x => x.ILEVEL < 3).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ProductCategory_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProductCategory((GSM09100DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.ProductCategory;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private R_TextBox CatName_textBox;
        private async Task ProductCategory_R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM09100DTO)eventArgs.Data;
                EnableDelete = loData.ILEVEL != 0 && eventArgs.ConductorMode == R_eConductorMode.Normal;
                await CatName_textBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void ProductCategory_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            var loData = (GSM09100DTO)_conductorRef.R_GetCurrentData();
            eventArgs.Allow = loData.ILEVEL < 3;
        }
        private void ProductCategory_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loData = (GSM09100DTO)_conductorRef.R_GetCurrentData();
            eventArgs.Allow = loData.ILEVEL != 0;
        }
        private bool EnableDelete = true;
        private R_TextBox CatId_textBox;
        private async Task ProductCategory_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loCurrentSelectDataList = (GSM09100DTO)_treeRef.CurrentSelectedData;
                var loData = (GSM09100DTO)eventArgs.Data;

                loData.CPARENT = loCurrentSelectDataList.CCATEGORY_ID;
                loData.CPARENT_NAME = loCurrentSelectDataList.CCATEGORY_NAME;

                loData.ILEVEL = loCurrentSelectDataList.ILEVEL + 1;

                loData.DCREATE_DATE = DateTime.Now;
                loData.DUPDATE_DATE = DateTime.Now;

                await CatId_textBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
            
        }
        private async Task ProductCategory_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.ValidationProductCategory((GSM09100DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ProductCategory_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifCancelSave"), R_eMessageBoxButtonType.YesNo);

                eventArgs.Cancel = loValidate != R_eMessageBoxResult.Yes;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ProductCategory_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveProductCategory((GSM09100DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.ProductCategory;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ProductCategory_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProductCategoryComboBoxList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ProductCategory_AfterDelete()
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProductCategoryComboBoxList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ProductCategory_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteProductCategory((GSM09100DTO)eventArgs.Data);
                await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifCategoryDelete"), R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ProductCategory_SetOther(R_SetEventArgs eventArgs)
        {
            _comboboxEnabled = eventArgs.Enable;
            _pageSupplierOnCRUDmode = !eventArgs.Enable;
        }
        private bool _EnablehasData = true;
        private void ProductCategory_SetHasData(R_SetEventArgs eventArgs)
        {
            _EnablehasData = eventArgs.Enable;
        }

        private void ProductCategory_ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loConductorData = (GSM09100DTO)eventArgs.Data;
            loConductorData.CCATEGORY_ID_NAME_DISPLAY = string.Format("[{0}] {1} - {2}", loConductorData.ILEVEL, loConductorData.CCATEGORY_ID, loConductorData.CCATEGORY_NAME);
            eventArgs.GridData = loConductorData;
        }
        #endregion

        private R_TabPage _tabPageList;
        private void _General_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM09100DTO)_conductorRef.R_GetCurrentData();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSM09110);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
            
        }

        private bool _comboboxEnabled = true;
        private bool _pageSupplierOnCRUDmode = false;
        private void R_TabEventCallback(object poValue)
        {
            _comboboxEnabled = (bool)poValue;
            _pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            eventArgs.Cancel = _pageSupplierOnCRUDmode;
        }

    }
}
