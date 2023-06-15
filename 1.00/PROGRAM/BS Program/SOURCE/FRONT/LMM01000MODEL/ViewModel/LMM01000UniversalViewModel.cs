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
    public class LMM01000UniversalViewModel
    {
        private LMM01000UniversalModel _LMM01000UniversalModel = new LMM01000UniversalModel();

        public List<LMM01000UniversalDTO> StatusList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> TaxExemptionList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> WithholdingTaxList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> UsageRateModeList = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> RateTypeList = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> AdminFeeTypeList = new List<LMM01000UniversalDTO>();

        public async Task GetStatusList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetStatusListAsync(poParam);

                StatusList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxExemptionList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetTaxExemptionCodeListAsync(poParam);

                TaxExemptionList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetWithHoldingTaxList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetWithholdingTaxTypeListAsync(poParam);

                WithholdingTaxList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUsageRateModelList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetUsageRateModeListAsync(poParam);

                UsageRateModeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetRateTypeListAsync(poParam);

                RateTypeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdminFeeTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetAdminFeeTypeListAsync(poParam);

                AdminFeeTypeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
