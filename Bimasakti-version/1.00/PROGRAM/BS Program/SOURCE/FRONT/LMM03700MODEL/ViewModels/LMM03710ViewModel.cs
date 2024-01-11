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
    public class LMM03710ViewModel : R_ViewModel<LMM03710DTO>
    {
        private LMM03710Model _LMM03710Model = new LMM03710Model();

        #region Public Property ViewModel
        public ObservableCollection<LMM03710DTO> TenantClassGrid { get; set; } = new ObservableCollection<LMM03710DTO>();
        public ObservableCollection<LMM03711DTO> TenantClassTenantGrid { get; set; } = new ObservableCollection<LMM03711DTO>();
        public LMM03710DTO TenantClass { get; set; } = new LMM03710DTO();
        #endregion
        public async Task GetTenantClassTenantList(LMM03711DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03710Model.GetTenantCLassTenantListAsync(poEntity);

                TenantClassTenantGrid = new ObservableCollection<LMM03711DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantClassList(LMM03710DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03710Model.GetTenantClassListAsync(poEntity);

                TenantClassGrid = new ObservableCollection<LMM03710DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantClass(LMM03710DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03710Model.R_ServiceGetRecordAsync(poEntity);

                TenantClass = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTenantClass(LMM03710DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM03710Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationTenantClass(LMM03710DTO poEntity)
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

        public async Task SaveTenantClass(LMM03710DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03710Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                TenantClass = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
