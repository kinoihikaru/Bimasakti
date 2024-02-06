using GLT00100COMMON;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00120ViewModel : R_ViewModel<GLT00100DTO>
    {
        #region Model
        private GLT00100Model _GLT00100Model = new GLT00100Model();
        private GLT00100UniversalModel _GLT00100UniversalModel = new GLT00100UniversalModel();
        #endregion

        #region Initial Property
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM = new GLT00100GLSystemParamDTO();
        #endregion

        #region Public Property ViewModel
        public ObservableCollection<GLT00100DTO> JournalGrid { get; set; } = new ObservableCollection<GLT00100DTO>();
        #endregion
        public async Task GetInitialVarData()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00100UniversalModel.GetGLSystemParamAsync();

                VAR_GL_SYSTEM_PARAM = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalList(GLT00100ParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00100Model.GetJournalListAsync(poParam);

                JournalGrid = new ObservableCollection<GLT00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GLT00100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
