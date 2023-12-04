using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON.Loggers;

namespace GSM09300BACK
{
    public class GSM09310Cls
    {
        private LoggerGSM09310 _logger;
        public GSM09310Cls()
        {
            _logger = LoggerGSM09310.R_GetInstanceLogger();
        }

        public List<GSM09310DTO> GetSupplierList(GSM09310ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            List<GSM09310DTO> loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_SUPPLIER_BY_CATEGORY_LIST " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CSUPPLIER_CATEGORY_ID, " +
                                 "@CLOGIN_LANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_CATEGORY_ID", DbType.String, 50, poEntity.CSUPPLIER_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_LANGUAGE_ID", DbType.String, 50, poEntity.CLOGIN_LANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_SUPPLIER_BY_CATEGORY_LIST {@Parameters} || GetSupplierList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM09310DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSupplierCategoryDTO GetSupplierCategory(GetSupplierCategoryParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            GetSupplierCategoryDTO loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_GS_GET_CATEGORY_DETAIL " +
                                 "@CLOGIN_COMPANY_ID, " +
                                 "@CSELECTED_PROPERTY_ID, " +
                                 "@CCATEGORY_TYPE, " +
                                 "@CSUPPLIER_CATEGORY_ID, " +
                                 "@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "50");
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_CATEGORY_ID", DbType.String, 50, poEntity.CSUPPLIER_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CATEGORY_DETAIL {@Parameters} || GetSupplierCategory(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetSupplierCategoryDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void MoveSupplierCategory(MoveSupplierCategoryParameterDTO poEntity)
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
                                 "@CFROM_SUPPLIER_CATEGORY_ID, " +
                                 "@CTO_SUPPLIER_CATEGORY_ID, " +
                                 "@CSUPPLIER_LIST";


                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, "50");
                loDb.R_AddCommandParameter(loCmd, "@CFROM_SUPPLIER_CATEGORY_ID", DbType.String, 50, poEntity.CFROM_SUPPLIER_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTO_SUPPLIER_CATEGORY_ID", DbType.String, 50, poEntity.CTO_SUPPLIER_CATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_LIST", DbType.String, 200, poEntity.CSUPPLIER_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MOVE_CATEGORY {@Parameters} || MoveSupplierCategory(Cls) ", loDbParam);

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
