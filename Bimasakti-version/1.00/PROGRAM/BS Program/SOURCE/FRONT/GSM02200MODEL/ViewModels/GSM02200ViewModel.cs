using GSM02200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;
using GSM02200FrontResources;

namespace GSM02200MODEL
{
    public class GSM02200ViewModel : R_ViewModel<GSM02200DTO>
    {
        private GSM02200Model _GSM02200Model = new GSM02200Model();

        public List<GSM02200DTO> GeographyList { get; set; } = new List<GSM02200DTO>();
        public GSM02200DTO Geography { get; set; } = new GSM02200DTO();

        public bool ShowInactiveGeography { get; set; } = false;
        public bool StatusChange { get; set; } = false;

        public async Task GetGeographyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM02200Model.GetGeographyAsync();

                if (ShowInactiveGeography)
                {
                    GeographyList = loResult;
                }
                else
                {
                    var loData = loResult.Where(x => x.LACTIVE).ToList();
                    GeographyList = loData;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetGeography(GSM02200DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM02200Model.R_ServiceGetRecordAsync(poParam);

                Geography = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationGeography(GSM02200DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CCODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "2204"));
                }
                else
                {
                    lCancel = poParam.CCODE.Length > 20;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "2206"));
                    }
                }
                
                lCancel = string.IsNullOrEmpty(poParam.CNAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "2205"));
                }
                else
                {
                    lCancel = poParam.CNAME.Length > 200;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "2207"));
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveGeography(GSM02200DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM02200Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                Geography = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync(GSM02200DTO poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                poParameter.LACTIVE = StatusChange;

                await _GSM02200Model.GSM02200GeographyChangeStatusAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task<GSM02200UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM02200UploadFileDTO loResult = null;

            try
            {
                loResult = await _GSM02200Model.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
