using LMM01000BACK;
using LMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01000UniversalController : ControllerBase, ILMM01000Universal
    {
        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetChargesTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllChargesType(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetStatusList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllStatus(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetTaxExemptionCodeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllTaxExemptionCode(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetWithholdingTaxTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllWithholdingTaxType(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetUsageRateModeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllUsageRateMode(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetRateTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllRateType(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01000UniversalDTO> GetAdminFeeTypeList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = null;

            try
            {
                var loCls = new LMM01000UniversalCls();
                loRtn = new LMM01000List<LMM01000UniversalDTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAdminFeeType(poParam);
                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}