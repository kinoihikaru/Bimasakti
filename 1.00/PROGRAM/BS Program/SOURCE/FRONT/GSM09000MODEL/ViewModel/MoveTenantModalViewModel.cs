using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09001;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000MODEL.ViewModel
{
    public class MoveTenantModalViewModel : R_ViewModel<TenantListForMoveProcessDTO>
    {
        private GSM09001Model loModel = new GSM09001Model();

        public GSM09001ResultDTO loRtn = new GSM09001ResultDTO();

        public GetTenantCategoryDTO loFromTenantCategory = new GetTenantCategoryDTO();

        public GetTenantCategoryDTO loToTenantCategory = new GetTenantCategoryDTO();

        public GetTenantCategoryResultDTO loTenantCategoryRtn = new GetTenantCategoryResultDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public ObservableCollection<TenantListForMoveProcessDTO> loTenantList = new ObservableCollection<TenantListForMoveProcessDTO>();

        public List<TenantListForMoveProcessDTO> loGetTenantBatchList = new List<TenantListForMoveProcessDTO>();

        public string lcTenantId = "";


        public async Task GetTenantListForMoveProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09001_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09001_FROM_TENANT_CATEGORY_ID_STREAMING_CONTEXT, loFromTenantCategory.CCATEGORY_ID);
                loRtn = await loModel.GetTenantListStreamAsync();

                loTenantList.Clear();
                foreach (var item in loRtn.Data)
                {
                    loTenantList.Add(new TenantListForMoveProcessDTO() { CTENANT_ID = item.CTENANT_ID, CTENANT_NAME = item.CTENANT_NAME });
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveMoveTenantCategory()
        {
            R_Exception loException = new R_Exception();
            SaveMoveTenantCategoryParameterDTO loParam = null;
            List<TenantListForMoveProcessDTO> loSelectedTenant = new List<TenantListForMoveProcessDTO>();

            try
            {
                loSelectedTenant = loGetTenantBatchList.Where(x => x.LSELECTED == true).ToList();
                if (loSelectedTenant.Count() == 0)
                {
                    return;
                }
                lcTenantId = "";
                foreach (TenantListForMoveProcessDTO item in loSelectedTenant)
                {
                    lcTenantId = lcTenantId + item.CTENANT_ID + ",";
                }
                if (!string.IsNullOrEmpty(lcTenantId))
                {
                    lcTenantId = lcTenantId.Substring(0, lcTenantId.Length - 1); // Remove the last comma and space
                }
                /*
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_TENANT_ID_CONTEXT, lcTenantId);
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_FROM_TENANT_CATEGORY_ID_CONTEXT, loFromTenantCategory.CCATEGORY_ID);
                R_FrontContext.R_SetContext(ContextConstant.GSM09001_TO_TENANT_CATEGORY_ID_CONTEXT, loToTenantCategory.CCATEGORY_ID);
                
                                SaveTenantContextParameterDTO loContext = new SaveTenantContextParameterDTO()
                                {
                                    CTENANT_ID = lcTenantId,
                                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                                    CFROM_TENANT_CATEGORY_ID = loFromTenantCategory.CTENANT_CATEGORY_ID,
                                    CTO_TENANT_CATEGORY_ID = loToTenantCategory.CTENANT_CATEGORY_ID
                                };

                                R_FrontContext.R_SetContext(ContextConstant.GSM09001_TENANT_ID_CONTEXT, loContext);
                */

                loParam = new SaveMoveTenantCategoryParameterDTO()
                {
                    CTENANT_ID = lcTenantId,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID,
                    CFROM_TENANT_CATEGORY_ID = loFromTenantCategory.CCATEGORY_ID,
                    CTO_TENANT_CATEGORY_ID = loToTenantCategory.CCATEGORY_ID
                };
                await loModel.SaveMoveTenantCategoryAsync(loParam);
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
