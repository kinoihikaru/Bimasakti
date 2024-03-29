﻿using LMM01500COMMON;
using LMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01510ViewModel : R_ViewModel<LMM01511DTO>
    {
        private LMM01510Model _LMM01510Model = new LMM01510Model();
        public ObservableCollection<LMM01511DTO> TemplateBankAccountGrid { get; set; } = new ObservableCollection<LMM01511DTO>();


        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";
        public bool StatusChange;

        public async Task GetListTemplateBankAccount()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01510Model.LMM01510TemplateAndBankAccountListAsync(PropertyValueContext, InvGrpCode);
                var loConvert = R_FrontUtility.ConvertCollectionToCollection<LMM01511DTO>(loResult);

                TemplateBankAccountGrid = new ObservableCollection<LMM01511DTO>(loConvert);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public LMM01511DTO TemplateBankAccount = new LMM01511DTO();
        public async Task GetTemplateBankAccount(LMM01511DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(poParam.CINVGRP_CODE))
                {
                    var loResult = await _LMM01510Model.R_ServiceGetRecordAsync(poParam);

                    TemplateBankAccount = loResult;
                }
                else
                {
                    TemplateBankAccount = poParam;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ValidationTemplateBankAccount(LMM01511DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.FileName);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1506"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1507"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CBANK_CODE);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1508"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CBANK_ACCOUNT);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1509"));
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveTemplateBankAccount(LMM01511DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01510Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                TemplateBankAccount = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTemplateBankAccount(LMM01511DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01510Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
