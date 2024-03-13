using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02600ViewModel : R_ViewModel<GSL02600DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL02600DTO> CBAccountGrid = new ObservableCollection<GSL02600DTO>();

        public async Task GetCBAccountList(GSL02600ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02600GetCBAccountListAsync(poParameter);

                CBAccountGrid = new ObservableCollection<GSL02600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
