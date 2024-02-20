using APM00300COMMON;
using APM00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APM00300MODEL
{
    public class APM00310ViewModel : R_ViewModel<APM00310DTO>
    {
        private APM00300Model _APM00300Model = new APM00300Model();
        private APM00310Model _APM00310Model = new APM00310Model();

        public List<APM00300PropertyDTO> PropertyList { get; set; } = new List<APM00300PropertyDTO>();
        public List<APM00300LOBDTO> LOBList { get; set; } = new List<APM00300LOBDTO>();
        public BindingList<APM00300PayTermDTO> PayTermList { get; set; } = new BindingList<APM00300PayTermDTO>();
        public List<APM00300CurrencyDTO> CurrencyList { get; set; } = new List<APM00300CurrencyDTO>();
        public List<APM00300TaxTypeDTO> TaxTypeList { get; set; } = new List<APM00300TaxTypeDTO>();
        public APM00310DTO Supplier { get; set; } = new APM00310DTO();

        public string PropertyValueContext { get; set; } = "";

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00300Model.GetGSPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetLOBList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00300Model.GetLOBListAsync();

                LOBList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPayTermList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00310Model.GetPayTermListAsync(PropertyValueContext);

                PayTermList = new BindingList<APM00300PayTermDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetCurrencyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00310Model.GetCurrencyListAsync();

                CurrencyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTaxTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00310Model.GetTaxTypeListAsync();

                TaxTypeList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSupplier(APM00310DTO poParam)
        {
            var loEx = new R_Exception();
            APM00300LOBDTO loLOBData = null;
            try
            {
                var loResult = await _APM00310Model.R_ServiceGetRecordAsync(poParam);

                if (LOBList.Count > 0)
                {
                    loLOBData = LOBList.Where(x => x.CLOB_CODE == loResult.CLOB_CODE).FirstOrDefault();

                    loResult.CLOB_NAME = loLOBData.CLOB_NAME;
                }
                
                Supplier = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveSupplier(APM00310DTO poEntity, eCRUDMode poCRUDMODE)
        {
            var loEx = new R_Exception();
            try
            {
                poEntity.CPROPERTY_ID = PropertyValueContext;
                poEntity.CTAX_REG_DATE = poEntity.CTAX_REG_DATE_DISPLAY.ToString("yyyyMMdd");

                var loResult = await _APM00310Model.R_ServiceSaveAsync(poEntity, poCRUDMODE);

                Supplier = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteSupplier(APM00310DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _APM00310Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // Supplier Status Radio Button
        public List<RadioModel> SupplierStatusList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "0", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Normal")},
            new RadioModel { Id = "1", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NoPurchase") },
            new RadioModel { Id = "2", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NoPayment") },
            new RadioModel { Id = "3", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NoPurchasePayment") },
        };

        // Delivery Options Radio Button
        public List<RadioModel> DeliverOptionsList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "I", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_IncludeDelivery")},
            new RadioModel { Id = "E", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ExcludeDelivery") }
        };

    }
    public class RadioModel
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
