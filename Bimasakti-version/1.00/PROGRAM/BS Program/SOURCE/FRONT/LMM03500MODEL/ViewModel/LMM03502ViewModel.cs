using LMM03500COMMON;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
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
    public class LMM03502ViewModel : R_ViewModel<ProfileTaxDTO>
    {
        private LMM03502Model loModel = new LMM03502Model();

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public LMM03502DTO loTenantProfile = null;

        public LMM03502ResultDTO loRtn = null;

        public GetTenantGroupResultDTO loTenantGroupRtn = null;

        public List<GetTenantGroupDTO> loTenantGroupList = null;

        public GetTenantCategoryResultDTO loTenantCategoryRtn = null;

        public List<GetTenantCategoryDTO> loTenantCategoryList = null;

        public GetTenantTypeResultDTO loTenantTypeRtn = null;

        public List<GetTenantTypeDTO> loTenantTypeList = null;


        public ProfileTaxDTO loProfileAndTaxInfo = new ProfileTaxDTO();


        public async Task GetTenantProfileAsync()
        {
            R_Exception loException = new R_Exception();
            LMM03502ParameterDTO loParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03502_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03502_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loParam = new LMM03502ParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loRtn = await loModel.GetTenantProfileAsync(loParam);
                loTenantProfile = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        
        public async Task GetTenantTypeListAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loTenantTypeRtn = await loModel.GetTenantTypeListAsync();
                loTenantTypeList = new List<GetTenantTypeDTO>(loTenantTypeRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void TenantProfileValidation(LMM03502DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant ID is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CADDRESS);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Address is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_GROUP_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Group is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_CATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Category is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Type is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Name is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CJRNGRP_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Journal Group is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CPAYMENT_TERM_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Payment Term is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Cuurency is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CSALESMAN_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Salesman is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CLOB_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Line Of Business is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void TenantProfileAndTaxInfoValidation(ProfileTaxDTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant ID is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CADDRESS);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Address is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_GROUP_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Group is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_CATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Category is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_TYPE_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Type is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CTENANT_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Tenant Name is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CJRNGRP_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Journal Group is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CPAYMENT_TERM_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Payment Term is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Cuurency is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CSALESMAN_ID);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Salesman is required");
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.Profile.CLOB_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Tab Profile's Line Of Business is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTenantProfileAndTaxInfoAsync(ProfileTaxDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {/*
                R_FrontContext.R_SetContext(ContextConstant.LMM03502_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.LMM03502_TENANT_ID_CONTEXT, loTenantProfile.CTENANT_ID);*/

                ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO()
                {
                    Profile = poEntity.Profile,
                    TaxInfo = poEntity.TaxInfo,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                ProfileTaxParameterDTO loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loProfileAndTaxInfo.Profile = loResult.Profile;
                loProfileAndTaxInfo.TaxInfo = loResult.TaxInfo;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteTenantProfileAndTaxInfoAsync(ProfileTaxDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {/*
                R_FrontContext.R_SetContext(ContextConstant.LMM03502_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.LMM03502_TENANT_ID_CONTEXT, loTenantProfile.CTENANT_ID);*/

                ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO()
                {
                    Profile = poEntity.Profile,
                    TaxInfo = poEntity.TaxInfo,
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
