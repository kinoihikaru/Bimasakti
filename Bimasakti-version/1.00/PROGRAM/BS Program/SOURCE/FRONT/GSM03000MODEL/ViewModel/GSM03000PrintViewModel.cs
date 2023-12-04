using GSM03000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GSM03000MODEL.ViewModel
{
    public class GSM03000PrintViewModel : R_ViewModel<GSM03000PrintParamDTO>
    {
        private GSM03000Model _GSM03000Model = new GSM03000Model();
        public List<GSM03000DTO> OtherChargeList { get; set; } = new List<GSM03000DTO>();

        public GSM03000DTO OtherChargesDetail = new GSM03000DTO();
        // Status Radio Button
        public List<RadioModel> ShortByList { get; set; } = new List<RadioModel>
        {
            new RadioModel { Id = "01", Text = "Charges Id"},
            new RadioModel { Id = "02", Text = "Charges Name" },
        };

        public GSM03000PropertyDTO Property = new GSM03000PropertyDTO();

        public async Task GetProperty(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loListPropery = await _GSM03000Model.GetPropertyAsync();

                Property = loListPropery.Where(k => k.CPROPERTY_ID == poParam.CPROPERTY_ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOtherChargesList(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                OtherChargesDetail = poParam;
                var loResult = await _GSM03000Model.GetOtherChargesListAsync(poParam.CPROPERTY_ID, poParam.CCHARGES_TYPE);

                OtherChargeList = new List<GSM03000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
    }
}
