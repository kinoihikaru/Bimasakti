using LMM06500BACK;
using LMM06500COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using System.Reflection;

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06501Controller : ControllerBase, ILMM06501
    {
        [HttpPost]
        public LMM06500UploadFileDTO DownloadTemplateFile()
        {
            var loEx = new R_Exception();
            var loRtn = new LMM06500UploadFileDTO();

            try
            {
                var loAsm = Assembly.GetExecutingAssembly();
                var lcResourceFile = "LMM06500SERVICE.File.Staff.xlsx";
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }


        [HttpPost]
        public LMM06500List<LMM06500DTO> GetStaffUploadList()
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500DTO> loRtn = null;
            var loParameter = new LMM06500DTO();

            try
            {
                var loCls = new LMM06501Cls();
                loRtn = new LMM06500List<LMM06500DTO>();

                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);

                var loResult = loCls.GetAllStaffUpload(loParameter);
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
