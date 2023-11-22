using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
using LMM03500COMMON.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500BACK
{
    public class LMM03504Cls : R_BusinessObject<LMM03504ParameterDTO>
    {
        private LoggerLMM03504 _logger;
        public LMM03504Cls()
        {
            _logger = LoggerLMM03504.R_GetInstanceLogger();
        }
        public LMM03504DTO GetBilling(GetBillingParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            LMM03504DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_BILLING_INFO " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CSELECTED_TENANT_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_TENANT_ID", DbType.String, 50, poEntity.CSELECTED_TENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_BILLING_INFO {@Parameters} || GetBilling(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM03504DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(LMM03504ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM03504ParameterDTO R_Display(LMM03504ParameterDTO poEntity)
        {
            GetBillingParameterDTO loParam = new GetBillingParameterDTO();
            LMM03504ParameterDTO loRtn = new LMM03504ParameterDTO();
            R_Exception loException = new R_Exception();

            try
            {
                loRtn = poEntity;
                loParam.CSELECTED_TENANT_ID = poEntity.Data.CTENANT_ID;
                loParam.CLOGIN_USER_ID = poEntity.CLOGIN_USER_ID;
                loParam.CSELECTED_PROPERTY_ID = poEntity.CSELECTED_PROPERTY_ID;
                loParam.CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID;
                loRtn.Data = GetBilling(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(LMM03504ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "EXEC RSP_LM_MAINTAIN_TENANT_BILLING_INFO " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CTENANT_ID, " +
                                 "@LBILLING_OVERWRITE, " +
                                 "@CBILLING_TO, " +
                                 "@CBILLING_ADDRESS, " +
                                 "@CBILLING_EMAIL, " +
                                 "@CBILLING_PHONE1, " +
                                 "@CBILLING_PHONE2, " +
                                 "@CBILLING_ATTENTION_NAME, " +
                                 "@CBILLING_ATTENTION_POSITION, " +
                                 "@CCUSTOMER_TYPE, " +
                                 "@CACTION, " +
                                 "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poNewEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poNewEntity.Data.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@LBILLING_OVERWRITE", DbType.Boolean, 1, poNewEntity.Data.LBILLING_OVERWRITE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_TO", DbType.String, 50, poNewEntity.Data.CBILLING_TO);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_ADDRESS", DbType.String, 50, poNewEntity.Data.CBILLING_ADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_EMAIL", DbType.String, 50, poNewEntity.Data.CBILLING_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_PHONE1", DbType.String, 50, poNewEntity.Data.CBILLING_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_PHONE2", DbType.String, 50, poNewEntity.Data.CBILLING_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_ATTENTION_NAME", DbType.String, 50, poNewEntity.Data.CBILLING_ATTENTION_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_ATTENTION_POSITION", DbType.String, 50, poNewEntity.Data.CBILLING_ATTENTION_POSITION);
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_TYPE", DbType.String, 50, poNewEntity.CCUSTOMER_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT_BILLING_INFO {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
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
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
