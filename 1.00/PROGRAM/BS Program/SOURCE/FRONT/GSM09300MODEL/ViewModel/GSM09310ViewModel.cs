using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09300COMMON.DTOs.GSM09310;
using GSM09300COMMON;

namespace GSM09300MODEL.ViewModel
{
    public class GSM09310ViewModel : R_ViewModel<GSM09310DTO>
    {
        private GSM09310Model loModel = new GSM09310Model();
        /*
                public GSM09310DTO loSupplier = new GSM09310DTO();*/

        public ObservableCollection<GSM09310DTO> loSupplierList = new ObservableCollection<GSM09310DTO>();

        public GSM09310ResultDTO loRtn = new GSM09310ResultDTO();

        public GetSupplierCategoryDTO loSupplierCategory = new GetSupplierCategoryDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public async Task GetSupplierListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09310_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09310_FROM_SUPPLIER_CATEGORY_ID_STREAMING_CONTEXT, loSupplierCategory.CCATEGORY_ID);
                loRtn = await loModel.GetSupplierListStreamAsync();
                loSupplierList = new ObservableCollection<GSM09310DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetSupplierCategoryAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09310_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.GSM09310_SUPPLIER_CATEGORY_ID_CONTEXT, loSupplierCategory.CCATEGORY_ID);
                GetSupplierCategoryResultDTO loResult = await loModel.GetSupplierCategoryAsync();

                loSupplierCategory = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
