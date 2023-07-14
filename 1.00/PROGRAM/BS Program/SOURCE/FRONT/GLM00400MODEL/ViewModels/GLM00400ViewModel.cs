using System;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using System.Collections.ObjectModel;
using GLM00400COMMON;

namespace GLM00400MODEL
{
    public class GLM00400ViewModel 
    {
        private GLM00400Model _GLM00400Model = new GLM00400Model();

        public ObservableCollection<GLM00400DTO> AllocationJournalHDGrid { get; set; } = new ObservableCollection<GLM00400DTO>();

        public GLM00400InitialDTO InitialVar = new GLM00400InitialDTO();
        public GLM00400GLSystemParamDTO SystemParam = new GLM00400GLSystemParamDTO();

        public async Task GetInitialVar(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00400Model.GetInitialVarAsync(poParam);

                InitialVar = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _GLM00400Model.GetSystemParamAsync(poParam);

                SystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAllocationJournalHDList(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLM00400Model.GetAllocationJournalHDListAsync(poParam);

                AllocationJournalHDGrid = new ObservableCollection<GLM00400DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
