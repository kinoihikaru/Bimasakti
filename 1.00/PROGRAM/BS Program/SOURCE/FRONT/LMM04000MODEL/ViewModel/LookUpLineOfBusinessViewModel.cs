using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL.ViewModel
{
    public class LookUpLineOfBusinessViewModel
    {
        private LMM04010Model loModel = new LMM04010Model();

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
