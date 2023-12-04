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
using System.Data.SqlClient;
using LMM01000COMMON.Print;

namespace LMM01000BACK
{
    public class LMM01020Cls : R_BusinessObject<LMM01020DTO>, R_IBatchProcess
    {
        private LoggerLMM01020 _Logger;
        private LoggerLMM01020Print _Printlogger;
        public LMM01020Cls()
        {
            _Logger = LoggerLMM01020.R_GetInstanceLogger();
        }
        public LMM01020Cls(LoggerLMM01020Print poParam)
        {
            _Printlogger = LoggerLMM01020Print.R_GetInstanceLogger();
        }
        public List<LMM01021DTO> GetAllRateWGList(LMM01021DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01021DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_RATE_WG_LIST";
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
                _Logger.LogDebug("EXEC RSP_LM_GET_RATE_WG_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01021DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }

        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01020DTO>(poBatchProcessPar.BigObject);
                var loListTempTable = R_Utility.R_ConvertCollectionToCollection<LMM01021DTO, LMM01021SaveBatchDTO>(loTempObject.CRATE_WG_LIST);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery += "CREATE TABLE #CRATE_WG_LIST (	NO INT " +
                        ", CCOMPANY_ID VARCHAR(20) " +
                        ", CPROPERTY_ID NVARCHAR(100) " +
                        ", CCHARGES_TYPE NVARCHAR(100) " +
                        ", CCHARGES_ID NVARCHAR(100) " +
                        ", IUP_TO_USAGE INT " +
                        ", CUSAGE_DESC NVARCHAR(100) " +
                        ", NBUY_USAGE_CHARGE NUMERIC(18,2) " +
                        ", NUSAGE_CHARGE NUMERIC(18,2) ) ";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<LMM01021SaveBatchDTO>((SqlConnection)loConn, "#CRATE_WG_LIST", loListTempTable);

                lcQuery = "RSP_LM_MAINTAIN_RATE_WG";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 15, loTempObject.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, loTempObject.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, loTempObject.CCHARGES_ID);

                loDb.R_AddCommandParameter(loCmd, "@CUSAGE_RATE_MODE", DbType.String, 100, loTempObject.CUSAGE_RATE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@NPIPE_SIZE", DbType.Decimal, 50, loTempObject.NPIPE_SIZE);
                loDb.R_AddCommandParameter(loCmd, "@NSTANDING_CHARGE", DbType.Decimal, 50, loTempObject.NSTANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_CHARGE_RATE", DbType.Decimal, 50, loTempObject.NUSAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@LUSAGE_MIN_CHARGE", DbType.Boolean, 50, loTempObject.LUSAGE_MIN_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_MIN_CHARGE_AMT", DbType.Decimal, 50, loTempObject.NUSAGE_MIN_CHARGE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@NMAINTENANCE_FEE", DbType.Decimal, 50, loTempObject.NMAINTENANCE_FEE);

                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 20, loTempObject.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, loTempObject.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_SC", DbType.Boolean, 50, loTempObject.LADMIN_FEE_SC);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_USAGE", DbType.Boolean, 50, loTempObject.LADMIN_FEE_USAGE);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_MAINTENANCE", DbType.Boolean, 50, loTempObject.LADMIN_FEE_MAINTENANCE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_STANDING_CHARGE", DbType.Decimal, 50, loTempObject.NBUY_STANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_USAGE_CHARGE_RATE", DbType.Decimal, 50, loTempObject.NBUY_USAGE_CHARGE_RATE);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, loTempObject.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);

                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (!(loConn.State == ConnectionState.Closed))
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

            if (loException.Haserror)
            {
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
            }
        }

