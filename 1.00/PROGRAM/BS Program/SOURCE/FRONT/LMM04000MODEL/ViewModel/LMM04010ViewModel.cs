using LMM04000COMMON;
using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
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
    public class LMM04010ViewModel : R_ViewModel<ProfileTaxDTO>
    {
        private LMM04010Model loModel = new LMM04010Model();

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public LMM04010DTO loContractorProfile = null;

        public LMM04010ResultDTO loRtn = null;

        //public GetContractorGroupResultDTO loContractorGroupRtn = null;

        //public List<GetContractorGroupDTO> loContractorGroupList = null;

        //public GetContractorCategoryResultDTO loContractorCategoryRtn = null;

        //public List<GetContractorCategoryDTO> loContractorCategoryList = null;
        
        public ProfileTaxDTO loProfileAndTaxInfo = new ProfileTaxDTO();


        public async Task GetContractorProfileAsync()
        {
            R_Exception loException = new R_Exception();
            LMM04010ParameterDTO poParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM04010_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                poParam = new LMM04010ParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };
                loRtn = await loModel.GetContractorProfileAsync(poParam);
                loContractorProfile = loRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        //public async Task GetContractorGroupListAsync()
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
        //        loContractorGroupRtn = await loModel.GetContractorGroupListAsync();
        //        loContractorGroupList = loContractorGroupRtn.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //}

        //public async Task GetContractorCategoryListAsync()
        //{
        //    R_Exception loException = new R_Exception();
        //    try
        //    {
        //        R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04010_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
        //        loContractorCategoryRtn = await loModel.GetContractorCategoryListAsync();
        //        loContractorCategoryList = loContractorCategoryRtn.Data;
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //    }
        //    loException.ThrowExceptionIfErrors();
        //}

        public void ContractorProfileValidation(LMM04010DTO poParam)
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

        public void ContractorProfileAndTaxInfoValidation(ProfileTaxDTO poParam)
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

        public async Task SaveContractorProfileAndTaxInfoAsync(ProfileTaxDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {/*
                R_FrontContext.R_SetContext(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.LMM04010_TENANT_ID_CONTEXT, loContractorProfile.CTENANT_ID);
*/
                ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO()
                {
                    Profile = poEntity.Profile,
                    TaxInfo = poEntity.TaxInfo,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loContractorProfile.CTENANT_ID
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

        public async Task DeleteContractorProfileAndTaxInfoAsync(ProfileTaxDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {/*
                R_FrontContext.R_SetContext(ContextConstant.LMM04010_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.LMM04010_TENANT_ID_CONTEXT, loContractorProfile.CTENANT_ID);*/
                ProfileTaxParameterDTO loParam = new ProfileTaxParameterDTO()
                {
                    Profile = poEntity.Profile,
                    TaxInfo = poEntity.TaxInfo,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loContractorProfile.CTENANT_ID
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
