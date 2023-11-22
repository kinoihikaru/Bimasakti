using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.Loggers;
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

namespace LMM04000BACK
{
    public class LMM04010Cls : R_BusinessObject<ProfileTaxParameterDTO>
    {
        private LoggerLMM04010 _logger;
        public LMM04010Cls()
        {
            _logger = LoggerLMM04010.R_GetInstanceLogger();
        }
        public List<GetCurrencyDTO> GetCurrencyList()
        {
            R_Exception loException = new R_Exception();
            List<GetCurrencyDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CCURRENCY_CODE, CCURRENCY_NAME " +
                    "FROM GSM_CURRENCY (NOLOCK)";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                _logger.LogDebug("SELECT CCURRENCY_CODE, CCURRENCY_NAME FROM GSM_CURRENCY (NOLOCK)");

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrencyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public LMM04010DTO GetContractorProfile(LMM04010ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM04010DTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_DETAIL " +
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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || GetContractorProfile(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM04010DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GetContractorGroupDTO> GetContractorGroupList(GetContractorGroupParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetContractorGroupDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_GROUP_LIST @CLOGIN_COMPANY_ID, @CSELECTED_PROPERTY_ID, @CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_GROUP_LIST {@Parameters} || GetContractorGroupList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetContractorGroupDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetContractorCategoryDTO> GetContractorCategoryist(GetContractorCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetContractorCategoryDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                
                lcQuery = "EXEC RSP_GS_GET_CATEGORY_LIST @CLOGIN_COMPANY_ID, @CSELECTED_PROPERTY_ID, @CLOGIN_USER_ID, @CCATEGORY_TYPE";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "20");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_LIST {@Parameters} || GetContractorCategoryist(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetContractorCategoryDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetJournalGroupDTO> GetJournalGroupList(GetJournalGroupParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetJournalGroupDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CJRNGRP_CODE, CJRNGRP_NAME " +
                    "FROM GSM_JRNGRP (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CLOGIN_COMPANY_ID " +
                    "AND CPROPERTY_ID  = @CSELECTED_PROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetJournalGroupDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetPaymentTermDTO> GetPaymentTermList(GetPaymentTermParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetPaymentTermDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CPAY_TERM_CODE, CPAY_TERM_NAME " +
                    "FROM GSM_PAYMENT_TERM (NOLOCK) " +
                    "WHERE CCOMPANY_ID = @CLOGIN_COMPANY_ID " +
                    "AND CPROPERTY_ID  = @CSELECTED_PROPERTY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPaymentTermDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            R_Exception loException = new R_Exception();
            List<GetLineOfBusinessDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CLOB_CODE, CLOB_NAME " +
                    "FROM SAM_LOB (NOLOCK) " +
                    "WHERE LACTIVE_FLAG = 1";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetLineOfBusinessDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        private void RSP_LM_MAINTAIN_TENANTMethod(ProfileTaxParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                lcQuery = "EXEC RSP_LM_MAINTAIN_TENANT " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CTENANT_ID, " +
                                 "@CTENANT_NAME, " +
                                 "@CADDRESS, " +
                                 "@CCITY_CODE, " +
                                 "@CSTATE_CODE, " +
                                 "@CCOUNTRY_CODE, " +
                                 "@CPOSTAL_CODE, " +
                                 "@CEMAIL, " +
                                 "@CPHONE1, " +
                                 "@CPHONE2, " +
                                 "@CTENANT_GROUP_ID, " +
                                 "@CTENANT_CATEGORY_ID, " +
                                 "@CTENANT_TYPE_ID, " +
                                 "@CATTENTION1_NAME, " +
                                 "@CATTENTION1_POSITION, " +
                                 "@CATTENTION1_EMAIL, " +
                                 "@CATTENTION1_MOBILE_PHONE1, " +
                                 "@CATTENTION1_MOBILE_PHONE2, " +
                                 "@CATTENTION2_NAME, " +
                                 "@CATTENTION2_POSITION, " +
                                 "@CATTENTION2_EMAIL, " +
                                 "@CATTENTION2_MOBILE_PHONE1, " +
                                 "@CATTENTION2_MOBILE_PHONE2, " +
                                 "@CJRNGRP_CODE, " +
                                 "@CPAYMENT_TERM_CODE, " +
                                 "@CCURRENCY_CODE, " +
                                 "@CSALESMAN_ID, " +
                                 "@CLOB_CODE, " +
                                 "@CFAMILY_CARD, " +
                                 "@CTAX_TYPE, " +
                                 "@LPPH, " +
                                 "@CTAX_ID, " +
                                 "@CTAX_NAME, " +
                                 "@CID_NO, " +
                                 "@CID_TYPE, " +
                                 "@CID_EXPIRED_DATE, " +
                                 "@CTAX_CODE, " +
                                 "@NTAX_CODE_LIMIT_AMOUNT, " +
                                 "@CTAX_ADDRESS, " +
                                 "@CTAX_PHONE1, " +
                                 "@CTAX_PHONE2, " +
                                 "@CTAX_EMAIL, " +
                                 "@CCUSTOMER_TYPE, " +
                                 "@CACTION, " +
                                 "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poEntity.Profile.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_NAME", DbType.String, 50, poEntity.Profile.CTENANT_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CADDRESS", DbType.String, 50, poEntity.Profile.CADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CCITY_CODE", DbType.String, 50, poEntity.Profile.CCITY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSTATE_CODE", DbType.String, 50, poEntity.Profile.CSTATE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCOUNTRY_CODE", DbType.String, 50, poEntity.Profile.CCOUNTRY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPOSTAL_CODE", DbType.String, 50, poEntity.Profile.CPOSTAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CEMAIL", DbType.String, 50, poEntity.Profile.CEMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE1", DbType.String, 50, poEntity.Profile.CPHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CPHONE2", DbType.String, 50, poEntity.Profile.CPHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 50, poEntity.Profile.CTENANT_GROUP_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_CATEGORY_ID", DbType.String, 50, poEntity.Profile.CTENANT_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_TYPE_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION1_NAME", DbType.String, 50, poEntity.Profile.CATTENTION1_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION1_POSITION", DbType.String, 50, poEntity.Profile.CATTENTION1_POSITION);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION1_EMAIL", DbType.String, 50, poEntity.Profile.CATTENTION1_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION1_MOBILE_PHONE1", DbType.String, 50, poEntity.Profile.CATTENTION1_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION1_MOBILE_PHONE2", DbType.String, 50, poEntity.Profile.CATTENTION1_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION2_NAME", DbType.String, 50, poEntity.Profile.CATTENTION2_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION2_POSITION", DbType.String, 50, poEntity.Profile.CATTENTION2_POSITION);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION2_EMAIL", DbType.String, 50, poEntity.Profile.CATTENTION2_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION2_MOBILE_PHONE1", DbType.String, 50, poEntity.Profile.CATTENTION2_MOBILE_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CATTENTION2_MOBILE_PHONE2", DbType.String, 50, poEntity.Profile.CATTENTION2_MOBILE_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 50, poEntity.Profile.CJRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPAYMENT_TERM_CODE", DbType.String, 50, poEntity.Profile.CPAYMENT_TERM_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poEntity.Profile.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSALESMAN_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CLOB_CODE", DbType.String, 50, poEntity.Profile.CLOB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFAMILY_CARD", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE", DbType.String, 50, poEntity.TaxInfo.CTAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@LPPH", DbType.Boolean, 10, poEntity.TaxInfo.LPPH);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poEntity.TaxInfo.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_NAME", DbType.String, 50, poEntity.TaxInfo.CTAX_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CID_NO", DbType.String, 50, poEntity.TaxInfo.CID_NO);
                loDb.R_AddCommandParameter(loCmd, "@CID_TYPE", DbType.String, 50, poEntity.TaxInfo.CID_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CID_EXPIRED_DATE", DbType.String, 50, poEntity.TaxInfo.CID_EXPIRED_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_CODE", DbType.String, 50, poEntity.TaxInfo.CTAX_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_CODE_LIMIT_AMOUNT", DbType.Decimal, 10, poEntity.TaxInfo.NTAX_CODE_LIMIT_AMOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ADDRESS", DbType.String, 50, poEntity.TaxInfo.CTAX_ADDRESS);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE1", DbType.String, 50, poEntity.TaxInfo.CTAX_PHONE1);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_PHONE2", DbType.String, 50, poEntity.TaxInfo.CTAX_PHONE2);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EMAIL", DbType.String, 50, poEntity.TaxInfo.CTAX_EMAIL);
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_TYPE", DbType.String, 50, "02");
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_MAINTAIN_TENANT {@Parameters} || RSP_LM_MAINTAIN_TENANTMethod(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    string.Format(ex.Message, "Contractor");
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


        protected override void R_Deleting(ProfileTaxParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            try
            {
                RSP_LM_MAINTAIN_TENANTMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override ProfileTaxParameterDTO R_Display(ProfileTaxParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            ProfileTaxParameterDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_DETAIL " +
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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || RSP_LM_MAINTAIN_TENANTMethod(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<ProfileTaxParameterDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(ProfileTaxParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            try
            {
                RSP_LM_MAINTAIN_TENANTMethod(poNewEntity);
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
