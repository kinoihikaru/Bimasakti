using APM00300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APM00300MODEL
{
    public class APM00320ViewModel : R_ViewModel<APM00320DTO>
    {
        private APM00320Model _APM00320Model = new APM00320Model();
        private APM00310Model _APM00310Model = new APM00310Model();

        public APM00320DTO Supplier { get; set; } = new APM00320DTO();
        public List<APM00300TaxTypeDTO> TaxTypeList { get; set; } = new List<APM00300TaxTypeDTO>();
        public List<APM00321DTO> SupplierSeqList { get; set; } = new List<APM00321DTO>();

        public string SupplierRecId = "";
        public string SupplierID = "";
        public string SupplierName = "";
        public string RecId = string.Empty;
        public DateTime TaxRegDate;
        public async Task GetSupplierSeqList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00320Model.GetSupplierSeqListAsync(SupplierRecId);

                SupplierSeqList = loResult;

                if (loResult.Count > 0)
                {
                    RecId = loResult.FirstOrDefault().CREC_ID;
                }
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
        public async Task GetSupplier()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = new APM00320DTO() { CREC_ID = RecId };
                var loResult = await _APM00320Model.GetSupplierInfoAsync(loData);
                TaxRegDate = DateTime.ParseExact(loResult.CTAX_REG_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                Supplier = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<APM00320DTO> SaveSupplier(APM00320DTO poEntity)
        {
            var loEx = new R_Exception();
            APM00320DTO loRtn = null;
            try
            {
                poEntity.CTAX_REG_DATE = TaxRegDate.ToString("yyyyMMdd");
                poEntity.CSUPPLIER_REC_ID = SupplierRecId;
                
                loRtn = await _APM00320Model.SaveSupplierInfoAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
