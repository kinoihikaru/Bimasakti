using GSM03000Back;
using GSM03000Common;
using GSM03000Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM03000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM03000Controller : ControllerBase, IGSM03000
    {
        [HttpPost]
        public IAsyncEnumerable<GSM03000DTO> GetOtherChargesList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GSM03000DTO> loRtn = null;
            GSM03000DTO loParameter = null;

            try
            {
                var loCls = new GSM03000Cls();
                loParameter = new GSM03000DTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CUSER_ID = "Admin";
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                var loResult = loCls.GetAllOtherCharges(loParameter);

                loRtn = GetOtherChargesStream(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        private async IAsyncEnumerable<GSM03000DTO> GetOtherChargesStream(List<GSM03000DTO> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GSM03000PropertyListDTO GetProperty()
        {
            var loEx = new R_Exception();
            GSM03000PropertyListDTO loRtn = null;
            var loParameter = new GSM03000PropertyDTO();

            try
            {
                var loCls = new GSM03000Cls();
                loRtn = new GSM03000PropertyListDTO();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CUSER_ID = "Admin";

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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM03000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CUSER_ID = "admin";


                var loCls = new GSM03000Cls();

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
        public R_ServiceGetRecordResultDTO<GSM03000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM03000DTO> poParameter) 
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM03000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM03000DTO>();

            try
            {
                var loCls = new GSM03000Cls();

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
        public R_ServiceSaveResultDTO<GSM03000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM03000DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<GSM03000DTO> loRtn = new R_ServiceSaveResultDTO<GSM03000DTO>();

            try
            {
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GSM03000Cls();

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
