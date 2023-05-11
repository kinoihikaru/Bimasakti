using GSM03000Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GSM03000MODEL.ViewModel
{
    public class GSM03000ViewModelDeducation : R_ViewModel<GSM03000DTO>
    {
        private GSM03000Model _GSM03000Model = new GSM03000Model();
        public ObservableCollection<GSM03000DTO> OtherChargeList { get; set; } = new ObservableCollection<GSM03000DTO>();
        public List<GSM03000PropertyDTO> PropertyList { get; set; } = new List<GSM03000PropertyDTO>();

        public GSM03000DTO OtherChargesDetail = new GSM03000DTO();

        public string PropertyValue = "";
        private string PropertyTypeContextDeducation = "D";

        public async Task GetOtherChargesList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM03000Model.GetOtherChargesListAsync(PropertyValue, PropertyTypeContextDeducation);

                OtherChargeList = new ObservableCollection<GSM03000DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM03000Model.GetPropertyAsync();
                PropertyList = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOtherChargesDetail(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM03000Model.R_ServiceGetRecordAsync(poParam);

                OtherChargesDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveOtherCharges(GSM03000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _GSM03000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                OtherChargesDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteOtherCharges(GSM03000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM03000Model.R_ServiceDeleteAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // Status Radio Button

        public List<RadioModelDecucation> OptionsStatus { get; set; } = new List<RadioModelDecucation>
        {
            new RadioModelDecucation { Id = "00", Text = "Draft"},
            new RadioModelDecucation { Id = "80", Text = "Active" },
            new RadioModelDecucation { Id = "90", Text = "Inactive" },
        };
    }

    public class RadioModelDecucation
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
