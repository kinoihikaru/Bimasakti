using GSM09000COMMON.DTOs.GSM09001;
using GSM09000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace GSM09000FRONT
{
    public partial class MoveTenantModal : R_Page
    {
        private MoveTenantModalViewModel loMoveTenantViewModel = new MoveTenantModalViewModel();

        //private LookUpTenantCategoryViewModel loTenantCategoryViewModel = new LookUpTenantCategoryViewModel();

        private R_ConductorGrid _conductorMoveTenantRef;

        //private R_Grid<GetTenantCategoryDTO> _gridTenantCategoryRef;

        private R_Grid<TenantListForMoveProcessDTO> _gridMoveTenantRef;

        //private bool IsMoveTenantModalHidden = true;

        //private bool IsTenantCategoryHidden = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            MoveProcessParameterDTO loParam = new MoveProcessParameterDTO();

            try
            {
                loParam = (MoveProcessParameterDTO)poParameter;

                loMoveTenantViewModel.loFromTenantCategory = loParam.TenantCategoryData;
                loMoveTenantViewModel.loProperty = loParam.PropertyData;
                //SwitchModal(false);

                await _gridMoveTenantRef.R_RefreshGrid(null);
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
                await loMoveTenantViewModel.GetTenantListForMoveProcess();
                eventArgs.ListEntityResult = loMoveTenantViewModel.loTenantList;
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
                if (loMoveTenantViewModel.loFromTenantCategory.CCATEGORY_ID == loMoveTenantViewModel.loToTenantCategory.CCATEGORY_ID)
                {
                    await R_MessageBox.Show("", "To Tenant Category cannot be same as From Tenant Category", R_eMessageBoxButtonType.OK);
                    return;
                }

                await _conductorMoveTenantRef.R_SaveBatch();
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
        /*
                public async Task R_ServiceGetListRecordAsync(R_ServiceGetListRecordEventArgs eventArgs)
                {
                    var loEx = new R_Exception();

                    try
                    {
                        loTenantCategoryViewModel.loProperty = loMoveTenantViewModel.loProperty;
                        await loTenantCategoryViewModel.GetTenantCategoryListStreamAsync();

                        eventArgs.ListEntityResult = loTenantCategoryViewModel.loTenantCategoryList;
                    }
                    catch (Exception ex)
                    {
                        loEx.Add(ex);
                    }

                    loEx.ThrowExceptionIfErrors();
                }*/
        /*
                private void SwitchModal(bool plParam)
                {
                    IsMoveTenantModalHidden = plParam;
                    IsTenantCategoryHidden = !IsMoveTenantModalHidden;
                }
        */
        /*
                private void Button_OnClickLookUp()
                {
                    SwitchModal(true);
                    _gridTenantCategoryRef.R_RefreshGrid(null);
                }
        */
        /*      
                public async Task Button_OnClickOk()
                {
                    var loData = _gridTenantCategoryRef.GetCurrentData();
                    GetTenantCategoryDTO loTemp = (GetTenantCategoryDTO)loData;
                    if (loTemp.CCATEGORY_ID == loMoveTenantViewModel.loFromTenantCategory.CCATEGORY_ID)
                    {
                        await R_MessageBox.Show("", "To Tenant Category cannot be same as From Tenant Category", R_eMessageBoxButtonType.OK);
                        return;
                    }
                    loMoveTenantViewModel.loToTenantCategory = loTemp;
                    SwitchModal(false);
                }
                public void Button_OnClickClose()
                {
                    SwitchModal(false);
                }
        */


        private void BeforeOpenTenantListLookUp(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LookUpTenantListParameterDTO()
            {
                CPROPERTY_ID = loMoveTenantViewModel.loProperty.CPROPERTY_ID
            };
            eventArgs.TargetPageType = typeof(LookUpTenantList);
        }

        private void AfterOpenTenantListLookUp(R_AfterOpenLookupEventArgs eventArgs)
        {
            GetTenantCategoryDTO loTempResult = (GetTenantCategoryDTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            loMoveTenantViewModel.loToTenantCategory.CCATEGORY_ID = loTempResult.CCATEGORY_ID;
            loMoveTenantViewModel.loToTenantCategory.CCATEGORY_NAME = loTempResult.CCATEGORY_NAME;  
        }

        private void R_BeforeSaveBatch(R_BeforeSaveBatchEventArgs events)
        {
            var loData = (List<TenantListForMoveProcessDTO>)events.Data;

            events.Cancel = loData.Count == 0;
        }

        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                loMoveTenantViewModel.loGetTenantBatchList = (List<TenantListForMoveProcessDTO>)eventArgs.Data;
                await loMoveTenantViewModel.SaveMoveTenantCategory();
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