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
    public class LMM01510Controller : ControllerBase, ILMM01510
    {
        [HttpPost]
        public LMM01500List<LMM01510DTO> LMM01510TemplateAndBankAccountList()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01510DTO> loRtn = null;
            var loParameter = new LMM01510DTO();

            try
            {
                var loCls = new LMM01510Cls();
                loRtn = new LMM01500List<LMM01510DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);

                var loResult = loCls.GetAllTemplateAndBankAccount(loParameter);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

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
        public R_ServiceGetRecordResultDTO<LMM01511DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01511DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01511DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

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
        public R_ServiceSaveResultDTO<LMM01511DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01511DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01511DTO> loRtn = new R_ServiceSaveResultDTO<LMM01511DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CINVGRP_CODE = R_Utility.R_GetContext<string>(ContextConstant.CINVGRP_CODE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01510Cls();

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
