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
    public class LMM02500ViewModel : R_ViewModel<LMM02500DTO>
    {
        private LMM02500Model _LMM02500Model = new LMM02500Model();
        private LMM02500UniversalModel _LMM02500UniversalModel = new LMM02500UniversalModel();

        #region Public Property ViewModel
        public ObservableCollection<LMM02500DTO> TenantGrpGrid { get; set; } = new ObservableCollection<LMM02500DTO>();
        public List<LMM02500PropertyDTO> PropertyList { get; set; } = new List<LMM02500PropertyDTO>();
        public string PropertyId { get; set; } = "";
        #endregion
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500UniversalModel.GetPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTenantGrpList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500Model.GetTenantGrpistAsync(PropertyId);

                TenantGrpGrid = new ObservableCollection<LMM02500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
