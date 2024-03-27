using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using GLB09900COMMON;
using System.Diagnostics;

namespace GLB09900BACK
{
    public class GLB09900Cls
    {
        RSP_GL_CLOSE_PERIODResources.Resources_Dummy_Class _loRsp = new RSP_GL_CLOSE_PERIODResources.Resources_Dummy_Class();

        private LoggerGLB09900 _Logger;
        private readonly ActivitySource _activitySource;
        public GLB09900Cls()
        {
            _Logger = LoggerGLB09900.R_GetInstanceLogger();
            _activitySource = GLB09900ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public GLB09900InitialDTO GetInitial(GLB09900InitialDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetALLSalesTax");
            var loEx = new R_Exception();
            GLB09900InitialDTO loResult = poEntity;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY ";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                string loCompanyIdLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT dbo.RFN_GET_DB_TODAY({0}) AS DTODAY", loCompanyIdLog);
                    _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loTempResult = R_Utility.R_ConvertTo<GLB09900InitialDTO>(loDataTable).FirstOrDefault();

                loResult.DTODAY = loTempResult.DTODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB09900GLSystemParamDTO GetSystemParam(GLB09900GLSystemParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParam");
            var loEx = new R_Exception();
            GLB09900GLSystemParamDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_PARAM {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB09900GLSystemParamDTO>(loDataTable).FirstOrDefault();

                loTempResult.CURRENT_PERIOD_DISPLAY = loTempResult.CCURRENT_PERIOD.Substring(0, 4) + "-" + loTempResult.CCURRENT_PERIOD.Substring(4, 2);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public GLB09900ValidateDTO GetValidateResultClosePeriod(GLB09900ValidateDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetValidateResultClosePeriod");
            var loEx = new R_Exception();
            GLB09900ValidateDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_VALIDATE_CLOSE_GL_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CPERIOD").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_VALIDATE_CLOSE_GL_PERIOD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB09900ValidateDTO>(loDataTable).FirstOrDefault();

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB09900DTO GetResultClosePeriod(GLB09900DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetResultClosePeriod");
            var loEx = new R_Exception();
            GLB09900DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_CLOSE_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CPERIOD_NO").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_CLOSE_PERIOD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB09900DTO>(loDataTable).FirstOrDefault();

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
