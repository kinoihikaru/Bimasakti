using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500MODEL.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500FRONT
{
    public partial class LMM03503 : R_Page
    {
        private LMM03502ViewModel loTenantProfileViewModel = new LMM03502ViewModel();

        private LMM03503ViewModel loTaxInfoViewModel = new LMM03503ViewModel();

        private R_Conductor _conductorTaxInfoRef;

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loTaxInfoViewModel.loTabParameter = (TabParameterDTO)poParameter;

                await loTenantProfileViewModel.GetTenantTypeListAsync();

                await loTaxInfoViewModel.GetTaxCodeListStreamAsync();
                await loTaxInfoViewModel.GetIdTypeListStreamAsync();
                await loTaxInfoViewModel.GetTaxTypeListStreamAsync();

                await _conductorTaxInfoRef.R_GetEntity(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task TaxInfo_GetTaxInfoRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                await loTaxInfoViewModel.GetTaxInfoAsync();
                eventArgs.Result = loTaxInfoViewModel.loTaxInfo;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task TaxInfo_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantProfileViewModel.SaveTenantProfileAndTaxInfoAsync(
                    (ProfileTaxDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loTenantProfileViewModel.loProfileAndTaxInfo.TaxInfo;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void TaxInfo_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                ProfileTaxDTO loParam = new ProfileTaxDTO()
                {
                    Profile = new LMM03502DTO(),
                    TaxInfo = (LMM03503DTO)eventArgs.Data
                };
                loTenantProfileViewModel.TenantProfileAndTaxInfoValidation(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
