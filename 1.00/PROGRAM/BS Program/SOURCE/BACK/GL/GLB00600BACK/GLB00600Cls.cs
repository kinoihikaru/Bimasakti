﻿using GLB00600COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLB00600BACK
{
    public class GLB00600Cls
    {
        private LoggerGLB00600 _Logger;
        public GLB00600Cls()
        {
            _Logger = LoggerGLB00600.R_GetInstanceLogger();
        }

        public GLB00600GLSystemParamDTO GetSystemParam(GLB00600GLSystemParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB00600GLSystemParamDTO loResult = null;

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
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_PARAM {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTempResult = R_Utility.R_ConvertTo<GLB00600GLSystemParamDTO>(loDataTable).FirstOrDefault();

                loTempResult.CURRENT_PERIOD_DISPLAY = loTempResult.CCURRENT_PERIOD   + "-" + loTempResult.CCURRENT_PERIOD.Substring(4, 2);
                loTempResult.CCLOSE_DEPT_CODE_NAME = string.Format("{0} - {1}", loTempResult.CCLOSE_DEPT_CODE, loTempResult.CCLOSE_DEPT_NAME);

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

        public GLB00600InitialDTO GetInitial(GLB00600InitialDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB00600InitialDTO loResult = poEntity;
            string lcQuery;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            GLB00600InitialDTO loTempResult = null;
            DataTable loDataTable = null;
            try
            {
                var loDb = new R_Db();
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GS_GET_PERIOD_YEAR_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);

                //Debug Logs
                var loDbParam2 = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CYEAR").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_INFO {@poParameter}", loDbParam2);

                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loTempResult = R_Utility.R_ConvertTo<GLB00600InitialDTO>(loDataTable).FirstOrDefault();

                loResult.INO_PERIOD = loTempResult.INO_PERIOD;
                loResult.LVALID = loTempResult.LVALID;
                loResult.LPERIOD_MODE = loTempResult.LPERIOD_MODE;

                lcQuery = "SELECT dbo.RFN_GET_DB_TODAY(@CCOMPANY_ID) AS DTODAY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

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

                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loTempResult = R_Utility.R_ConvertTo<GLB00600InitialDTO>(loDataTable).FirstOrDefault();

                loResult.DTODAY = loTempResult.DTODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB00600SuspenseAmountDTO GetSuspenseAmount(GLB00600SuspenseAmountDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB00600SuspenseAmountDTO loResult = poEntity;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ACCOUNT_BALANCE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CGLACCOUNT_NO" ||
                    x.ParameterName == "@CCENTER_CODE" ||
                    x.ParameterName == "@CPERIOD_NO").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ACCOUNT_BALANCE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loTempResult = R_Utility.R_ConvertTo<GLB00600SuspenseAmountDTO>(loDataTable).FirstOrDefault();

                loResult.NSUSPENSE = loTempResult.NSUSPENSE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLB00600GSMTransactionCodeDTO GetGSMTransactionCode(GLB00600GSMTransactionCodeDTO poEntity)
        {
            var loEx = new R_Exception();
            GLB00600GSMTransactionCodeDTO loResult = poEntity;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "000060");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CTRANS_CODE").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_TRANS_CODE_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loTempResult = R_Utility.R_ConvertTo<GLB00600GSMTransactionCodeDTO>(loDataTable).FirstOrDefault();

                loResult.LINCREMENT_FLAG = loTempResult.LINCREMENT_FLAG;
                loResult.LAPPROVAL_FLAG = loTempResult.LAPPROVAL_FLAG;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLB00600DTO> GetResult(GLB00600DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLB00600DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_DEPT_LOOKUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_DEPT_LOOKUP_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLB00600DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void GetClosingEntries(GLB00600DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_CLOSING_ENTRIES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CUSER_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_CLOSING_ENTRIES {@poParameter}", loDbParam);

                loDb.SqlExecQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
