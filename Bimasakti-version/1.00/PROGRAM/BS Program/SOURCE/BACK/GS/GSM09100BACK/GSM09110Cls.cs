using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using GSM09100COMMON;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace GSM09100BACK
{
    public class GSM09110Cls 
    {
        private LoggerGSM09110 _Logger;
        private readonly ActivitySource _activitySource;

        public GSM09110Cls()
        {
            _Logger = LoggerGSM09110.R_GetInstanceLogger();
            _activitySource = GSM09110ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<GSM09110DTO> GetProductList(GSM09110DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetProductList");
            var loEx = new R_Exception();
            List<GSM09110DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PRODUCT_BY_CATEGORY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 50, poEntity.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PRODUCT_BY_CATEGORY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM09110DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void MoveProduct(GSM09120DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("MoveProduct");
            var loEx = new R_Exception();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            var loDb = new R_Db();

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_MOVE_CATEGORY";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, ContextConstant.VAR_CATEGORY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_CATEGORY", DbType.String, 50, poEntity.CFROM_CATEGORY);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CATEGORY", DbType.String, 50, poEntity.CTO_CATEGORY);
                loDb.R_AddCommandParameter(loCmd, "@CID_LIST", DbType.String, 100, poEntity.CID_LIST);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GS_MOVE_CATEGORY {@poParameter}", loDbParam);

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