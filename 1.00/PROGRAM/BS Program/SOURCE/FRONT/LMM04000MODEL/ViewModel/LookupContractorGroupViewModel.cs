using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.DTOs.LMM04000;

namespace LMM04000MODEL.ViewModel
{
    public class LookupContractorGroupViewModel
    {
        private LMM04010Model loModel = new LMM04010Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetContractorGroupResultDTO loContractorGroupRtn = null;

        public ObservableCollection<GetContractorGroupDTO> loContractorGroupList = new ObservableCollection<GetContractorGroupDTO>();

        public async Task GetContractorGroupListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loContractorGroupRtn = await loModel.GetContractorGroupListAsync();
                loContractorGroupList = new ObservableCollection<GetContractorGroupDTO>(loContractorGroupRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
