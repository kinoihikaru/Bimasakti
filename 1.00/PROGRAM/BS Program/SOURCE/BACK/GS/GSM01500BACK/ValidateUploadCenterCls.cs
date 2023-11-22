using GSM01500COMMON.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Text.Json;

namespace GSM01500BACK
{
    public class ValidateUploadCenterCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            List<UploadCenterDTO> loResult = null;
            List<UploadCenterSaveBulkDTO> loParam = null;
            int count = 1;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand(); 

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadCenterDTO>>(poBatchProcessPar.BigObject);

                loParam = new List<UploadCenterSaveBulkDTO>();

                foreach (var item in loTempObject)
                {
                    loParam.Add(new UploadCenterSaveBulkDTO()
                    {
                        SeqNo = count,
                        CCENTER_CODE = item.CCENTER_CODE,
                        CCENTER_NAME = item.CCENTER_NAME,
                        ACTIVE = item.LACTIVE,
                        NONACTIVE_DATE = item.NONACTIVE_DATE
                    });
                    count++;
                };

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_CENTER_IS_OVERWRITE_CONTEXT)).FirstOrDefault().Value;
                bool IsOverwrite = ((System.Text.Json.JsonElement)loVar).GetBoolean();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {

                    lcQuery = $"CREATE TABLE #CENTERS " +
                        $"(SeqNo INT, " +
                        $"CCENTER_CODE VARCHAR(15), " +
                        $"CCENTER_NAME VARCHAR(40), " +
                        $"ACTIVE BIT, " +
                        $"NONACTIVE_DATE VARCHAR(8));";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<UploadCenterSaveBulkDTO>((SqlConnection)loConn, "#CENTERS", loParam);

                    lcQuery = $"EXEC RSP_VALIDATE_UPLOAD_GSM_CENTER " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@CKEY_GUID, " +
                        $"@Var_Overwrite";

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@Var_Overwrite", DbType.String, 50, IsOverwrite);

                    loCmd.CommandText = lcQuery;

                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    TransScope.Complete();
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
                    if (loConn.State != ConnectionState.Closed)
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

            loException.ThrowExceptionIfErrors();
        }

        public List<UploadCenterDTO> GetSuccessProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            List<UploadCenterDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<UploadCenterDTO>(loDataTableResult).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
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

        public List<UploadCenterErrorDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            R_Exception loEx = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            List<UploadCenterErrorDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<UploadCenterErrorDTO>(loDataTableResult).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
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
    }
}
