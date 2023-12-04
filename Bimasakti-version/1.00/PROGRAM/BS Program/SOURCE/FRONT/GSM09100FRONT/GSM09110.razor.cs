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

namespace GSM09100FRONT
{
    public partial class GSM09110 : R_Page, R_ITabPage
    {
        private GSM09110ViewModel _viewModel = new GSM09110ViewModel();
        private R_Grid<GSM09110DTO> _gridRef = new();
        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GSM09110DTO>(poParameter);
                //Set Data
                _viewModel.ProductCategoryName = loData.CCATEGORY_NAME;
                _viewModel.ProductCategoryId = loData.CCATEGORY_ID;
                _viewModel.ParamPopupMoveProduct = loData;

                await _gridRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Product_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetProductList((GSM09110DTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _viewModel.ProductGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private void Product_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
      
        private void Product_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.ParamPopupMoveProduct;
            eventArgs.TargetPageType = typeof(GSM09120);
        }

        private async Task Product_After_Open_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result is null)
                {
                    return;
                }
                var loData = R_FrontUtility.ConvertObjectToObject<GSM09110DTO>(eventArgs.Result);
                await _gridRef.R_RefreshGrid(loData);
                await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifProductMoveSuccess"), R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GSM09110DTO>(poParam);

                //Set Data
                _viewModel.ProductCategoryName = loData.CCATEGORY_NAME;
                _viewModel.ProductCategoryId = loData.CCATEGORY_ID;
                _viewModel.ParamPopupMoveProduct = loData;

                await _gridRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
