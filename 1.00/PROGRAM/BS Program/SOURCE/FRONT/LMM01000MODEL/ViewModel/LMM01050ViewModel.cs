using LMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01050ViewModel : R_ViewModel<LMM01050DTO>
    {
        private LMM01050Model _LMM01050Model = new LMM01050Model();
        private LMM01000Model _LMM01000Model = new LMM01000Model();

        public LMM01050DTO RateOT = new LMM01050DTO();

        public ObservableCollection<LMM01051DTO> RateOTWDDetailList = new ObservableCollection<LMM01051DTO>();
        public ObservableCollection<LMM01051DTO> RateOTWKDetailList = new ObservableCollection<LMM01051DTO>();

        public List<LMM01051DTO> RateOTWDDetailListData = new List<LMM01051DTO>();
        public List<LMM01051DTO> RateOTWKDetailListData = new List<LMM01051DTO>();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();

        public async Task GetProperty(LMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loListPropery = await _LMM01000Model.GetPropertyAsync();

                Property = loListPropery.Where(k => k.CPROPERTY_ID == poParam.CPROPERTY_ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateOTWDDetailList(LMM01051DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {

                poParam.CDAY_TYPE = "WD";

                var loResult = await _LMM01050Model.GetRateOTListAsync(poParam);

                RateOTWDDetailList = new ObservableCollection<LMM01051DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateOTWKDetailList(LMM01051DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CDAY_TYPE = "WK";

                var loResult = await _LMM01050Model.GetRateOTListAsync(poParam);

                RateOTWKDetailList = new ObservableCollection<LMM01051DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LMM01050DTO> GetRateOTCheckData(LMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01050DTO loResult = null;
            try
            {
                loResult = await _LMM01050Model.R_ServiceGetRecordAsync(poParam);

                RateOT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetRateOT(LMM01050DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateOT = await GetRateOTCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SavingBatchListOTWD(List<LMM01051DTO> poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateOTWDDetailListData = new List<LMM01051DTO>(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void SavingBatchListOTWK(List<LMM01051DTO> poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateOTWKDetailListData = new List<LMM01051DTO>(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRateOT(LMM01050DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CRATE_OT_LIST = new List<LMM01051DTO>();
                poNewEntity.CRATE_OT_LIST.AddRange(RateOTWDDetailListData);
                poNewEntity.CRATE_OT_LIST.AddRange(RateOTWKDetailListData);
                var loResult = await _LMM01050Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RateOT = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
