using GSM03000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM03000Common;
using System.Data.Common;

namespace GSM03000Back
{
    public class GSM03000Cls : R_BusinessObject<GSM03000DTO>
    {
        protected override void R_Deleting(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");

            try
            {
                // set action delete
                poEntity.CACTION = "DELETE";

                lcQuery = $"EXEC RSP_GS_MAINTAIN_OTHER_CHARGES  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', " +
                    $"@CCHARGES_TYPE = '{poEntity.CCHARGES_TYPE}', " +
                    $"@CCHARGES_ID = '{poEntity.CCHARGES_ID}', " +
                    $"@CCHARGES_NAME = '{poEntity.CCHARGES_NAME}', " +
                    $"@CDESCRIPTION = '{poEntity.CDESCRIPTION}', " +
                    $"@CSTATUS = '{poEntity.CSTATUS}', " +
                    $"@CGLACCOUNT_NO = '{poEntity.CGLACCOUNT_NO}', " +
                    $"@CACTION = '{poEntity.CACTION}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}' "; 

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
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

        protected override GSM03000DTO R_Display(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            GSM03000DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var param = poEntity;

                var lcQuery = $"EXEC dbo.RSP_GS_GET_OTHER_CHARGES_DETAIL  @CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', @CCHARGES_TYPE = '{poEntity.CCHARGES_TYPE}', " +
                    $"@CCHARGES_ID = '{poEntity.CCHARGES_ID}', @CUSER_ID = '{poEntity.CUSER_ID}'";

                loResult = loDb.SqlExecObjectQuery<GSM03000DTO>(lcQuery, loConn, true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM03000DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                

                lcQuery = "exec RSP_GS_MAINTAIN_OTHER_CHARGES @CCOMPANY_ID ,@CPROPERTY_ID ,@CCHARGES_TYPE, " +
                    "@CCHARGES_ID ,@CCHARGES_NAME ,@CDESCRIPTION ,@CSTATUS ,@CGLACCOUNT_NO ,@CACTION ,@CUSER_ID;";
                loCmd.CommandText = lcQuery;

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
                loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 50, poNewEntity.CDESCRIPTION);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_NAME", DbType.String, 50, poNewEntity.CCHARGES_NAME);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poNewEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 50, poNewEntity.CGLACCOUNT_NO);
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

        public List<GSM03000DTO> GetAllOtherCharges(GSM03000DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM03000DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();
                
                var lcQuery = $"exec RSP_GS_GET_OTHER_CHARGES_LIST @CCOMPANY_ID, @CPROPERTY_ID, @CCHARGES_TYPE, @CUSER_LOGIN_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM03000PropertyDTO> GetProperty(GSM03000PropertyDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM03000PropertyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"exec RSP_GS_GET_PROPERTY_LIST @CCOMPANY_ID, @CUSER_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM03000PropertyDTO>(loDataTable).ToList();

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
