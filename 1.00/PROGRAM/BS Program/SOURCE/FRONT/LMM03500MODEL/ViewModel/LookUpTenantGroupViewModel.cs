using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON;

namespace LMM03500MODEL.ViewModel
{
    public class LookUpTenantGroupViewModel
    {
        private LMM03502Model loModel = new LMM03502Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetTenantGroupResultDTO loTenantGroupRtn = null;

        public ObservableCollection<GetTenantGroupDTO> loTenantGroupList = new ObservableCollection<GetTenantGroupDTO>();

        public async Task GetTenantGroupListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03502_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loTenantGroupRtn = await loModel.GetTenantGroupListAsync();
                loTenantGroupList = new ObservableCollection<GetTenantGroupDTO>(loTenantGroupRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
