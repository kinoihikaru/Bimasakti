﻿using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Input;
using LMM06500COMMON;

namespace LMM06500BACK
{
    public class LMM06500UniversalCls 
    {
        private LoggerLMM06500Universal _Logger;

        public LMM06500UniversalCls()
        {
            _Logger = LoggerLMM06500Universal.R_GetInstanceLogger();
        }

        public List<LMM06500UniversalDTO> GetAllPosition(LMM06500UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM06500UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    "('BIMASAKTI', @CCOMPANY_ID , '_LM_STAFF_POSITION', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loUserLanLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CUSER_LANGUAGE":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('BIMASAKTI', {0} , " +
                    "'_LM_STAFF_POSITION', '' , {1})", loCompanyIdLog, loUserLanLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM06500UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<LMM06500UniversalDTO> GetAllGender(LMM06500UniversalDTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM06500UniversalDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO " +
                    "('SIAPP', @CCOMPANY_ID , '_GENDER', '', @CUSER_LANGUAGE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                string loCompanyIdLog = null;
                string loUserLanLog = null;
                List<DbParameter> loDbParam = loCmd.Parameters.Cast<DbParameter>().ToList();
                loDbParam.ForEach(x =>
                {
                    switch (x.ParameterName)
                    {
                        case "@CCOMPANY_ID":
                            loCompanyIdLog = (string)x.Value;
                            break;
                        case "@CUSER_LANGUAGE":
                            loUserLanLog = (string)x.Value;
                            break;
                    }
                });
                var loDebugLogResult = string.Format("SELECT CCODE, CDESCRIPTION FROM " +
                    "RFT_GET_GSB_CODE_INFO('SIAPP', {0} , " +
                    "'_GENDER', '' , {1})", loCompanyIdLog, loUserLanLog);
                _Logger.LogDebug(loDebugLogResult);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<LMM06500UniversalDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
