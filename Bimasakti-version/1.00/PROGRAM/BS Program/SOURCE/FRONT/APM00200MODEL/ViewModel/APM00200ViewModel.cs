using APM00200COMMON.DTOs;
using APM00200COMMON.DTOs.APM00200;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM00200MODEL.ViewModel
{
    public class APM00200ViewModel : R_ViewModel<APM00200DetailDTO>
    {
        private APM00200Model loModel = new APM00200Model();

        public APM00200DTO loExpenditure = null;

        public APM00200DetailDTO loExpenditureDetail = null;

        public ObservableCollection<APM00200DTO> loExpenditureList = new ObservableCollection<APM00200DTO>();

        public List<GetPropertyDTO> loPropertyList = null;

        public GetPropertyResultDTO loPropertyRtn = null;

        public GetPropertyDTO loProperty = new GetPropertyDTO();

        public List<GetWithholdingTaxTypeDTO> loWithholdingTaxTypeList = null;

        public GetWithholdingTaxTypeResultDTO loWithholdingTaxTypeRtn = null;

        public APM00200ResultDTO loRtn = null;

        public GetSelectedExpenditureResultDTO loSelectedExpenditureRtn = null;

        public APM00200DetailDTO loSelectedExpenditure = null;

        public async Task GetExpenditureListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.APM00200_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetExpenditureListStreamAsync();
                loExpenditureList = new ObservableCollection<APM00200DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetWithholdingTaxTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loWithholdingTaxTypeRtn = await loModel.GetWithholdingTaxTypeListStreamAsync();
                loWithholdingTaxTypeList = new List<GetWithholdingTaxTypeDTO>(loWithholdingTaxTypeRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task UpdateExpenditureActiveFlagAsync()
        {
            R_Exception loEx = new R_Exception();
            UpdateExpenditureActiveFlagParameterDTO loParam = null;
            try
            {
                loParam = new UpdateExpenditureActiveFlagParameterDTO()
                {
                    CSELECTED_REC_ID = loExpenditureDetail.CREC_ID,
                    LACTIVE = !loExpenditure.LACTIVE
                };
                await loModel.UpdateExpenditureActiveFlagAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetExpenditureAsync(APM00200DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APM00200ParameterDTO loParam = null;
            APM00200ParameterDTO loResult = null;
            try
            {
                loParam = new APM00200ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loExpenditureDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ExpenditureValidation(APM00200DetailDTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CEXPENDITURE_ID);
                if (llCancel)
                {
                    loEx.Add("", "Expenditure ID is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CEXPENDITURE_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Expenditure Name is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CCATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add("", "Category is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CJRNGRP_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Journal Group is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveExpenditureAsync(APM00200DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APM00200ParameterDTO loParam = null;
            APM00200ParameterDTO loResult = null;
            try
            {
                loParam = new APM00200ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loExpenditureDetail = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteExpenditureAsync(APM00200DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APM00200ParameterDTO loParam = null;
            try
            {
                loParam = new APM00200ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateExpenditureDTO> DownloadTemplateExpenditureAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateExpenditureDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateExpenditureAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

    }
}
