using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON.DTOs.LMM04030;
using LMM04000COMMON.Loggers;

namespace LMM04000BACK
{
    public class LMM04030Cls : R_BusinessObject<LMM04030ParameterDTO>
    {
        private LoggerLMM04030 _logger;
        public LMM04030Cls()
        {
            _logger = LoggerLMM04030.R_GetInstanceLogger();
        }
        private void RSP_LM_MAINTAIN_TENANT_BANK_INFOMethod(LMM04030ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_LM_MAINTAIN_TENANT_BANK_INFO " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CSELECTED_PROPERTY_ID, " +
                                 $"@CSELECTED_TENANT_ID, " +
                                 $"@CBANK_CODE, " +
                                 $"@CBANK_ACCOUNT_NO, " +
                                 $"@CCURRENCY_CODE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_TENANT_ID", DbType.String, 50, poEntity.CSELECTED_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.Data.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT_NO", DbType.String, 50, poEntity.Data.CBANK_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poEntity.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT_BANK_INFO {@Parameters} || RSP_LM_MAINTAIN_TENANT_BANK_INFOMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        public TenantDTO GetTenant(TenantParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            TenantDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_LM_GET_TENANT_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_TENANT_ID, " +
                    $"@CLOGIN_USER_ID";

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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || GetTenant(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<TenantDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM04030DTO> GetBankInfoList(GetBankInfoParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<LMM04030DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_LM_GET_TENANT_BANK_INFO_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_TENANT_ID, " +
                    $"@CLOGIN_USER_ID";

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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_BANK_INFO_LIST {@Parameters} || GetBankInfoList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM04030DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetBankCodeDTO> GetBankCodeList(GetBankCodeParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetBankCodeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"SELECT CCB_CODE, CCB_NAME " +
                    $"FROM GSM_CB(NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CLOGIN_COMPANY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("SELECT CCB_CODE, CCB_NAME FROM GSM_CB(NOLOCK) WHERE CCOMPANY_ID = {@Parameters} || GetBankCodeList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetBankCodeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM04030ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_LM_MAINTAIN_TENANT_BANK_INFOMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override LMM04030ParameterDTO R_Display(LMM04030ParameterDTO poEntity)
        {
            LMM04030ParameterDTO loRtn = new LMM04030ParameterDTO();
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_LM_GET_TENANT_BANK_INFO_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_TENANT_ID, " +
                    $"@CBANK_CODE, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_TENANT_ID", DbType.String, 50, poEntity.CSELECTED_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 50, poEntity.Data.CBANK_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_BANK_INFO_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn.Data = R_Utility.R_ConvertTo<LMM04030DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(LMM04030ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            try
            {
                RSP_LM_MAINTAIN_TENANT_BANK_INFOMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
