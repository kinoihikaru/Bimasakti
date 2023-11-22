using LMM01000COMMON;
using LMM01000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01002ViewModel : R_ViewModel<LMM01000PrintParamDTO>
    {
        private LMM01000UniversalModel _LMM01000UniversalModel = new LMM01000UniversalModel();

        public List<LMM01000UniversalDTO> RateTypeFromList = new List<LMM01000UniversalDTO>();

        public List<LMM01000UniversalDTO> RateTypeToList = new List<LMM01000UniversalDTO>();

        public LMM01000DTOPropety Property = new LMM01000DTOPropety();
        // Status Radio Button
        public List<RadioButton> ShortByList { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ChargesId")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_ChargesName") },
        };

        public async Task GetRateTypeFromList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetChargesTypeListAsync();

                RateTypeFromList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetRateTypeToList(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01000UniversalModel.GetChargesTypeListAsync();

                RateTypeToList = new List<LMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }
}
