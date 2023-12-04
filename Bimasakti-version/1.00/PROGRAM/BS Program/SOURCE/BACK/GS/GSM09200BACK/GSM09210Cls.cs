using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON.Loggers;

namespace GSM09200BACK
{
    public class GSM09210Cls
    {
        private LoggerGSM09210 _logger;
        public GSM09210Cls()
        {
            _logger = LoggerGSM09210.R_GetInstanceLogger();
        }

        public List<GSM09210DTO> GetExpenditureList(GSM09210ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            List<GSM09210DTO> loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_EXPENDITURE_BY_CATEGORY_LIST " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CEXPENDITURE_CATEGORY_ID, " +
                                 "@CLOGIN_LANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_CATEGORY_ID", DbType.String, 50, poEntity.CEXPENDITURE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_EXPENDITURE_BY_CATEGORY_LIST {@Parameters} || GetExpenditureList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM09210DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetExpenditureCategoryDTO GetExpenditureCategory(GetExpenditureCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            GetExpenditureCategoryDTO loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_CATEGORY_DETAIL " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CCATEGORY_TYPE, " +
                                 "@CEXPENDITURE_CATEGORY_ID, " +
                                 "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "40");
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_CATEGORY_ID", DbType.String, 50, poEntity.CEXPENDITURE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_DETAIL {@Parameters} || GetExpenditureCategory(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetExpenditureCategoryDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void MoveExpenditureCategory(MoveExpenditureCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_MOVE_CATEGORY " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CLOGIN_USER_ID, " +
                                 "@CCATEGORY_TYPE, " +
                                 "@CFROM_EXPENDITURE_CATEGORY_ID, " +
                                 "@CTO_EXPENDITURE_CATEGORY_ID, " +
                                 "@CEXPENDITURE_LIST";


                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "40");
                loDb.R_AddCommandParameter(loCmd, "@CFROM_EXPENDITURE_CATEGORY_ID", DbType.String, 50, poEntity.CFROM_EXPENDITURE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_EXPENDITURE_CATEGORY_ID", DbType.String, 50, poEntity.CTO_EXPENDITURE_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CEXPENDITURE_LIST", DbType.String, 200, poEntity.CEXPENDITURE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MOVE_CATEGORY {@Parameters} || MoveExpenditureCategory(Cls) ", loDbParam);

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
    }
}
