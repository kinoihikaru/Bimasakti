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
    public class LMM02520ViewModel : R_ViewModel<LMM02500TenantDTO>
    {
        private LMM02500Model _LMM02500Model = new LMM02500Model();

        #region Public Property ViewModel
        public ObservableCollection<LMM02500TenantDTO> TenantGrid { get; set; } = new ObservableCollection<LMM02500TenantDTO>();
        public LMM02500DTO TenantGrp { get; set; } = new LMM02500DTO();
        #endregion

        public async Task GetTenantList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500Model.GetTenantListAsync(TenantGrp.CPROPERTY_ID, TenantGrp.CTENANT_GROUP_ID);

                TenantGrid = new ObservableCollection<LMM02500TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
