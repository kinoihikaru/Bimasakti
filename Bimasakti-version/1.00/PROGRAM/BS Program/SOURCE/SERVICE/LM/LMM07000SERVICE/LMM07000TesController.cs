using LMM07000BACK;
using LMM07000COMMON;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using R_AuthenticationBack;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LMM07000SERVICE
{
    [ApiController, AllowAnonymous]
    [Route("api/[controller]/[action]")]
    public class LMM07000TesController : ControllerBase
    {
        private HttpContextAccessor _HttpContextAccessor;

        public LMM07000TesController(HttpContextAccessor poHttpContext)
        {
            _HttpContextAccessor = poHttpContext;
        }
        [HttpGet]
        public LMM07000TesDTO GetMe()
        {
            LMM07000TesDTO loReturn = null;

            var loEx = new R_Exception();
            try
            {
                loReturn = new LMM07000TesDTO() 
                {
                    APPLICATION_ID = _HttpContextAccessor.HttpContext.User.FindFirstValue(R_UserClaim.APPLICATION_ID),
                    TENANT_ID = _HttpContextAccessor.HttpContext.User.FindFirstValue(R_UserClaim.TENANT_ID),
                    USER_ID = _HttpContextAccessor.HttpContext.User.FindFirstValue(R_UserClaim.USER_ID),
                    COMPANY_ID = _HttpContextAccessor.HttpContext.User.FindFirstValue(R_UserClaim.COMPANY_ID),
                    ROLES = _HttpContextAccessor.HttpContext.User.FindFirstValue(R_UserClaim.USER_ROLE)
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loReturn;
        }

    }
}