using LMM04000COMMON.DTOs.LMM04010;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LMM04000COMMON.DTOs.LMM04000;

namespace LMM04000MODEL.ViewModel
{
    public class LookUpCurrencyViewModel
    {
        private LMM04010Model loModel = new LMM04010Model();

        public GetCurrencyResultDTO loCurrencyRtn = null;

        public ObservableCollection<GetCurrencyDTO> loCurrencyList = new ObservableCollection<GetCurrencyDTO>();


        public async Task GetCurrencyListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCurrencyRtn = await loModel.GetCurrencyListAsync();
                loCurrencyList = new ObservableCollection<GetCurrencyDTO>(loCurrencyRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
