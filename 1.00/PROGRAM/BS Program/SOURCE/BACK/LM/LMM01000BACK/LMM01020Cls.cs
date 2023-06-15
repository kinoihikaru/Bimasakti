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
    public class LMM01020Cls : R_BusinessObject<LMM01020DTO>
    {
        public List<LMM01021DTO> GetAllRateWGList(LMM01021DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM01021DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_RATE_WG_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM01021DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(LMM01020DTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override LMM01020DTO R_Display(LMM01020DTO poEntity)
        {
            var loEx = new R_Exception();
            LMM01020DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_LM_GET_CHARGES_UTILITY_RATE_WG";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<LMM01020DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(LMM01020DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCmd = loDb.GetCommand();

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

                    lcQuery = "DECLARE @CRATE_WG_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CRATE_WG_LIST != null && poNewEntity.CRATE_WG_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CRATE_WG_LIST " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01, CATTRIBUTE02, CATTRIBUTE03, CATTRIBUTE04, CATTRIBUTE05, CATTRIBUTE06) " +
                            "VALUES ";
                        foreach (var loRate in poNewEntity.CRATE_WG_LIST)
                        {
                            lcQuery += $"('{loRate.CCOMPANY_ID}', '{loRate.CPROPERTY_ID}', '{loRate.CCHARGES_TYPE_ID}', '{loRate.CCHARGES_ID}', '{loRate.IUP_TO_USAGE}', " +
                                $"'{loRate.CUSAGE_DESC}', '{loRate.NBUY_USAGE_CHARGE}', '{loRate.NUSAGE_CHARGE}'),";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXECUTE RSP_LM_MAINTAIN_RATE_WG " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CPROPERTY_ID = '{poNewEntity.CPROPERTY_ID}' " +
                        $",@CCHARGES_TYPE = '{poNewEntity.CCHARGES_TYPE}' " +
                        $",@CCHARGES_ID = '{poNewEntity.CCHARGES_ID}' " +
                        $",@CUSAGE_RATE_MODE = '{poNewEntity.CUSAGE_RATE_MODE}' " +
                        $",@NPIPE_SIZE = {poNewEntity.NPIPE_SIZE} " +
                        $",@NSTANDING_CHARGE = {poNewEntity.NSTANDING_CHARGE} " +
                        $",@NUSAGE_CHARGE_RATE = {poNewEntity.NUSAGE_CHARGE_RATE} " +
                        $",@LUSAGE_MIN_CHARGE = {poNewEntity.LUSAGE_MIN_CHARGE} " +
                        $",@NUSAGE_MIN_CHARGE_AMT = {poNewEntity.NUSAGE_MIN_CHARGE_AMT} " +
                        $",@NMAINTENANCE_FEE = {poNewEntity.NMAINTENANCE_FEE} " +
                        $",@CADMIN_FEE = '{poNewEntity.CADMIN_FEE}' " +
                        $",@NADMIN_FEE_PCT = {poNewEntity.NADMIN_FEE_PCT} " +
                        $",@NADMIN_FEE_AMT = {poNewEntity.NADMIN_FEE_AMT} " +
                        $",@LADMIN_FEE_TAX = {poNewEntity.LADMIN_FEE_TAX} " +
                        $",@LADMIN_FEE_SC = {poNewEntity.LADMIN_FEE_SC} " +
                        $",@LADMIN_FEE_USAGE = {poNewEntity.LADMIN_FEE_USAGE} " +
                        $",@LADMIN_FEE_MAINTENANCE = {poNewEntity.LADMIN_FEE_MAINTENANCE} " +
                        $",@NBUY_STANDING_CHARGE = {poNewEntity.NBUY_STANDING_CHARGE} " +
                        $",@NBUY_USAGE_CHARGE_RATE = {poNewEntity.NBUY_USAGE_CHARGE_RATE} " +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CRATE_WG_LIST = @CRATE_WG_LIST ";

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
