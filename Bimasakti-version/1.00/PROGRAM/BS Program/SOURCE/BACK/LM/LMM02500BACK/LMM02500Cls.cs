using LMM02500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using R_CommonFrontBackAPI;
using System.Data.SqlClient;

namespace LMM02500BACK
{
    public class LMM02500Cls : R_IBatchProcess
    {
        private LoggerLMM02500 _Logger;
        public LMM02500Cls()
        {
            _Logger = LoggerLMM02500.R_GetInstanceLogger();
        }
        public List<LMM02500DTO> GetAllTenantGroup(string poPropertyId)
        {
            var loEx = new R_Exception();
            List<LMM02500DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_TENANT_GROUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                // Log Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                      .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_TENANT_GROUP_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM02500DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<LMM02500TenantDTO> GetAllTenant(string poPropertyId, string poTenantGrpId)
        {
            var loEx = new R_Exception();
            List<LMM02500TenantDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_LM_GET_TENANT_LIST_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 50, poTenantGrpId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                // Log Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                      .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_LM_GET_TENANT_LIST_GROUP {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM02500TenantDTO>(loDataTable).ToList();
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

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM02500MoveTenantDTO>(poBatchProcessPar.BigObject);
                var loObject = R_Utility.R_ConvertCollectionToCollection<LMM02500TenantDTO, LMM02500BulkInsertTenantDTO>(loTempObject.Detail);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "CREATE TABLE #CTENANT_LIST (CTENANT_ID	VARCHAR(20)) ";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<LMM02500BulkInsertTenantDTO>((SqlConnection)loConn, "#CTENANT_LIST", loObject);

                lcQuery = "RSP_LM_MOVE_TENANT_GROUP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, loTempObject.Header.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_TENANT_GROUP", DbType.String, 50, loTempObject.Header.CFROM_TENANT_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CTO_TENANT_GROUP", DbType.String, 50, loTempObject.Header.CTO_TENANT_GROUP);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecQuery(loConn, loCmd, false);
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
    }
}