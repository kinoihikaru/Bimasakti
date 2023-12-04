using GSM01500COMMON.DTOs;
using GSM01500COMMON.DTOs.GSM01500Print;
using GSM01500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GSM01500BACK
{
    public class GSM01500Cls : R_BusinessObject<CreateUpdateDeleteParameterDTO>
    {
        private LoggerGSM01500 _loggerGSM01500;
        private LoggerGSM01500Print _loggerGSM01500Print;
        public GSM01500Cls()
        {
            _loggerGSM01500 = LoggerGSM01500.R_GetInstanceLogger();
        }
        public GSM01500Cls(LoggerGSM01500Print logger)
        {
            _loggerGSM01500Print = LoggerGSM01500Print.R_GetInstanceLogger();
        }

        public List<GSM01500PrintCenterDTO> GetPrintCenterList(GSM01500PrintCenterParameterDTO poParam)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GSM01500PrintCenterDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_ReportConnectionString");

                lcQuery = "EXEC RSP_GS_PRINT_CENTER @CCOMPANY_ID, @CCENTER_CODE_FROM, @CCENTER_CODE_TO, @LPRINT_DEPT, @LPRINT_INACTIVE, @CUSER_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE_FROM", DbType.String, 50, poParam.CCENTER_CODE_FROM);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE_TO", DbType.String, 50, poParam.CCENTER_CODE_TO);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_DEPT", DbType.Boolean, 50, poParam.LPRINT_DEPT);
                loDb.R_AddCommandParameter(loCmd, "@LPRINT_INACTIVE", DbType.Boolean, 50, poParam.LPRINT_INACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_LOGIN_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => 
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);
/*
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCENTER_CODE_FROM" ||
                        x.ParameterName == "@CCENTER_CODE_TO" ||
                        x.ParameterName == "@LPRINT_DEPT" ||
                        x.ParameterName == "@LPRINT_INACTIVE" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);
*/
                _loggerGSM01500Print.LogDebug("EXEC RSP_GS_PRINT_CENTER {@Parameters} || GetPrintCenterList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM01500PrintCenterDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSM01500DTO> GetCenterList(GetCenterListParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<GSM01500DTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = "EXEC RSP_GS_GET_CENTER_LIST @CCOMPANY_ID, @CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => 
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("EXEC RSP_GS_GET_CENTER_LIST {@Parameters} || GetCenterList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM01500DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public CompanyDTO GetCompanyBasedOnId(string CCOMPANY_ID)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            CompanyDTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "SELECT CCOMPANY_ID, CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = @CCOMPANY_ID;";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID")
                    .Select(x => x.Value);

                _loggerGSM01500Print.LogDebug("SELECT CCOMPANY_ID, CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = {@Parameters} || GetCompanyBasedOnId(Cls) ", loDbParam);


                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CompanyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<CopyFromProcessCompanyDTO> GetCompanyList(GetCenterListParameterDTO poParam)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<CopyFromProcessCompanyDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = @"EXEC RSP_GS_GET_PRIMARY_COMPANY_LIST @CUSER_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParam.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("EXEC RSP_GS_GET_PRIMARY_COMPANY_LIST {@Parameters} || GetCompanyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CopyFromProcessCompanyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public ValidateCompanyDataDTO ValidateCompanyData(ValidateCompanyDataParameterDTO poParam)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            ValidateCompanyDataDTO loResult = new ValidateCompanyDataDTO();
            ValidateCompanyDataTempDTO loTemp = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = "IF EXISTS (SELECT TOP 1 1 FROM GSM_CENTER WITH (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID) SELECT 1 AS Result ELSE SELECT 0 AS Result;";
                
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.SELECTED_COMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("IF EXISTS (SELECT TOP 1 1 FROM GSM_CENTER WITH (NOLOCK) WHERE CCOMPANY_ID = {@Parameters}) SELECT 1 AS Result ELSE SELECT 0 AS Result; || ValidateCompanyData(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loTemp = R_Utility.R_ConvertTo<ValidateCompanyDataTempDTO>(loDataTable).FirstOrDefault();
                loResult.Result = loTemp.Result == 1;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
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

            return loResult;
        }

        public void CopyFromProcess(CopyFromProcessParameter poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                lcQuery = "EXEC RSP_GS_COPY_CENTER @CLOGIN_COMPANY_ID, @CCOPY_FROM_COMPANY_ID, @CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOPY_FROM_COMPANY_ID", DbType.String, 50, poEntity.CCOPY_FROM_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CLOGIN_COMPANY_ID" ||
                        x.ParameterName == "@CCOPY_FROM_COMPANY_ID" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("EXEC RSP_GS_COPY_CENTER {@Parameters} || CopyFromProcess(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public void RSP_GS_ACTIVE_INACTIVE_CENTERMethod(ActiveInactiveParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                lcQuery = "EXEC RSP_GS_ACTIVE_INACTIVE_CENTER @CCOMPANY_ID, @CCENTER_CODE, @LACTIVE, @CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCENTER_CODE" ||
                        x.ParameterName == "@LACTIVE" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("EXEC RSP_GS_ACTIVE_INACTIVE_CENTER {@Parameters} || RSP_GS_ACTIVE_INACTIVE_CENTERMethod(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        private void RSP_GS_MAINTAIN_CENTERMethod(CreateUpdateDeleteParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_GS_MAINTAIN_CENTER @CCOMPANY_ID, @CCENTER_CODE, @CCENTER_NAME, @LACTIVE, @CACTION, @CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poEntity.Data.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_NAME", DbType.String, 50, poEntity.Data.CCENTER_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.Data.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCENTER_CODE" ||
                        x.ParameterName == "@CCENTER_NAME" ||
                        x.ParameterName == "@LACTIVE" ||
                        x.ParameterName == "@CACTION" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                if (poEntity.CACTION == "ADD" || poEntity.CACTION == "EDIT")
                {
                    _loggerGSM01500.LogDebug("EXEC RSP_GS_MAINTAIN_CENTER {@Parameters} || R_Saving(Cls) ", loDbParam);
                }
                else if (poEntity.CACTION == "DELETE")
                {
                    _loggerGSM01500.LogDebug("EXEC RSP_GS_MAINTAIN_CENTER {@Parameters} || R_Deleting(Cls) ", loDbParam);
                }

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
                _loggerGSM01500.LogError(loException);
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

        protected override void R_Deleting(CreateUpdateDeleteParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poEntity.CACTION = "DELETE";
                RSP_GS_MAINTAIN_CENTERMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override CreateUpdateDeleteParameterDTO R_Display(CreateUpdateDeleteParameterDTO poEntity)
        {
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            CreateUpdateDeleteParameterDTO loResult = new CreateUpdateDeleteParameterDTO();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                lcQuery = "EXEC RSP_GS_GET_CENTER_DETAIL @CCOMPANY_ID, @CCENTER_CODE, @CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poEntity.Data.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName == "@CCOMPANY_ID" ||
                        x.ParameterName == "@CCENTER_CODE" ||
                        x.ParameterName == "@CUSER_ID")
                    .Select(x => x.Value);

                _loggerGSM01500.LogDebug("EXEC RSP_GS_GET_CENTER_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM01500DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(CreateUpdateDeleteParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                RSP_GS_MAINTAIN_CENTERMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM01500.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
