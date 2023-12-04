using APM00200COMMON.DTOs.APM00200;
using APM00200COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APM00200BACK
{
    public class APM00200Cls : R_BusinessObject<APM00200ParameterDTO>
    {
        private LoggerAPM00200 _logger;
        public APM00200Cls()
        {
            _logger = LoggerAPM00200.R_GetInstanceLogger();
        }

        public List<GetWithholdingTaxTypeDTO> GetWithholdingTaxType(GetWithholdingTaxTypeParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetWithholdingTaxTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_GSB_CODE_LIST " +
                    "'BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_BS_TAX_TYPE', " +
                    "@CLOGIN_LANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poParameter.CLOGIN_LANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CLOGIN_COMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLOGIN_LANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("EXEC RSP_GS_GET_GSB_CODE_LIST " +
                    "'BIMASAKTI', {0}, " +
                    "'_BS_TAX_TYPE', '', {1} || GetApprovalList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetWithholdingTaxTypeDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }


        public void UpdateExpenditureActiveFlag(UpdateExpenditureActiveFlagParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetPropertyDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_UPDATE_EXPENDITURE_ACTIVE_FLAG " +
                    "@CLOGIN_USER_ID, " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_REC_ID, " +
                    "@LACTIVE";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_REC_ID", DbType.String, 50, poParameter.CSELECTED_REC_ID);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poParameter.LACTIVE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_UPDATE_EXPENDITURE_ACTIVE_FLAG {@Parameters} || UpdateExpenditureActiveFlag(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        public List<GetPropertyDTO> GetPropertyList(GetPropertyParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<GetPropertyDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_PROPERTY_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@Parameters} || GetPropertyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPropertyDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<APM00200DTO> GetExpenditureList(APM00200GetListParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            List<APM00200DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_GET_EXPENDITURE_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CLOGIN_LANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poParameter.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poParameter.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_EXPENDITURE_LIST {@Parameters} || GetExpenditureList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00200DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(APM00200ParameterDTO poEntity)
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

                lcQuery = "EXEC RSP_AP_DELETE_EXPENDITURE @CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_DELETE_EXPENDITURE {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }

        public APM00200DetailDTO GetSelectedExpenditure(GetSelectedExpenditureParameterDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            APM00200DetailDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_AP_GET_EXPENDITURE " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CREC_ID, " +
                    "@CLOGIN_LANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poParameter.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_GET_EXPENDITURE {@Parameters} || GetSelectedExpenditure(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<APM00200DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override APM00200ParameterDTO R_Display(APM00200ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            APM00200ParameterDTO loResult = new APM00200ParameterDTO();
            GetSelectedExpenditureParameterDTO loParam = null;

            try
            {
                loParam = new GetSelectedExpenditureParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLOGIN_LANGUAGE_ID = poEntity.CLOGIN_LANGUAGE_ID,
                    CREC_ID = poEntity.Data.CREC_ID
                };

                loResult.Data = GetSelectedExpenditure(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(APM00200ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            APM00200SaveResultDTO loResult = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_AP_SAVE_EXPENDITURE " +
                                 "@CLOGIN_USER_ID, " +
                                 "@CACTION, " +
                                 "@CREC_ID, " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CEXPENDITURE_ID, " +
                                 "@CEXPENDITURE_NAME, " +
                                 "@CEXPENDITURE_DESC, " +
                                 "@CJRNGRP_CODE, " +
                                 "@CCATEGORY_ID, " +
                                 "@COTHER_TAX_TYPE, " +
                                 "@COTHER_TAX_ID, " +
                                 "@LTAXABLE, " +
                                 "@LWITHHOLDING_TAX, " +
                                 "@CWITHHOLDING_TAX_TYPE, " +
                                 "@CWITHHOLDING_TAX_ID, " +
                                 "@CUNIT, " +
                                 "@LACTIVE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poNewEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_ID", DbType.String, 50, poNewEntity.Data.CEXPENDITURE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_NAME", DbType.String, 50, poNewEntity.Data.CEXPENDITURE_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_DESC", DbType.String, 50, poNewEntity.Data.CEXPENDITURE_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, poNewEntity.Data.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 50, poNewEntity.Data.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_TYPE", DbType.String, 50, poNewEntity.Data.COTHER_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.Data.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poNewEntity.Data.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@LWITHHOLDING_TAX", DbType.Boolean, 50, poNewEntity.Data.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_TYPE", DbType.String, 50, poNewEntity.Data.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_ID", DbType.String, 50, poNewEntity.Data.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT", DbType.String, 50, poNewEntity.Data.CUNIT);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 50, poNewEntity.Data.LACTIVE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_AP_SAVE_EXPENDITURE {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<APM00200SaveResultDTO>(loDataTable).FirstOrDefault();

                    poNewEntity.Data.CREC_ID = loResult.CREC_ID;
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }
    }
}
