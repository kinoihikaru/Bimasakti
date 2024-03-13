using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02400ViewModel : R_ViewModel<GSL02400DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL02400DTO> FloorGrid = new ObservableCollection<GSL02400DTO>();

        public async Task GetFloorList(GSL02400ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02400GetFloorListAsync(poParameter);

                FloorGrid = new ObservableCollection<GSL02400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
