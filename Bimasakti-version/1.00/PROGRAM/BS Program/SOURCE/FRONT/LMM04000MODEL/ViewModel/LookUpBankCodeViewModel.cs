using LMM04000COMMON;
using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04030;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL.ViewModel
{
    public class LookUpBankCodeViewModel
    {
        private LMM04030Model loModel = new LMM04030Model();

        public ObservableCollection<GetBankCodeDTO> loBankCodeList = new ObservableCollection<GetBankCodeDTO>();

        public GetBankCodeResultDTO loBankCodeRtn = null;

        public async Task GetBankCodeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loBankCodeRtn = await loModel.GetBankCodeListStreamAsync();
                loBankCodeList = new ObservableCollection<GetBankCodeDTO>(loBankCodeRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
