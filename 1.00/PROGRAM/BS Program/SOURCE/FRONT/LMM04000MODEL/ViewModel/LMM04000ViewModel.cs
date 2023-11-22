using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04010;

namespace LMM04000MODEL.ViewModel
{
    public class LMM04000ViewModel : R_ViewModel<LMM04000DTO>
    {
        private LMM04000Model loModel = new LMM04000Model();

        public LMM04000DTO loContractor = null;

        public ObservableCollection<LMM04000DTO> loContractorList = new ObservableCollection<LMM04000DTO>();

        public LMM04000ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = null;

        public GetPropertyListResultDTO loPropertyRtn = null;

        public SelectedPropertyDTO SelectedProperty = new SelectedPropertyDTO();

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetContractorListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM04000_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loRtn = await loModel.GetContractorListStreamAsync();
                loContractorList = new ObservableCollection<LMM04000DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        #region Template
        public async Task<TemplateContractorDTO> DownloadTemplateContractorAsync()
        {
            var loEx = new R_Exception();
            TemplateContractorDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateContractorAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion
    }
}
