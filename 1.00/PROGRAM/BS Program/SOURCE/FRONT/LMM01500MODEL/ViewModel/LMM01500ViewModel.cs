using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using System.Diagnostics.Tracing;

namespace LMM01500MODEL
{
    public class LMM01500ViewModel : R_ViewModel<LMM01500DTO>
    {
        private LMM01500Model _LMM01500Model = new LMM01500Model();

        public ObservableCollection<LMM01501DTO> InvoinceGroupGrid { get; set; } = new ObservableCollection<LMM01501DTO>();
        public List<LMM01500DTOPropety> PropertyList { get; set; } = new List<LMM01500DTOPropety>();

        public LMM01500DTO InvoiceGroup = new LMM01500DTO();

        public string PropertyValueContext = "";
        public bool StatusChange;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01500Model.GetPropertyAsync();
                PropertyList = loResult.Data;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);

                var loResult = await _LMM01500Model.GetInvoiceGrpListAsync();

                foreach (var item in loResult.Data)
                {
                    item.CINVGRP_CODE_NAME = $"{item.CINVGRP_CODE} - {item.CINVGRP_NAME}";
                }

                InvoinceGroupGrid = new ObservableCollection<LMM01501DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceGroup(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, poParam.CINVGRP_CODE);

                var loResult = await _LMM01500Model.R_ServiceGetRecordAsync(poParam);

                loResult.CSEQUENCEInt = int.Parse(loResult.CSEQUENCE);
                

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceGroup(LMM01500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, poNewEntity.CINVGRP_CODE);

                poNewEntity.CSEQUENCE = $"{poNewEntity.CSEQUENCEInt}";
                var loResult = await _LMM01500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationInvoiceGrp(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CINVGRP_CODE);
                if (lCancel)
                {
                    loEx.Add("", "Invoice Group Code is required");
                }

                lCancel = string.IsNullOrEmpty(poParam.CINVGRP_NAME);

                if (lCancel)
                {
                    loEx.Add("", "Invoice Group Name is required");

                }

                lCancel = poParam.IBEFORE_LIMIT_INVOICE_DATE < poParam.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    loEx.Add("", "Before Limit Invoice Date cannot be smaller than Limit Invoice Dates*");
                }

                lCancel = poParam.IAFTER_LIMIT_INVOICE_DATE > poParam.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    loEx.Add("", "After Limit Invoice Date cannot be smaller than Limit Invoice Dates*");
                }

                if (poParam.LUSE_STAMP)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CSTAMP_ADD_ID);

                    if (lCancel)
                    {
                        loEx.Add("", "Additional Id and Name is required");
                    }
                }

                if (!poParam.LBY_DEPARTMENT)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CINVOICE_TEMPLATE);

                    if (lCancel)
                    {
                        loEx.Add("", "Invoice Template Is Required");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);

                    if (lCancel)
                    {
                        loEx.Add("", "Departement Is Required");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CBANK_CODE);

                    if (lCancel)
                    {
                        loEx.Add("", "Bank Is Required");
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CBANK_ACCOUNT);
                    if (lCancel)
                    {
                        loEx.Add("", "Bank Account Is Required");
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoiceGroup(LMM01500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync(LMM01500DTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.LACTIVE = StatusChange;

                await _LMM01500Model.LMM01500ActiveInactiveAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public List<RadioButton> RadioInvDMList { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Tenant"},
            new RadioButton { Id = "02", Text = "Invoice Group" },
        };

        public List<RadioButton> RadioInvGrpMode { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = "Due Days"},
            new RadioButton { Id = "02", Text = "Fixed Due Date"},
            new RadioButton { Id = "03", Text = "Range Fixed Due Date"},
        };

    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class RadioButtonInt
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
