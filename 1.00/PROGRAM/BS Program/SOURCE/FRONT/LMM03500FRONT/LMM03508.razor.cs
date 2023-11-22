using LMM03500COMMON.DTOs.LMM03507;
using LMM03500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03500COMMON.DTOs.LMM03508;
using R_BlazorFrontEnd.Exceptions;
using LMM03500COMMON.DTOs;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;

namespace LMM03500FRONT
{
    public partial class LMM03508 : R_Page, R_ITabPage
    {
        private LMM03508ViewModel loFixVAViewModel = new LMM03508ViewModel();

        private R_ConductorGrid _conductorFixVARef;

        private R_Grid<LMM03508DTO> _gridFixVARef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loFixVAViewModel.loTabParameter = (TabParameterDTO)poParameter;

                await loFixVAViewModel.GetTenantAsync();

                await _gridFixVARef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task FixVA_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            loFixVAViewModel.loTabParameter = (TabParameterDTO)poParam;

            if (loFixVAViewModel.loTabParameter.CSELECTED_TENANT_ID != null)
            {
                await loFixVAViewModel.GetFixVAListStreamAsync();
            }
            else
            {
                loFixVAViewModel.loFixVAList.Clear();
            }
        }

        private async Task Grid_R_ServiceGetFixVAListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loFixVAViewModel.GetFixVAListStreamAsync();
                eventArgs.ListEntityResult = loFixVAViewModel.loFixVAList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
