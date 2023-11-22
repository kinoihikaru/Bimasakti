using LMM04000COMMON.DTOs;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.DTOs.LMM04020;
using LMM04000MODEL.ViewModel;
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

namespace LMM04000FRONT
{
    public partial class LMM04020 : R_Page
    {
        private LMM04010ViewModel loContractorProfileViewModel = new LMM04010ViewModel();

        private LMM04020ViewModel loTaxInfoViewModel = new LMM04020ViewModel();

        private R_Conductor _conductorTaxInfoRef;

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private NextParameterDTO loParam = new NextParameterDTO();


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loParam = (NextParameterDTO)poParameter;
                loTaxInfoViewModel.loTabParameter = loParam.TabParameter;

                await loTaxInfoViewModel.GetTaxCodeListStreamAsync();
                await loTaxInfoViewModel.GetIdTypeListStreamAsync();
                await loTaxInfoViewModel.GetTaxTypeListStreamAsync();

                if (loContractorProfileViewModel.loTabParameter != null)
                {
                    if (loParam.ConductorMode == "ADD")
                    {

                    }
                    else if (loParam.ConductorMode == "NORMAL")
                    {
                        await _conductorTaxInfoRef.R_GetEntity(null);
                    }
                }
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
                ProfileTaxDTO loSaveParam = new ProfileTaxDTO()
                {
                    Profile = loParam.ProfileData,
                    TaxInfo = (LMM04020DTO)eventArgs.Data
                };
                await loContractorProfileViewModel.SaveContractorProfileAndTaxInfoAsync(
                    loSaveParam,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loContractorProfileViewModel.loProfileAndTaxInfo.TaxInfo;
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
                ProfileTaxDTO loSavingParam = new ProfileTaxDTO()
                {
                    Profile = loParam.ProfileData,
                    TaxInfo = (LMM04020DTO)eventArgs.Data
                };
                loContractorProfileViewModel.ContractorProfileAndTaxInfoValidation(loSavingParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
