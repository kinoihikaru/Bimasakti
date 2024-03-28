using LMM01000COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Transactions;
using System.Diagnostics;
using LMM01000COMMON.Print;

namespace LMM01000BACK
{
    public class LMM01040Cls : R_BusinessObject<LMM01040DTO>
    {
        private LoggerLMM01040 _Logger;
        private LoggerLMM01040Print _Printlogger;
        private readonly ActivitySource _activitySource;

        public LMM01040Cls()
        {
            _Logger = LoggerLMM01040.R_GetInstanceLogger();
            _activitySource = LMM01040ActivitySourceBase.R_GetInstanceActivitySource();
        }
        public LMM01040Cls(LoggerLMM01040Print poParam)
        {
            _Printlogger = LoggerLMM01040Print.R_GetInstanceLogger();
            _activitySource = LMM01040PrintActivitySourceBase.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(LMM01040DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM01040DTO R_Display(LMM01040DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            LMM01040DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_GU";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_GU {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01040DTO>(loDataTable).FirstOrDefault();

                loResult.CADMIN_FEE = string.IsNullOrWhiteSpace(loResult.CADMIN_FEE) ? "00" : loResult.CADMIN_FEE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01040DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            LMM01040DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            var loDb = new R_Db();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_MAINTAIN_RATE_GU";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_STANDING_CHARGE", DbType.Decimal, 50, poNewEntity.NBUY_STANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NSTANDING_CHARGE", DbType.Decimal, 50, poNewEntity.NSTANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_USAGE_CHARGE_RATE", DbType.Decimal, 50, poNewEntity.NBUY_USAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_CHARGE_RATE", DbType.Decimal, 50, poNewEntity.NUSAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NMAINTENANCE_FEE", DbType.Decimal, 50, poNewEntity.NMAINTENANCE_FEE);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 50, poNewEntity.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Int32, 50, poNewEntity.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Int32, 50, poNewEntity.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_SC", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_SC);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_USAGE", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_USAGE);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_MAINTENANCE", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_MAINTENANCE);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Logs Debug
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_RATE_GU {@poParameter}", loDbParam);

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
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();

        }
        public LMM01040DTO GetBaseHeaderLogoCompany(LMM01040PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            LMM01040DTO loResult = null;

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
                _Printlogger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01040DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public LMM01040DTO GetReportRateGU(LMM01040DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetReportRateGU");
            var loEx = new R_Exception();
            LMM01040DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_GU";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Printlogger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_GU {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01040DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
