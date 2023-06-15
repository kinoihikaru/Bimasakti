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
    public class LMM01030ViewModel : R_ViewModel<LMM01030DTO>
    {
        private LMM01030Model _LMM01030Model = new LMM01030Model();

        public LMM01030DTO RatePG = new LMM01030DTO();
        public async Task<LMM01030DTO> GetRatePGCheckData(LMM01030DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01030DTO loResult = null;
            try
            {
                loResult = await _LMM01030Model.R_ServiceGetRecordAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetRatePG(LMM01030DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RatePG = await GetRatePGCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRatePC(LMM01030DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01030Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RatePG = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
