using LMM03500COMMON.DTOs.LMM03502;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL.ViewModel
{
    public class LookUpLineOfBusinessViewModel
    {
        private LMM03502Model loModel = new LMM03502Model();

        public GetLineOfBusinessResultDTO loLineOfBusinessRtn = null;

        public ObservableCollection<GetLineOfBusinessDTO> loLineOfBusinessList = new ObservableCollection<GetLineOfBusinessDTO>();

        public async Task GetLineOfBusinessListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loLineOfBusinessRtn = await loModel.GetLineOfBusinessListAsync();
                loLineOfBusinessList = new ObservableCollection<GetLineOfBusinessDTO>(loLineOfBusinessRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
