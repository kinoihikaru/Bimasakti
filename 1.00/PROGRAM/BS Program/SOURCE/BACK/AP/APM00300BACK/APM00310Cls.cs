using APM00300COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace APM00300BACK
{
    public class APM00310Cls : R_BusinessObject<APM00310DTO>
    {
        private LoggerAPM00310 _Logger;
        public APM00310Cls()
        {
            _Logger = LoggerAPM00310.R_GetInstanceLogger();
        }

        protected override void R_Deleting(APM00310DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_AP_DELETE_SUPPLIER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poEntity.CREC_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_AP_DELETE_SUPPLIER {@poParameter}", loDbParam);

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
            }
            loEx.ThrowExceptionIfErrors();
        }

        protected override APM00310DTO R_Display(APM00310DTO poEntity)
        {
            var loEx = new R_Exception();
            APM00310DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_AP_GET_SUPPLIER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_SUPPLIER {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<APM00310DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(APM00310DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";

                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";

                }

                lcQuery = "RSP_AP_SAVE_SUPPLIER";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 20, poNewEntity.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_NAME", DbType.String, 100, poNewEntity.CSUPPLIER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 50, poNewEntity.CJRNGRP_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 20, poNewEntity.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, poNewEntity.CCATEGORY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 20, poNewEntity.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPAY_TERM_CODE", DbType.String, 20, poNewEntity.CPAY_TERM_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 20, poNewEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLOB_CODE", DbType.String, 20, poNewEntity.CLOB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LONETIME", DbType.Boolean, 20, poNewEntity.LONETIME);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 20, poNewEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CDELIVERY_OPTION", DbType.String, 50, poNewEntity.CDELIVERY_OPTION);
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
                    _Logger.LogDebug("EXEC RSP_AP_SAVE_SUPPLIER {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loResult = R_Utility.R_ConvertTo<APM00310DTO>(loDataTable).FirstOrDefault();

                    _Logger.LogInfo("Set CREC_ID IF ADD Data");
                    poNewEntity.CREC_ID = loResult.CREC_ID;
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

        public List<APM00300PayTermDTO> GetAllPayTerm(APM00300PayTermDTO poEntity)
        {
            var loEx = new R_Exception();
            List<APM00300PayTermDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PAYMENT_TERM_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PAYMENT_TERM_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00300PayTermDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<APM00300CurrencyDTO> GetAllCurrency(APM00300CurrencyDTO poEntity)
        {
            var loEx = new R_Exception();
            List<APM00300CurrencyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_CURRENCY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_CURRENCY_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00300CurrencyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<APM00300TaxTypeDTO> GetAllTaxType(APM00300TaxTypeDTO poEntity)
        {
            var loEx = new R_Exception();
            List<APM00300TaxTypeDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCODE ,CDESCRIPTION AS CNAME " +
                    "FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', @CCOMPANY_ID, '_BS_TAX_FOR_TYPE', '', @CLANGUANGE_ID) ORDER BY CCODE";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUANGE_ID", DbType.String, 50, poEntity.CLANGUANGE_ID);

                //Debug Logs
                string loCompanyIdLog = null;
                string loLanIdLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLANGUANGE_ID":
                            loLanIdLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE ,CDESCRIPTION AS CNAME FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0}, '_BS_TAX_FOR_TYPE', '', {1}) ORDER BY CCOD", loCompanyIdLog, loLanIdLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00300TaxTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
