using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using System.Xml.Linq;
using LMM03500COMMON.DTOs.LMM03500;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd;
using Microsoft.JSInterop;

namespace LMM03500FRONT
{
    public partial class LMM03501 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }

        private LMM03501ViewModel loTenantViewModel = new LMM03501ViewModel();

        private R_ConductorGrid _conductorTenantRef;

        private R_Grid<LMM03501DTO> _gridTenantRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loTenantViewModel.SelectedProperty = (SelectedPropertyDTO)poParameter;
                loTenantViewModel.loProperty.CPROPERTY_ID = loTenantViewModel.SelectedProperty.CPROPERTY_ID;

                await _gridTenantRef.R_RefreshGrid(null);
                /*
                                if (!string.IsNullOrWhiteSpace(_poPropertyId))
                                {
                                    loTenantViewModel.loProperty.CPROPERTY_ID = _poPropertyId;
                                    await _gridTenantRef.R_RefreshGrid(null);
                                }*/
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateTenantDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loTenantViewModel.DownloadTemplateTenantAsync();

                    var saveFileName = $"Tenant.xlsx";
                    /*
                                        var saveFileName = $"Staff {CenterViewModel.PropertyValueContext}.xlsx";*/

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new UploadTenantParameterDTO()
            {
                PropertyData = loTenantViewModel.SelectedProperty
            };
            eventArgs.TargetPageType = typeof(UploadTenant);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridTenantRef.R_RefreshGrid(null);
            }
        }

        private async Task Grid_Tenant_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loTenantViewModel.GetTenantListStreamAsync();
                eventArgs.ListEntityResult = loTenantViewModel.loTenantList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Tenant_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Normal)
            {
                loTenantViewModel.loTenant = (LMM03501DTO)eventArgs.Data;
            }
        }
    }
}