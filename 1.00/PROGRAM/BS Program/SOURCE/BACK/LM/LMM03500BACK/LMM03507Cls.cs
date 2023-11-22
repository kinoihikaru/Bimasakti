using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON.DTOs.LMM03507;
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
    public class LMM03507Cls : R_BusinessObject<LMM03507ParameterDTO>
    {
        private LoggerLMM03507 _logger;
        public LMM03507Cls()
        {
            _logger = LoggerLMM03507.R_GetInstanceLogger();
        }
        private void RSP_LM_MAINTAIN_TENANT_MEMBERMethod(LMM03507ParameterDTO poEntity)
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
                
                lcQuery = "EXEC RSP_LM_MAINTAIN_TENANT_MEMBER " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CTENANT_ID, " +
                                 "@CMEMBER_SEQ_NO, " +
                                 "@CMEMBER_NAME, " +
                                 "@CGENDER, " +
                                 "@CDATE_BIRTH, " +
                                 "@CRELATIONSHIP, " +
                                 "@CID_TYPE, " +
                                 "@CID_NO, " +
                                 "@CNATIONALITY, " +
                                 "@CEMAIL, " +
                                 "@CMOBILE_PHONE1, " +
                                 "@CMOBILE_PHONE2, " +
                                 "@CACTION, " +
                                 "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poEntity.Data.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMEMBER_SEQ_NO", DbType.String, 50, poEntity.Data.CMEMBER_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CMEMBER_NAME", DbType.String, 50, poEntity.Data.CMEMBER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CGENDER", DbType.String, 50, poEntity.Data.CGENDER);
                loDb.R_AddCommandParameter(loCmd, "@CDATE_BIRTH", DbType.String, 50, poEntity.Data.CDATE_BIRTH);
                loDb.R_AddCommandParameter(loCmd, "@CRELATIONSHIP", DbType.String, 50, poEntity.Data.CRELATIONSHIP);
                loDb.R_AddCommandParameter(loCmd, "@CID_TYPE", DbType.String, 50, poEntity.Data.CID_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CID_NO", DbType.String, 50, poEntity.Data.CID_NO);
                loDb.R_AddCommandParameter(loCmd, "@CNATIONALITY", DbType.String, 50, poEntity.Data.CNATIONALITY);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 50, poEntity.Data.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE1", DbType.String, 50, poEntity.Data.CMOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CMOBILE_PHONE2", DbType.String, 50, poEntity.Data.CMOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT_MEMBER {@Parameters} || RSP_LM_MAINTAIN_TENANT_MEMBERMethod(Cls) ", loDbParam);

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
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }

        public List<LMM03507DTO> GetMembersList(GetMembersListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<LMM03507DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_MEMBER_LIST " +
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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_MEMBER_LIST {@Parameters} || GetMembersList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM03507DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetIdTypeDTO> GetIdTypeList(GetIdTypeParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetIdTypeDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT * FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_BS_ID_TYPE', " +
                    "'', " +
                    "@CLOGIN_LANGUAGE_ID)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

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
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_BS_ID_TYPE', '' , {1}) || GetIdTypeList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetIdTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(LMM03507ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_LM_MAINTAIN_TENANT_MEMBERMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        public GetAgeDTO GetAge(GetAgeParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetAgeDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT DBO.RFN_GET_AGE (@CDATE_BIRTH, 'YY')";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CDATE_BIRTH", DbType.DateTime, 50, poEntity.CDATE_BIRTH);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("SELECT DBO.RFN_GET_AGE ({@Parameters}, 'YY') || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GetAgeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override LMM03507ParameterDTO R_Display(LMM03507ParameterDTO poEntity)
        {
            LMM03507ParameterDTO loRtn = new LMM03507ParameterDTO();
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_MEMBER_DT " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CTENANT_ID, " +
                                 "@CMEMBER_SEQ_NO, " +
                                 "@CLOGIN_USER_ID";
                
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poEntity.Data.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CMEMBER_SEQ_NO", DbType.String, 50, poEntity.Data.CMEMBER_SEQ_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_MEMBER_DT {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loRtn.Data = R_Utility.R_ConvertTo<LMM03507DetailDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        protected override void R_Saving(LMM03507ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_LM_MAINTAIN_TENANT_MEMBERMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
