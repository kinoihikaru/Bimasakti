using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02300ViewModel : R_ViewModel<GSL02300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public ObservableCollection<GSL02300DTO> BuildingUnitGrid = new ObservableCollection<GSL02300DTO>();

        public async Task GetBuildingUnitList(GSL02300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02300GetBuildingUnitListAsync(poParameter);

                BuildingUnitGrid = new ObservableCollection<GSL02300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
