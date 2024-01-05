using LMM02500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace LMM02500MODEL
{
    public class LMM02510ViewModel : R_ViewModel<LMM02510DTO>
    {
        private LMM02510Model _LMM02510Model = new LMM02510Model();
        private LMM02500UniversalModel _LMM02500UniversalModel = new LMM02500UniversalModel();

        #region Public Property ViewModel
        public List<LMM02500UniversalDTO> TaxTypeList { get; set; } = new List<LMM02500UniversalDTO>();
        public List<LMM02500UniversalDTO> TaxCodeList { get; set; } = new List<LMM02500UniversalDTO>();
        public List<LMM02500UniversalDTO> IDTypeList { get; set; } = new List<LMM02500UniversalDTO>();
        public LMM02510DTO TenantGrp { get; set; } = new LMM02510DTO();
        public DateTime ExpiredDate { get; set; }
        public string PropertyId { get; set; } = "";
        #endregion
        public async Task GetAllUniversalListData()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500UniversalModel.GetALlUniversalDataAsync();

                TaxCodeList = loResult.TAX_CODE_LIST;
                TaxTypeList = loResult.TAX_TYPE_LIST;
                IDTypeList = loResult.ID_TYPE_LIST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantGrp(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02510Model.R_ServiceGetRecordAsync(poEntity);
                if (!string.IsNullOrWhiteSpace(loResult.CID_EXPIRED_DATE))
                {
                    ExpiredDate =  DateTime.ParseExact(loResult.CID_EXPIRED_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }

                TenantGrp = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTenantGrp(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM02510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationTenantGrp(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(poEntity.CTENANT_GROUP_ID))
                    loEx.Add("", "Tenant Group Id cannot be null.");

                if (string.IsNullOrEmpty(poEntity.CTENANT_GROUP_NAME))
                    loEx.Add("", "Tenant Group Name cannot be null.");

                if (string.IsNullOrEmpty(poEntity.CADDRESS))
                    loEx.Add("", "Address cannot be null.");

                if (string.IsNullOrEmpty(poEntity.CPIC_NAME))
                    loEx.Add("", "PIC Name cannot be null.");

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTenantGrp(LMM02510DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02510Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                TenantGrp = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
