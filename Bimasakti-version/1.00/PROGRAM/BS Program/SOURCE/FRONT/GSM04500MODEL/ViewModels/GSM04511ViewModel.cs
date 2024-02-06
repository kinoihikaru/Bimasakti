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
    public class GSM04511ViewModel : R_ViewModel<GSM04511DTO>
    {
        #region Model
        private GSM04511Model _GSM04511Model = new GSM04511Model();
        #endregion

        #region Property ViewModel
        public ObservableCollection<GSM04511DTO> JournalGroupGOAByDeptGrid { get; set; } = new ObservableCollection<GSM04511DTO>();
        public GSM04511DTO JournalGroupGOAByDept { get; set; } = new GSM04511DTO();
        #endregion

        public async Task GetJournalGroupGOAByDeptList(GSM04511ParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04511Model.GetJournalGroupGOAByDeptListAsync(poEntity);
                JournalGroupGOAByDeptGrid = new ObservableCollection<GSM04511DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalGroupGOAByDept(GSM04511DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04511Model.R_ServiceGetRecordAsync(poParam);

                JournalGroupGOAByDept = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationFieldEmpty(GSM04511DTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                //if (string.IsNullOrEmpty(poEntity.CJRNGRP_CODE))
                //{
                //    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_01");
                //    loEx.Add(loErr);
                //}
                //if (string.IsNullOrEmpty(poEntity.CJRNGRP_NAME))
                //{
                //    var loErr = R_FrontUtility.R_GetError(typeof(Resources_GSM04500_Class), "Error_02");
                //    loEx.Add(loErr);
                //}
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournalGroupGOAByDept(GSM04511DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04511Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                JournalGroupGOAByDept = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteJournalGroup(GSM04511DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM04511Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
