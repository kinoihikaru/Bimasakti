using LMM02500COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using R_CommonFrontBackAPI;
using System.Reflection;

namespace LMM02500BACK
{
    public class LMM02510Cls : R_BusinessObject<LMM02510DTO>
    {
        private LoggerLMM02510 _Logger;
        public LMM02510Cls()
        {
            _Logger = LoggerLMM02510.R_GetInstanceLogger();
        }

        protected override void R_Deleting(LMM02510DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                //Set Action
                poEntity.CACTION = "DELETE";

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 20, poEntity.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_NAME", DbType.String, 100, poEntity.CTENANT_GROUP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 255, poEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE1", DbType.String, 30, poEntity.CPHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE2", DbType.String, 30, poEntity.CPHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 100, poEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_NAME", DbType.String, 100, poEntity.CPIC_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_POSITION", DbType.String, 100, poEntity.CPIC_POSITION);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_EMAIL", DbType.String, 100, poEntity.CPIC_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_MOBILE_PHONE1", DbType.String, 30, poEntity.CPIC_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_MOBILE_PHONE2", DbType.String, 30, poEntity.CPIC_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_GROUP_TAX", DbType.Boolean, 10, poEntity.LUSE_GROUP_TAX);

                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE", DbType.String, 2, poEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 40, poEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 100, poEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LPPH", DbType.Boolean, 10, poEntity.LPPH);
                loDb.R_AddCommandParameter(loCmd, "@CID_NO", DbType.String, 40, poEntity.CID_NO);
                loDb.R_AddCommandParameter(loCmd, "@CID_TYPE", DbType.String, 2, poEntity.CID_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CID_EXPIRED_DATE", DbType.String, 8, poEntity.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CODE", DbType.String, 20, poEntity.CTAX_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 100, poEntity.NTAX_CODE_LIMIT_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ADDRESS", DbType.String, 255, poEntity.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE1", DbType.String, 30, poEntity.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE2", DbType.String, 30, poEntity.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL", DbType.String, 100, poEntity.CTAX_EMAIL);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT_GRP {@Parameters} || R_Deleting(Cls) ", loDbParam);

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
                _Logger.LogError(loException);
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

            loException.ThrowExceptionIfErrors();
        }
        protected override LMM02510DTO R_Display(LMM02510DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM02510DTO loResult = null;
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            DataTable loDataTable = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 50, poEntity.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Get Param Log
                var loDbParam = loCmd.Parameters.Cast<DbParameter>().Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                lcQuery = "RSP_LM_GET_TENANT_GROUP_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                // Log Profile Debug
                _Logger.LogDebug("EXEC RSP_LM_GET_TENANT_GROUP_DETAIL {@Parameters}", loDbParam);

                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<LMM02510DTO>(loDataTable).FirstOrDefault();

                if (loResult.LUSE_GROUP_TAX)
                {
                    lcQuery = "RSP_LM_GET_TENANT_GROUP_TAX_INFO";
                    loCmd.CommandText = lcQuery;
                    loCmd.CommandType = CommandType.StoredProcedure;

                    // Log Tax Info Debug
                    _Logger.LogDebug("EXEC RSP_LM_GET_TENANT_GROUP_TAX_INFO {@Parameters}", loDbParam);
                    loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    var loTempResult = R_Utility.R_ConvertTo<LMM02510DTO>(loDataTable).FirstOrDefault();

                    loResult.CTAX_TYPE = loTempResult.CTAX_TYPE;
                    loResult.CTAX_TYPE_DESCR = loTempResult.CTAX_TYPE_DESCR;
                    loResult.LPPH = loTempResult.LPPH;
                    loResult.CTAX_ID = loTempResult.CTAX_ID;
                    loResult.CTAX_NAME = loTempResult.CTAX_NAME;
                    loResult.CID_TYPE = loTempResult.CID_TYPE;
                    loResult.CID_TYPE_DESCR = loTempResult.CID_TYPE_DESCR;
                    loResult.CID_NO = loTempResult.CID_NO;
                    loResult.CID_EXPIRED_DATE = loTempResult.CID_EXPIRED_DATE;
                    loResult.CTAX_CODE = loTempResult.CTAX_CODE;
                    loResult.CTAX_CODE_DESCR = loTempResult.CTAX_CODE_DESCR;
                    loResult.NTAX_CODE_LIMIT_AMOUNT = loTempResult.NTAX_CODE_LIMIT_AMOUNT;
                    loResult.CTAX_ADDRESS = loTempResult.CTAX_ADDRESS;
                    loResult.CTAX_EMAIL = loTempResult.CTAX_EMAIL;
                    loResult.CTAX_PHONE1 = loTempResult.CTAX_PHONE1;
                    loResult.CTAX_PHONE2 = loTempResult.CTAX_PHONE2;
                }
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

            return loResult;
        }
        protected override void R_Saving(LMM02510DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                //Set Action
                poNewEntity.CACTION = poCRUDMode == eCRUDMode.AddMode ? "ADD" : poCRUDMode == eCRUDMode.EditMode ? "EDIT" : "";

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_LM_MAINTAIN_TENANT_GRP";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 20, poNewEntity.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_NAME", DbType.String, 100, poNewEntity.CTENANT_GROUP_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 255, poNewEntity.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE1", DbType.String, 30, poNewEntity.CPHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE2", DbType.String, 30, poNewEntity.CPHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 100, poNewEntity.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_NAME", DbType.String, 100, poNewEntity.CPIC_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_POSITION", DbType.String, 100, poNewEntity.CPIC_POSITION);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_EMAIL", DbType.String, 100, poNewEntity.CPIC_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_MOBILE_PHONE1", DbType.String, 30, poNewEntity.CPIC_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPIC_MOBILE_PHONE2", DbType.String, 30, poNewEntity.CPIC_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_GROUP_TAX", DbType.Boolean, 10, poNewEntity.LUSE_GROUP_TAX);

                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE", DbType.String, 2, poNewEntity.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 40, poNewEntity.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 100, poNewEntity.CTAX_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LPPH", DbType.Boolean, 10, poNewEntity.LPPH);
                loDb.R_AddCommandParameter(loCmd, "@CID_NO", DbType.String, 40, poNewEntity.CID_NO);
                loDb.R_AddCommandParameter(loCmd, "@CID_TYPE", DbType.String, 2, poNewEntity.CID_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CID_EXPIRED_DATE", DbType.String, 8, poNewEntity.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CODE", DbType.String, 20, poNewEntity.CTAX_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 100, poNewEntity.NTAX_CODE_LIMIT_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ADDRESS", DbType.String, 255, poNewEntity.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE1", DbType.String, 30, poNewEntity.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE2", DbType.String, 30, poNewEntity.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL", DbType.String, 100, poNewEntity.CTAX_EMAIL);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT_GRP {@Parameters} || R_Saving(Cls) ", loDbParam);

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
                _Logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }
    }
}