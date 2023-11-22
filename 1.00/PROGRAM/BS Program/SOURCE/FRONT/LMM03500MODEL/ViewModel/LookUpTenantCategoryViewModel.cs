using LMM03500COMMON.DTOs.LMM03502;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;

namespace LMM03500MODEL.ViewModel
{
    public class LookUpTenantCategoryViewModel
    {
        private LMM03502Model loModel = new LMM03502Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetTenantCategoryResultDTO loTenantCategoryRtn = null;

        public ObservableCollection<GetTenantCategoryDTO> loTenantCategoryList = new ObservableCollection<GetTenantCategoryDTO>();


        public async Task GetTenantCategoryListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03502_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loTenantCategoryRtn = await loModel.GetTenantCategoryListAsync();
                loTenantCategoryList = new ObservableCollection<GetTenantCategoryDTO>(loTenantCategoryRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
