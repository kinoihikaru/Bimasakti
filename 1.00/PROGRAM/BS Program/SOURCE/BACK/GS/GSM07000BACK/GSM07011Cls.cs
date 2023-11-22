using GSM07000COMMON.DTOs;
using GSM07000COMMON.Loggers;
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

namespace GSM07000BACK
{
    public class GSM07011Cls : R_BusinessObject<GSM07011ParameterDTO>
    {
        private LoggerGSM07011 _logger;

        public GSM07011Cls()
        {
            _logger = LoggerGSM07011.R_GetInstanceLogger();
        }

        public List<GSM07011DTO> GetMultipleUserAssignmentList(GetMultipleUserAssignmentListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07011DTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_LOOKUP_USER_LIST " +
                    "@LOGIN_COMPANY_ID, " +
                    "@CPROGRAM_ID, " +
                    "@CAPPROVAL_CODE";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@LOGIN_COMPANY_ID", DbType.String, 50, poEntity.LOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 50, "GSM07000");
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, poEntity.CAPPROVAL_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_LOOKUP_USER_LIST {@Parameters} || GetMultipleUserAssignmentList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM07011DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GSM07011ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override GSM07011ParameterDTO R_Display(GSM07011ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM07011ParameterDTO loResult = new GSM07011ParameterDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_USER_DETAIL " +
                    "@LOGIN_COMPANY_ID, " +
                    "@CPROGRAM_ID, " +
                    "@CUSER_ID, " +
                    "@SELECTED_APPROVAL_CODE";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@LOGIN_COMPANY_ID", DbType.String, 50, poEntity.LOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 50, "GSM07000");
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.Data.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poEntity.SELECTED_APPROVAL_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_USER_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM07011DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM07011ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "EXEC RSP_GS_MAINTAIN_ACT_APPR_USER " +
                        "@LOGIN_COMPANY_ID, " +
                        "@SELECTED_APPROVAL_CODE, " +
                        "@CUSER_ID, " +
                        "@CACTION, " +
                        "@LOGIN_USER_ID";

                loDb.R_AddCommandParameter(loCmd, "@LOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.LOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poNewEntity.SELECTED_APPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.Data.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "ADD");
                loDb.R_AddCommandParameter(loCmd, "@LOGIN_USER_ID", DbType.String, 50, poNewEntity.LOGIN_USER_ID);

                loCmd.CommandText = lcQuery;

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_ACT_APPR_USER {@Parameters} || R_Saving(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public void SaveMultipleUserAssignment(SaveMultipleUserAssignmentParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");
            DbCommand loCmd = loDb.GetCommand();
            IEnumerable<object?> loDbParam = null;
            string lcQuery = "";
            int count = 0;

            try
            {
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, poEntity.CAPPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "ADD");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                foreach (var item in poEntity.CSELECTED_USER_ID_LIST)
                {
                    lcQuery = "EXEC RSP_GS_MAINTAIN_ACT_APPR_USER " +
                        "@CLOGIN_COMPANY_ID, " +
                        "@CAPPROVAL_CODE, " +
                        string.Format("@CSELECTED_USER_ID{0}, ", count) +
                        "@CACTION, " +
                        "@CLOGIN_USER_ID";

                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, string.Format("@CSELECTED_USER_ID{0}", count), DbType.String, 50, item);

                    loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x =>
                            x.ParameterName == "@CLOGIN_COMPANY_ID" ||
                            x.ParameterName == "@CAPPROVAL_CODE" ||
                            x.ParameterName == string.Format("@CSELECTED_USER_ID{0}", count) ||
                            x.ParameterName == "@CACTION" ||
                            x.ParameterName == "@CLOGIN_USER_ID")
                        .Select(x => x.Value);

                    _logger.LogDebug("EXEC RSP_GS_MAINTAIN_ACT_APPR_USER {@Parameters} || SaveMultipleUserAssignment(Cls) ", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                    count++;
                }
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

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
