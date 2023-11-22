using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APM00200COMMON.DTOs.APM00200;
using System.Data.SqlClient;
using APM00200COMMON.DTOs;

namespace APM00200BACK
{
    public class UploadExpenditureCls : R_IBatchProcess
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
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            DbConnection loConn = null;
            DbCommand loCmd = null;
            UploadExpenditureErrorDTO loErrorResult = null;

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadExpenditureDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_EXPENDITURE_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                List<UploadExpenditureSaveBulkDTO> loParam = new List<UploadExpenditureSaveBulkDTO>();

                loParam = loObject.Select(item => new UploadExpenditureSaveBulkDTO()
                {
                    INO = item.No,
                    CEXPENDITURE_ID = item.Expenditure_Id,
                    CEXPENDITURE_NAME = item.Expenditure_Name,
                    CEXPENDITURE_DESC = item.Expenditure_Description,
                    CCATEGORY_ID = item.Category_Id,
                    CJRNGRP_CODE = item.Journal_Group_Id,
                    CUNIT = item.Unit,
                    CTAXABLE = item.Taxable,
                    CTAX_ID = item.Tax_ID
                }).ToList();

                lcQuery = "CREATE TABLE #APM00200_EXPENDITURE_UPLOAD " +
                    "(INO INT NOT NULL DEFAULT(0), " +
                    "CEXPENDITURE_ID VARCHAR(10) NOT NULL DEFAULT(''), " +
                    "CEXPENDITURE_NAME VARCHAR(20) NOT NULL DEFAULT(''), " +
                    "CEXPENDITURE_DESC NVARCHAR(100) NOT NULL DEFAULT(''), " +
                    "CCATEGORY_ID VARCHAR(10) NOT NULL DEFAULT(''), " +
                    "CJRNGRP_CODE VARCHAR(10) NOT NULL DEFAULT(''), " +
                    "CUNIT VARCHAR(10) NOT NULL DEFAULT(''), " +
                    "CTAXABLE VARCHAR(10) NOT NULL DEFAULT(''), " +
                    "CTAX_ID VARCHAR(20) NOT NULL DEFAULT(''));";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadExpenditureSaveBulkDTO>((SqlConnection)loConn, "#APM00200_EXPENDITURE_UPLOAD", loParam);

                lcQuery = "EXEC RSP_AP_VALIDATE_EXPENDITURE_UPLOAD " +
                    "@USER_ID, " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " + 
                    "@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@USER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loErrorResult = R_Utility.R_ConvertTo<UploadExpenditureErrorDTO>(loDataTable).FirstOrDefault();

                if (loErrorResult.IERROR_COUNT == 0)
                {
                    lcQuery = "EXEC RSP_AP_SAVE_EXPENDITURE_UPLOAD " +
                        "@USER_ID, " +
                        "@CCOMPANY_ID, " +
                        "@CPROPERTY_ID, " +
                        "@KEY_GUID";

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
