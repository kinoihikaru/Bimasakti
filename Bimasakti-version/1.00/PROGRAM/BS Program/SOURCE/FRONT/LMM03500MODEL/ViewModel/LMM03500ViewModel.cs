using LMM03500COMMON.DTOs.LMM03500;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03500ViewModel : R_ViewModel<GetPropertyListDTO>
    {
        private LMM03500Model loModel = new LMM03500Model();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = null;

        public GetPropertyListResultDTO loPropertyRtn = null;

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
