using LMM03700COMMON;
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

namespace LMM03700MODEL
{
    public class LMM03700ViewModel : R_ViewModel<LMM03700DTO>
    {
        private LMM03700Model _LMM03700Model = new LMM03700Model();

        #region Public Property ViewModel
        public ObservableCollection<LMM03700DTO> TenantClassGrpGrid { get; set; } = new ObservableCollection<LMM03700DTO>();
        public List<LMM03700PropetyDTO> PropertyList { get; set; } = new List<LMM03700PropetyDTO>();
        public LMM03700DTO TenantClassGrp { get; set; } = new LMM03700DTO();
        public string PropertyId { get; set; } = "";
        #endregion
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03700Model.GetPropertyListAsync();
                PropertyId = loResult.FirstOrDefault().CPROPERTY_ID;

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantClassGrpList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03700Model.GetTenantClassGrpListAsync(PropertyId);

                TenantClassGrpGrid = new ObservableCollection<LMM03700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantClassGrp(LMM03700DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03700Model.R_ServiceGetRecordAsync(poEntity);

                TenantClassGrp = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTenantClassGrp(LMM03700DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM03700Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationTenantClassGrp(LMM03700DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                //if (string.IsNullOrEmpty(poEntity.CTENANT_GROUP_ID))
                //    loEx.Add("", "Tenant Group Id cannot be null.");

                //if (string.IsNullOrEmpty(poEntity.CTENANT_GROUP_NAME))
                //    loEx.Add("", "Tenant Group Name cannot be null.");

                //if (string.IsNullOrEmpty(poEntity.CADDRESS))
                //    loEx.Add("", "Address cannot be null.");

                //if (string.IsNullOrEmpty(poEntity.CPIC_NAME))
                //    loEx.Add("", "PIC Name cannot be null.");

                //await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTenantClassGrp(LMM03700DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03700Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                TenantClassGrp = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
