using GLR00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace GLR00200BACK
{
    public class GLR00210Cls
    {
        private LoggerGLR00210Print _LoggerPrint;
        private readonly ActivitySource _activitySource;
        public GLR00210Cls()
        {
            _LoggerPrint = LoggerGLR00210Print.R_GetInstanceLogger();
            _activitySource = GLR00210PrintActivitySourceBase.R_GetInstanceActivitySource();
        }

        public GLR00210DTO GetBaseHeaderLogoCompany(GLR00200PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            GLR00210DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _LoggerPrint.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLR00210DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GLR00210DTO> GetSummaryGLLedger(GLR00200PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSummaryGLLedger");
            var loEx = new R_Exception();
            List<GLR00210DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_REP_LEDGER_BY_CENTER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 50, "S");
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 50, poEntity.CCURRENCY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CPRINT_METHOD", DbType.String, 50, poEntity.CPRINT_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_ACCOUNT_NO", DbType.String, 50, poEntity.CFROM_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_ACCOUNT_NO", DbType.String, 50, poEntity.CTO_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CENTER_CODE", DbType.String, 50, poEntity.CFROM_CENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CENTER_CODE", DbType.String, 50, poEntity.CTO_CENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MODE", DbType.String, 50, poEntity.CPERIOD_MODE);     
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);     
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD_NO", DbType.String, 50, poEntity.CFROM_PERIOD_NO);     
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD_NO", DbType.String, 50, poEntity.CTO_PERIOD_NO);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 50, poEntity.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 50, poEntity.CTO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LINCLUDE_AUDIT_JRN", DbType.Boolean, 10, poEntity.LINCLUDE_AUDIT_JRN);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _LoggerPrint.LogDebug("EXEC RSP_GL_REP_LEDGER_BY_CENTER {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00210DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLR00211DTO> GetDetailGLLEdger(GLR00200PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailGLLEdger");
            var loEx = new R_Exception();
            List<GLR00211DTO> loResult = null;

            try
            {
                //Hardcore Report Type
                poEntity.CREPORT_TYPE = "D";

                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_REP_LEDGER_BY_CENTER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 50, poEntity.CREPORT_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 50, poEntity.CCURRENCY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CPRINT_METHOD", DbType.String, 50, poEntity.CPRINT_METHOD);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_ACCOUNT_NO", DbType.String, 50, poEntity.CFROM_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_ACCOUNT_NO", DbType.String, 50, poEntity.CTO_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CENTER_CODE", DbType.String, 50, poEntity.CFROM_CENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CENTER_CODE", DbType.String, 50, poEntity.CTO_CENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MODE", DbType.String, 50, poEntity.CPERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD_NO", DbType.String, 50, poEntity.CFROM_PERIOD_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD_NO", DbType.String, 50, poEntity.CTO_PERIOD_NO);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 50, poEntity.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 50, poEntity.CTO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LINCLUDE_AUDIT_JRN", DbType.Boolean, 10, poEntity.LINCLUDE_AUDIT_JRN);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _LoggerPrint.LogDebug("EXEC RSP_GL_REP_LEDGER_BY_CENTER {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00211DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
