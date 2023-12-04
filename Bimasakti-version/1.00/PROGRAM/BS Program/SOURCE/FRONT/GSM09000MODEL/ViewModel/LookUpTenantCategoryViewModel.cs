using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09001;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000MODEL.ViewModel
{
    public class LookUpTenantCategoryViewModel : R_ViewModel<GetTenantCategoryDTO>
    {
        private GSM09001Model loModel = new GSM09001Model();

        public ObservableCollection<GetTenantCategoryDTO> loTenantCategoryList = new ObservableCollection<GetTenantCategoryDTO>();

        public GetTenantCategoryListResultDTO loTenantCategoryListRtn = new GetTenantCategoryListResultDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public async Task GetTenantCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09001_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loTenantCategoryListRtn = await loModel.GetTenantCategoryListStreamAsync();
                loTenantCategoryList = new ObservableCollection<GetTenantCategoryDTO>(loTenantCategoryListRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
