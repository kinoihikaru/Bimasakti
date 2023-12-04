using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON;

namespace LMM04000MODEL.ViewModel
{
    public class LookupContractorCategoryViewModel
    {
        private LMM04010Model loModel = new LMM04010Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetContractorCategoryResultDTO loContractorCategoryRtn = null;

        public ObservableCollection<GetContractorCategoryDTO> loContractorCategoryList = new ObservableCollection<GetContractorCategoryDTO>();


        public async Task GetContractorCategoryListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loContractorCategoryRtn = await loModel.GetContractorCategoryListAsync();
                loContractorCategoryList = new ObservableCollection<GetContractorCategoryDTO>(loContractorCategoryRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
