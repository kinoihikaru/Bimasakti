using Lookup_LMBACK;
using Lookup_LMCOMMON;
using Lookup_LMCOMMON.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;

namespace Lookup_LMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMController : ControllerBase, IPublicLookupLM
    {
        [HttpPost]
        public LMLGenericList<LML00100DTO> LML00100GetSalesTaxList(LML00100ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00100DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllSalesTax(poParameter);

                loRtn = new LMLGenericList<LML00100DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMLGenericList<LML00200DTO> LML00200UnitChargesList(LML00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00200DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllUnitCharges(poParameter);
                loRtn = new LMLGenericList<LML00200DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMLGenericList<LML00300DTO> LML00300SupervisorList(LML00300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00300DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllSupervisor(poParameter);
                loRtn = new LMLGenericList<LML00300DTO> { Data = loResult };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public LMLGenericList<LML00400DTO> LML00400UtilityChargesList(LML00400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            LMLGenericList<LML00400DTO> loRtn = null;

            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllUtilityCharges(poParameter);
                loRtn = new LMLGenericList<LML00400DTO> { Data = loResult };
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