using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON;

namespace GSM09200MODEL.ViewModel
{
    public class GSM09210ViewModel : R_ViewModel<GSM09210DTO>
    {
        private GSM09210Model loModel = new GSM09210Model();
        /*
                public GSM09210DTO loExpenditure = new GSM09210DTO();*/

        public ObservableCollection<GSM09210DTO> loExpenditureList = new ObservableCollection<GSM09210DTO>();

        public GSM09210ResultDTO loRtn = new GSM09210ResultDTO();

        public GetExpenditureCategoryDTO loExpenditureCategory = new GetExpenditureCategoryDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public async Task GetExpenditureListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09210_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09210_FROM_EXPENDITURE_CATEGORY_ID_STREAMING_CONTEXT, loExpenditureCategory.CCATEGORY_ID);
                loRtn = await loModel.GetExpenditureListStreamAsync();
                loExpenditureList = new ObservableCollection<GSM09210DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetExpenditureCategoryAsync()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetContext(ContextConstant.GSM09210_PROPERTY_ID_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetContext(ContextConstant.GSM09210_EXPENDITURE_CATEGORY_ID_CONTEXT, loExpenditureCategory.CCATEGORY_ID);
                GetExpenditureCategoryResultDTO loResult = await loModel.GetExpenditureCategoryAsync();

                loExpenditureCategory = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
