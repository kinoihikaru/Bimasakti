using APT00300COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using R_CommonFrontBackAPI;

namespace APT00300BACK
{
    public class APT00320Cls : R_BusinessObject<APT00311DTO>
    {
        private LoggerAPT00320 _Logger;

        public APT00320Cls()
        {
            _Logger = LoggerAPT00320.R_GetInstanceLogger();
        }

        protected override void R_Deleting(APT00311DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_DELETE_TRANS_PD " +
                          "@CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _Logger.LogDebug("EXEC RSP_AP_DELETE_TRANS_PD {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override APT00311DTO R_Display(APT00311DTO poEntity)
        {
            var loEx = new R_Exception();
            APT00311DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_TRANS_PD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CSEQ_NO", DbType.String, 50, poEntity.CSEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_TRANS_PD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<APT00311DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(APT00311DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00311DTO loResult = null;
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";
                    poNewEntity.CREC_ID = "";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "RSP_AP_SAVE_TRANS_PD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, 50, poNewEntity.CPARENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPROD_DEPT_CODE", DbType.String, 50, poNewEntity.CPROD_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPROD_TYPE", DbType.String, 50, poNewEntity.CPROD_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CPRODUCT_ID", DbType.String, 50, poNewEntity.CPRODUCT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poNewEntity.CALLOC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDETAIL_DESC", DbType.String, 255, poNewEntity.CDETAIL_DESC);
                loDb.R_AddCommandParameter(loCmd, "@IBILL_UNIT", DbType.Int16, 50, poNewEntity.IBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@CBILL_UNIT", DbType.String, 50, poNewEntity.CBILL_UNIT);
                loDb.R_AddCommandParameter(loCmd, "@NSUPP_CONV_FACTOR", DbType.Decimal, 50, poNewEntity.NSUPP_CONV_FACTOR);
                loDb.R_AddCommandParameter(loCmd, "@NBILL_UNIT_QTY", DbType.Decimal, 50, poNewEntity.NTRANS_QTY);
                loDb.R_AddCommandParameter(loCmd, "@NUNIT_PRICE", DbType.Decimal, 50, poNewEntity.NUNIT_PRICE);
                loDb.R_AddCommandParameter(loCmd, "@CDISC_TYPE", DbType.String, 50, poNewEntity.CDISC_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_PCT", DbType.Decimal, 50, poNewEntity.NDISC_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NDISC_AMOUNT", DbType.Decimal, 50, poNewEntity.NDISC_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_PCT", DbType.Decimal, 50, poNewEntity.NTAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_AMOUNT", DbType.Decimal, 50, poNewEntity.NTAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_TAX_PCT", DbType.Decimal, 50, poNewEntity.NOTHER_TAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_TAX_AMOUNT", DbType.Decimal, 50, poNewEntity.NOTHER_TAX_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_DISCOUNT", DbType.Decimal, 50, poNewEntity.NDIST_DISCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@NDIST_ADD_ON", DbType.Decimal, 50, poNewEntity.NDIST_ADD_ON);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_AP_SAVE_TRANS_PD {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<APT00311DTO>(loDataTable).FirstOrDefault();

                    _Logger.LogInfo("Set CREC_ID IF ADD Data");
                    if (poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREC_ID = loResult.CREC_ID;
                    }
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
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}