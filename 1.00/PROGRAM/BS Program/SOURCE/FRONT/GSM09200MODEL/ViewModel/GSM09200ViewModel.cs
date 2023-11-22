using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09200COMMON.DTOs.GSM09200;
using GSM09200COMMON;

namespace GSM09200MODEL.ViewModel
{
    public class GSM09200ViewModel : R_ViewModel<GSM09200DetailDTO>
    {
        private GSM09200Model loModel = new GSM09200Model();

        public GSM09200DetailDTO loExpenditureCategoryDetail = null;

        public ObservableCollection<GSM09200DTO> loExpenditureCategoryList = new ObservableCollection<GSM09200DTO>();

        public GSM09200ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public List<GSM09200DTO> loParentPositionList = new List<GSM09200DTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        //public GetParentPositionIfEmptyDTO loParentPositionIfEmpty = new GetParentPositionIfEmptyDTO();

        public void UpdateParentPositionList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loParentPositionList = new List<GSM09200DTO>(loExpenditureCategoryList);
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

        public void ExpenditureCategoryValidation(GSM09200DetailDTO poParam)
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

        public async Task GetExpenditureCategoryListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09200_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetExpenditureCategoryListStreamAsync();
                loParentPositionList = new List<GSM09200DTO>(loRtn.Data);
                loExpenditureCategoryList = new ObservableCollection<GSM09200DTO>(loRtn.Data);
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


        public async Task GetExpenditureCategoryAsync(GSM09200DetailDTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09200DetailDTO loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loExpenditureCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveExpenditureCategoryAsync(GSM09200DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                GSM09200DetailDTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loExpenditureCategoryDetail = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteExpenditureCategoryAsync(GSM09200DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09200_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
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
