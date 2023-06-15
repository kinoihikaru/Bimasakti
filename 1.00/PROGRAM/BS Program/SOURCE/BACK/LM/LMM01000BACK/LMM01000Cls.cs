using LMM01000COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace LMM01000BACK
{
    public class LMM01000Cls : R_BusinessObject<LMM01000DTO>
    {
        public List<LMM01000DTOPropety> GetProperty(LMM01000PropertyParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01000DTOPropety> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000DTOPropety>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public LMM01000UniversalDTO GetChargesType(LMM01000UniversalDTO poEntity) 
        {
            var loEx = new R_Exception();
            LMM01000UniversalDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    $"('BIMASAKTI', @CCOMPANY_ID , '_BS_UTILITY_CHARGES_TYPE', @CCODE , @CUSER_LANGUAGE)";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);
                loDb.R_AddCommandParameter(loCmd, "@CCODE", DbType.String, 50, poEntity.CCODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01000UniversalDTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM01002DTO> GetAllChargesUtility(LMM01002DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01002DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_CHARGES_UTILITY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01002DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM01000DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_LM_MAINTAIN_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                poEntity.CACTION = "DELETE";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@LACCRUAL", DbType.Boolean, 50, poEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 50, poEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@ITAX_EXEMPTION_PCT", DbType.Int32, 50, poEntity.ITAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "@LOTHER_TAX", DbType.Boolean, 50, poEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LWITHHOLDING_TAX", DbType.Boolean, 50, poEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_TYPE", DbType.String, 50, poEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_ID", DbType.String, 50, poEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_JRNGRP_CODE", DbType.String, 50, poEntity.CUTILITY_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
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

            loEx.ThrowExceptionIfErrors();
        }

        protected override LMM01000DTO R_Display(LMM01000DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01000DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_CHARGES_UTILITY_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01000DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_LM_MAINTAIN_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;


                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poNewEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poNewEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@LACCRUAL", DbType.Boolean, 50, poNewEntity.LACCRUAL);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poNewEntity.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@LTAX_EXEMPTION", DbType.Boolean, 50, poNewEntity.LTAX_EXEMPTION);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_EXEMPTION_CODE", DbType.String, 50, poNewEntity.CTAX_EXEMPTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@ITAX_EXEMPTION_PCT", DbType.Int32, 50, poNewEntity.ITAX_EXEMPTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "@LOTHER_TAX", DbType.Boolean, 50, poNewEntity.LOTHER_TAX);
                loDb.R_AddCommandParameter(loCmd, "@COTHER_TAX_ID", DbType.String, 50, poNewEntity.COTHER_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@LWITHHOLDING_TAX", DbType.Boolean, 50, poNewEntity.LWITHHOLDING_TAX);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_TYPE", DbType.String, 50, poNewEntity.CWITHHOLDING_TAX_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CWITHHOLDING_TAX_ID", DbType.String, 50, poNewEntity.CWITHHOLDING_TAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUTILITY_JRNGRP_CODE", DbType.String, 50, poNewEntity.CUTILITY_JRNGRP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
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

            loEx.ThrowExceptionIfErrors();
        }

        public void LMM01000ChangeStatusSP(LMM01000DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_ACTIVE_INACTIVE_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public void LMM01000CopySource(LMM01003DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_COPY_NEW_UTILITY_CHARGES";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_CHARGES_ID", DbType.String, 50, poEntity.CCURRENT_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_CHARGES_ID", DbType.String, 50, poEntity.CNEW_CHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_CHARGES_NAME", DbType.String, 50, poEntity.CNEW_CHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
