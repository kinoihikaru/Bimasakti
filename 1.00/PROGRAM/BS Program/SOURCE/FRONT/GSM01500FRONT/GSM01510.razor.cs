using GSM01500COMMON.DTOs;
using GSM01500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace GSM01500FRONT
{
    public partial class GSM01510 : R_Page
    {
        private GSM01510ViewModel DepartmentViewModel = new GSM01510ViewModel();

        private R_ConductorGrid _conGridDeptRef;

        private R_Grid<GSM01510DepartmentDTO> _gridDeptRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                DepartmentViewModel.loCenter = (SelectedCenterCodeDTO)poParameter;
                await _gridDeptRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Departemnt_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await DepartmentViewModel.GetDepartmentListStreamAsync();
                eventArgs.ListEntityResult = DepartmentViewModel.loDeptList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
