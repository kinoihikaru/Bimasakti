using APT00600COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace APT00600MODEL
{
    public class APT00620ViewModel : R_ViewModel<APT00610DTO>
    {
        private APT00610Model _APT00610Model = new APT00610Model();
        private APT00600InitModel _APT00600InitModel = new APT00600InitModel();

        #region Property Class
        public ObservableCollection<APT00610DTO> PurchaseAdjustmentAllocGrid { get; set; } = new ObservableCollection<APT00610DTO>();
        public APT00610DTO PurchaseAdjustmentAlloc { get; set; } = new APT00610DTO();
        public APT00600DTO HeaderPurchaseAdju { get; set; } = new APT00600DTO();
        public APT00600CompanyInfoGSDTO VAR_GSM_COMPANY { get; set; } = new APT00600CompanyInfoGSDTO();
        public APT00600TransCodeInfoGSDTO VAR_ALLOCATION_TRANS_CODE { get; set; } = new APT00600TransCodeInfoGSDTO();
        public List<APT00600AllocTrxTypeAPDTO> VAR_ALLOC_TRX_TYPE_LIST { get; set; } = new List<APT00600AllocTrxTypeAPDTO>();
        #endregion

        #region Property ViewModel
        public DateTime? RefDate { get; set; }
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _APT00600InitModel.GetAllInitialProcessPopupAllocAsync();

                VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                VAR_ALLOCATION_TRANS_CODE = loResult.VAR_ALLOCATION_TRANS_CODE;
                VAR_ALLOC_TRX_TYPE_LIST = loResult.VAR_ALLOC_TRX_TYPE_LIST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseAdjustmentAllocList()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<APT00610DTO>(HeaderPurchaseAdju);
                var loResult = await _APT00610Model.GetPurhcaseAdjustmentAllocStreamAsync(loData);

                PurchaseAdjustmentAllocGrid = new ObservableCollection<APT00610DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPurchaseAdjustmentAlloc(APT00610DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = HeaderPurchaseAdju.CPROPERTY_ID;
                var loResult = await _APT00610Model.R_ServiceGetRecordAsync(poEntity);

                RefDate = DateTime.ParseExact(HeaderPurchaseAdju.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                PurchaseAdjustmentAlloc = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task CalculateAllocationProses()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (APT00610DTO)R_GetCurrentData();
                loData.NLTO_AP_AMOUNT = (loData.NTO_AP_AMOUNT / loData.NLBASE_RATE) * loData.NLCURRENCY_RATE;
                loData.NBTO_AP_AMOUNT = (loData.NTO_AP_AMOUNT / loData.NBBASE_RATE) * loData.NBCURRENCY_RATE;

                loData.NLTO_TAX_AMOUNT = (loData.NTO_TAX_AMOUNT / loData.NTAX_BASE_RATE) * loData.NTAX_CURRENCY_RATE;
                loData.NBTO_TAX_AMOUNT = (loData.NTO_TAX_AMOUNT / loData.NBBASE_RATE) * loData.NBCURRENCY_RATE;

                loData.NALLOC_AMOUNT = loData.NTO_AP_AMOUNT + loData.NTO_TAX_AMOUNT;
                loData.NLALLOC_AMOUNT = loData.NLTO_AP_AMOUNT + loData.NLTO_TAX_AMOUNT;
                loData.NBALLOC_AMOUNT = loData.NBTO_AP_AMOUNT + loData.NBTO_TAX_AMOUNT;

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeletePurchaseAdjustmentAlloc(APT00610DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _APT00610Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SavePurchaseAdjustmentAlloc(APT00610DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CPROPERTY_ID = HeaderPurchaseAdju.CPROPERTY_ID;
                poEntity.CFR_REC_ID = HeaderPurchaseAdju.CREC_ID;
                poEntity.CFR_REF_NO = HeaderPurchaseAdju.CREF_NO;
                poEntity.CFR_REF_DATE = HeaderPurchaseAdju.CREF_DATE;
                poEntity.CFR_SUPPLIER_ID = HeaderPurchaseAdju.CSUPPLIER_ID;
                poEntity.CFR_SUPPLIER_SEQ_NO = HeaderPurchaseAdju.CSUPPLIER_SEQ_NO;
                poEntity.CFR_DEPT_CODE = HeaderPurchaseAdju.CDEPT_CODE;

                var loResult = await _APT00610Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                PurchaseAdjustmentAlloc = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
