using LMM06500BACK;
using LMM06500COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06502Controller : ControllerBase, ILMM06502
    {
        [HttpPost]
        public LMM06500List<LMM06502DetailDTO> GetStaffMoveList(LMM06502DetailDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06502DetailDTO> loRtn = null;

            try
            {
                var loCls = new LMM06502Cls();
                loRtn = new LMM06500List<LMM06502DetailDTO>();

                poEntity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loResult = loCls.GetAllStaffMove(poEntity);
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
        public LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM06502DTO loRtn = new LMM06502DTO();
            LMM06502Cls loCls = new LMM06502Cls();

            try
            {
                poEntity.Header.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poEntity.Header.CUSER_ID = R_BackGlobalVar.USER_ID;

                loCls.SaveNewStaffMove(poEntity);
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
