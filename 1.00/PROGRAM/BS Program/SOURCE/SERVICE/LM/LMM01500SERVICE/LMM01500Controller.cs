using LMM01500BACK;
using LMM01500COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01500Controller : ControllerBase, ILMM01500
    {
        [HttpPost]
        public LMM01500List<LMM01501DTO> GetInvoiceGrpList()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01501DTO> loRtn = null;
            var loParameter = new LMM01501ParameterDTO();

            try
            {
                var loCls = new LMM01500Cls();
                loRtn = new LMM01500List<LMM01501DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);

                var loResult = loCls.GetAllInvoiceGrp(loParameter);
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
        public LMM01500List<LMM01500DTOPropety> GetProperty()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01500DTOPropety> loRtn = null;
            var loParameter = new LMM01500PropertyParameterDTO();

            try
            {
                var loCls = new LMM01500Cls();
                loRtn = new LMM01500List<LMM01500DTOPropety>();

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
        public LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam)
        {
            R_Exception loException = new R_Exception();
            LMM01500DTO loRtn = new LMM01500DTO();
            LMM01500Cls loCls = new LMM01500Cls();

            try
            {
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.LMM01500ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public LMM01500List<LMM01502DTO> LMM01500LookupBank()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01502DTO> loRtn = null;
            var loParameter = new LMM01502DTO();

            try
            {
                var loCls = new LMM01500Cls();
                loRtn = new LMM01500List<LMM01502DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.LMM01530LookupBank(loParameter);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

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
        public R_ServiceGetRecordResultDTO<LMM01500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01500DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01500DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID); 
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

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
        public R_ServiceSaveResultDTO<LMM01500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01500DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01500DTO> loRtn = new R_ServiceSaveResultDTO<LMM01500DTO>();

            try
            {
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01500Cls();

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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
