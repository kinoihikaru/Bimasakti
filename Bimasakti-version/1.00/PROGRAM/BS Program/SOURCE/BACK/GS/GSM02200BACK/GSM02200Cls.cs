using GSM02200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace GSM02200BACK
{
    public class GSM02200Cls : R_BusinessObject<GSM02200DTO>, R_IBatchProcess
    {
        RSP_GS_MAINTAIN_GEOGRAPHYResources.Resources_Dummy_Class _loRSP = new RSP_GS_MAINTAIN_GEOGRAPHYResources.Resources_Dummy_Class();
        RSP_GS_UPLOAD_GEOGRAPHYResources.Resources_Dummy_Class _loRSP_Upload = new RSP_GS_UPLOAD_GEOGRAPHYResources.Resources_Dummy_Class();

        private LoggerGSM02200 _Logger;
        public GSM02200Cls()
        {
            _Logger = LoggerGSM02200.R_GetInstanceLogger();
        }

        public List<GSM02200DTO> GetAllGeography(GSM02200DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM02200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_GEOGRAPHY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_GEOGRAPHY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02200DTO>(loDataTable).ToList();

                foreach (var x in loResult)
                {
                    x.CCODE_CNAME_DISPLAY = string.Format("{0} - {1}", x.CCODE, x.CNAME);
                    x.CPARENT_CODE = string.IsNullOrWhiteSpace(x.CPARENT_CODE) ? null : x.CPARENT_CODE;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GSM02200DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override GSM02200DTO R_Display(GSM02200DTO poEntity)
        {
            var loEx = new R_Exception();
            GSM02200DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_GEOGRAPHY_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CGEOGRAPHY_CODE", DbType.String, 100, poEntity.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_GEOGRAPHY_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02200DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02200DTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_GEOGRAPHY";
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
                loDb.R_AddCommandParameter(loCmd, "@CGEOGRAPHY_CODE", DbType.String, 100, poNewEntity.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@CGEOGRAPHY_NAME", DbType.String, 200, poNewEntity.CNAME);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_CODE", DbType.String, 100, poNewEntity.CPARENT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE_FLAG", DbType.Boolean, 50, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_CODE", DbType.String, 200, poNewEntity.CREF_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {

                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GS_MAINTAIN_GEOGRAPHY {@poParameter}", loDbParam);

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

        public void GSM02200ChangeStatusSP(GSM02200DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbCommand loCmd = loDb.GetCommand();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = "RSP_GS_ACTIVE_INACTIVE_GEOGRAPHY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGEOGRAPHY_CODE", DbType.String, 100, poEntity.CCODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_GEOGRAPHY {@poParameter}", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
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

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM02201DTO>>(poBatchProcessPar.BigObject);
                var loObject = R_Utility.R_ConvertCollectionToCollection<GSM02201DTO, GSM02201ReqDTO>(loTempObject);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery += "CREATE TABLE #GEOGRAPHY (	SeqNo INT " +
                        ", GeographyCode VARCHAR(100) " +
                        ", GeographyName NVARCHAR(200) " +
                        ", ParentCode NVARCHAR(100) " +
                        ", Active BIT )";
                       

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<GSM02201ReqDTO>((SqlConnection)loConn, "#GEOGRAPHY", loObject);

                lcQuery = "EXECUTE RSP_GS_UPLOAD_GEOGRAPHY @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

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
    }
}