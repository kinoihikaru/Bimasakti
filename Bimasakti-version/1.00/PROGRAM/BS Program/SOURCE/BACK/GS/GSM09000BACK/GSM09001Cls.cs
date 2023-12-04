using GSM09000COMMON.DTOs.GSM09000;
using GSM09000COMMON.DTOs.GSM09001;
using GSM09000COMMON.Loggers;
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

namespace GSM09000BACK
{
    public class GSM09001Cls 
    {
        private LoggerGSM09001 _logger;
        public GSM09001Cls()
        {
            _logger = LoggerGSM09001.R_GetInstanceLogger();
        }

        public List<GSM09001DTO> GetTenantList(GSM09001ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            List<GSM09001DTO> loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_TENANT_LIST_CTG " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CTENANT_CATEGORY_ID, " +
                                 "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CATEGORY_ID", DbType.String, 50, poEntity.CTENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_TENANT_LIST_CTG {@Parameters} || GetTenantList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM09001DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {   
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetTenantCategoryDTO> GetTenantCategoryList(GetTenantCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            List<GetTenantCategoryDTO> loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_CATEGORY_LIST " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CLOGIN_USER_ID, " +
                                 "@CCATEGORY_TYPE";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "20");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_LIST {@Parameters} || GetTenantCategoryList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTenantCategoryDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetTenantCategoryDTO GetTenantCategory(GetTenantCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            GetTenantCategoryDTO loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_CATEGORY_DETAIL " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CCATEGORY_TYPE, " +
                                 "@CTENANT_CATEGORY_ID, " +
                                 "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "20");
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CATEGORY_ID", DbType.String, 50, poEntity.CTENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_DETAIL {@Parameters} || GetTenantCategory(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTenantCategoryDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            { 
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void SaveMoveTenantCategory(SaveMoveTenantCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_MOVE_TENANT_CATEGORY " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CFROM_TENANT_CATEGORY_ID, " +
                                 "@CTO_TENANT_CATEGORY_ID, " +
                                 "@CLOGIN_USER_ID, " +
                                 "@CTENANT_LIST";

                
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_TENANT_CATEGORY_ID", DbType.String, 50, poEntity.CFROM_TENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_TENANT_CATEGORY_ID", DbType.String, 50, poEntity.CTO_TENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_LIST", DbType.String, 200, poEntity.CTENANT_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MOVE_TENANT_CATEGORY {@Parameters} || SaveMoveTenantCategory(Cls) ", loDbParam);

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
    }
}
