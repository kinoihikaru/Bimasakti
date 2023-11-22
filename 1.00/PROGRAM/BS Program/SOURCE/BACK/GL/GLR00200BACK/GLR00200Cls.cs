using GLR00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLR00200BACK
{
    public class GLR00200Cls
    {
        private LoggerGLR00200Print _LoggerPrint;
        private LoggerGLR00200 _Logger;
        public GLR00200Cls()
        {
            _Logger = LoggerGLR00200.R_GetInstanceLogger();
        }

        public GLR00200Cls(LoggerGLR00200Print loggerPrint)
        {
            _LoggerPrint = LoggerGLR00200Print.R_GetInstanceLogger();
        }

        public GLR00200InitialDTO GetInitial(GLR00200InitialDTO poEntity)
        {
            var loEx = new R_Exception();
            GLR00200InitialDTO loResult = poEntity;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, "");

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CYEAR" ||
                    x.ParameterName == "@CMODE").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam2);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loTempResult = R_Utility.R_ConvertTo<GLR00200InitialDTO>(loDataTable).FirstOrDefault();

                loResult.IMAX_YEAR = loTempResult.IMAX_YEAR;
                loResult.IMIN_YEAR = loTempResult.IMIN_YEAR;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLR00200GLSystemParamDTO GetSystemParam(GLR00200GLSystemParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLR00200GLSystemParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_PARAM {@poParameter}", loDbParam2);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00200GLSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GLR00200GetMinMaxAccount GetMinMaxAccount()
        {
            var loEx = new R_Exception();
            GLR00200GetMinMaxAccount loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_MIN_MAX_GLACCOUNT_NO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_TYPE", DbType.String, 50, "N");

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CGLACCOUNT_TYPE").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_MIN_MAX_GLACCOUNT_NO {@poParameter}", loDbParam2);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00200GetMinMaxAccount>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GLR00200UniversalDTO> GetAllPrintMethod(GLR00200UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLR00200UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_GL_TRIAL_BAL_PRINT_METHOD");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CAPPLICATION" ||
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CCLASS_ID" ||
                    x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_GSB_CODE_LIST {@poParameter}", loDbParam2);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00200UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLR00200PeriodDTO> GetAllPriod(GLR00200PeriodDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLR00200PeriodDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CCYEAR);

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CYEAR").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_LIST {@poParameter}", loDbParam2);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00200PeriodDTO>(loDataTable).ToList();
                loResult.ForEach(x => x.CPERIOD_NO_DISPLAY = x.CPERIOD_NO);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GLR00200DTO GetBaseHeaderLogoCompany(GLR00200PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLR00200DTO loResult = null;

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
                loResult = R_Utility.R_ConvertTo<GLR00200DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GLR00200DTO GetSummaryGLLedger(GLR00200PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLR00200DTO loResult = null;

            try
            {
                //Hardcore Report Type
                poEntity.CREPORT_TYPE = "S";

                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_REP_LEDGER";
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
                _LoggerPrint.LogDebug("EXEC RSP_GL_REP_LEDGER {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00200DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLR00201DTO> GetDetailGLLEdger(GLR00200PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLR00201DTO> loResult = null;

            try
            {
                //Hardcore Report Type
                poEntity.CREPORT_TYPE = "D";

                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_REP_LEDGER";
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
                _LoggerPrint.LogDebug("EXEC RSP_GL_REP_LEDGER {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLR00201DTO>(loDataTable).ToList();

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
