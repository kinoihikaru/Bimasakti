using LMM04000COMMON;
using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04030;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM04000MODEL.ViewModel
{
    public class LMM04030ViewModel : R_ViewModel<LMM04030DTO>
    {
        private LMM04030Model loModel = new LMM04030Model();

        public LMM04030DTO loBankInfo = null;

        public ObservableCollection<LMM04030DTO> loBankInfoList = new ObservableCollection<LMM04030DTO>();

        public LMM04030ResultDTO loRtn = null;

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public TenantDTO loTenant = new TenantDTO();

        public TenantResultDTO loTenantRtn = null;

        public async Task GetBankInfoListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04030_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04030_TENANT_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loRtn = await loModel.GetBankInfoListStreamAsync();
                loBankInfoList = new ObservableCollection<LMM04030DTO>(loRtn.Data);
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
                loParam = new TenantParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loTenantRtn = await loModel.GetTenantAsync(loParam);
                loTenant = loTenantRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetBankInfoAsync(LMM04030DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM04030ParameterDTO loParam = null;
            LMM04030ParameterDTO loResult = null;

            try
            {
                loParam = new LMM04030ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);

                loResult = await loModel.R_ServiceGetRecordAsync(loParam);
                loBankInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveBankInfoAsync(LMM04030DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            LMM04030ParameterDTO loParam = null;
            LMM04030ParameterDTO loResult = null;

            try
            {
                loParam = new LMM04030ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
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

        public async Task DeleteBankInfoAsync(LMM04030DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM04030ParameterDTO loParam = null;
            LMM04030ParameterDTO loResult = null;

            try
            {
                loParam = new LMM04030ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04030_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
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
