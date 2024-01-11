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
    public class LMM03712ViewModel : R_ViewModel<LMM03711DTO>
    {
        private LMM03710Model _LMM03710Model = new LMM03710Model();

        #region Public Property ViewModel
        public List<LMM03710DTO> TenantClassList { get; set; } = new List<LMM03710DTO>();
        public ObservableCollection<LMM03711DTO> TenantClassTenantGrid { get; set; } = new ObservableCollection<LMM03711DTO>();
        #endregion

        public async Task GetTenantClassList(LMM03710DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM03710Model.GetTenantClassListAsync(poEntity);

                TenantClassList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantClassTenantGrid(LMM03711DTO poEntity)
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
        public async Task MoveTenantClass(LMM03711MoveTenantDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM03710Model.MoveTenantClassAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
