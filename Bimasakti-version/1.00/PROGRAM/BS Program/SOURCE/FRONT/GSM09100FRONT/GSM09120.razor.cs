using BlazorClientHelper;
using GSM09100COMMON;
using GSM09100FrontResources;
using GSM09100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Xml.Linq;

namespace GSM09100FRONT
{
    public partial class GSM09120 : R_Page
    {
        private GSM09110ViewModel _viewModel = new GSM09110ViewModel();
        private GSM09120ViewModel _viewModel_MoveProduct = new GSM09120ViewModel();
        private R_Grid<GSM09110DTO> _gridRef = new();

        [Inject] IClientHelper clientHelper { get; set; }

        private R_TextBox ToCategoty_TextBox;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM09110DTO)poParameter;

                //Set Data
                _viewModel_MoveProduct.MoveProduct = R_FrontUtility.ConvertObjectToObject<GSM09120DTO>(loData);
                _viewModel_MoveProduct.MoveProduct.CFROM_CATEGORY = loData.CCATEGORY_ID;
                _viewModel_MoveProduct.MoveProduct.CFROM_CATEGORY_NAME = loData.CCATEGORY_NAME;
                await _gridRef.R_RefreshGrid(loData);

                await ToCategoty_TextBox.FocusAsync();
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

        private async Task Catogory_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel_MoveProduct.MoveProduct.CTO_CATEGORY.Length > 0)
                {
                    var param = new GSL01800DTOParameter
                    {
                        CPROPERTY_ID = _viewModel_MoveProduct.MoveProduct.CPROPERTY_ID,
                        CCATEGORY_TYPE = ContextConstant.VAR_CATEGORY_TYPE,
                        CSEARCH_TEXT = _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY
                    };

                    LookupGSL01800ViewModel loLookupViewModel = new LookupGSL01800ViewModel();

                    var loResult = await loLookupViewModel.GetCategory(param);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY_NAME = "";
                        goto EndBlock;
                    }
                    _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY = loResult.CCATEGORY_ID;
                    _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY_NAME = loResult.CCATEGORY_NAME;
                }
                else
                {
                    _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Product_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01800DTOParameter()
            {
                CPROPERTY_ID = _viewModel_MoveProduct.MoveProduct.CPROPERTY_ID,
                CCATEGORY_TYPE = ContextConstant.VAR_CATEGORY_TYPE
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void Product_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY = loTempResult.CCATEGORY_ID;
            _viewModel_MoveProduct.MoveProduct.CTO_CATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            List<string> loProductCategoryIDList = null;

            try
            {
                loProductCategoryIDList = new List<string>();

                var loDetailData = _gridRef.DataSource;

                foreach (var item in loDetailData)
                {
                    if (item.LSELECTED)
                    {
                        loProductCategoryIDList.Add(item.CPRODUCT_ID);
                    }
                }

                    
                if (!string.IsNullOrWhiteSpace(_viewModel_MoveProduct.MoveProduct.CTO_CATEGORY))
                {
                    if (_viewModel_MoveProduct.MoveProduct.CTO_CATEGORY != _viewModel_MoveProduct.MoveProduct.CFROM_CATEGORY)
                    {
                        string StringListData = string.Join(",", loProductCategoryIDList);
                        await _viewModel_MoveProduct.MoveProductSelected(StringListData);
                        
                        await this.Close(true, _viewModel_MoveProduct.MoveProduct);
                    }
                    else
                    {
                        await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifDuplicateCategory"), R_eMessageBoxButtonType.OK);
                    }
                }
                else
                {
                    await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifProductCatReq"), R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, null);
        }
    }
}
