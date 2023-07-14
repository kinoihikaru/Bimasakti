using System;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using System.Collections.ObjectModel;
using GLM00400COMMON;
using R_BlazorFrontEnd;

namespace GLM00400MODEL
{
    public class GLM00410ViewModel : R_ViewModel<GLM00410DTO>
    {
        private GLM00410Model _GLM00410Model = new GLM00410Model();

        GLM00410DTO AllocationJournalDT { get; set; } = new GLM00410DTO();

        public ObservableCollection<GLM00411DTO> AllocationAccountGrid { get; set; } = new ObservableCollection<GLM00411DTO>();

        public async Task GetAllocationAccountList(GLM00411DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00410Model.GetAllocationAccountListAsync(poParam);

                AllocationAccountGrid = new ObservableCollection<GLM00411DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void GetAllocationJournalDT(GLM00410DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                AllocationJournalDT = poParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
