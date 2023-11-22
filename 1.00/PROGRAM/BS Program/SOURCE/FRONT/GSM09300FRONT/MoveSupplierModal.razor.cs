using GSM09300COMMON.DTOs.GSM09310;
using GSM09300MODEL.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GSM09300FRONT
{
    public partial class MoveSupplierModal : R_Page
    {
        private MoveSupplierModalViewModel loMoveSupplierViewModel = new MoveSupplierModalViewModel();

        private R_ConductorGrid _conductorMoveSupplierRef;

        private R_Grid<SupplierListForMoveProcessDTO> _gridMoveSupplierRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();

            try
            {
                loParam = (MoveProcessParameterDTO)poParameter;

                loMoveSupplierViewModel.loFromSupplierCategory = loParam.SupplierCategoryData;
                loMoveSupplierViewModel.loProperty = loParam.PropertyData;

                await _gridMoveSupplierRef.R_RefreshGrid(null);
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
                await loMoveSupplierViewModel.GetSupplierListForMoveProcess();
                eventArgs.ListEntityResult = loMoveSupplierViewModel.loSupplierList;
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
                if (loMoveSupplierViewModel.loFromSupplierCategory.CCATEGORY_ID == loMoveSupplierViewModel.loToSupplierCategory.CCATEGORY_ID)
                {
                    await R_MessageBox.Show("", "To Supplier Category cannot be same as From Supplier Category", R_eMessageBoxButtonType.OK);
                    return;
                }

                await _conductorMoveSupplierRef.R_SaveBatch();
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

        private void BeforeOpenSupplierListLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL01800DTOParameter()
            {
                CPROPERTY_ID = loMoveSupplierViewModel.loProperty.CPROPERTY_ID,
                CCATEGORY_TYPE = "50"
            };
            eventArgs.TargetPageType = typeof(GSL01800);
        }

        private void AfterOpenSupplierListLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL01800DTO loTempResult = (GSL01800DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loMoveSupplierViewModel.loToSupplierCategory.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
            loMoveSupplierViewModel.loToSupplierCategory.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;
        }

        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<SupplierListForMoveProcessDTO>)events.Data;

            events.Cancel = loData.Count == 0;
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                loMoveSupplierViewModel.loGetSupplierBatchList = (List<SupplierListForMoveProcessDTO>)eventArgs.Data;
                await loMoveSupplierViewModel.MoveSupplierCategory();
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