using LMM01000COMMON;
using LMM01000FrontResources;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01010ViewModel : R_ViewModel<LMM01010DTO>
    {
        private LMM01010Model _LMM01010Model = new LMM01010Model();
        private LMM01000UniversalModel _LMM01000Model = new LMM01000UniversalModel();

        public LMM01010DTO RateEC = new LMM01010DTO();

        public ObservableCollection<LMM01011DTO> RateUCDetailList = new ObservableCollection<LMM01011DTO>();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();

        public async Task GetProperty(LMM01010DTO poParam)
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

        public void ValidationUtiliryEC(LMM01010DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = poParam.NRETRIBUTION_PCT < 0 || poParam.NRETRIBUTION_PCT > 100;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Resources_Dummy_Class),
                    "10019"));
                }

                lCancel = poParam.NKWH_USED_MAX < 0 || poParam.NKWH_USED_MAX > 100;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Resources_Dummy_Class),
                    "10020"));
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
