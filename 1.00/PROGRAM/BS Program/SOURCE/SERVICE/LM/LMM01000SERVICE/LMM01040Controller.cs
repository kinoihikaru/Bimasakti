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
    public class LMM01040Controller : ControllerBase, ILMM01040
    {
        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01040DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01040DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01040DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01040DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01040Cls();

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
        public R_ServiceSaveResultDTO<LMM01040DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01040DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01040DTO> loRtn = new R_ServiceSaveResultDTO<LMM01040DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01040Cls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01040DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        
    }
}