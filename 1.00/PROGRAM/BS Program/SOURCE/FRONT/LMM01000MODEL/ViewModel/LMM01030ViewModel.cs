using LMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01030ViewModel : R_ViewModel<LMM01030DTO>
    {
        private LMM01030Model _LMM01030Model = new LMM01030Model();
        private LMM01000Model _LMM01000Model = new LMM01000Model();

        public LMM01030DTO RatePG = new LMM01030DTO();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();

        public async Task GetProperty(LMM01030DTO poParam)
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


        public async Task<LMM01030DTO> GetRatePGCheckData(LMM01030DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01030DTO loResult = null;
            try
            {
                loResult = await _LMM01030Model.R_ServiceGetRecordAsync(poParam);

                RatePG = loResult;
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
