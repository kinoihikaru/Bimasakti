using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System.Collections.ObjectModel;
using R_CommonFrontBackAPI;
using R_BlazorFrontEnd.Helpers;

namespace LMM01500MODEL
{
    public class LMM01510ViewModel : R_ViewModel<LMM01511DTO>
    {
        private LMM01510Model _LMM01510Model = new LMM01510Model();
        public ObservableCollection<LMM01510DTO> TemplateBankAccountGrid { get; set; } = new ObservableCollection<LMM01510DTO>();

        public LMM01511DTO TemplateBankAccount = new LMM01511DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode = "";
        public bool StatusChange;

        public async Task GetListTemplateBankAccount()
        {
            var loEx = new R_Exception();

            try
            {

                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                var loResult = await _LMM01510Model.LMM01510TemplateAndBankAccountListAsync();

                TemplateBankAccountGrid = new ObservableCollection<LMM01510DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTemplateBankAccount(LMM01511DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

                var loResult = await _LMM01510Model.R_ServiceGetRecordAsync(poParam);

                TemplateBankAccount = loResult;
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
                R_FrontContext.R_SetContext(ContextConstant.CPROPERTY_ID, PropertyValueContext);
                R_FrontContext.R_SetContext(ContextConstant.CINVGRP_CODE, InvGrpCode);

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
