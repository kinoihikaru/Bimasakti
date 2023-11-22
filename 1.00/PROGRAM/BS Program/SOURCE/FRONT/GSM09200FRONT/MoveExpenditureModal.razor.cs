using GSM09200COMMON.DTOs.GSM09210;
using GSM09200MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GSM09200FRONT
{
    public partial class MoveExpenditureModal : R_Page
    {
        private MoveExpenditureModalViewModel loMoveExpenditureViewModel = new MoveExpenditureModalViewModel();

        private R_ConductorGrid _conductorMoveExpenditureRef;

        private R_Grid<ExpenditureListForMoveProcessDTO> _gridMoveExpenditureRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();

            try
            {
                loParam = (MoveProcessParameterDTO)poParameter;

                loMoveExpenditureViewModel.loFromExpenditureCategory = loParam.ExpenditureCategoryData;
                loMoveExpenditureViewModel.loProperty = loParam.PropertyData;
                
                await _gridMoveExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loMoveExpenditureViewModel.GetExpenditureListForMoveProcess();
                eventArgs.ListEntityResult = loMoveExpenditureViewModel.loExpenditureList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickProcessButton()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (loMoveExpenditureViewModel.loFromExpenditureCategory.CCATEGORY_ID == loMoveExpenditureViewModel.loToExpenditureCategory.CCATEGORY_ID)
                {
                    await R_MessageBox.Show("", "To Expenditure Category cannot be same as From Expenditure Category", R_eMessageBoxButtonType.OK);
                    return;
                }

                await _conductorMoveExpenditureRef.R_SaveBatch();
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancelButton()
        {
            await this.Close(true, false);
        }

        private void BeforeOpenExpenditureListLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL01800DTOParameter()
            {
                CPROPERTY_ID = loMoveExpenditureViewModel.loProperty.CPROPERTY_ID,
                CCATEGORY_TYPE = "40"
            };
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void AfterOpenExpenditureListLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL01800DTO loTempResult = (GSL01800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loMoveExpenditureViewModel.loToExpenditureCategory.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
            loMoveExpenditureViewModel.loToExpenditureCategory.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }

        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<ExpenditureListForMoveProcessDTO>)events.Data;

            events.Cancel = loData.Count == 0;
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                loMoveExpenditureViewModel.loGetExpenditureBatchList = (List<ExpenditureListForMoveProcessDTO>)eventArgs.Data;
                await loMoveExpenditureViewModel.MoveExpenditureCategory();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_AfterSaveBatch(R_AfterSaveBatchEventArgs eventArgs)
        {

        }
    }
}