﻿using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Transactions;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs;

namespace GSM02500BACK
{
    public class UploadUnitCls : R_IBatchProcess
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
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_Exception loException = new R_Exception();
            string lcQuery;

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadUnitDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_BUILDING_ID_CONTEXT)).FirstOrDefault().Value;
                string BuildingId = ((System.Text.Json.JsonElement)loVar1).GetString();
                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_FLOOR_ID_CONTEXT)).FirstOrDefault().Value;
                string FloorId = ((System.Text.Json.JsonElement)loVar2).GetString();


                List<UploadUnitSaveDTO> loParam = new List<UploadUnitSaveDTO>();

                loParam = loObject.Select(item => new UploadUnitSaveDTO()
                {
                    NO = item.No,
                    UnitId = item.UnitId,
                    UnitName = item.UnitName,
                    UnitType = item.UnitType,
                    UnitView = item.UnitView,
                    GrossSize = item.GrossSize,
                    NetSize = item.NetSize,
                    CommonArea = item.CommonArea,
                    UnitCategory = item.UnitCategory,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                lcQuery = $"CREATE TABLE #UNIT(NO INT , " +
                    $"UnitId VARCHAR(20), " +
                    $"UnitName VARCHAR(100), " +
                    $"UnitType VARCHAR(20) , " +
                    $"UnitView VARCHAR(20) , " +
                    $"GrossSize NUMERIC(5,2) , " +
                    $"NetSize NUMERIC(5,2) , " +
                    $"CommonArea NUMERIC(5,2) , " +
                    $"UnitCategory VARCHAR(2) , " +
                    $"Active BIT , " +
                    $"NonActiveDate VARCHAR(8))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadUnitSaveDTO>((SqlConnection)loConn, "#UNIT", loParam);

                lcQuery = $"EXEC RSP_GS_UPLOAD_BUILDING_UNIT " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CBUILDING_ID, " +
                    $"@CFLOOR_ID, " +
                    $"@CUSER_ID, " +
                    $"@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, BuildingId);
                loDb.R_AddCommandParameter(loCmd, "@CFLOOR_ID", DbType.String, 50, FloorId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

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
                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                            $"VALUES " +
                            $"('{poBatchProcessPar.Key.COMPANY_ID}', " +
                            $"'{poBatchProcessPar.Key.USER_ID}', " +
                            $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                            $"-100, " +
                            $"'{loException.ErrorList[0].ErrDescp}');";

                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                    $"'{poBatchProcessPar.Key.USER_ID}', " +
                    $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                    $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
