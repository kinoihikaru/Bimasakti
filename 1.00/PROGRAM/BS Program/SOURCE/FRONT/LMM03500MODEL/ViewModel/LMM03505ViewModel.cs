using LMM03500COMMON;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.DTOs.LMM03505;
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
    public class LMM03505ViewModel : R_ViewModel<LMM03505DTO>
    {
        private LMM03500Model loSharedModel = new LMM03500Model();

        private LMM03505Model loModel = new LMM03505Model();

        public LMM03505DTO loBankInfo = null;

        public ObservableCollection<LMM03505DTO> loBankInfoList = new ObservableCollection<LMM03505DTO>();

        public LMM03505ResultDTO loRtn = null;

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public TenantDTO loTenant = new TenantDTO();

        public TenantResultDTO loTenantRtn = null;

        public async Task GetBankInfoListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03505_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03505_TENANT_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loRtn = await loModel.GetBankInfoListStreamAsync();
                loBankInfoList = new ObservableCollection<LMM03505DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task GetTenantAsync()
        {
            R_Exception loException = new R_Exception();
            TenantParameterDTO loParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new TenantParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                loTenantRtn = await loSharedModel.GetTenantAsync(loParam);
                loTenant = loTenantRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetBankInfoAsync(LMM03505DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM03505ParameterDTO loParam = null;
            LMM03505ParameterDTO loResult = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new LMM03505ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loResult = await loModel.R_ServiceGetRecordAsync(loParam);
                loBankInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveBankInfoAsync(LMM03505DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            LMM03505ParameterDTO loParam = null;
            LMM03505ParameterDTO loResult = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new LMM03505ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loBankInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteBankInfoAsync(LMM03505DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM03505ParameterDTO loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03505_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new LMM03505ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
