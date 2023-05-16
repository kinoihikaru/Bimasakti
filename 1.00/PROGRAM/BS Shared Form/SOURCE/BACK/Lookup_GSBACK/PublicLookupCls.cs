using Lookup_GSCOMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSLBACK
{
    public class PublicLookupCls
    {
        public List<GSL00100DTO> GetALLSalesTax(GSL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00100DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_SALES_TAX_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00100DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00200DTO> GetALLWithholdingTax(GSL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CCOMPANY_ID, CTAX_ID, CTAX_NAME, NTAX_PERCENTAGE FROM GSM_WITHHOLDING_TAX (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' " +
                    $"AND CTAX_TYPE = '{poEntity.CTAX_TYPE}' " +
                    $"AND LACTIVE = 1 ";

                loResult = loDb.SqlExecObjectQuery<GSL00200DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00300DTO> GetALLCurrency()
        {
            var loEx = new R_Exception();
            List<GSL00300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CCURRENCY_CODE, CCURRENCY_NAME, CCURRENCY_SYMBOL FROM GSM_CURRENCY (NOLOCK) ";

                loResult = loDb.SqlExecObjectQuery<GSL00300DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00400DTO> GetALLJournalGroup(GSL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00400DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CJRNGRP_CODE, CJRNGRP_NAME FROM GSM_JRNGRP (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' " +
                    $"AND CPROPERTY_ID = '{poEntity.CPROPERTY_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00400DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00500DTO> GetALLGLAccount(GSL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00500DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_GL_ACCOUNT_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', " +
                    $"@CPROGRAM_CODE = '{poEntity.CPROGRAM_CODE}', " +
                    $"@CBSIS = '{poEntity.CBSIS}', " +
                    $"@CDBCR = '{poEntity.CDBCR}', " +
                    $"@LCENTER_RESTR = '{poEntity.LCENTER_RESTR}', " +
                    $"@LUSER_RESTR = '{poEntity.LUSER_RESTR}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}', " +
                    $"@CCENTER_CODE = '{poEntity.CCENTER_CODE}', " +
                    $"@CUSER_LANGUAGE = '{poEntity.CUSER_LANGUAGE}' ";

               loResult = loDb.SqlExecObjectQuery<GSL00500DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00550DTO> GetALLGOA(GSL00550ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00550DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_GOA_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00550DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00600DTO> GetALLUnitTypeCategory(GSL00600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00600DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00600DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00700DTO> GetALLDepartment(GSL00700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00700DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00700DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00800DTO> GetALLCurrencyRateType(GSL00800ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00800DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CURRENCY_TYPE_LIST " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00800DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00900DTO> GetALLCenter(GSL00900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00900DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CENTER_LIST " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL00900DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01000DTO> GetALLUser()
        {
            var loEx = new R_Exception();
            List<GSL01000DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CUSER_ID, CUSER_NAME FROM SAM_USER (NOLOCK) ";

                loResult = loDb.SqlExecObjectQuery<GSL01000DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01100DTO> GetALLUserApproval(GSL01100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01100DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT A.CUSER_ID, B.CUSER_NAME, B.CPOSITION FROM GSM_PROPERTY_USER A (NOLOCK) " +
                    $"INNER JOIN SAM_USER B (NOLOCK) ON A.CUSER_ID = B.CUSER_ID " +
                    $"WHERE A.CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' " +
                    $"AND A.CPROPERTY_ID = '{poEntity.CPROPERTY_ID}' " +
                    $"AND NOT EXISTS (SELECT TOP 1 1 FROM GSM_TRANSACTION_APPROVAL C (NOLOCK) " +
                    $"WHERE A.CCOMPANY_ID = C.CCOMPANY_ID " +
                    $"AND A.CUSER_ID = C.CUSER_ID) ";

                loResult = loDb.SqlExecObjectQuery<GSL01100DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01200DTO> GetALLBank(GSL01200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CCB_CODE, CCB_NAME FROM GSM_CB (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' " +
                    $"AND CCB_TYPE = '{poEntity.CCB_TYPE}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01200DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01300DTO> GetALLBankAccount(GSL01300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CCB_ACCOUNT_NO, CCB_ACCOUNT_NAME FROM GSM_CB_ACCOUNT (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = '{poEntity.CCOMPANY_ID}' " +
                    $"AND CBANK_TYPE = '{poEntity.CBANK_TYPE}' " +
                    $"AND CCB_CODE   = '{poEntity.CCB_CODE}' " +
                    $"AND CDEPT_CODE = '{poEntity.CDEPT_CODE}'  ";

                loResult = loDb.SqlExecObjectQuery<GSL01300DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01400DTO> GetALLOtherCharges(GSL01400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01400DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_OTHER_CHARGES_LIST " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', " +
                    $"@CCHARGES_TYPE  = '{poEntity.CCHARGES_TYPE_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_LOGIN_ID}'  ";

                loResult = loDb.SqlExecObjectQuery<GSL01400DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01500ResultGroupDTO> GetALLCashFlowGroup(GSL01500ParameterGroupDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01500ResultGroupDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_GRP_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01500ResultGroupDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01500ResultDetailDTO> GetALLCashFlowDetail(GSL01500ParameterDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01500ResultDetailDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CCASH_FLOW_GROUP_CODE = '{poEntity.CCASH_FLOW_GROUP_CODE}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01500ResultDetailDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01600DTO> GetALLCashFlowGruopType(GSL01600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01600DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_GRP_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01600DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
