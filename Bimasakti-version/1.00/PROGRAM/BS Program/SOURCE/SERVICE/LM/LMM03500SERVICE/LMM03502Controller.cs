using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03502Controller : ControllerBase, ILMM03502
    {
        private LoggerLMM03502 _logger;
        public LMM03502Controller(ILogger<LMM03502Controller> logger)
        {
            LoggerLMM03502.R_InitializeLogger(logger);
            _logger = LoggerLMM03502.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM03502ResultDTO GetTenantProfile(LMM03502ParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetTenantProfile(Controller)");
            R_Exception loException = new R_Exception();
            LMM03502ResultDTO loRtn = new LMM03502ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantProfile(Controller)");
                LMM03502Cls loCls = new LMM03502Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03502_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03502_TENANT_ID_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenantProfile(Cls) || GetTenantProfile(Controller)");
                loRtn.Data = loCls.GetTenantProfile(poParam);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantProfile(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            _logger.LogInfo("Start || GetLineOfBusinessList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLineOfBusinessDTO> loRtn = null;
            LMM03502Cls loCls = new LMM03502Cls();
            List<GetLineOfBusinessDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Run GetLineOfBusinessList(Cls) || GetLineOfBusinessList(Controller)");
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
        public IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList()
        {
            _logger.LogInfo("Start || GetTenantCategoryList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetTenantCategoryDTO> loRtn = null;
            GetTenantCategoryParameterDTO loParam = new GetTenantCategoryParameterDTO();
            LMM03502Cls loCls = new LMM03502Cls();
            List<GetTenantCategoryDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantCategoryList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03502_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetTenantCategoryist(Cls) || GetTenantCategoryList(Controller)");
                loTempRtn = loCls.GetTenantCategoryist(loParam);

                loRtn = GetTenantCategoryStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantCategoryList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryStream(List<GetTenantCategoryDTO> poParameter)
        {
            foreach (GetTenantCategoryDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetTenantGroupDTO> GetTenantGroupList()
        {
            _logger.LogInfo("Start || GetTenantGroupList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetTenantGroupDTO> loRtn = null;
            GetTenantGroupParameterDTO loParam = new GetTenantGroupParameterDTO();
            LMM03502Cls loCls = new LMM03502Cls();
            List<GetTenantGroupDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantGroupList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03502_PROPERTY_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetTenantGroupList(Cls) || GetTenantGroupList(Controller)");
                loTempRtn = loCls.GetTenantGroupList(loParam);

                _logger.LogInfo("Run GetTenantGroupStream(Controller) || GetTenantGroupList(Controller)");
                loRtn = GetTenantGroupStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantGroupList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetTenantGroupDTO> GetTenantGroupStream(List<GetTenantGroupDTO> poParameter)
        {
            foreach (GetTenantGroupDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetTenantTypeDTO> GetTenantTypeList()
        {
            _logger.LogInfo("Start || GetTenantTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetTenantTypeDTO> loRtn = null;
            GetTenantTypeParameterDTO loParam = new GetTenantTypeParameterDTO();
            LMM03502Cls loCls = new LMM03502Cls();
            List<GetTenantTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTenantTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTenantTypeist(Cls) || GetTenantTypeList(Controller)");
                loTempRtn = loCls.GetTenantTypeist(loParam);

                _logger.LogInfo("Run GetTenantTypeStream(Controller) || GetTenantTypeList(Controller)");
                loRtn = GetTenantTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenantTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetTenantTypeDTO> GetTenantTypeStream(List<GetTenantTypeDTO> poParameter)
        {
            foreach (GetTenantTypeDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<ProfileTaxParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            LMM03502Cls loCls = new LMM03502Cls();
            ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
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
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<ProfileTaxParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<ProfileTaxParameterDTO>();
            ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO();


            try
            {
                _logger.LogInfo("Start || R_ServiceDelete(Controller)");
                LMM03502Cls loCls = new LMM03502Cls();/*

                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03502_PROPERTY_ID_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03502_TENANT_ID_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
*/

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start || R_ServiceDelete(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity); 
/*
                loRtn.data.Profile = loTempRtn.data.Profile;
                loRtn.data.TaxInfo = loTempRtn.data.TaxInfo;*/
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
        public R_ServiceSaveResultDTO<ProfileTaxParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<ProfileTaxParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<ProfileTaxParameterDTO> loRtn = new R_ServiceSaveResultDTO<ProfileTaxParameterDTO>();
            LMM03502Cls loCls = new LMM03502Cls();
            ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO();

            try
            {
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
