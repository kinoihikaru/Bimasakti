using APM00300COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace APM00300BACK
{
    public class APM00320Cls
    {
        private LoggerAPM00320 _Logger;
        public APM00320Cls()
        {
            _Logger = LoggerAPM00320.R_GetInstanceLogger();
        }

        public APM00320DTO GetSupplierInfo(APM00320DTO poEntity)
        {
            var loEx = new R_Exception();
            APM00320DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_SUPPLIER_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_SUPPLIER_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<APM00320DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<APM00321DTO> GetSupplierSeq(APM00321PARAMDTO poEntity)
        {
            var loEx = new R_Exception();
            List<APM00321DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_SUPPLIER_SEQ_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_REC_ID", DbType.String, 50, poEntity.CSUPPLIER_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_SUPPLIER_SEQ_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00321DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public APM00320DTO SaveSupplierInfo(APM00320DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();
            APM00320DTO loRtn = poNewEntity;

            try
            {
                // set action 
                poNewEntity.CACTION = "EDIT";

                lcQuery = "RSP_AP_SAVE_SUPPLIER_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 20, poNewEntity.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_REC_ID", DbType.String, 255, poNewEntity.CSUPPLIER_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 100, poNewEntity.CSUPPLIER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, int.MaxValue, poNewEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CCITY_CODE", DbType.String, 20, poNewEntity.CCITY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSTATE_CODE", DbType.String, 20, poNewEntity.CSTATE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCOUNTRY_CODE", DbType.String, 20, poNewEntity.CCOUNTRY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPOSTAL_CODE", DbType.String, 20, poNewEntity.CPOSTAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE1", DbType.String, 20, poNewEntity.CPHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE2", DbType.String, 20, poNewEntity.CPHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CFAX1", DbType.String, 20, poNewEntity.CFAX1);
                loDb.R_AddCommandParameter(loCmd, "@CFAX2", DbType.String, 20, poNewEntity.CFAX2);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL1", DbType.String, 100, poNewEntity.CEMAIL1);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL2", DbType.String, 100, poNewEntity.CEMAIL2);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_NAME1", DbType.String, 100, poNewEntity.CCONTACT_NAME1);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_PHONE1", DbType.String, 20, poNewEntity.CCONTACT_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_EMAIL1", DbType.String, 100, poNewEntity.CCONTACT_EMAIL1);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_POSITION1", DbType.String, 100, poNewEntity.CCONTACT_POSITION1);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_NAME2", DbType.String, 100, poNewEntity.CCONTACT_NAME2);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_PHONE2", DbType.String, 20, poNewEntity.CCONTACT_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_EMAIL2", DbType.String, 100, poNewEntity.CCONTACT_EMAIL2);
                loDb.R_AddCommandParameter(loCmd, "@CCONTACT_POSITION2", DbType.String, 100, poNewEntity.CCONTACT_POSITION2);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE", DbType.String, 20, poNewEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 20, poNewEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_REG_ID", DbType.String, 20, poNewEntity.CTAX_REG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_REG_DATE", DbType.String, 20, poNewEntity.CTAX_REG_DATE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_AP_SAVE_SUPPLIER_INFO {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loResult = R_Utility.R_ConvertTo<APM00320DTO>(loDataTable).FirstOrDefault();
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

            return loRtn;
        }

    }
}
