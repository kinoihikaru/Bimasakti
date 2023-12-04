using APM00200COMMON.DTOs.APM00200;
using APM00200MODEL.ViewModel;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APM00200FRONT
{
    public partial class APM00200CopyFrom : R_Page
    {
        private APM00200ViewModel loExpenditureViewModel = new APM00200ViewModel();

        private R_Grid<APM00200DTO> _gridExpenditureRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureViewModel.GetPropertyListStreamAsync();

                if (loExpenditureViewModel.loPropertyList.Count() > 0)
                {
                    loExpenditureViewModel.loProperty = loExpenditureViewModel.loPropertyList.FirstOrDefault();
                    await PropertyDropdown_ValueChanged(loExpenditureViewModel.loProperty.CPROPERTY_ID);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_ValueChanged(string poParam)
        {
            var loEx = new R_Exception();

            try
            {
                loExpenditureViewModel.loProperty.CPROPERTY_ID = poParam;
                await _gridExpenditureRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loExpenditureViewModel.GetExpenditureListStreamAsync();
                eventArgs.ListEntityResult = loExpenditureViewModel.loExpenditureList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRefresh()
        {
            await _gridExpenditureRef.R_RefreshGrid(null);
        }

        private void OnClickOK()
        {
            var loTemp = _gridExpenditureRef.CurrentSelectedData;
            this.Close(true, loTemp);
        }

        private void OnClickCancel()
        {
            this.Close(false, null);
        }
    }
}
