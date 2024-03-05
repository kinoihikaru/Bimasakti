using APT00600COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using R_CommonFrontBackAPI;

namespace APT00600BACK
{
    public class APT00610Cls : R_BusinessObject<APT00610DTO>
    {
        private LoggerAPT00610 _Logger;
        private readonly ActivitySource _activitySource;

        public APT00610Cls()
        {
            _Logger = LoggerAPT00610.R_GetInstanceLogger();
            _activitySource = APT00610ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<APT00610DTO> GetAllPurchaseAdjustmentAlloc(APT00610DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllPurchaseAdjustmentAlloc");
            var loEx = new R_Exception();
            List<APT00610DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_PURCHASE_ADJUSTMENT_ALLOC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 100, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_PURCHASE_ADJUSTMENT_ALLOC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APT00610DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override APT00610DTO R_Display(APT00610DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            APT00610DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_PURCHASE_ADJUSTMENT_ALLOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE) ? "" : poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CTRANS_CODE) ? "" : poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 100, string.IsNullOrWhiteSpace(poEntity.CREF_NO) ? "" : poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_PURCHASE_ADJUSTMENT_ALLOC {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<APT00610DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(APT00610DTO poNewEntity, eCRUDMode poCRUDMode)
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
                    poNewEntity.CALLOC_ID = "";
                    poNewEntity.CALLOC_NO = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                lcQuery = "RSP_AP_SAVE_PURCHASE_ADJUSTMENT_ALLOC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poNewEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CFR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CALLOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 10, poNewEntity.CFR_REF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 20, poNewEntity.CFR_SUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_SEQ_NO", DbType.String, 20, poNewEntity.CFR_SUPPLIER_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, 10, "AP");
                loDb.R_AddCommandParameter(loCmd, "@CFR_REC_ID", DbType.String, 100, poNewEntity.CFR_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, 100, poNewEntity.CFR_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFR_TRANS_CODE", DbType.String, 20, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFR_REF_NO", DbType.String, 50, poNewEntity.CFR_REF_NO);

                loDb.R_AddCommandParameter(loCmd, "@CTO_REC_ID", DbType.String, 100, poNewEntity.CTO_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 30, poNewEntity.CTO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_TRANS_CODE", DbType.String, 20, poNewEntity.CTO_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_REF_NO", DbType.String, 50, poNewEntity.CTO_REF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CTO_CURRENCY_CODE", DbType.String, 20, poNewEntity.CTO_CURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_AP_AMOUNT", DbType.Decimal, 100, poNewEntity.NTO_AP_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_AMOUNT", DbType.Decimal, 100, poNewEntity.NTO_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NTO_DISC_AMOUNT", DbType.Decimal, 100, 0);
                loDb.R_AddCommandParameter(loCmd, "@NTO_LBASE_RATE", DbType.Decimal, 100, poNewEntity.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_LCURRENCY_RATE", DbType.Decimal, 100, poNewEntity.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_BBASE_RATE", DbType.Decimal, 100, poNewEntity.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_BCURRENCY_RATE", DbType.Decimal, 100, poNewEntity.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_BASE_RATE", DbType.Decimal, 100, poNewEntity.NTAX_BASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTO_TAX_CURRENCY_RATE", DbType.Decimal, 100, poNewEntity.NTAX_CURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DEPT_CODE", DbType.String, 50, poNewEntity.CCHARGES_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_AP_SAVE_PURCHASE_ADJUSTMENT_ALLOC {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loResult = R_Utility.R_ConvertTo<APT00600DTO>(loDataTable).FirstOrDefault();

                    _Logger.LogInfo("Set CREC_ID IF ADD Data");
                    poNewEntity.CALLOC_ID = loResult.CREC_ID;
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

        protected override void R_Deleting(APT00610DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_AP_DELETE_ALLOCATION";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poEntity.CALLOC_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_AP_DELETE_ALLOCATION {@poParameter}", loDbParam);

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