using LMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01040ViewModel : R_ViewModel<LMM01040DTO>
    {
        private LMM01040Model _LMM01040Model = new LMM01040Model();
        private LMM01000Model _LMM01000Model = new LMM01000Model();

        public LMM01040DTO RateGU = new LMM01040DTO();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();

        public async Task GetProperty(LMM01040DTO poParam)
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

        public async Task<LMM01040DTO> GetRateGUCheckData(LMM01040DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01040DTO loResult = null;
            try
            {
                loResult = await _LMM01040Model.R_ServiceGetRecordAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task GetRateUG(LMM01040DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                RateGU = await GetRateGUCheckData(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveRateGU(LMM01040DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01040Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                RateGU = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
