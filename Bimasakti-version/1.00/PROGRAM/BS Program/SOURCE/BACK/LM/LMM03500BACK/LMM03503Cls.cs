using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500BACK
{
    public class LMM03503Cls 
    {
        private LoggerLMM03503 _logger;
        public LMM03503Cls() 
        {
            _logger = LoggerLMM03503.R_GetInstanceLogger();
        }
        public LMM03503DTO GetTaxInfo(LMM03503ParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            LMM03503DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_DETAIL " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CSELECTED_TENANT_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_TENANT_ID", DbType.String, 50, poEntity.CSELECTED_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || GetTaxInfo(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM03503DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GetLMM03503ListDTO> GetTaxTypeList(GetLMM03503ListParameterDTO poEntity)
        {
            R_Db loDb = new R_Db(); 
            R_Exception loException = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT * " +
                    "FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_BS_TAX_FOR_TYPE', " +
                    "'', " +
                    "@CLOGIN_LANGUAGE_ID)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_BS_TAX_FOR_TYPE', '' , {1}) || GetTaxTypeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetLMM03503ListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetLMM03503ListDTO> GetIdTypeList(GetLMM03503ListParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT * " +
                    "FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_BS_ID_TYPE', " +
                    "'', " +
                    "@CLOGIN_LANGUAGE_ID)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_BS_ID_TYPE', '' , {1}) || GetIdTypeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetLMM03503ListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetLMM03503ListDTO> GetTaxCodeList(GetLMM03503ListParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CCODE, CDESCRIPTION " +
                    "FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_TAX_CODE', " +
                    "'', " +
                    "@CLOGIN_LANGUAGE_ID)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_TAX_CODE', '' , {1}) || GetTaxCodeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetLMM03503ListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
