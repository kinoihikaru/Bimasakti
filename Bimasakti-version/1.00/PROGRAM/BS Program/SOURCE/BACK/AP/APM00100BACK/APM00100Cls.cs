using APM00100COMMON.DTOs.APM00100;
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
using APM00100COMMON.Loggers;

namespace APM00100BACK
{
    public class APM00100Cls : R_BusinessObject<APM00100ParameterDTO>
    {
        private LoggerAPM00100 _logger;
        public APM00100Cls()
        {
            _logger = LoggerAPM00100.R_GetInstanceLogger();
        }

        public GetAPMSystemParamDTO GetAPMSystemParam(GetAPMSystemParamParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            GetAPMSystemParamDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT TOP 1 1 " +
                    "FROM APM_SYSTEM_PARAM (NOLOCK) " +
                    "WHERE CCOMPANY_ID=@CLOGIN_COMPANY_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("SELECT TOP 1 1 FROM APM_SYSTEM_PARAM (NOLOCK) WHERE CCOMPANY_ID = {@Parameters} || GetAPMSystemParam(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = new GetAPMSystemParamDTO()
                {
                    IRESULT = loDataTable.Rows.Count
                };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private void RSP_AP_SAVE_SYSTEM_PARAMMethod(APM00100ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_AP_SAVE_SYSTEM_PARAM  " +
                                 "@CUSER_ID, " +
                                 "@CACTION, " +
                                 "@CCOMPANY_ID, " +
                                 "@CDEPT_CODE, " +
                                 "@CCUR_RATETYPE_CODE, " +
                                 "@CTAX_RATETYPE_CODE, " +
                                 "@LBACKDATE, " +
                                 "@LGLLINK";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCUR_RATETYPE_CODE", DbType.String, 50, poEntity.Data.CCUR_RATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_RATETYPE_CODE", DbType.String, 50, poEntity.Data.CTAX_RATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LBACKDATE", DbType.String, 50, poEntity.Data.LBACKDATE);
                loDb.R_AddCommandParameter(loCmd, "@LGLLINK", DbType.String, 50, poEntity.Data.LGLLINK);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_SAVE_SYSTEM_PARAM {@Parameters} || RSP_AP_SAVE_SYSTEM_PARAMMethod(Cls) ", loDbParam);

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

        protected override APM00100ParameterDTO R_Display(APM00100ParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            APM00100ParameterDTO loResult = new APM00100ParameterDTO();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                lcQuery = "EXEC RSP_AP_GET_SYSTEM_PARAM " +
                                 "@CCOMPANY_ID, " +
                                 "@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_SYSTEM_PARAM {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<APM00100DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(APM00100ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_AP_SAVE_SYSTEM_PARAMMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override void R_Deleting(APM00100ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
    }
}
