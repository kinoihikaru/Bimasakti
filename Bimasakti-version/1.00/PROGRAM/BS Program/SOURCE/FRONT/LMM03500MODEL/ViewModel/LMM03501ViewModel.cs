using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.DTOs.LMM03500;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Helpers;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03501ViewModel : R_ViewModel<LMM03501DTO>
    {
        private LMM03501Model loModel = new LMM03501Model();

        public LMM03501DTO loTenant = null;

        public ObservableCollection<LMM03501DTO> loTenantList = new ObservableCollection<LMM03501DTO>();

        public LMM03501ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public async Task GetTenantListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03501_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetTenantListStreamAsync();
                loTenantList = new ObservableCollection<LMM03501DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateTenantDTO> DownloadTemplateTenantAsync()
        {
            var loEx = new R_Exception();
            TemplateTenantDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateTenantAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}