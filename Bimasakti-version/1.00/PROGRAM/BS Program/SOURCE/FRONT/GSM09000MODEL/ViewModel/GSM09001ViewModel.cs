using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09001;
using GSM09000MODEL;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Text;
using System.Threading.Tasks;

namespace GSM09001MODEL.ViewModel
{
    public class GSM09001ViewModel : R_ViewModel<GSM09001DTO>
    {
        private GSM09001Model loModel = new GSM09001Model();
/*
        public GSM09001DTO loTenant = new GSM09001DTO();*/

        public ObservableCollection<GSM09001DTO> loTenantList = new ObservableCollection<GSM09001DTO>();

        public GSM09001ResultDTO loRtn = new GSM09001ResultDTO();

        public GetTenantCategoryDTO loTenantCategory = new GetTenantCategoryDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public async Task GetTenantListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09001_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09001_FROM_TENANT_CATEGORY_ID_STREAMING_CONTEXT, loTenantCategory.CCATEGORY_ID);
                loRtn = await loModel.GetTenantListStreamAsync();
                loTenantList = new ObservableCollection<GSM09001DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTenantCategoryAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_TENANT_CATEGORY_ID_CONTEXT, loTenantCategory.CCATEGORY_ID);
                GetTenantCategoryResultDTO loResult = await loModel.GetTenantCategoryAsync();

                loTenantCategory = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
