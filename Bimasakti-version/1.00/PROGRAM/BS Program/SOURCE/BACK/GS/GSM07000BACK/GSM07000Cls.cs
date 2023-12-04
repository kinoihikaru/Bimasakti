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
    public class GSM07000Cls : R_BusinessObject<GSM07000ParameterDTO>
    {
        private LoggerGSM07000 _logger;
        public GSM07000Cls()
        {
            _logger = LoggerGSM07000.R_GetInstanceLogger();
        }

        protected override void R_Deleting(GSM07000ParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override GSM07000ParameterDTO R_Display(GSM07000ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM07000ParameterDTO loResult = new GSM07000ParameterDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_ACT_APPR_DETAIL " +
                                 "@CCOMPANY_ID, " +
                                 "@CAPPROVAL_CODE, " +
                                 "@CUPDATE_BY";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, poEntity.Data.CAPPROVAL_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poEntity.Data.CUPDATE_BY);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_ACT_APPR_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM07000DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM07000ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if(poCRUDMode == eCRUDMode.EditMode)
                {
                    string lcQuery = "";
                    R_Db loDb = new R_Db();
                    DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                    lcQuery = "EXEC RSP_GS_MAINTAIN_ACT_APPR " +
                        "@CCOMPANY_ID, " +
                        "@CAPPROVAL_CODE, " +
                        "@IAPPROVAL_OPTION, " +
                        "@CACTION, " +
                        "@CUPDATE_BY";

                    DbCommand loCmd = loDb.GetCommand();
                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_CODE", DbType.String, 50, poNewEntity.Data.CAPPROVAL_CODE);
                    loDb.R_AddCommandParameter(loCmd, "@IAPPROVAL_OPTION", DbType.String, 50, poNewEntity.Data.IAPPROVAL_OPTION);
                    loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, "EDIT");
                    loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 50, poNewEntity.Data.CUPDATE_BY);

                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x =>
                        x != null && x.ParameterName.StartsWith("@"))
                        .Select(x => x.Value);

                    _logger.LogDebug("EXEC RSP_GS_MAINTAIN_ACT_APPR {@Parameters} || R_Saving(Cls) ", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, true);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
        }

        public List<GSM07000DTO> GetActivityApprovalList(GetActivityApprovalListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM07000DTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_ACT_APPR_LIST " +
                    "@CCOMPANY_ID, " +
                    "@CUSER_LOGIN_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_ACT_APPR_LIST {@Parameters} || GetActivityApprovalList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM07000DTO>(loDataTable).ToList();

                foreach (var loItem in loResult)
                {
                    loItem.CAPPROVAL_OPTION = Convert.ToString(loItem.IAPPROVAL_OPTION);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<ApprovalDTO> GetApprovalList(GetApprovalListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<ApprovalDTO> loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', @CCOMPANY_ID,'_GS_APVOPTION', '', @CLANGUAGE_ID) B";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                string loCompanyIdLog = "";
                string loUserLanLog = "";
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CLANGUAGE_ID":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_GS_APVOPTION', '' , {1}) || GetApprovalList(Cls)", loCompanyIdLog, loUserLanLog);
                _logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                    
                loResult = R_Utility.R_ConvertTo<ApprovalDTO>(loDataTable).ToList();

                foreach (var loItem in loResult)
                {
                    loItem.ICODE = Convert.ToInt32(loItem.CCODE);
                }
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
