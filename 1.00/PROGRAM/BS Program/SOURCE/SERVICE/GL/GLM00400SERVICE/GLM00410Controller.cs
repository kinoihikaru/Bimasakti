using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00410Controller : ControllerBase, IGLM00410
    {
        [HttpPost]
        public GLM00400List<GLM00411DTO> GetAllocationAccountList(GLM00411DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00411DTO> loRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loRtn = new GLM00400List<GLM00411DTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllAllocationAccount(poParam);
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
        public GLM00400List<GLM00415DTO> GetAllocationPeriodByTargetCenterList(GLM00415DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00415DTO> loRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loRtn = new GLM00400List<GLM00415DTO>();

                var loResult = loCls.GetAllAllocationPeriodByTargetCenter(poParam);
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
        public GLM00400List<GLM00414DTO> GetAllocationPeriodList(GLM00414DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00414DTO> loRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loRtn = new GLM00400List<GLM00414DTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllAllocationPeriod(poParam);
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
        public GLM00400List<GLM00413DTO> GetAllocationTargetCenterByPeriodList(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00413DTO> loRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loRtn = new GLM00400List<GLM00413DTO>();

                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllAllocationTargetCenterByPeriod(poParam);
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
        public GLM00400List<GLM00412DTO> GetAllocationTargetCenterList(GLM00412DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00412DTO> loRtn = null;

            try
            {
                var loCls = new GLM00410Cls();
                loRtn = new GLM00400List<GLM00412DTO>();

                var loResult = loCls.GetAllAllocationTargetCenter(poParam);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GLM00410DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GLM00410DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GLM00410DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<GLM00410DTO> loRtn = new R_ServiceGetRecordResultDTO<GLM00410DTO>();

            try
            {
                var loCls = new GLM00410Cls();

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
        public R_ServiceSaveResultDTO<GLM00410DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GLM00410DTO> poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
