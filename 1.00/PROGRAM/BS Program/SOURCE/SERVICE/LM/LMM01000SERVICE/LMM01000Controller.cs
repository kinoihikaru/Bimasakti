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
    public class LMM01000Controller : ControllerBase, ILMM01000
    {
        [HttpPost]
        public LMM01000List<LMM01000DTOPropety> GetProperty()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000DTOPropety> loRtn = null;
            var loParameter = new LMM01000PropertyParameterDTO();

            try
            {
                var loCls = new LMM01000Cls();
                loRtn = new LMM01000List<LMM01000DTOPropety>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetProperty(loParameter);
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
        public LMM01000UniversalDTO GetChargesType(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000UniversalDTO loRtn = new LMM01000UniversalDTO();

            try
            {
                var loCls = new LMM01000Cls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetChargesType(poParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000List<LMM01002DTO> GetChargesUtilityList()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01002DTO> loRtn = null;
            var loParameter = new LMM01002DTO();

            try
            {
                var loCls = new LMM01000Cls();
                loRtn = new LMM01000List<LMM01002DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllChargesUtility(loParameter);
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
        public R_ServiceGetRecordResultDTO<LMM01000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01000DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01000DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

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
        public R_ServiceSaveResultDTO<LMM01000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01000DTO> loRtn = new R_ServiceSaveResultDTO<LMM01000DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM01000DTO loRtn = new LMM01000DTO();

            try
            {
                LMM01000Cls loCls = new LMM01000Cls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.LMM01000ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM01003DTO loRtn = new LMM01003DTO();

            try
            {
                LMM01000Cls loCls = new LMM01000Cls();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.LMM01000CopySource(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}