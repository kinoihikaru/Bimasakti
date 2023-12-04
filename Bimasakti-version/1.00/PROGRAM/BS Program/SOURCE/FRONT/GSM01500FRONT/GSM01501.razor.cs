using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM01500COMMON.DTOs;
using GSM01500COMMON.DTOs.GSM01500Print;
using GSM01500MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500FRONT
{
    public partial class GSM01501 : R_Page
    {
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private GSM01500ViewModel loCenterViewModel = new GSM01500ViewModel();
        
        private CenterPrintParameterDTO loPrintParameter = new CenterPrintParameterDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await loCenterViewModel.GetCenterListStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task CenterCodeToComboBoxValueChanged(string poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (loPrintParameter.CCENTER_CODE_FROM == poValue)
                {
                    await R_MessageBox.Show("", "Centers From cannot be the same with Centers To", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loPrintParameter.CCENTER_CODE_TO = poValue;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private async Task CenterCodeFromComboBoxValueChanged(string poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (loPrintParameter.CCENTER_CODE_TO == poValue)
                {
                    await R_MessageBox.Show("", "Centers To cannot be the same with Centers From", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loPrintParameter.CCENTER_CODE_FROM = poValue;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private async Task OnClickPrintButton()
        {
            R_Exception loEx = new R_Exception();
            GSM01500PrintCenterParameterDTO loParam = null;

            try
            {
                if (loPrintParameter.CCENTER_CODE_FROM == "")
                {
                    loEx.Add("", "Centers From cannot be empty");
                }
                if (loPrintParameter.CCENTER_CODE_TO == "")
                {
                    loEx.Add("", "Centers To cannot be empty");
                }

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                loParam = new GSM01500PrintCenterParameterDTO();

                //memasukkan nilai parameter
                loParam.CCENTER_CODE_FROM = loPrintParameter.CCENTER_CODE_FROM;
                loParam.CCENTER_CODE_TO = loPrintParameter.CCENTER_CODE_TO;
                loParam.LPRINT_DEPT = loPrintParameter.LPRINT_DEPT;
                loParam.LPRINT_INACTIVE = loPrintParameter.LPRINT_INACTIVE;
                loParam.CUSER_LOGIN_ID = _clientHelper.UserId;
                loParam.CLOGIN_COMPANY_ID = _clientHelper.CompanyId;


                await _reportService.GetReport(
                    "R_DefaultServiceUrl",
                    "GS",
                    "rpt/GSM01500Print/PrintCenterPost",
                    "rpt/GSM01500Print/PrintCenterGet",
                    loParam);

                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
