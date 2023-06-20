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
    public class LMM01010ViewModel : R_ViewModel<LMM01010DTO>
    {
        private LMM01010Model _LMM01010Model = new LMM01010Model();

        public LMM01010DTO RateEC = new LMM01010DTO();

        public ObservableCollection<LMM01011DTO> RateUCDetailList = new ObservableCollection<LMM01011DTO>();

        public async Task GetRateUCDetailList(LMM01011DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01010Model.GetRateECListAsync(poParam);

                RateUCDetailList = new ObservableCollection<LMM01011DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LMM01010DTO> GetRateECCheckData(LMM01010DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01010DTO loResult = null;
            try
            {
                 loResult = await _LMM01010Model.R_ServiceGetRecordAsync(poParam);

                 RateEC = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetRateEC(LMM01010DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = await GetRateECCheckData(poParam);
                RateEC = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRateEC(LMM01010DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01010Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RateEC = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
