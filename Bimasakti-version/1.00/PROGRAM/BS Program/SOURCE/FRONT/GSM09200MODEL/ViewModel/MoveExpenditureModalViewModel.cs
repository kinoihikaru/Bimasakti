using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using GSM09200COMMON.DTOs.GSM09210;
using GSM09200COMMON;
using System.Linq;

namespace GSM09200MODEL.ViewModel
{
    public class MoveExpenditureModalViewModel : R_ViewModel<ExpenditureListForMoveProcessDTO>
    {
        private GSM09210Model loModel = new GSM09210Model();

        public GSM09210ResultDTO loRtn = new GSM09210ResultDTO();

        public GetExpenditureCategoryDTO loFromExpenditureCategory = new GetExpenditureCategoryDTO();

        public GetExpenditureCategoryDTO loToExpenditureCategory = new GetExpenditureCategoryDTO();

        public GetExpenditureCategoryResultDTO loExpenditureCategoryRtn = new GetExpenditureCategoryResultDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public ObservableCollection<ExpenditureListForMoveProcessDTO> loExpenditureList = new ObservableCollection<ExpenditureListForMoveProcessDTO>();

        public List<ExpenditureListForMoveProcessDTO> loGetExpenditureBatchList = new List<ExpenditureListForMoveProcessDTO>();

        public string lcExpenditureId = "";


        public async Task GetExpenditureListForMoveProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09210_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.GSM09210_FROM_EXPENDITURE_CATEGORY_ID_STREAMING_CONTEXT, loFromExpenditureCategory.CCATEGORY_ID);
                loRtn = await loModel.GetExpenditureListStreamAsync();

                loExpenditureList.Clear();
                foreach (var item in loRtn.Data)
                {
                    loExpenditureList.Add(new ExpenditureListForMoveProcessDTO() { CEXPENDITURE_ID = item.CEXPENDITURE_ID, CEXPENDITURE_NAME = item.CEXPENDITURE_NAME });
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task MoveExpenditureCategory()
        {
            R_Exception loException = new R_Exception();
            MoveExpenditureCategoryParameterDTO loParam = null;
            List<ExpenditureListForMoveProcessDTO> loSelectedExpenditure = new List<ExpenditureListForMoveProcessDTO>();

            try
            {
                loSelectedExpenditure = loGetExpenditureBatchList.Where(x => x.LSELECTED == true).ToList();
                if (loSelectedExpenditure.Count == 0)
                {
                    return;
                }
                lcExpenditureId = "";
                foreach (ExpenditureListForMoveProcessDTO item in loSelectedExpenditure)
                {
                    lcExpenditureId = lcExpenditureId + item.CEXPENDITURE_ID + ",";
                }
                if (!string.IsNullOrEmpty(lcExpenditureId))
                {
                    lcExpenditureId = lcExpenditureId.Substring(0, lcExpenditureId.Length - 1); // Remove the last comma and space
                }

                loParam = new MoveExpenditureCategoryParameterDTO()
                {
                    CEXPENDITURE_ID = lcExpenditureId,
                    CSELECTED_PROPERTY_ID = loProperty.CPROPERTY_ID,
                    CFROM_EXPENDITURE_CATEGORY_ID = loFromExpenditureCategory.CCATEGORY_ID,
                    CTO_EXPENDITURE_CATEGORY_ID = loToExpenditureCategory.CCATEGORY_ID
                };
                await loModel.MoveExpenditureCategoryAsync(loParam);
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
