using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02000ViewModel : R_ViewModel<GSL02000CityDTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();

        public List<GSL02000CountryDTO> CountryGeography { get; set; } = new List<GSL02000CountryDTO>();
        public List<GSL02000CityDTO> CityGeographyTree { get; set; } = new List<GSL02000CityDTO>();
        public GSL02000CountryDTO Country { get ; set; } = new GSL02000CountryDTO();
        public string CountryID { get; set; } = "";
        public async Task GetCountryGeographyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02000GetCountryGeographyListAsync();

                CountryGeography = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCityGeographyList(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02000GetCityGeographyListListAsync(poParam);

                var loParentData = R_FrontUtility.ConvertObjectToObject<GSL02000CityDTO>(Country);

                loResult.Add(loParentData);

                CityGeographyTree = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
