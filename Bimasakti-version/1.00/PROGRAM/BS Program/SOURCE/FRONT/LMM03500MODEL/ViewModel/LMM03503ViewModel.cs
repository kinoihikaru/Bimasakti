using LMM03500COMMON;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03503ViewModel : R_ViewModel<LMM03503DTO>
    {
        private LMM03503Model loModel = new LMM03503Model();

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public LMM03503DTO loTaxInfo = null;

        public LMM03503ResultDTO loRtn = null;

        public List<GetLMM03503ListDTO> loTaxTypeList = null;

        public GetLMM03503ListResultDTO loTaxTypeRtn = null;

        public List<GetLMM03503ListDTO> loIdTypeList = null;

        public GetLMM03503ListResultDTO loIdTypeRtn = null;

        public List<GetLMM03503ListDTO> loTaxCodeList = null;

        public GetLMM03503ListResultDTO loTaxCodeRtn = null;

        public async Task GetTaxInfoAsync()
        {
            R_Exception loException = new R_Exception();
            LMM03503ParameterDTO loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03503_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03503_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new LMM03503ParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

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
                loTaxTypeList = new List<GetLMM03503ListDTO>(loTaxTypeRtn.Data);
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
                loIdTypeList = new List<GetLMM03503ListDTO>(loIdTypeRtn.Data);
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
                loTaxCodeList = new List<GetLMM03503ListDTO>(loTaxCodeRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}