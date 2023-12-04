using APM00300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APM00300MODEL
{
    public class APM00330ViewModel : R_ViewModel<APM00330DTO>
    {
        private APM00310Model _APM00310Model = new APM00310Model();
        private APM00330Model _APM00330Model = new APM00330Model();

        public List<APM00300CurrencyDTO> CurrencyList { get; set; } = new List<APM00300CurrencyDTO>();
        public ObservableCollection<APM00330DTO> SupplierBankGrid { get; set; } = new ObservableCollection<APM00330DTO>();

        public APM00330DTO SupplierBank { get; set; } = new APM00330DTO();

        public string SupplierRecId = "";
        public string SupplierID = "";
        public string SupplierName = "";
        public string PropertyValue = "";

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

        public async Task GetSupplierBankList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00330Model.GetSupplierBankListAsync(SupplierRecId);

                SupplierBankGrid = new ObservableCollection<APM00330DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSupplierBank(APM00330DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _APM00330Model.R_ServiceGetRecordAsync(poParam);

                SupplierBank = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveSupplierBank(APM00330DTO poEntity, eCRUDMode poCRUDMODE)
        {
            var loEx = new R_Exception();
            try
            {
                poEntity.CPROPERTY_ID = PropertyValue;
                poEntity.CSUPPLIER_REC_ID = SupplierRecId;
                poEntity.CSUPPLIER_ID = SupplierID;

                var loResult = await _APM00330Model.R_ServiceSaveAsync(poEntity, poCRUDMODE);

                SupplierBank = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteSupplierBank(APM00330DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                await _APM00330Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
