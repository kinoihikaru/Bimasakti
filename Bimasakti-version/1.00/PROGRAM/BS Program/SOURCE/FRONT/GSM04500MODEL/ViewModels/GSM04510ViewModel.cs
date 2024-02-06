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
    public class GSM04510ViewModel : R_ViewModel<GSM04510DTO>
    {
        #region Model
        private GSM04510Model _GSM04510Model = new GSM04510Model();
        #endregion

        #region Property ViewModel
        public ObservableCollection<GSM04510DTO> JournalGroupGOAGrid { get; set; } = new ObservableCollection<GSM04510DTO>();
        public GSM04510DTO JournalGroupGOA { get; set; } = new GSM04510DTO();
        #endregion

        public async Task GetJournalGroupGOAList(GSM04510ParameterDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04510Model.GetJournalGroupGOAListAsync(poEntity);
                loResult.ForEach(x => 
                    { 
                        x.CGOA_NAME_CODE = string.Format("{0} ({1})", x.CGOA_NAME, x.CGOA_CODE);
                    });

                JournalGroupGOAGrid = new ObservableCollection<GSM04510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalGroupGOA(GSM04510DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04510Model.R_ServiceGetRecordAsync(poParam);
                loResult.CGOA_NAME_CODE = string.Format("{0} ({1})", loResult.CGOA_NAME, loResult.CGOA_CODE);

                JournalGroupGOA = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationFieldEmpty(GSM04510DTO poEntity)
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
        public async Task SaveJournalGroupGOA(GSM04510DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM04510Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                JournalGroupGOA = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteJournalGroupGOA(GSM04510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM04510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
