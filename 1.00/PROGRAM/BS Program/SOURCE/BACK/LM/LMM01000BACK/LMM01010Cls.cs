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
using System.Transactions;
using System.Diagnostics;

namespace LMM01000BACK
{
    public class LMM01010Cls : R_BusinessObject<LMM01010DTO>
    {
        public List<LMM01011DTO> GetAllRateECList(LMM01011DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01011DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_RATE_EC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01011DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM01010DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM01010DTO R_Display(LMM01010DTO poEntity)  
        {
            var loEx = new R_Exception();
            LMM01010DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_CHARGES_UTILITY_RATE_EC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01010DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01010DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCmd = loDb.GetCommand();
            LMM01000RDTCommonObjectDTO loRDTCommonObject = null;

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    lcQuery = "DECLARE @CRATE_EC_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CRATE_EC_LIST != null && poNewEntity.CRATE_EC_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CRATE_EC_LIST " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01, CATTRIBUTE02, CATTRIBUTE03, CATTRIBUTE04, CATTRIBUTE05, CATTRIBUTE06, CATTRIBUTE07, CATTRIBUTE08) " +
                            "VALUES ";
                        foreach (var loRate in poNewEntity.CRATE_EC_LIST)
                        {
                            lcQuery += $"('{loRate.CCOMPANY_ID}', '{loRate.CPROPERTY_ID}', '{loRate.CCHARGES_TYPE}', '{loRate.CCHARGES_ID}', '{loRate.IUP_TO_USAGE}', " +
                                $"'{loRate.CUSAGE_DESC}', '{loRate.NBUY_LWBP_CHARGE}', '{loRate.NBUY_WBP_CHARGE}', '{loRate.NLWBP_CHARGE}', '{loRate.NWBP_CHARGE}'),";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXECUTE RSP_LM_MAINTAIN_RATE_EC " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CPROPERTY_ID = '{poNewEntity.CPROPERTY_ID}' " +
                        $",@CCHARGES_TYPE = '{poNewEntity.CCHARGES_TYPE}' " +
                        $",@CCHARGES_ID = '{poNewEntity.CCHARGES_ID}' " +
                        $",@CUSAGE_RATE_MODE = '{poNewEntity.CUSAGE_RATE_MODE}' " +
                        $",@CRATE_TYPE = '{poNewEntity.CRATE_TYPE}' " +
                        $",@NSTANDING_CHARGE = {poNewEntity.NSTANDING_CHARGE} " +
                        $",@NBUY_STANDING_CHARGE = {poNewEntity.NBUY_STANDING_CHARGE}" +
                        $",@NLWBP_CHARGE = {poNewEntity.NLWBP_CHARGE}" +
                        $",@NBUY_LWBP_CHARGE = {poNewEntity.NBUY_LWBP_CHARGE} " +
                        $",@NWBP_CHARGE = {poNewEntity.NWBP_CHARGE} " +
                        $",@NBUY_WBP_CHARGE = {poNewEntity.NBUY_WBP_CHARGE} " +
                        $",@NTRANSFORMATOR_FEE = {poNewEntity.NTRANSFORMATOR_FEE} " +
                        $",@NBUY_TRANSFORMATOR_FEE = {poNewEntity.NBUY_TRANSFORMATOR_FEE} " +
                        $",@LUSAGE_MIN_CHARGE = {poNewEntity.LUSAGE_MIN_CHARGE} " +
                        $",@NUSAGE_MIN_CHARGE = {poNewEntity.NUSAGE_MIN_CHARGE} " +
                        $",@NKWH_USED_MAX = {poNewEntity.NKWH_USED_MAX} " +
                        $",@NKWH_USED_RATE = {poNewEntity.NKWH_USED_RATE} " +
                        $",@NBUY_KWH_USED_RATE = {poNewEntity.NBUY_KWH_USED_RATE} " +
                        $",@NRETRIBUTION_PCT = {poNewEntity.NRETRIBUTION_PCT} " +
                        $",@LRETRIBUTION_EXCLUDED_VAT = {poNewEntity.LRETRIBUTION_EXCLUDED_VAT} " +
                        $",@CADMIN_FEE = '{poNewEntity.CADMIN_FEE}' " +
                        $",@NADMIN_FEE_PCT = {poNewEntity.NADMIN_FEE_PCT} " +
                        $",@NADMIN_FEE_AMT = {poNewEntity.NADMIN_FEE_AMT} " +
                        $",@LADMIN_FEE_TAX = {poNewEntity.LADMIN_FEE_TAX} " +
                        $",@NOTHER_DISINCENTIVE_FACTOR = {poNewEntity.NOTHER_DISINCENTIVE_FACTOR} " +
                        $",@NKVA_RANGE = {poNewEntity.NKVA_RANGE} " +
                        $",@NBUY_KVA_RANGE = {poNewEntity.NBUY_KVA_RANGE}" +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CRATE_EC_LIST = @CRATE_EC_LIST ";

                        R_ExternalException.R_SP_Init_Exception(loConn);

                        try
                        {
                            loDb.SqlExecQuery(lcQuery, loConn, false);
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                        }

                        loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    TransScope.Complete();
                };
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
    }
}
