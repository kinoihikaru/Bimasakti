using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09300COMMON.DTOs.GSM09300;
using GSM09300COMMON;

namespace GSM09300MODEL.ViewModel
{
    public class GSM09300ViewModel : R_ViewModel<GSM09300DetailDTO>
    {
        private GSM09300Model loModel = new GSM09300Model();

        public GSM09300DetailDTO loSupplierCategoryDetail = null;

        public ObservableCollection<GSM09300DTO> loSupplierCategoryList = new ObservableCollection<GSM09300DTO>();

        public GSM09300ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public List<GSM09300DTO> loParentPositionList = new List<GSM09300DTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        //public GetParentPositionIfEmptyDTO loParentPositionIfEmpty = new GetParentPositionIfEmptyDTO();

        public void UpdateParentPositionList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loParentPositionList = new List<GSM09300DTO>(loSupplierCategoryList);
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
                loPropertyList = new List<GetPropertyListDTO>(loPropertyRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void SupplierCategoryValidation(GSM09300DetailDTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CCATEGORY_ID);
                if (llCancel)
                {
                    loEx.Add("", "Category Code is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCATEGORY_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Category Name is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSupplierCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09300_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetSupplierCategoryListStreamAsync();
                loParentPositionList = new List<GSM09300DTO>(loRtn.Data);
                loSupplierCategoryList = new ObservableCollection<GSM09300DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        
        public async Task ValidateIsCategoryExistAsync()
        {
            R_Exception loEx = new R_Exception();
            ValidateIsCategoryExistParameterDTO loParam = null;

            try
            {
                loParam = new ValidateIsCategoryExistParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID
                };
                await loModel.ValidateIsCategoryExistAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task GetSupplierCategoryAsync(GSM09300DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09300DetailDTO loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loSupplierCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveSupplierCategoryAsync(GSM09300DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09300DetailDTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loSupplierCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteSupplierCategoryAsync(GSM09300DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09300_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
