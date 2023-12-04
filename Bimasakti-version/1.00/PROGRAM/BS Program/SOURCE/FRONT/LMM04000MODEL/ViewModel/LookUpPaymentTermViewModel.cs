using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON;
using System.Collections.ObjectModel;
using LMM04000COMMON.DTOs.LMM04000;

namespace LMM04000MODEL.ViewModel
{
    public class LookUpPaymentTermViewModel
    {
        private LMM04010Model loModel = new LMM04010Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public GetPaymentTermResultDTO loPaymentTermRtn = null;

        public ObservableCollection<GetPaymentTermDTO> loPaymentTermList = new ObservableCollection<GetPaymentTermDTO>();

        public async Task GetPaymentTermListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loPaymentTermRtn = await loModel.GetPaymentTermListAsync();
                loPaymentTermList = new ObservableCollection<GetPaymentTermDTO>(loPaymentTermRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

    }
}
