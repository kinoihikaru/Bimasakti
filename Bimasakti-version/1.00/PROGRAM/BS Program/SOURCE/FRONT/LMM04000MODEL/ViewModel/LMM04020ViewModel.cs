using LMM04000COMMON;
using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04020;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL.ViewModel
{
    public class LMM04020ViewModel : R_ViewModel<LMM04020DTO>
    {
        private LMM04020Model loModel = new LMM04020Model();

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public LMM04020DTO loTaxInfo = null;

        public LMM04020ResultDTO loRtn = null;

        public List<GetLMM04020ListDTO> loTaxTypeList = null;

        public GetLMM04020ListResultDTO loTaxTypeRtn = null;

        public List<GetLMM04020ListDTO> loIdTypeList = null;

        public GetLMM04020ListResultDTO loIdTypeRtn = null;

        public List<GetLMM04020ListDTO> loTaxCodeList = null;

        public GetLMM04020ListResultDTO loTaxCodeRtn = null;

        public async Task GetTaxInfoAsync()
        {
            R_Exception loException = new R_Exception();
            LMM04020ParameterDTO loParam = null;
            try
            {
                loParam = new LMM04020ParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.LMM04020_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04020_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loRtn = await loModel.GetTaxInfoAsync(loParam);
                loTaxInfo = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTaxTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loTaxTypeRtn = await loModel.GetTaxTypeListStreamAsync();
                loTaxTypeList = loTaxTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetIdTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loIdTypeRtn = await loModel.GetIdTypeListStreamAsync();
                loIdTypeList = loIdTypeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTaxCodeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loTaxCodeRtn = await loModel.GetTaxCodeListStreamAsync();
                loTaxCodeList = loTaxCodeRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}