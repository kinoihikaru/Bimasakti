using GSM03000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM03000Common;
using System.Data.Common;
using GSM03000Common.Loggers;
using System.Reflection;

namespace GSM03000Back
{
    public class GSM03000Cls : R_BusinessObject<GSM03000DTO>
    {
        private LoggerGSM03000 _Gsm03000logger;
        private LoggerGSM03000Print _Gsm03000Printlogger;

        public GSM03000Cls()
        {
            _Gsm03000logger = LoggerGSM03000.R_GetInstanceLogger();
        }
        public GSM03000Cls(LoggerGSM03000Print poLogger)
        {
            _Gsm03000Printlogger = LoggerGSM03000Print.R_GetInstanceLogger();
        }
        protected override void R_Deleting(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = "RSP_GS_MAINTAIN_OTHER_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                
                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Gsm03000logger.LogDebug("EXEC RSP_GS_MAINTAIN_OTHER_CHARGES {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();

        }

        protected override GSM03000DTO R_Display(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            GSM03000DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_OTHER_CHARGES_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Gsm03000logger.LogDebug("EXEC RSP_GS_MAINTAIN_OTHER_CHARGES {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM03000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();
                lcQuery = "RSP_GS_MAINTAIN_OTHER_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poNewEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poNewEntity.CGLACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Gsm03000logger.LogDebug("EXEC RSP_GS_MAINTAIN_OTHER_CHARGES {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        public List<GSM03000DTO> GetAllOtherCharges(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM03000DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();
                
                var lcQuery = "EXEC RSP_GS_GET_OTHER_CHARGES_LIST @CCOMPANY_ID, @CPROPERTY_ID, @CCHARGES_TYPE, @CUSER_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@"+ poEntity.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Gsm03000logger.LogDebug("EXEC RSP_GS_GET_OTHER_CHARGES_LIST {@poParameter}", loDbParam);

                //Execute Query
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM03000PropertyDTO> GetProperty(GSM03000PropertyDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM03000PropertyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "exec RSP_GS_GET_PROPERTY_LIST @CCOMPANY_ID, @CUSER_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Gsm03000logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000PropertyDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void GSM03000ChangeStatusSP(GSM03000ActiveParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbCommand loCmd = loDb.GetCommand();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = "RSP_GS_ACTIVE_INACTIVE_OTHER_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Gsm03000logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_OTHER_CHARGES {@poParameter}", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Gsm03000logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public GSM03000ResultDTO GetBaseHeaderLogoCompany(GSM03000PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            GSM03000ResultDTO loResult = null;

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
                _Gsm03000Printlogger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GSM03000ResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000Printlogger.LogError(loEx);
            }
            
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM03000ResultDTO> GetPrintDataResult(GSM03000PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM03000ResultDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_PRINT_OTHER_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 15, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID_FROM", DbType.String, 50, poEntity.CCHARGES_ID_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID_TO", DbType.String, 50, poEntity.CCHARGES_ID_TO);
                loDb.R_AddCommandParameter(loCmd, "@CSHORT_BY", DbType.String, 50, poEntity.CSHORT_BY);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_INACTIVE", DbType.Boolean, 50, poEntity.LPRINT_INACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Gsm03000Printlogger.LogDebug("EXEC RSP_GS_PRINT_OTHER_CHARGES {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000ResultDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000Printlogger.LogError(loEx);
            }
            
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
