﻿using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00121;
using APT00100MODEL.ViewModel;
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
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace APT00100FRONT
{
    public partial class APT00121
    {
        private R_TextBox _productDeptRef;

        private APT00121ViewModel loViewModel = new APT00121ViewModel();

        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private bool IsAllocationEnable = false;

        private string lcExpProdLabel = "Product/Expenditure";

        private LinkedList<PurchaseUnitDTO> loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>();

        private List<SupplierOptionRadioButton> loOptionDiscountRadioButtonList = new List<SupplierOptionRadioButton>()
        {
            new SupplierOptionRadioButton() { CSUPPLIER_OPTION_CODE = "P", CSUPPLIER_OPTION_NAME = "%" },
            new SupplierOptionRadioButton() { CSUPPLIER_OPTION_CODE = "V", CSUPPLIER_OPTION_NAME = "Amount"}
        };

        private bool IsPurchaseUnitOther = false;

        private bool IsDiscountPercentEnable = false;

        private bool IsDiscountAmountEnable = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (TabItemEntryParameterDTO)poParameter;
                if (loViewModel.loTabParam != null)
                {
                    if (loViewModel.loTabParam.Data != null)
                    {
                        loViewModel.loHeader = loViewModel.loTabParam.Data;
                    }
                    await loViewModel.GetProductTypeListStreamAsync();
                    if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.CREC_ID))
                    {
                        await _conductorRef.R_GetEntity(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task InvoiceItem_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _productDeptRef.FocusAsync();
                if (loViewModel.Data.CDISC_TYPE == "P")
                {
                    IsDiscountPercentEnable = true;
                }
                else if (loViewModel.Data.CDISC_TYPE == "V")
                {
                    IsDiscountAmountEnable = true;
                }
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                    IsAllocationEnable = true;
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                    IsAllocationEnable = false;
                }
            }
        }

        private async Task InvoiceItem_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                await loViewModel.GetInfoAsync();
                if (loViewModel.loInvoiceItem.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                }
                else if (loViewModel.loInvoiceItem.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loInvoiceItem.CUNIT1))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 1,
                        CDESCRIPTION = loViewModel.loInvoiceItem.CUNIT1
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loInvoiceItem.CUNIT2))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 2,
                        CDESCRIPTION = loViewModel.loInvoiceItem.CUNIT2
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.loInvoiceItem.CUNIT3))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 3,
                        CDESCRIPTION = loViewModel.loInvoiceItem.CUNIT3
                    });
                }

                loPurchaseUnitList.Add(new PurchaseUnitDTO()
                {
                    ICODE = 4,
                    CDESCRIPTION = "Other"
                });

                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);
                eventArgs.Result = loViewModel.loInvoiceItem;
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
                await loViewModel.DeleteInvoiceItemAsync((APT00121DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void InvoiceItem_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00121DTO loData = (APT00121DTO)eventArgs.Data;
                loViewModel.InvoiceItemValidation(loData);
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
            APT00121DTO loData = (APT00121DTO)eventArgs.Data;
            loData.CTAX_ID = loViewModel.loHeader.CTAX_ID;
            loData.CTAX_NAME = loViewModel.loHeader.CTAX_NAME;
            loData.NTAX_PCT = loViewModel.loHeader.NTAX_PCT;
            loData.CDISC_TYPE = loOptionDiscountRadioButtonList.FirstOrDefault().CSUPPLIER_OPTION_CODE;
            IsDiscountPercentEnable = true;
        }

        private void InvoiceItem_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private void InvoiceItem_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsAllocationEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private void InvoiceItem_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            IsAllocationEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private async Task InvoiceItem_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveInvoiceItemAsync((APT00121DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loInvoiceItem;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Lookup
        private void R_Before_Open_LookupProductDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CPROPERTY_ID))
            {
                return;
            }
            GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void R_After_Open_LookupProductDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00710DTO loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CPROD_DEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CPROD_DEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private void R_Before_Open_LookupAllocation(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcMode = "";

            try
            {
                if (string.IsNullOrWhiteSpace(loViewModel.loTabParam.Data.CPROPERTY_ID))
                {
                    return;
                }
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    lcMode = "1";
                }
                else
                {
                    lcMode = "0";
                }
                APL00400ParameterDTO loParam = new APL00400ParameterDTO()
                {
                    CCOMPANY_ID = "",
                    CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                    CACTIVE_TYPE = lcMode,
                    CLANGUAGE_ID = ""
                };
                eventArgs.Parameter = loParam;
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
            APL00400DTO loTempResult = (APL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loViewModel.Data.CALLOC_ID = loTempResult.CALLOC_ID;
            loViewModel.Data.CALLOC_NAME = loTempResult.CALLOC_NAME;
        }

        private void R_Before_Open_LookupProductExpenditure(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcTaxableFlag = "";
            string lcMode = "";
            try
            {
                if (loViewModel.loTabParam.Data.LTAXABLE)
                {
                    lcTaxableFlag = "1";
                }
                else
                {
                    lcTaxableFlag = "2";
                }
                if (_conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    lcMode = "1";
                }
                else
                {
                    lcMode = "0";
                }
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    APL00300ParameterDTO loParam1 = new APL00300ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                        CTAXABLE_TYPE = lcTaxableFlag,
                        CACTIVE_TYPE = lcMode,
                        CLANGUAGE_ID = "",
                        CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE
                    };
                    eventArgs.Parameter = loParam1;
                    eventArgs.TargetPageType = typeof(APL00300);
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    APL00200ParameterDTO loParam2 = new APL00200ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = loViewModel.loTabParam.Data.CPROPERTY_ID,
                        CTAXABLE_TYPE = lcTaxableFlag,
                        CACTIVE_TYPE = lcMode,
                        CLANGUAGE_ID = "",
                        CTAX_DATE = loViewModel.loTabParam.Data.CREF_DATE
                    };
                    eventArgs.Parameter = loParam2;
                    eventArgs.TargetPageType = typeof(APL00200);
                }
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
            List<PurchaseUnitDTO> loPurchaseUnitList = new List<PurchaseUnitDTO>();

            try
            {
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    APL00300DTO loTempResult1 = (APL00300DTO)eventArgs.Result;
                    if (loTempResult1 == null)
                    {
                        return;
                    }
                    loViewModel.Data.CPRODUCT_ID = loTempResult1.CPRODUCT_ID;
                    loViewModel.Data.CPRODUCT_NAME = loTempResult1.CPRODUCT_NAME;
                    loViewModel.Data.CSUP_PRODUCT_ID = loTempResult1.CALIAS_ID;
                    loViewModel.Data.CSUP_PRODUCT_NAME = loTempResult1.CALIAS_NAME;
                    loViewModel.Data.COTHER_TAX_ID = loTempResult1.COTHER_TAX_ID;
                    loViewModel.Data.COTHER_TAX_NAME = loTempResult1.COTHER_TAX_NAME;
                    loViewModel.Data.NOTHER_TAX_PCT = loTempResult1.NOTHER_TAX_PCT;
                    loViewModel.Data.CUNIT1 = loTempResult1.CUNIT1;
                    loViewModel.Data.CUNIT2 = loTempResult1.CUNIT2;
                    loViewModel.Data.CUNIT3 = loTempResult1.CUNIT3;
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    APL00200DTO loTempResult2 = (APL00200DTO)eventArgs.Result;
                    if (loTempResult2 == null)
                    {
                        return;
                    }
                    loViewModel.Data.CPRODUCT_ID = loTempResult2.CEXPENDITURE_ID;
                    loViewModel.Data.CPRODUCT_NAME = loTempResult2.CEXPENDITURE_NAME;
                    loViewModel.Data.COTHER_TAX_ID = loTempResult2.COTHER_TAX_ID;
                    loViewModel.Data.COTHER_TAX_NAME = loTempResult2.COTHER_TAX_NAME;
                    loViewModel.Data.NOTHER_TAX_PCT = loTempResult2.NOTHER_TAX_PCT;
                    loViewModel.Data.CUNIT1 = loTempResult2.CUNIT;
                    loViewModel.Data.CUNIT2 = "";
                    loViewModel.Data.CUNIT3 = "";
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT1))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 1,
                        CDESCRIPTION = loViewModel.Data.CUNIT1
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT2))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 2,
                        CDESCRIPTION = loViewModel.Data.CUNIT2
                    });
                }

                if (!string.IsNullOrWhiteSpace(loViewModel.Data.CUNIT3))
                {
                    loPurchaseUnitList.Add(new PurchaseUnitDTO()
                    {
                        ICODE = 3,
                        CDESCRIPTION = loViewModel.Data.CUNIT3
                    });
                }

                loPurchaseUnitList.Add(new PurchaseUnitDTO()
                {
                    ICODE = 4,
                    CDESCRIPTION = "Other"
                });

                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>(loPurchaseUnitList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion


        #region OnChange

        private async Task ProductTypeComboBox_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CPROD_TYPE = poParam;
                loViewModel.Data.CPRODUCT_ID = "";
                loViewModel.Data.CPRODUCT_NAME = "";
                if (loViewModel.Data.CPROD_TYPE == "P")
                {
                    lcExpProdLabel = "Product";
                    IsAllocationEnable = true;
                }
                else if (loViewModel.Data.CPROD_TYPE == "E")
                {
                    lcExpProdLabel = "Expenditure";
                    loViewModel.Data.CALLOC_ID = "";
                    loViewModel.Data.CALLOC_NAME = "";
                    IsAllocationEnable = false;
                }
                loViewModel.Data.CBILL_UNIT = "";
                loPurchaseUnitComboboxLinkedList = new LinkedList<PurchaseUnitDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DiscountOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CDISC_TYPE = poParam;
                if (loViewModel.Data.CDISC_TYPE == "P")
                {
                    loViewModel.Data.NDISC_AMOUNT = 0;
                    IsDiscountPercentEnable = true;
                    IsDiscountAmountEnable = false;
                }
                else if (loViewModel.Data.CDISC_TYPE == "V")
                {
                    loViewModel.Data.NDISC_PCT = 0;
                    IsDiscountPercentEnable = false;
                    IsDiscountAmountEnable = true;
                }
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void RefreshItemValue()
        {
            loViewModel.Data.NAMOUNT = loViewModel.Data.NTRANS_QTY * loViewModel.Data.NUNIT_PRICE;
            if (loViewModel.Data.CDISC_TYPE == "P")
            {
                loViewModel.Data.NDISC_AMOUNT = loViewModel.Data.NDISC_PCT * loViewModel.Data.NAMOUNT * 0.01m;
            }
            loViewModel.Data.NTAXABLE_AMOUNT = loViewModel.Data.NAMOUNT - loViewModel.Data.NDISC_AMOUNT - loViewModel.Data.NDIST_DISCOUNT + loViewModel.Data.NDIST_ADD_ON;
            loViewModel.Data.NTAX_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT * loViewModel.Data.NTAX_PCT * 0.01m;
            loViewModel.Data.NOTHER_TAX_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT * loViewModel.Data.NOTHER_TAX_PCT * 0.01m;
            loViewModel.Data.NTOTAL_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT + loViewModel.Data.NTAX_AMOUNT + loViewModel.Data.NOTHER_TAX_AMOUNT;
        }

        private async Task PurchaseQtyTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NTRANS_QTY = poParam;
                loViewModel.Data.NAPPV_QTY = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitPriceTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NUNIT_PRICE = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task DiscountPercentageTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NDISC_PCT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task DiscountAmountTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NDISC_AMOUNT = poParam;
                RefreshItemValue();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PurchaseUnitComboBox_ValueChanged(int poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.IBILL_UNIT = poParam;
                if (loViewModel.Data.IBILL_UNIT == 4)
                {
                    IsPurchaseUnitOther = true;
                }
                else
                {
                    loViewModel.Data.CBILL_UNIT = "";
                    loViewModel.Data.NSUPP_CONV_FACTOR = 0;
                    IsPurchaseUnitOther = false;
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
