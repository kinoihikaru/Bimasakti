using GSM01500BACK;
using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using GSM01500COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Reflection;

namespace GSM01500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //LoggerGSM01500
    public class GSM01500Controller : ControllerBase, IGSM01500
    {
        private LoggerGSM01500 _logger;

        public GSM01500Controller(ILogger<GSM01500Controller> logger)
        {
            LoggerGSM01500.R_InitializeLogger(logger);
            _logger = LoggerGSM01500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM01500DTO> GetCenterList()
        {
            _logger.LogInfo("Start || GetCenterList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM01500DTO> loRtn = null;
            GetCenterListParameterDTO loParam = new GetCenterListParameterDTO();
            GSM01500Cls loCls = new GSM01500Cls();
            List<GSM01500DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetCenterList(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetCenterList(Cls) || GetCenterList(Controller)");
                loTempRtn = loCls.GetCenterList(loParam);

                _logger.LogInfo("Run GetCenterStream(Controller) || GetCenterList(Controller)");
                loRtn = GetCenterStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCenterList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM01500DTO> GetCenterStream(List<GSM01500DTO> poParameter)
        {
            foreach (GSM01500DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TemplateCenterDTO DownloadTemplateCenter()
        {
            _logger.LogInfo("Start || DownloadTemplateCenter(Controller)");
            var loException = new R_Exception();
            var loRtn = new TemplateCenterDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateCenter(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_GS_API");
                var lcResourceFile = "BIMASAKTI_GS_API.Template.Center.xlsx";

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
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || DownloadTemplateCenter(Controller)");
            return loRtn;
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM01500DTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            CreateUpdateDeleteParameterDTO loParam = new CreateUpdateDeleteParameterDTO();
            GSM01500Cls loCls = new GSM01500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM01500DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM01500DTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM01500DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM01500DTO>();
            CreateUpdateDeleteParameterDTO loParam = new CreateUpdateDeleteParameterDTO();
            R_ServiceGetRecordResultDTO<CreateUpdateDeleteParameterDTO> loTempRtn = new R_ServiceGetRecordResultDTO<CreateUpdateDeleteParameterDTO>();


            try
            {
                GSM01500Cls loCls = new GSM01500Cls();

                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loTempRtn.data = loCls.R_GetRecord(loParam);

                loRtn.data = loTempRtn.data.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");
            return loRtn;
        }

        [HttpPost]
        public CopyFromProcessDTO CopyFromProcess(CopyFromProcessParameter poParam)
        {
            _logger.LogInfo("Start || CopyFromProcess(Controller)");
            R_Exception loException = new R_Exception();
            //CopyFromProcessParameter loParam = new CopyFromProcessParameter();
            CopyFromProcessDTO loRtn = new CopyFromProcessDTO();
            GSM01500Cls loCls = new GSM01500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || CopyFromProcess(Controller)");
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CCOPY_FROM_COMPANY_ID = R_Utility.R_GetContext<string>(ContextConstant.COPY_FROM_COMPANY_ID_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run CopyFromProcess(Cls) || CopyFromProcess(Controller)");
                loCls.CopyFromProcess(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || CopyFromProcess(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<GSM01500DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM01500DTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM01500DTO> loRtn = new R_ServiceSaveResultDTO<GSM01500DTO>();
            R_ServiceSaveResultDTO<CreateUpdateDeleteParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<CreateUpdateDeleteParameterDTO>();
            GSM01500Cls loCls = new GSM01500Cls();
            CreateUpdateDeleteParameterDTO loParam = new CreateUpdateDeleteParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loTempRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
                loRtn.data = loTempRtn.data.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CopyFromProcessCompanyDTO> GetCompanyList()
        {
            _logger.LogInfo("Start || GetCompanyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<CopyFromProcessCompanyDTO> loRtn = null;
            GetCenterListParameterDTO loParam = new GetCenterListParameterDTO();
            GSM01500Cls loCls = new GSM01500Cls();
            List<CopyFromProcessCompanyDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetCompanyList(Controller)");
                loParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetCompanyList(Cls) || GetCompanyList(Controller)");
                loTempRtn = loCls.GetCompanyList(loParam);

                _logger.LogInfo("Run GetCompanyStream(Controller) || GetCompanyList(Controller)");
                loRtn = GetCompanyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCompanyList(Controller)");

            return loRtn;
        }
        private async IAsyncEnumerable<CopyFromProcessCompanyDTO> GetCompanyStream(List<CopyFromProcessCompanyDTO> poParameter)
        {
            foreach (CopyFromProcessCompanyDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public ActiveInactiveDTO RSP_GS_ACTIVE_INACTIVE_CenterMethod(ActiveInactiveParameterDTO poParam)
        {
            _logger.LogInfo("Start || RSP_GS_ACTIVE_INACTIVE_CenterMethod(Controller)");
            R_Exception loException = new R_Exception();
            //ActiveInactiveParameterDTO loParam = new ActiveInactiveParameterDTO();
            ActiveInactiveDTO loRtn = new ActiveInactiveDTO();
            GSM01500Cls loCls = new GSM01500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || RSP_GS_ACTIVE_INACTIVE_CenterMethod(Controller)");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CCENTER_CODE = R_Utility.R_GetContext<string>(ContextConstant.ACTIVE_INACTIVE_CENTER_CODE_CONTEXT);
                //loParam.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.ACTIVE_INACTIVE_LACTIVE_CONTEXT);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run RSP_GS_ACTIVE_INACTIVE_CenterMethod(Cls)|| RSP_GS_ACTIVE_INACTIVE_CenterMethod(Controller)");
                loCls.RSP_GS_ACTIVE_INACTIVE_CENTERMethod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || RSP_GS_ACTIVE_INACTIVE_CenterMethod(Controller)");
            return loRtn;
        }

        [HttpPost]
        public ValidateCompanyDataResultDTO ValidateCompanyData(ValidateCompanyDataParameterDTO poParam)
        {
            _logger.LogInfo("Start || ValidateCompanyData(Controller)");
            R_Exception loException = new R_Exception();
            ValidateCompanyDataResultDTO loRtn = new ValidateCompanyDataResultDTO();
            GSM01500Cls loCls = new GSM01500Cls();

            try
            {
                _logger.LogInfo("Run ValidateCompanyData(Cls) || ValidateCompanyData(Controller)");
                loRtn.Data = loCls.ValidateCompanyData(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || ValidateCompanyData(Controller)");
            return loRtn;
        }
    }
}
