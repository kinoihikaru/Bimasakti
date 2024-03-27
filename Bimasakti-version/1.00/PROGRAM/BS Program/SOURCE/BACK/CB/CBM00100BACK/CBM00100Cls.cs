using CBM00100COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;
using System.Transactions;

namespace CBM00100BACK
{
    public class CBM00100Cls
    {
        RSP_CB_SAVE_SYSTEM_PARAMResources.Resources_Dummy_Class _loRsp = new RSP_CB_SAVE_SYSTEM_PARAMResources.Resources_Dummy_Class();

        private LoggerCBM00100 _Logger;
        private readonly ActivitySource _activitySource;

        public CBM00100Cls()
        {
            _Logger = LoggerCBM00100.R_GetInstanceLogger();
            _activitySource = CBM00100ActivityInitSourceBase.R_GetInstanceActivitySource();
        }

        public CBM00100ValidateInitDTO GetInitValidate()
        {
            using Activity activity = _activitySource.StartActivity("GetTodayDateDB");
            var loEx = new R_Exception();
            CBM00100ValidateInitDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT TOP 1 1 AS CRESULT FROM CBM_SYSTEM_PARAM (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                string loCompanyIdLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT TOP 1 1 AS CRESULT FROM CBM_SYSTEM_PARAM (NOLOCK) WHERE CCOMPANY_ID = {0}", loCompanyIdLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBM00100ValidateInitDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public CBM00100DTO GetSystemParamCB()
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParamCB");
            var loEx = new R_Exception();
            CBM00100DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_CB_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_CB_GET_SYSTEM_PARAM {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBM00100DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public CBM00100DTO SaveSystemParamCB(CBM00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("SaveSystemParamCB");
            var loEx = new R_Exception();
            CBM00100DTO loResult = null;

            try
            {
                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    SPSaveSystemParamCB(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loResult = GetSystemParamCB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        private void SPSaveSystemParamCB(CBM00100DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                lcQuery = "RSP_CB_SAVE_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, "");
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 10, poNewEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_LINK_DATE", DbType.String, 10, poNewEntity.CCB_LINK_DATE);
                loDb.R_AddCommandParameter(loCmd, "@LINPUT_CHEQUE_DATE", DbType.Boolean, 10, poNewEntity.LINPUT_CHEQUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_IN_MODE", DbType.String, 10, poNewEntity.CBANK_IN_MODE);
                loDb.R_AddCommandParameter(loCmd, "@LALLOW_CANCEL_SOFT_END", DbType.Boolean, 10, poNewEntity.LALLOW_CANCEL_SOFT_END);
                loDb.R_AddCommandParameter(loCmd, "@CCONTRA_ACCOUNT_NO", DbType.String, 20, poNewEntity.CCONTRA_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCRDVG_ACCOUNT_NO", DbType.String, 20, poNewEntity.CCRDVG_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CCRDVL_ACCOUNT_NO", DbType.String, 20, poNewEntity.CCRDVL_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@LCB_NUMBERING", DbType.Boolean, 20, poNewEntity.LCB_NUMBERING);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_CB_SAVE_SYSTEM_PARAM {@poParameter}", loDbParam);

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
    }
}