using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.DTOs.LMM03508;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.Logging;

namespace LMM03500BACK
{
    public class LMM03508Cls
    {
        private LoggerLMM03508 _logger;
        public LMM03508Cls()
        {
            _logger = LoggerLMM03508.R_GetInstanceLogger();
        }

        public List<LMM03508DTO> GetFixVAList(LMM03508ParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<LMM03508DTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "EXEC RSP_LM_GET_TENANT_VA_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CSELECTED_PROPERTY_ID, " +
                    "@CSELECTED_TENANT_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_TENANT_ID", DbType.String, 50, poEntity.CSELECTED_TENANT_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_LM_GET_TENANT_VA_LIST {@Parameters} || GetFixVAList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM03508DTO>(loDataTable).ToList();
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
