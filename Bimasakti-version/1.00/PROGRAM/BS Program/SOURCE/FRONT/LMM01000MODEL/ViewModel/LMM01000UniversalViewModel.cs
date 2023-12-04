using LMM01000COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01000UniversalViewModel : R_ViewModel<LMM01000UniversalDTO>
    {
        private LMM01000UniversalModel _LMM01000UniversalModel = new LMM01000UniversalModel();

        public List<LMM01000UniversalDTO> StatusList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> TaxExemptionList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> WithholdingTaxList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> UsageRateModeList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> RateTypeList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> AdminFeeTypeList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000UniversalDTO> AccrualMethodList { get; set; } = new List<LMM01000UniversalDTO>();
        public List<LMM01000DTOPropety> PropertyList { get; set; } = new List<LMM01000DTOPropety>();

        public async Task GetStatusList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetStatusListAsync();

                StatusList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxExemptionList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetTaxExemptionCodeListAsync();

                TaxExemptionList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetWithHoldingTaxList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetWithholdingTaxTypeListAsync();

                WithholdingTaxList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUsageRateModelList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetUsageRateModeListAsync();

                UsageRateModeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetRateTypeListAsync();

                RateTypeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAdminFeeTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetAdminFeeTypeListAsync();

                AdminFeeTypeList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAccrualMethodList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetAccrualMethodListAsync();

                AccrualMethodList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetPropertyAsync();
                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
