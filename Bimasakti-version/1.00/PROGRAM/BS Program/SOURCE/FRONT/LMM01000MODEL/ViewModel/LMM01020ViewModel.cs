using LMM01000COMMON;
using LMM01000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01020ViewModel : R_ViewModel<LMM01020DTO>
    {
        private LMM01020Model _LMM01020Model = new LMM01020Model();
        private LMM01000UniversalModel _LMM01000Model = new LMM01000UniversalModel();

        public LMM01020DTO RateWG = new LMM01020DTO();

        public ObservableCollection<LMM01021DTO> RateWGDetailList = new ObservableCollection<LMM01021DTO>();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();

        public async Task GetProperty(LMM01020DTO poParam)
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

        public void ValidationUtiliryWG(LMM01020DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = poParam.NADMIN_FEE_PCT == 0 || poParam.NADMIN_FEE_PCT < 0 || poParam.NADMIN_FEE_PCT > 100;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10016"));
                }

                if (poParam.CADMIN_FEE == "01")
                {
                    lCancel = poParam.NADMIN_FEE_PCT == 0 || poParam.NADMIN_FEE_PCT < 0 || poParam.NADMIN_FEE_PCT > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10014"));
                    }

                }

                if (poParam.CADMIN_FEE == "02")
                {
                    lCancel = poParam.NADMIN_FEE_AMT == 0 || poParam.NADMIN_FEE_AMT < 0;

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10015"));
                    }
                }
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
