using GSM04500COMMON;
using GSM04500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GSM04500MODEL
{
    public class GSM04500ViewModel : R_ViewModel<GSM04500DTO>
    {
        #region Model
        private GSM04500Model _GSM04500Model = new GSM04500Model();
        #endregion

        #region Init Data
        public List<GSM04500PropetyDTO> PropertyList { get; set; } = new List<GSM04500PropetyDTO>();
        public List<GSM04500JournalGrpTypeDTO> JournalTypeList { get; set; } = new List<GSM04500JournalGrpTypeDTO>();
        #endregion

        #region Property ViewModel
        public ObservableCollection<GSM04500DTO> JournalGroupGrid { get; set; } = new ObservableCollection<GSM04500DTO>();
        public GSM04500DTO JournalGroup { get; set; } = new GSM04500DTO();
        public string PropertyID { get; set; }
        public string JournalGroupTypeCode { get; set; }
        #endregion
        public async Task GetAllInitData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Init Data
                var loResult = await _GSM04500Model.GetInitDataAsync();

                //Set Init Data
                PropertyList = loResult.PropertyList;
                JournalTypeList = loResult.JournalGroupTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04500Model.GetJournalGroupListAsync(PropertyID, JournalGroupTypeCode);
                JournalGroupGrid = new ObservableCollection<GSM04500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalGroup(GSM04500DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04500Model.R_ServiceGetRecordAsync(poParam);

                JournalGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationFieldEmpty(GSM04500DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(poEntity.CJRNGRP_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_01");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrEmpty(poEntity.CJRNGRP_NAME))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_02");
                    loEx.Add(loErr);
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournalGroup(GSM04500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                JournalGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteJournalGroup(GSM04500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM04500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSM04500FileByteDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            GSM04500FileByteDTO loResult = null;

            try
            {
                loResult = await _GSM04500Model.GetTemplateJournalGroupExcelAsync();
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
