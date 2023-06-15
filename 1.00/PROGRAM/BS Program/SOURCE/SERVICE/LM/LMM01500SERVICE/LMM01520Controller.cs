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
    public class LMM01520Controller : ControllerBase, ILMM01520
    {
        [HttpPost]
        public LMM01500List<LMM01522DTO> GetAdditionalIdLookup()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01522DTO> loRtn = null;
            var loParameter = new LMM01522DTO();

            try
            {
                var loCls = new LMM01520Cls();
                loRtn = new LMM01500List<LMM01522DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);

                var loResult = loCls.GetAllAdditionalId(loParameter);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01520DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01520DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01520DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01520DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01520DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01520Cls();

                var loTempData = loCls.R_GetRecord(poParameter.Entity);
                loRtn.data = loTempData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01520DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01520DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01520DTO> loRtn = new R_ServiceSaveResultDTO<LMM01520DTO>();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01520Cls();

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
