using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00510ViewModel : R_ViewModel<GSL00510DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL00510DTO> COAGrid = new ObservableCollection<GSL00510DTO>();
        public GSL00510ParameterDTO COAParameter {  get; set; } = new GSL00510ParameterDTO();
        public bool Inactive_Coa { get; set; } = false;
        public async Task GetCOAList()
        {
            var loEx = new R_Exception();

            try
            {
                //set Inactive COA Param
                COAParameter.LINACTIVE_COA = Inactive_Coa;
                var loResult = await _model.GSL00510GetCOAListAsync(COAParameter);

                COAGrid = new ObservableCollection<GSL00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
