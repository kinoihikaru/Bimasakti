using APM00100COMMON.DTOs.APM00100;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace APM00100MODEL.ViewModel
{
    public class APM00100ViewModel : R_ViewModel<APM00100DTO>
    {
        private APM00100Model loModel = new APM00100Model();

        public APM00100DTO loAPSystemParam = null;

        public APM00100ParameterDTO loRtn = null;

        public GetAPMSystemParamDTO loGetSystemParam = null;

        public GetAPMSystemParamResultDTO loGetSystemParamRtn = null;

        public void APSystemParameterValidation(APM00100DTO poParam)
        {
            bool llCancel = false;

            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Default Department is required!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCUR_RATETYPE_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Currency Rate Type is required!");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CTAX_RATETYPE_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tax Rate Type is required!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loGetSystemParamRtn = await loModel.GetAPMSystemParamAsync();
                loGetSystemParam = loGetSystemParamRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAPSystemParamAsync(APM00100DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APM00100ParameterDTO loParam = new APM00100ParameterDTO();
            APM00100ParameterDTO loResult = null;

            try
            {
                loParam.Data = poEntity;
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loAPSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveAPSystemParamAsync(APM00100DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            APM00100ParameterDTO loParam = new APM00100ParameterDTO();
            APM00100ParameterDTO loResult = null;

            try
            {
                loParam.Data = poEntity;
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loAPSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

    }
}
