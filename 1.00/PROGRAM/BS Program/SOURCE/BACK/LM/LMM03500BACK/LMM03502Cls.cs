using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
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
    public class LMM03502Cls : R_BusinessObject<ProfileTaxParameterDTO>
    {
        private LoggerLMM03502 _logger;
        public LMM03502Cls()
        {
            _logger = LoggerLMM03502.R_GetInstanceLogger();
        }

        public LMM03502DTO GetTenantProfile(LMM03502ParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            LMM03502DTO loResult = null;
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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || GetTenantProfile(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM03502DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GetTenantGroupDTO> GetTenantGroupList(GetTenantGroupParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetTenantGroupDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_GROUP_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_GROUP_LIST {@Parameters} || GetTenantGroupList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTenantGroupDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetTenantCategoryDTO> GetTenantCategoryist(GetTenantCategoryParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetTenantCategoryDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_CATEGORY_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CCATEGORY_TYPE";

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

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_LIST {@Parameters} || GetTenantCategoryist(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTenantCategoryDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetTenantTypeDTO> GetTenantTypeist(GetTenantTypeParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetTenantTypeDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT * " +
                    "FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', " +
                    "@CLOGIN_COMPANY_ID, " +
                    "'_BS_TENANT_TYPE', " +
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
                    "'_BS_TENANT_TYPE', '' , {1}) || GetApprovalList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTenantTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GetLineOfBusinessDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_LOB_LIST 1, 0, 'CODE'";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_LIST 1, 0, 'CODE' || GetTenantCategoryist(Cls) ");

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetLineOfBusinessDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            string lcErrorMessage;
            Exception loError;
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
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_TYPE_ID", DbType.String, 50, poEntity.Profile.CTENANT_TYPE_ID);
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
                loDb.R_AddCommandParameter(loCmd, "@CSALESMAN_ID", DbType.String, 50, poEntity.Profile.CSALESMAN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOB_CODE", DbType.String, 50, poEntity.Profile.CLOB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFAMILY_CARD", DbType.String, 50, poEntity.Profile.CFAMILY_CARD);
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
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_TYPE", DbType.String, 50, "01");
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
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            ProfileTaxParameterDTO loResult = null;
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

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                var loTemp1 = R_Utility.R_ConvertTo<LMM03502DTO>(loDataTable).FirstOrDefault();
                var loTemp2 = R_Utility.R_ConvertTo<LMM03503DTO>(loDataTable).FirstOrDefault();
                loResult = R_Utility.R_ConvertTo<ProfileTaxParameterDTO>(loDataTable).FirstOrDefault();
                loResult.Profile = loTemp1;
                loResult.TaxInfo = loTemp2;
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
