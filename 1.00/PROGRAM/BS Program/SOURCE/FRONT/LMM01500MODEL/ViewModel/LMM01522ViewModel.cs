using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;

namespace LMM01500MODEL
{
    public class LMM01522ViewModel
    {
        private LMM01520Model _LMM01520Model = new LMM01520Model();

        public ObservableCollection<LMM01522DTO> LookupAdditional { get; set; } = new ObservableCollection<LMM01522DTO>();

        public async Task GetLookupAdditionalId()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01520Model.GetAdditionalIdLookupAsync();

                LookupAdditional = new ObservableCollection<LMM01522DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
   
}
