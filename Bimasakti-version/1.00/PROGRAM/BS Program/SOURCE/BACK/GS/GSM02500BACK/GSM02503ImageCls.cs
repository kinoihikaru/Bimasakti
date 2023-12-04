using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02550;
using GSM02500COMMON.Loggers;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500BACK
{
    public class GSM02503ImageCls : R_BusinessObject<GSM02503ImageParameterDTO>
    {
        private LoggerGSM02503Image _logger;
        public GSM02503ImageCls()
        {
            _logger = LoggerGSM02503Image.R_GetInstanceLogger();
        }

        public ShowUnitTypeImageDTO ShowUnitTypeImage(ShowUnitTypeImageParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            ShowUnitTypeImageDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_ID, " +
                    $"@CSELECTED_IMAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_IMAGE_ID", DbType.String, 50, poEntity.CSELECTED_IMAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL {@Parameters} || ShowUnitTypeImage(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<ShowUnitTypeImageDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;

        }

        private void RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(GSM02503ImageParameterDTO poEntity)
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

                lcQuery = $"EXEC RSP_GS_MAINTAIN_UNIT_TYPE_IMAGE " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CPROPERTY_ID, " +
                                 $"@CUNIT_TYPE_ID, " +
                                 $"@CIMAGE_ID, " +
                                 $"@CIMAGE_NAME, " +
                                 $"@OIMAGE, " +
                                 $"@CACTION, " +
                                 $"@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_ID", DbType.String, 50, poEntity.Data.CIMAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_NAME", DbType.String, 50, poEntity.Data.CIMAGE_NAME);
                //loDb.R_AddCommandParameter(loCmd, "@OIMAGE", DbType.Binary, 100, poEntity.OIMAGE);
                /*
                                var loPar = loCmd.CreateParameter();
                                loPar.ParameterName = "@OIMAGE";
                                //loPar.Value = poEntity.OIMAGE;

                                loPar.Value = new SqlBinary(poEntity.OIMAGE);

                                loCmd.Parameters.Add(loPar);
                */

                var loPar = loDb.GetParameter();
                loPar.ParameterName = "@OIMAGE";
                loPar.DbType = DbType.Binary;
                loPar.Value = poEntity.OIMAGE == null? DBNull.Value: poEntity.OIMAGE;

                loCmd.Parameters.Add(loPar);

                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_MAINTAIN_UNIT_TYPE_IMAGE {@Parameters} || RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(Cls) ", loDbParam);

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

        public List<GSM02503ImageDTO> GetUnitTypeImageList(GetUnitTypeImageListParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GSM02503ImageDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CSELECTED_UNIT_TYPE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_UNIT_TYPE_ID", DbType.String, 50, poEntity.CSELECTED_UNIT_TYPE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_LIST {@Parameters} || GetUnitTypeImageList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSM02503ImageDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GSM02503ImageParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }

        protected override GSM02503ImageParameterDTO R_Display(GSM02503ImageParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GSM02503ImageParameterDTO loResult = new GSM02503ImageParameterDTO();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSELECTED_PROPERTY_ID, " +
                    $"@CUNIT_TYPE_ID, " +
                    $"@CIMAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSELECTED_PROPERTY_ID", DbType.String, 50, poEntity.CSELECTED_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_ID", DbType.String, 50, poEntity.CUNIT_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CIMAGE_ID", DbType.String, 50, poEntity.Data.CIMAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_UNIT_TYPE_IMAGE_DETAIL {@Parameters} || R_Display(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult.Data = R_Utility.R_ConvertTo<GSM02503ImageDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GSM02503ImageParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                RSP_GS_MAINTAIN_UNIT_TYPE_IMAGEMethod(poNewEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
