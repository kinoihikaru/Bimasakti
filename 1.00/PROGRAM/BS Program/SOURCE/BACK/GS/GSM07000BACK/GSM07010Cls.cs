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
using System.Windows.Input;

namespace GSM07000BACK
{
    public class GSM07010Cls : R_BusinessObject<GSM07010ParameterDTO>
    {
        private LoggerGSM07010 _logger;

        public GSM07010Cls()
        {
            _logger = LoggerGSM07010.R_GetInstanceLogger();
        }

        protected override void R_Deleting(GSM07010ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();

            try
            {
                string lcQuery = "EXEC RSP_GS_MAINTAIN_ACT_APPR_USER " +
                    "@COMPANY_ID, " +
                    "@SELECTED_APPROVAL_CODE, " +
                    "@CAPPROVAL_USER, " +
                    "@CACTION, " +
                    "@LOGIN_USER_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poEntity.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poEntity.SELECTED_APPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_USER", DbType.String, 50, poEntity.Data.CAPPROVAL_USER);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "DELETE");
                loDb.R_AddCommandParameter(loCmd, "@LOGIN_USER_ID", DbType.String, 50, poEntity.LOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_ACT_APPR_USER {@Parameters} || R_Deleting(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        protected override GSM07010ParameterDTO R_Display(GSM07010ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM07010ParameterDTO loResult = new GSM07010ParameterDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_ACT_APPR_USER_DETAIL " +
                    "@COMPANY_ID, " +
                    "@SELECTED_APPROVAL_CODE, " +
                    "@CAPPROVAL_USER";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;


                loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poEntity.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poEntity.SELECTED_APPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_USER", DbType.String, 50, poEntity.Data.CAPPROVAL_USER);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_ACT_APPR_USER_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM07010DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM07010ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            if (poCRUDMode == eCRUDMode.AddMode)
            {
                R_Exception loException = new R_Exception();
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();

                try
                {

                    string lcQuery = "EXEC RSP_GS_MAINTAIN_ACT_APPR_USER " +
                        "@COMPANY_ID, " +
                        "@SELECTED_APPROVAL_CODE, " +
                        "@CAPPROVAL_USER, " +
                        "@CACTION, " +
                        "@LOGIN_USER_ID";

                    DbCommand loCmd = loDb.GetCommand();
                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poNewEntity.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poNewEntity.SELECTED_APPROVAL_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_USER", DbType.String, 50, poNewEntity.Data.CAPPROVAL_USER);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "ADD");
                    loDb.R_AddCommandParameter(loCmd, "@LOGIN_USER_ID", DbType.String, 50, poNewEntity.LOGIN_USER_ID);

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
                loException.ThrowExceptionIfErrors();
            }
        }

        public List<GSM07010DTO> GetActivityApprovalUserList(GetActivityApprovalUserListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07010DTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_ACT_APPR_USER_LIST @COMPANY_ID, @SELECTED_APPROVAL_CODE, @LOGIN_USER_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poEntity.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@SELECTED_APPROVAL_CODE", DbType.String, 50, poEntity.SELECTED_APPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LOGIN_USER_ID", DbType.String, 50, poEntity.LOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_ACT_APPR_USER_LIST {@Parameters} || GetActivityApprovalUserList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM07010DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM07010UserDTO> GetLookUpUserList(GSM07010UserParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07010UserDTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_LOOKUP_USER_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CCLASS_ID, " +
                    "@CSELECTED_APPROVAL_CODE";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "GSM07000");
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_APPROVAL_CODE", DbType.String, 50, poEntity.CSELECTED_APPROVAL_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_LOOKUP_USER_LIST {@Parameters} || GetLookUpUserList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM07010UserDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
