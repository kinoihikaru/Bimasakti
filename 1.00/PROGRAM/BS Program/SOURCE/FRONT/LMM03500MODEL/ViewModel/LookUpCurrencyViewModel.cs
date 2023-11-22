using LMM03500COMMON.DTOs.LMM03502;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LMM03500COMMON.DTOs.LMM03500;

namespace LMM03500MODEL.ViewModel
{
    public class LookUpCurrencyViewModel
    {
        private LMM03500Model loModel = new LMM03500Model();

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
