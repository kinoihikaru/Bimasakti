using APT00300COMMON;
using APT00300FrontResources;
using APT00300MODEL;
using BlazorClientHelper;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APFRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace APT00300FRONT
{
    public partial class APT00321
    {
        private R_TextBox _productDeptRef;

        private APT00320ViewModel _viewModel = new APT00320ViewModel();

        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] R_ILocalizer<APT00300FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllInitialVar();

                var loParam = R_FrontUtility.ConvertObjectToObject<APT00311DTO>(poParameter);
                _viewModel.Ref_Id = loParam.CREF_NO;
                _viewModel.Dept_Code = loParam.CDEPT_CODE;
                if (string.IsNullOrWhiteSpace(loParam.CPARENT_ID))
                {
                    _viewModel.Parent_ID = loParam.CREC_ID;
                }
                else
                {
                    _viewModel.Parent_ID = loParam.CPARENT_ID;
                    await _conductorRef.R_GetEntity(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Invoice item
        private async Task InvoiceItem_Display(R_DisplayEventArgs eventArgs)
        {
            var loData = (APT00311DTO)eventArgs.Data;
            
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _productDeptRef.FocusAsync();
            }
            else if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                if (_viewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Product");
                }
                else if (_viewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Expen");
                }
            }

        }

        private async Task InvoiceItem_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetPurchaseDebitDT((APT00311DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.PurchaseDebitDT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceItem_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.DeletePurchaseDebitDT((APT00311DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceItem_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00311DTO loData = (APT00311DTO)eventArgs.Data;
                await _viewModel.InvoiceItemValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task InvoiceItem_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await _productDeptRef.FocusAsync();
            APT00311DTO loData = (APT00311DTO)eventArgs.Data;
            loData.CDISC_TYPE = _viewModel.DiscountTypeOptions.FirstOrDefault().Key;
        }

        private async Task InvoiceItem_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.SavePurchaseDebitDT((APT00311DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.PurchaseDebitDT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Lookup
        #region Product Dept
        private void ProductDeptID_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupProductDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            //    if (string.IsNullOrWhiteSpace(_viewModel.loTabParam.Data.CPROPERTY_ID))
            //    {
            //        return;
            //    }
            //    GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            //    {
            //        CCOMPANY_ID = "",
            //        CPROPERTY_ID = _viewModel.loTabParam.Data.CPROPERTY_ID,
            //        CUSER_LOGIN_ID = ""
            //    };
            //    eventArgs.Parameter = loParam;
            //eventArgs.TargetPageType = typeof(GSL00710);
        }
        private void R_After_Open_LookupProductDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            //GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            //if (loTempResult == null)
            //{
            //    return;
            //}
            //_viewModel.Data.CPROD_DEPT_CODE = loTempResult.CDEPT_CODE;
            //_viewModel.Data.CPROD_DEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion

        #region Allocation
        private void AllocationID_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupAllocation(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcMode = "";

            try
            {
                //if (string.IsNullOrWhiteSpace(_viewModel.loTabParam.Data.CPROPERTY_ID))
                //{
                //    return;
                //}
                //if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                //{
                //    lcMode = "1";
                //}
                //else
                //{
                //    lcMode = "0";
                //}
                //APL00400ParameterDTO loParam = new APL00400ParameterDTO()
                //{
                //    CCOMPANY_ID = "",
                //    CPROPERTY_ID = _viewModel.loTabParam.Data.CPROPERTY_ID,
                //    CACTIVE_TYPE = lcMode,
                //    CLANGUAGE_ID = ""
                //};
                //eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(APL00400);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_After_Open_LookupAllocation(R_AfterOpenLookupEventArgs eventArgs)
        {
            //APL00400DTO loTempResult = (APL00400DTO)eventArgs.Result;
            //if (loTempResult == null)
            //{
            //    return;
            //}
            //_viewModel.Data.CALLOC_ID = loTempResult.CALLOC_ID;
            //_viewModel.Data.CALLOC_NAME = loTempResult.CALLOC_NAME;
        }
        #endregion

        #region ProductExpenditure
        private void ProductExpenditureID_OnLostFocus(object poParam)
        {

        }
        private void R_Before_Open_LookupProductExpenditure(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcTaxableFlag = "";
            string lcMode = "";
            try
            {
                //if (_viewModel.loTabParam.Data.LTAXABLE)
                //{
                //    lcTaxableFlag = "1";
                //}
                //else
                //{
                //    lcTaxableFlag = "2";
                //}
                //if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                //{
                //    lcMode = "1";
                //}
                //else
                //{
                //    lcMode = "0";
                //}
                //if (_viewModel.Data.CPROD_TYPE == "P")
                //{
                //    APL00300ParameterDTO loParam1 = new APL00300ParameterDTO()
                //    {
                //        CCOMPANY_ID = "",
                //        CPROPERTY_ID = _viewModel.loTabParam.Data.CPROPERTY_ID,
                //        CTAXABLE_TYPE = lcTaxableFlag,
                //        CACTIVE_TYPE = lcMode,
                //        CLANGUAGE_ID = "",
                //        CTAX_DATE = _viewModel.loTabParam.Data.CREF_DATE
                //    };
                //    eventArgs.Parameter = loParam1;
                //    eventArgs.TargetPageType = typeof(APL00300);
                //}
                //else if (_viewModel.Data.CPROD_TYPE == "E")
                //{
                //    APL00200ParameterDTO loParam2 = new APL00200ParameterDTO()
                //    {
                //        CCOMPANY_ID = "",
                //        CPROPERTY_ID = _viewModel.loTabParam.Data.CPROPERTY_ID,
                //        CTAXABLE_TYPE = lcTaxableFlag,
                //        CACTIVE_TYPE = lcMode,
                //        CLANGUAGE_ID = "",
                //        CTAX_DATE = _viewModel.loTabParam.Data.CREF_DATE
                //    };
                //    eventArgs.Parameter = loParam2;
                //    eventArgs.TargetPageType = typeof(APL00200);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void R_After_Open_LookupProductExpenditure(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            //List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                //if (_viewModel.Data.CPROD_TYPE == "P")
                //{
                //    APL00300DTO loTempResult1 = (APL00300DTO)eventArgs.Result;
                //    if (loTempResult1 == null)
                //    {
                //        return;
                //    }
                //    _viewModel.Data.CPRODUCT_ID = loTempResult1.CPRODUCT_ID;
                //    _viewModel.Data.CPRODUCT_NAME = loTempResult1.CPRODUCT_NAME;
                //    _viewModel.Data.CSUP_PRODUCT_ID = loTempResult1.CALIAS_ID;
                //    _viewModel.Data.CSUP_PRODUCT_NAME = loTempResult1.CALIAS_NAME;
                //    _viewModel.Data.COTHER_TAX_ID = loTempResult1.COTHER_TAX_ID;
                //    _viewModel.Data.COTHER_TAX_NAME = loTempResult1.COTHER_TAX_NAME;
                //    _viewModel.Data.NOTHER_TAX_PCT = loTempResult1.NOTHER_TAX_PCT;
                //    _viewModel.Data.CUNIT1 = loTempResult1.CUNIT1;
                //    _viewModel.Data.CUNIT2 = loTempResult1.CUNIT2;
                //    _viewModel.Data.CUNIT3 = loTempResult1.CUNIT3;
                //}
                //else if (_viewModel.Data.CPROD_TYPE == "E")
                //{
                //    APL00200DTO loTempResult2 = (APL00200DTO)eventArgs.Result;
                //    if (loTempResult2 == null)
                //    {
                //        return;
                //    }
                //    _viewModel.Data.CPRODUCT_ID = loTempResult2.CEXPENDITURE_ID;
                //    _viewModel.Data.CPRODUCT_NAME = loTempResult2.CEXPENDITURE_NAME;
                //    _viewModel.Data.COTHER_TAX_ID = loTempResult2.COTHER_TAX_ID;
                //    _viewModel.Data.COTHER_TAX_NAME = loTempResult2.COTHER_TAX_NAME;
                //    _viewModel.Data.NOTHER_TAX_PCT = loTempResult2.NOTHER_TAX_PCT;
                //    _viewModel.Data.CUNIT1 = loTempResult2.CUNIT;
                //    _viewModel.Data.CUNIT2 = "";
                //    _viewModel.Data.CUNIT3 = "";
                //}

                //if (!string.IsNullOrWhiteSpace(_viewModel.Data.CUNIT1))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 1,
                //        CDESCRIPTION = _viewModel.Data.CUNIT1
                //    });
                //}

                //if (!string.IsNullOrWhiteSpace(_viewModel.Data.CUNIT2))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 2,
                //        CDESCRIPTION = _viewModel.Data.CUNIT2
                //    });
                //}

                //if (!string.IsNullOrWhiteSpace(_viewModel.Data.CUNIT3))
                //{
                //    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //    {
                //        ICODE = 3,
                //        CDESCRIPTION = _viewModel.Data.CUNIT3
                //    });
                //}

                //loPurchaseUnitList.Add(new PurchaseUnitDTO()
                //{
                //    ICODE = 4,
                //    CDESCRIPTION = "Other"
                //});

                //loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #endregion

        #region Refresh Value
        private void RefreshItemValue()
        {
            //_viewModel.Data.NAMOUNT = _viewModel.Data.NTRANS_QTY * _viewModel.Data.NUNIT_PRICE;
            //if (_viewModel.Data.CDISC_TYPE == "P")
            //{
            //    _viewModel.Data.NDISC_AMOUNT = _viewModel.Data.NDISC_PCT * _viewModel.Data.NAMOUNT * 0.01m;
            //}
            //_viewModel.Data.NTAXABLE_AMOUNT = _viewModel.Data.NAMOUNT - _viewModel.Data.NDISC_AMOUNT - _viewModel.Data.NDIST_DISCOUNT + _viewModel.Data.NDIST_ADD_ON;
            //_viewModel.Data.NTAX_AMOUNT = _viewModel.Data.NTAXABLE_AMOUNT * _viewModel.Data.NTAX_PCT * 0.01m;
            //_viewModel.Data.NOTHER_TAX_AMOUNT = _viewModel.Data.NTAXABLE_AMOUNT * _viewModel.Data.NOTHER_TAX_PCT * 0.01m;
            //_viewModel.Data.NTOTAL_AMOUNT = _viewModel.Data.NTAXABLE_AMOUNT + _viewModel.Data.NTAX_AMOUNT + _viewModel.Data.NOTHER_TAX_AMOUNT;
        }
        #endregion

        #region OnChange

        private string lcExpProdLabel = "Product/Expenditure";
        private void ProductTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.CPROD_TYPE = poParam;
                _viewModel.Data.CPRODUCT_ID = "";
                _viewModel.Data.CPRODUCT_NAME = "";
                if (_viewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Product");
                }
                else if (_viewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Expen");
                    _viewModel.Data.CALLOC_ID = "";
                    _viewModel.Data.CALLOC_NAME = "";
                }
                //_viewModel.Data.CBILL_UNIT = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DiscountOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.CDISC_TYPE = poParam;
                if (_viewModel.Data.CDISC_TYPE == "P")
                {
                    _viewModel.Data.NDISC_AMOUNT = 0;
                }
                else if (_viewModel.Data.CDISC_TYPE == "V")
                {
                    _viewModel.Data.NDISC_PCT = 0;
                }
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        
        private void PurchaseQtyTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.NTRANS_QTY = poParam;
                _viewModel.Data.NAPPV_QTY = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void UnitPriceTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.NUNIT_PRICE = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DiscountPercentageTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.NDISC_PCT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void DiscountAmountTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.NDISC_AMOUNT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void PurchaseUnitComboBox_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.Data.IBILL_UNIT = poParam;
                if (_viewModel.Data.IBILL_UNIT == 4)
                {
                }
                else
                {
                    _viewModel.Data.CBILL_UNIT = "";
                    _viewModel.Data.NSUPP_CONV_FACTOR = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
