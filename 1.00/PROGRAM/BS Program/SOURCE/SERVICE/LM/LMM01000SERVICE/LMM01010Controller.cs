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
    public class LMM01010Controller : ControllerBase, ILMM01010
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01010DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01010DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01010DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01010DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01010DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01010DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01010DTO> loRtn = new R_ServiceSaveResultDTO<LMM01010DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01010DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public LMM01000List<LMM01011DTO> GetRateECList(LMM01011DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01011DTO> loRtn = null;

            try
            {
                var loCls = new LMM01010Cls();
                loRtn = new LMM01000List<LMM01011DTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE_ID = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllRateECList(poParam);
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