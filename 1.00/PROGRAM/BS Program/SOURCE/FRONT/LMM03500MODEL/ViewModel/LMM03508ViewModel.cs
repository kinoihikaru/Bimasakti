using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.DTOs.LMM03508;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03508ViewModel : R_ViewModel<LMM03508DTO>
    {
        private LMM03500Model loSharedModel = new LMM03500Model();

        private LMM03508Model loModel = new LMM03508Model();

        public LMM03508DTO loFixVA = null;

        public ObservableCollection<LMM03508DTO> loFixVAList = new ObservableCollection<LMM03508DTO>();

        public LMM03508ResultDTO loRtn = null;

        public TabParameterDTO loTabParameter = null;

        public TenantDTO loTenant = new TenantDTO();

        public TenantResultDTO loTenantRtn = null;

        public async Task GetFixVAListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03508_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03508_TENANT_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loRtn = await loModel.GetFixVAListStreamAsync();
                loFixVAList = new ObservableCollection<LMM03508DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTenantAsync()
        {
            R_Exception loException = new R_Exception();
            TenantParameterDTO loParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new TenantParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loTenantRtn = await loSharedModel.GetTenantAsync(loParam);
                loTenant = loTenantRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
