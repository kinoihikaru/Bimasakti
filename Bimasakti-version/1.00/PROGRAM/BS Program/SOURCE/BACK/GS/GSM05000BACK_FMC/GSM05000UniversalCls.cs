using GSM05000COMMON_FMC;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GSM05000BACK_FMC;

public class GSM05000UniversalCls
{
    private LoggerGSM05000Universal _logger;

    public GSM05000UniversalCls()
    {
        _logger = LoggerGSM05000Universal.R_GetInstanceLogger();
    }

    public List<GSM05000UniversalDTO> GetDelimiterListDb()
    {
        R_Exception loEx = new();
        List<GSM05000UniversalDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = string.Format("SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', '{0}', '_GS_REFNO_DELIMITER', '', '{1}')", R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.CULTURE);
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000UniversalDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}