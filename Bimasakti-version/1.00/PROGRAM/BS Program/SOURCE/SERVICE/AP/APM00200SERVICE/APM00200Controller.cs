using APM00200BACK;
using APM00200COMMON;
using APM00200COMMON.DTOs;
using APM00200COMMON.DTOs.APM00200;
using APM00200COMMON.Loggers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APM00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APM00200Controller : ControllerBase, IAPM00200
    {
        private LoggerAPM00200 _logger;
        public APM00200Controller(ILogger<APM00200Controller> logger)
        {
            LoggerAPM00200.R_InitializeLogger(logger);
            _logger = LoggerAPM00200.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<APM00200DTO> GetExpenditureList()
        {
            _logger.LogInfo("Start || GetExpenditureList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<APM00200DTO> loRtn = null;
            APM00200Cls loCls = new APM00200Cls();
            List<APM00200DTO> loTempRtn = null;
            APM00200GetListParameterDTO loParameter = new APM00200GetListParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetExpenditureList(Controller)");
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParameter.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.APM00200_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetExpenditureList(Cls) || GetExpenditureList(Controller)");
                loTempRtn = loCls.GetExpenditureList(loParameter);

                _logger.LogInfo("Run GetExpenditureStream(Controller) || GetExpenditureList(Controller)");
                loRtn = GetExpenditureStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetExpenditureList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<APM00200DTO> GetExpenditureStream(List<APM00200DTO> poParameter)
        {
            foreach (APM00200DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyDTO> GetPropertyList()
        {
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyDTO> loRtn = null;
            List<GetPropertyDTO> loTempRtn = null;
            GetPropertyParameterDTO loParam = new GetPropertyParameterDTO();
            APM00200Cls loCls = new APM00200Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParam);

                _logger.LogInfo("Run GetPropertyStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyDTO> GetPropertyStream(List<GetPropertyDTO> poParameter)
        {
            foreach (GetPropertyDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APM00200ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            APM00200Cls loCls = new APM00200Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CACTION = "DELETE";
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
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
        public R_ServiceGetRecordResultDTO<APM00200ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APM00200ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<APM00200ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<APM00200ParameterDTO>();
            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                APM00200Cls loCls = new APM00200Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
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
        public R_ServiceSaveResultDTO<APM00200ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<APM00200ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<APM00200ParameterDTO> loRtn = new R_ServiceSaveResultDTO<APM00200ParameterDTO>();
            APM00200Cls loCls = new APM00200Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "NEW";
                    poParameter.Entity.Data.CREC_ID = "";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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
        public TemplateExpenditureDTO DownloadTemplateExpenditure()
        {
            _logger.LogInfo("Start || DownloadTemplateExpenditure(Controller)");
            R_Exception loException = new R_Exception();
            TemplateExpenditureDTO loRtn = new TemplateExpenditureDTO();

            try
            {
                _logger.LogInfo("Read File || DownloadTemplateExpenditure(Controller)");
                var loAsm = Assembly.Load("BIMASAKTI_AP_API");
                var lcResourceFile = "BIMASAKTI_AP_API.Template.EXPENDITURE_UPLOAD.xlsx";

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
            _logger.LogInfo("End || DownloadTemplateExpenditure(Controller)");

            return loRtn;
        }

        [HttpPost]
        public UpdateExpenditureActiveFlagResultDTO UpdateExpenditureActiveFlag(UpdateExpenditureActiveFlagParameterDTO poParameter)
        {
            _logger.LogInfo("Start || UpdateExpenditureActiveFlag(Controller)");
            R_Exception loException = new R_Exception();
            UpdateExpenditureActiveFlagResultDTO loRtn = new UpdateExpenditureActiveFlagResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || UpdateExpenditureActiveFlag(Controller)");
                APM00200Cls loCls = new APM00200Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run UpdateExpenditureActiveFlag(Cls) || UpdateExpenditureActiveFlag(Controller)");
                loCls.UpdateExpenditureActiveFlag(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || UpdateExpenditureActiveFlag(Controller)");

            return loRtn;
        }

        [HttpPost]
        public GetSelectedExpenditureResultDTO GetSelectedExpenditure(GetSelectedExpenditureParameterDTO poParameter)
        {
            _logger.LogInfo("Start || GetSelectedExpenditure(Controller)");
            R_Exception loException = new R_Exception();
            GetSelectedExpenditureResultDTO loRtn = new GetSelectedExpenditureResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetSelectedExpenditure(Controller)");
                APM00200Cls loCls = new APM00200Cls();
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetSelectedExpenditure(Cls) || GetSelectedExpenditure(Controller)");
                loRtn.Data = loCls.GetSelectedExpenditure(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetSelectedExpenditure(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetWithholdingTaxTypeDTO> GetWithholdingTaxTypeList()
        {
            _logger.LogInfo("Start || GetWithholdingTaxTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetWithholdingTaxTypeDTO> loRtn = null;
            APM00200Cls loCls = new APM00200Cls();
            List<GetWithholdingTaxTypeDTO> loTempRtn = null;
            GetWithholdingTaxTypeParameterDTO loParameter = new GetWithholdingTaxTypeParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetWithholdingTaxTypeList(Controller)");
                loParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetWithholdingTaxType(Cls) || GetWithholdingTaxTypeList(Controller)");
                loTempRtn = loCls.GetWithholdingTaxType(loParameter);

                _logger.LogInfo("Run GetWithholdingTaxTypeStream(Controller) || GetWithholdingTaxTypeList(Controller)");
                loRtn = GetWithholdingTaxTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetWithholdingTaxTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetWithholdingTaxTypeDTO> GetWithholdingTaxTypeStream(List<GetWithholdingTaxTypeDTO> poParameter)
        {
            foreach (GetWithholdingTaxTypeDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
