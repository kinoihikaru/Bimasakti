using GSM01500COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM01500MODEL.ViewModel
{
    public class GSM01510ViewModel : R_ViewModel<GSM01510DepartmentDTO>
    {

        private GSM01510Model loModel = new GSM01510Model();

        public ObservableCollection<GSM01510DepartmentDTO> loDeptList = new ObservableCollection<GSM01510DepartmentDTO>();

        public GSM01510DepartmentListDTO loRtn = null;

        public SelectedCenterCodeDTO loCenter = new SelectedCenterCodeDTO();

        public async Task GetDepartmentListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CENTER_CODE_STREAM_CONTEXT, loCenter.CENTER_ID);

                loRtn = await loModel.GetCenterDepartmentListStreamAsync();
                loDeptList = new ObservableCollection<GSM01510DepartmentDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
