using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using LMM03500COMMON.DTOs.LMM03501;
using System.Data.SqlClient;
using LMM03500COMMON;

namespace LMM03500BACK
{
    public class UploadTenantCls : R_IBatchProcess
    {
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
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }


        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Db loDb = new R_Db();
            string lcQuery;
            R_Exception loException = new R_Exception();
            int liFinishFlag;
            string PropertyId;
            
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadTenantDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_TENANT_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();


                List<UploadTenantSaveDTO> loParam = new List<UploadTenantSaveDTO>();

                loParam = loObject.Select(item => new UploadTenantSaveDTO()
                {
                    NO = item.NO,
                    TenantId = item.TenantId,
                    TenantName = item.TenantName,
                    Address = item.Address,
                    City = item.City,
                    Province = item.Province,
                    Country = item.Country,
                    ZipCode = item.ZipCode,
                    Email = item.Email,
                    PhoneNo1 = item.PhoneNo1,
                    PhoneNo2 = item.PhoneNo2,
                    TenantGroup = item.TenantGroup,
                    TenantCategory = item.TenantCategory,
                    TenantType = item.TenantType,
                    JournalGroup = item.JournalGroup,
                    PaymentTerm = item.PaymentTerm,
                    Currency = item.Currency,
                    Salesman = item.Salesman,
                    LineOfBusiness = item.LineOfBusiness,
                    FamilyCard = item.FamilyCard
                }).ToList();

                lcQuery = "CREATE TABLE #TENANT " +
                    "(NO INT, " +
                    "TenantId VARCHAR(20), " +
                    "TenantName NVARCHAR(100), " +
                    "Address NVARCHAR(255), " +
                    "City VARCHAR(20), " +
                    "Province VARCHAR(20), " +
                    "Country VARCHAR(20), " +
                    "ZipCode VARCHAR(20), " +
                    "Email VARCHAR(100), " +
                    "PhoneNo1 VARCHAR(30), " +
                    "PhoneNo2 VARCHAR(30), " +
                    "TenantGroup VARCHAR(20), " +
                    "TenantCategory VARCHAR(20), " +
                    "TenantType VARCHAR(20), " +
                    "JournalGroup VARCHAR(8), " +
                    "PaymentTerm VARCHAR(8), " +
                    "Currency CHAR(3), " +
                    "Salesman VARCHAR(8), " +
                    "LineOfBusiness VARCHAR(20), " +
                    "FamilyCard VARCHAR(40))";


                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadTenantSaveDTO>((SqlConnection)loConn, "#TENANT", loParam);

                lcQuery = "EXEC RSP_LM_UPLOAD_TENANT " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CUSER_ID, " +
                    "@KEY_GUID, " +
                    "@CCUSTOMER_TYPE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_TYPE", DbType.String, 50, "01");

                loCmd.CommandText = lcQuery;
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
            }
        }
    }
}
