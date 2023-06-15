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
    public class LMM01020ViewModel : R_ViewModel<LMM01020DTO>
    {
        private LMM01020Model _LMM01020Model = new LMM01020Model();

        public LMM01020DTO RateWG = new LMM01020DTO();

        public ObservableCollection<LMM01021DTO> RateWGDetailList = new ObservableCollection<LMM01021DTO>();

        public async Task GetRateWGDetailList(LMM01021DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01020Model.GetRateWGListAsync(poParam);

                RateWGDetailList = new ObservableCollection<LMM01021DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LMM01020DTO> GetRateWGCheckData(LMM01020DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01020DTO loResult = null;
            try
            {
                loResult = await _LMM01020Model.R_ServiceGetRecordAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetRateWG(LMM01020DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateWG = await GetRateWGCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRateEC(LMM01020DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01020Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RateWG = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
