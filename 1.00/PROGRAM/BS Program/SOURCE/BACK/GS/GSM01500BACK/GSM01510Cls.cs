using GSM01500COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM01500COMMON.Loggers;

namespace GSM01500BACK
{
    public class GSM01510Cls 
    {
        private LoggerGSM01510 _logger;

        public GSM01510Cls()
        {
            _logger = LoggerGSM01510.R_GetInstanceLogger();
        }

        public List<GSM01510DepartmentDTO> GetCenterDepartmentList(GetCenterDeptListParameter poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GSM01510DepartmentDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                string lcQuery = "EXEC RSP_GS_GET_CENTER_DEPT_LIST @CCOMPANY_ID, @CCENTER_CODE, @CUSER_LOGIN_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCENTER_CODE" ||
                        x.ParameterName == "@CUSER_LOGIN_ID")
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CENTER_DEPT_LIST {@Parameters} || GetCenterDepartmentList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM01510DepartmentDTO>(loDataTable).ToList();
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
