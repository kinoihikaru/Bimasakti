using GSM01500COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using R_CommonFrontBackAPI;
using System.Transactions;
using System.Text.Json;
using System.Windows.Input;
using GSM01500COMMON.Loggers;

namespace GSM01500BACK
{
    public class UploadCenterCls : R_IBatchProcess
    {
        private LoggerUploadCenter _logger;
        public UploadCenterCls()
        {
            _logger = LoggerUploadCenter.R_GetInstanceLogger();
        }
        public CompanyDTO GetCompany (CompanyParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            CompanyDTO loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "SELECT CCOMPANY_ID, CCOMPANY_NAME " +
                    "FROM SAM_COMPANIES (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @COMPANY_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poEntity.COMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID")
                    .Select(x => x.Value);

                _logger.LogDebug("SELECT CCOMPANY_ID, CCOMPANY_NAME FROM SAM_COMPANIES (NOLOCK) WHERE CCOMPANY_ID = {@Parameters} || GetCompany(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CompanyDTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();

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
                _logger.LogError(ex);
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadCenterDTO>>(poBatchProcessPar.BigObject);

                List<UploadCenterSaveBulkDTO> loParam = new List<UploadCenterSaveBulkDTO>();

                loParam = loObject.Select(item => new UploadCenterSaveBulkDTO()
                {
                    SeqNo = item.INO,
                    CCENTER_CODE = item.CCENTER_CODE,
                    CCENTER_NAME = item.CCENTER_NAME,
                    ACTIVE = item.LACTIVE,
                    NONACTIVE_DATE = item.NONACTIVE_DATE
                }).ToList();
/*
                foreach (var item in loObject)
                {
                    loParam.Add(new UploadCenterSaveBulkDTO()
                    {
                        SeqNo = item.INO,
                        CCENTER_CODE = item.CCENTER_CODE,
                        CCENTER_NAME = item.CCENTER_NAME,
                        ACTIVE = item.LACTIVE,
                        NONACTIVE_DATE = item.NONACTIVE_DATE
                    });
                };*/

                lcQuery = "CREATE TABLE #CENTERS " +
                    "(SeqNo INT, " +
                    "CCENTER_CODE VARCHAR(15), " +
                    "CCENTER_NAME VARCHAR(40), " +
                    "ACTIVE BIT, " +
                    "NONACTIVE_DATE VARCHAR(8));";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadCenterSaveBulkDTO>((SqlConnection)loConn, "#CENTERS", loParam);

                lcQuery = "EXEC RSP_UPLOAD_GSM_CENTER @CCOMPANY_ID, @USER_ID, @KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@USER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@USER_ID" ||
                        x.ParameterName == "@KEY_GUID")
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_UPLOAD_GSM_CENTER {@Parameters} || _BatchProcess(Cls) ", loDbParam);

                loCmd.CommandText = lcQuery;
                loDb.SqlExecNonQuery(loConn, loCmd, false);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                loException.Add(ex);
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
                //lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES ('{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}', '{poBatchProcessPar.Key.KEY_GUID}', -100, '{loException.ErrorList[0].ErrDescp}');";
                //lcQuery = String.Format(lcQuery, poBatchProcessPar.Key.COMPANY_ID)
                //loDb.SqlExecNonQuery(lcQuery);

                //lcQuery = "EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                //   "'{poBatchProcessPar.Key.USER_ID}', " +
                //   "'{poBatchProcessPar.Key.KEY_GUID}', " +
                //   "100, '{loException.ErrorList[0].ErrDescp}', 9";

                //loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
