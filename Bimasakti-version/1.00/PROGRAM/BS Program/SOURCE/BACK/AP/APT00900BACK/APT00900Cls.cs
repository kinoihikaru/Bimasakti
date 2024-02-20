using APT00900COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace APT00900BACK
{
    public class APT00900Cls : R_IBatchProcess
    {
        private readonly ActivitySource _activitySource;
        private LoggerAPT00900 _Logger;
        public APT00900Cls()
        {
            _Logger = LoggerAPT00900.R_GetInstanceLogger();
            var loActivity = APT00900ActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity is null)
            {
                _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }

        public List<APT00900PropertyDTO> GetProperty()
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            List<APT00900PropertyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00900PropertyDTO>(loDataTable).ToList();
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
            using Activity activity = _activitySource.StartActivity("R_BatchProcess");
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
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                //Get Property Id
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                //Mapping List Temp Table
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<APT00900DisplayDTO>>(poBatchProcessPar.BigObject);
                var loObject = loTempObject.Select(item => new APT00900DTO()
                {
                    INO = item.SEQ_NO,
                    CDEPT_CODE = item.Department_Code,
                    CREF_NO = item.Reference_No,
                    CREF_DATE = item.Reference_Date,
                    CSUPPLIER_ID = item.Supplier_Id,
                    CDOC_NO = item.Supplier_Reference_No,
                    CDOC_DATE = item.Supplier_Reference_Date,
                    CCURRENCY_CODE = item.Currency_Code,
                    CPAY_TERM_CODE = item.Term_Code,
                    CTAX_ID = item.Tax_Code,
                    NLBASE_RATE = item.Local_Currency_Base_Rate,
                    NLBASE_CURRENCY_RATE = item.Local_Currency_Rate,
                    NBBASE_RATE = item.Base_Currency_Base_Rate,
                    NBBASE_CURRENCY_RATE = item.Base_Currency_Rate,
                    NTAX_BASE_RATE = item.Tax_Base_Rate,
                    NTAX_CURRENCY_RATE = item.Tax_Rate,
                    CTRANS_DESC = item.Header_Notes,
                    CPROD_DEPT_CODE = item.Product_Department,
                    CPROD_TYPE = item.Product_Type,
                    CPRODUCT_ID = item.Product_Id,
                    CALLOC_ID = item.Allocation_Id,
                    NTRANS_QTY = item.Quantity,
                    NUNIT_PRICE = item.Unit_Price,
                    CDETAIL_DESC = item.Detail_Notes,
                    CREF_NO_NEW = ""
                }).ToList();

                lcQuery = @"CREATE TABLE #APT00900_INVOICE_UPLOAD (
                            INO INT, 
                            CDEPT_CODE VARCHAR(20), 
                            CREF_NO VARCHAR(30),
                            CREF_DATE VARCHAR(8),
                            CSUPPLIER_ID VARCHAR(20),
                            CDOC_NO VARCHAR(30),
                            CDOC_DATE VARCHAR(8),
                            CCURRENCY_CODE VARCHAR(3), 
                            CPAY_TERM_CODE VARCHAR(8), 
                            CTAX_ID VARCHAR(20), 
                            NLBASE_RATE NUMERIC(20,6), 
                            NLBASE_CURRENCY_RATE NUMERIC(20,6), 
                            NBBASE_RATE NUMERIC(20,6), 
                            NBBASE_CURRENCY_RATE NUMERIC(20,6),
                            NTAX_BASE_RATE NUMERIC(20,6),
                            NTAX_CURRENCY_RATE NUMERIC(20,6),
                            CTRANS_DESC NVARCHAR(200),
                            CPROD_DEPT_CODE VARCHAR(20),
                            CPROD_TYPE VARCHAR(1),
                            CPRODUCT_ID VARCHAR(20),
                            CALLOC_ID VARCHAR(20),
                            NTRANS_QTY NUMERIC(17,6),
                            NUNIT_PRICE NUMERIC(19,2),
                            CDETAIL_DESC NVARCHAR(200),
                            CREF_NO_NEW VARCHAR(30),
                        );";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<APT00900DTO>((SqlConnection)loConn, "#APT00900_INVOICE_UPLOAD", loObject);

                lcQuery = @"EXEC RSP_AP_VALIDATE_INVOICE_UPLOAD 
                            @CUSER_ID,
                            @CCOMPANY_ID,
                            @CPROPERTY_ID,
                            @CKEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, lcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loErrorResult = R_Utility.R_ConvertTo<APT00900ValidateDTO>(loDataTable).FirstOrDefault();

                if (loErrorResult.IERROR_COUNT == 0)
                {
                    lcQuery = @"EXEC RSP_AP_SAVE_INVOICE_UPLOAD  
                              @CUSER_ID, 
                              @CCOMPANY_ID, 
                              @CPROPERTY_ID, 
                              @CKEY_GUID";

                    loCmd.CommandText = lcQuery;
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
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