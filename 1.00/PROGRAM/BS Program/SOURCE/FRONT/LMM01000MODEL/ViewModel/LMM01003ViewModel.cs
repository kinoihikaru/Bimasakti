using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using LMM01000COMMON;

namespace LMM01000MODEL
{
    public class LMM01003ViewModel : R_ViewModel<LMM01003DTO>
    {
        private LMM01000Model _LMM01000Model = new LMM01000Model();

        public LMM01003DTO ChargesType = new LMM01003DTO();

        public async Task CopyNewCharges(LMM01003DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                 await _LMM01000Model.LMM01000CopyNewChargesAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }



    }
}