        protected override void R_Deleting(LMM01020DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM01020DTO R_Display(LMM01020DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01020DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_CHARGES_UTILITY_RATE_WG";
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
                _Logger.LogDebug("EXEC RSP_LM_GET_CHARGES_UTILITY_RATE_WG {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01020DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01020DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCmd = loDb.GetCommand();

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

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    lcQuery = "DECLARE @CRATE_WG_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CRATE_WG_LIST != null && poNewEntity.CRATE_WG_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CRATE_WG_LIST " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01, CATTRIBUTE02, CATTRIBUTE03, CATTRIBUTE04, CATTRIBUTE05, CATTRIBUTE06) " +
                            "VALUES ";
                        foreach (var loRate in poNewEntity.CRATE_WG_LIST)
                        {
                            lcQuery += $"('{loRate.CCOMPANY_ID}', '{loRate.CPROPERTY_ID}', '{loRate.CCHARGES_TYPE}', '{loRate.CCHARGES_ID}', '{loRate.IUP_TO_USAGE}', " +
                                $"'{loRate.CUSAGE_DESC}', '{loRate.NBUY_USAGE_CHARGE}', '{loRate.NUSAGE_CHARGE}'),";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXECUTE RSP_LM_MAINTAIN_RATE_WG " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CPROPERTY_ID = '{poNewEntity.CPROPERTY_ID}' " +
                        $",@CCHARGES_TYPE = '{poNewEntity.CCHARGES_TYPE}' " +
                        $",@CCHARGES_ID = '{poNewEntity.CCHARGES_ID}' " +
                        $",@CUSAGE_RATE_MODE = '{poNewEntity.CUSAGE_RATE_MODE}' " +
                        $",@NPIPE_SIZE = {poNewEntity.NPIPE_SIZE} " +
                        $",@NSTANDING_CHARGE = {poNewEntity.NSTANDING_CHARGE} " +
                        $",@NUSAGE_CHARGE_RATE = {poNewEntity.NUSAGE_CHARGE_RATE} " +
                        $",@LUSAGE_MIN_CHARGE = {poNewEntity.LUSAGE_MIN_CHARGE} " +
                        $",@NUSAGE_MIN_CHARGE_AMT = {poNewEntity.NUSAGE_MIN_CHARGE_AMT} " +
                        $",@NMAINTENANCE_FEE = {poNewEntity.NMAINTENANCE_FEE} " +
                        $",@CADMIN_FEE = '{poNewEntity.CADMIN_FEE}' " +
                        $",@NADMIN_FEE_PCT = {poNewEntity.NADMIN_FEE_PCT} " +
                        $",@NADMIN_FEE_AMT = {poNewEntity.NADMIN_FEE_AMT} " +
                        $",@LADMIN_FEE_TAX = {poNewEntity.LADMIN_FEE_TAX} " +
                        $",@LADMIN_FEE_SC = {poNewEntity.LADMIN_FEE_SC} " +
                        $",@LADMIN_FEE_USAGE = {poNewEntity.LADMIN_FEE_USAGE} " +
                        $",@LADMIN_FEE_MAINTENANCE = {poNewEntity.LADMIN_FEE_MAINTENANCE} " +
                        $",@NBUY_STANDING_CHARGE = {poNewEntity.NBUY_STANDING_CHARGE} " +
                        $",@NBUY_USAGE_CHARGE_RATE = {poNewEntity.NBUY_USAGE_CHARGE_RATE} " +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CRATE_WG_LIST = @CRATE_WG_LIST ";

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        loDb.SqlExecQuery(lcQuery, loConn, false);
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    TransScope.Complete();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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

        public LMM01020DTO GetBaseHeaderLogoCompany(LMM01020PrintParamDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01020DTO loResult = null;

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
                loResult = R_Utility.R_ConvertTo<LMM01020DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public LMM01020DTO GetHDReportRateWG(LMM01020DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01020DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_CHARGES_UTILITY_RATE_WG";
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
                _Printlogger.LogDebug("EXEC RSP_LM_GET_CHARGES_UTILITY_RATE_WG {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01020DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01021DTO> GetDetailReportRateWG(LMM01021DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01021DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_ReportConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_RATE_WG_LIST";
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
                _Printlogger.LogDebug("EXEC RSP_LM_GET_RATE_WG_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01021DTO>(loDataTable).ToList();

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
