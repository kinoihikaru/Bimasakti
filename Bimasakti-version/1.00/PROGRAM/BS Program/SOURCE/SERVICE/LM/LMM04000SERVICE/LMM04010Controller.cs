using LMM04000BACK;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LMM04000COMMON.Loggers;
using Microsoft.Extensions.Logging;

namespace LMM04000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM04010Controller : ControllerBase, ILMM04010
    {
        private LoggerLMM04010 _logger;
        public LMM04010Controller(ILogger<LMM04010Controller> logger)
        {
            LoggerLMM04010.R_InitializeLogger(logger);
            _logger = LoggerLMM04010.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GetCurrencyDTO> GetCurrencyList()
        {
            _logger.LogInfo("Start || GetCurrencyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetCurrencyDTO> loRtn = null;
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetCurrencyDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Run GetCurrencyList(Cls) || GetCurrencyList(Controller)");
                loTempRtn = loCls.GetCurrencyList();

                _logger.LogInfo("Run GetCurrencyStream(Controller) || GetCurrencyList(Controller)");
                loRtn = GetCurrencyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCurrencyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetCurrencyDTO> GetCurrencyStream(List<GetCurrencyDTO> poParameter)
        {
            foreach (GetCurrencyDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public LMM04010ResultDTO GetContractorProfile(LMM04010ParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetContractorProfile(Controller)");
            R_Exception loException = new R_Exception();
            LMM04010ResultDTO loRtn = new LMM04010ResultDTO();
            //LMM04010ParameterDTO loParam = new LMM04010ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetContractorProfile(Controller)");
                LMM04010Cls loCls = new LMM04010Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_TENANT_ID_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetContractorProfile(Cls) || GetContractorProfile(Controller)");
                loRtn.Data = loCls.GetContractorProfile(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetContractorProfile(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetJournalGroupDTO> GetJournalGroupList()
        {
            _logger.LogInfo("Start || GetJournalGroupList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetJournalGroupDTO> loRtn = null;
            GetJournalGroupParameterDTO loParam = new GetJournalGroupParameterDTO();
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetJournalGroupDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetJournalGroupList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetJournalGroupList(Cls) || GetJournalGroupList(Controller)");
                loTempRtn = loCls.GetJournalGroupList(loParam);

                _logger.LogInfo("Run GetJournalGroupStream(Controller) || GetJournalGroupList(Controller)");
                loRtn = GetJournalGroupStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetJournalGroupList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetJournalGroupDTO> GetJournalGroupStream(List<GetJournalGroupDTO> poParameter)
        {
            foreach (GetJournalGroupDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            _logger.LogInfo("Start || GetLineOfBusinessList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLineOfBusinessDTO> loRtn = null;
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetLineOfBusinessDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("run GetLineOfBusinessList(Cls) || GetLineOfBusinessList(Controller)");
                loTempRtn = loCls.GetLineOfBusinessList();

                _logger.LogInfo("Run GetLineOfBusinessStream(Controller) || GetLineOfBusinessList(Controller)");
                loRtn = GetLineOfBusinessStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetLineOfBusinessList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessStream(List<GetLineOfBusinessDTO> poParameter)
        {
            foreach (GetLineOfBusinessDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPaymentTermDTO> GetPaymentTermList()
        {
            _logger.LogInfo("Start || GetPaymentTermList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPaymentTermDTO> loRtn = null;
            GetPaymentTermParameterDTO loParam = new GetPaymentTermParameterDTO();
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetPaymentTermDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPaymentTermList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetPaymentTermList(Cls) || GetPaymentTermList(Controller)");
                loTempRtn = loCls.GetPaymentTermList(loParam);

                _logger.LogInfo("Run GetPaymentTermStream(Controller) || GetPaymentTermList(Controller)");
                loRtn = GetPaymentTermStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPaymentTermList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPaymentTermDTO> GetPaymentTermStream(List<GetPaymentTermDTO> poParameter)
        {
            foreach (GetPaymentTermDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetContractorCategoryDTO> GetContractorCategoryList()
        {
            _logger.LogInfo("Start || GetContractorCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetContractorCategoryDTO> loRtn = null;
            GetContractorCategoryParameterDTO loParam = new GetContractorCategoryParameterDTO();
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetContractorCategoryDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetContractorCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetContractorCategoryist(Cls) || GetContractorCategoryList(Controller)");
                loTempRtn = loCls.GetContractorCategoryist(loParam);

                _logger.LogInfo("Run GetContractorCategoryStream(Controller) || GetContractorCategoryList(Controller)");
                loRtn = GetContractorCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetContractorCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetContractorCategoryDTO> GetContractorCategoryStream(List<GetContractorCategoryDTO> poParameter)
        {
            foreach (GetContractorCategoryDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetContractorGroupDTO> GetContractorGroupList()
        {
            _logger.LogInfo("Start || GetContractorGroupList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetContractorGroupDTO> loRtn = null;
            GetContractorGroupParameterDTO loParam = new GetContractorGroupParameterDTO();
            LMM04010Cls loCls = new LMM04010Cls();
            List<GetContractorGroupDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetContractorGroupList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Ryb GetContractorGroupList(Cls) || GetContractorGroupList(Controller)");
                loTempRtn = loCls.GetContractorGroupList(loParam);

                _logger.LogInfo("Run GetContractorGroupStream(Controller) || GetContractorGroupList(Controller)");
                loRtn = GetContractorGroupStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetContractorGroupList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetContractorGroupDTO> GetContractorGroupStream(List<GetContractorGroupDTO> poParameter)
        {
            foreach (GetContractorGroupDTO item in poParameter)
            {
                yield return item;
            }
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<ProfileTaxParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            LMM04010Cls loCls = new LMM04010Cls();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {/*
                loParam.Profile = poParameter.Entity.Profile;
                loParam.TaxInfo = poParameter.Entity.TaxInfo;

                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_TENANT_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
*/
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                poParameter.Entity.CACTION = "DELETE";

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
        public R_ServiceGetRecordResultDTO<ProfileTaxParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<ProfileTaxParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<ProfileTaxParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<ProfileTaxParameterDTO>();

            try
            {
                LMM04010Cls loCls = new LMM04010Cls();
                /*
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_TENANT_ID_CONTEXT);
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                */
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("RUn R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
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
        public R_ServiceSaveResultDTO<ProfileTaxParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<ProfileTaxParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<ProfileTaxParameterDTO> loRtn = new R_ServiceSaveResultDTO<ProfileTaxParameterDTO>();
            LMM04010Cls loCls = new LMM04010Cls();

            try
            {/*
                loParam = poParameter.Entity;
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04010_TENANT_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
*/
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
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
    }
}
