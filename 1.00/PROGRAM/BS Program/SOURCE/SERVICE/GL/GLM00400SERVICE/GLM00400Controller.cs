using GLM00400BACK;
using GLM00400COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLM00400SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLM00400Controller : ControllerBase, IGLM00400
    {
        [HttpPost]
        public GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400InitialDTO loRtn = null;

            try
            {
                loRtn = new GLM00400InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLM00400Cls();

                loRtn = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400GLSystemParamDTO loRtn = null;

            try
            {
                loRtn = new GLM00400GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLM00400Cls();

                loRtn = loCls.GetSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;

        }


        [HttpPost]
        public GLM00400List<GLM00400DTO> GetAllocationJournalHDList(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00400DTO> loRtn = null;

            try
            {
                var loCls = new GLM00400Cls();
                loRtn = new GLM00400List<GLM00400DTO>();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loResult = loCls.GetAllAllocationJournalHD(poParam);

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
