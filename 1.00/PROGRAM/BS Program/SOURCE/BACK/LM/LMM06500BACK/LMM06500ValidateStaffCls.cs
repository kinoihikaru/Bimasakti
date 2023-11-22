using LMM06500COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace LMM06500BACK
{
    public class LMM06500ValidateStaffCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcPropertyId;
            string lcCmd;
            string lcQuery = "";
            var loDb = new R_Db();
            
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            List<LMM06501DTO> loResult = null;

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMM06501DTO>>(poBatchProcessPar.BigObject);
                var loObject = R_Utility.R_ConvertCollectionToCollection<LMM06501DTO, LMM06501RequestDTO>(loTempObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    DbConnection loConn = null;
                    DbCommand loCmd = null;

                    try
                    {
                        loConn = loDb.GetConnection();
                        loCmd = loDb.GetCommand();

                        lcQuery += "CREATE TABLE #STAFF (	NO INT " +
                                ", StaffId VARCHAR(20) " +
                                ", StaffName NVARCHAR(100) " +
                                ", Active BIT " +
                                ", Department VARCHAR(20) " +
                                ", Position VARCHAR(2) " +
                                ", JoinDate VARCHAR(8) " +
                                ", Supervisor VARCHAR(20) " +
                                ", EmailAddress NVARCHAR(100) " +
                                ", MobileNo1 VARCHAR(30) " +
                                ", MobileNo2 VARCHAR(30) " +
                                ", Gender VARCHAR(2) " +
                                ", Address NVARCHAR(255) " +
                                ", InActiveDate VARCHAR(8) " +
                                ", InactiveNote  NVARCHAR(255) ) ";

                        loDb.SqlExecNonQuery(lcQuery, loConn, false);

                        loDb.R_BulkInsert<LMM06501RequestDTO>((SqlConnection)loConn, "#STAFF", loObject);

                        lcQuery = "EXECUTE RSP_LM_VALIDATE_UPLOAD_STAFF @CCOMPANY_ID, @CPROPERTY_ID, @CUSER_ID, @CKEY_GUID";
                        loCmd.CommandText = lcQuery;

                        loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                        loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                        loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

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

                    TransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        public List<LMM06501ErrorValidateDTO> GetErrorProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<LMM06501ErrorValidateDTO> loResult = null;
            DbConnection loConn = null;

            try
            {
                loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<LMM06501ErrorValidateDTO>(loDataTableResult).ToList();
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
            }
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
