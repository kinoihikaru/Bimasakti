using LMM03500COMMON;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03504ViewModel : R_ViewModel<LMM03504DTO>
    {
        private LMM03504Model loModel = new LMM03504Model();

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public LMM03504DTO loBilling = null;

        public LMM03504ResultDTO loRtn = null;

        public async Task GetBillingAsync()
        {
            R_Exception loException = new R_Exception();
            GetBillingParameterDTO loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03504_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03504_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new GetBillingParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loRtn = await loModel.GetBillingAsync(loParam);
                loBilling = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveBillingAsync(LMM03504DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            LMM03504ParameterDTO loParam = null;
            LMM03504ParameterDTO loResult = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03504_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new LMM03504ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                };

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loBilling = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public void BillingValidation(LMM03504DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CBILLING_TO);
                if (llCancel)
                {
                    loEx.Add("", "Billing To is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CBILLING_ADDRESS);
                if (llCancel)
                {
                    loEx.Add("", "Billing Address is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}