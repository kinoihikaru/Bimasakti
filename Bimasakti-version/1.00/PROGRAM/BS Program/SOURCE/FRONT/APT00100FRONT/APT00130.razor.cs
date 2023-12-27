using APT00100COMMON.DTOs.APT00110;
using APT00100COMMON.DTOs.APT00111;
using APT00100COMMON.DTOs.APT00121;
using APT00100COMMON.DTOs.APT00130;
using APT00100COMMON.DTOs.APT00131;
using APT00100MODEL.ViewModel;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APT00100FRONT
{
    public partial class APT00130 : R_Page
    {
        private APT00130ViewModel loViewModel = new APT00130ViewModel();

        private List<SupplierOptionRadioButton> loOptionDiscountRadioButtonList = new List<SupplierOptionRadioButton>()
        {
            new SupplierOptionRadioButton() { CSUPPLIER_OPTION_CODE = "P", CSUPPLIER_OPTION_NAME = "%" },
            new SupplierOptionRadioButton() { CSUPPLIER_OPTION_CODE = "V", CSUPPLIER_OPTION_NAME = "Amount"}
        };

        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private bool IsDiscountTypeEnable = false;

        private bool IsDiscountPercentEnable = false;

        private bool IsDiscountAmountEnable = false;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loTabParam = (SummaryParameterDTO)poParameter;
                if (!string.IsNullOrWhiteSpace(loViewModel.loTabParam.CREC_ID))
                {
                    await loViewModel.GetCompanyInfoAsync();
                    await loViewModel.GetHeaderInfoAsync();
                    await _conductorRef.R_GetEntity(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Summary_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                if (loViewModel.Data.LSUMMARY_DISCOUNT)
                {
                    IsDiscountTypeEnable = true;
                    if (loViewModel.Data.CSUMMARY_DISC_TYPE == "P")
                    {
                        IsDiscountPercentEnable = true;
                        IsDiscountAmountEnable = false;
                    }
                    else if (loViewModel.Data.CSUMMARY_DISC_TYPE == "V")
                    {
                        IsDiscountPercentEnable = false;
                        IsDiscountAmountEnable = true;
                    }
                }
                else
                {
                    IsDiscountTypeEnable = false;
                }
            }
        }

        private async Task Summary_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.GetSummaryInfoAsync();
                eventArgs.Result = loViewModel.loSummary;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Summary_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                APT00130DTO loData = (APT00130DTO)eventArgs.Data;
                loViewModel.SummaryValidation(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Summary_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private void Summary_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private void Summary_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            IsDiscountTypeEnable = false;
            IsDiscountAmountEnable = false;
            IsDiscountPercentEnable = false;
        }

        private async Task Summary_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveSummaryAsync((APT00130DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loViewModel.loSummary;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void CalculateProcess()
        {
            loViewModel.Data.NTAXABLE_AMOUNT = loViewModel.Data.NAMOUNT - loViewModel.Data.NDISCOUNT - loViewModel.Data.NSUMMARY_DISCOUNT + loViewModel.Data.NADD_ON;
            loViewModel.Data.NTOTAL_AMOUNT = loViewModel.Data.NTAXABLE_AMOUNT + loViewModel.Data.NTAX + loViewModel.Data.NOTHER_TAX + loViewModel.Data.NADDITION - loViewModel.Data.NDEDUCTION;
        }

        #region PopUp

        private void Before_Open_Deduction_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new AdditionalParameterDTO()
            {
                CADDITIONAL_TYPE = "D",
                CREC_ID = loViewModel.loHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(APT00131);
        }

        private void After_Open_Deduction_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            loViewModel.Data.NDEDUCTION = (decimal)eventArgs.Result;
            CalculateProcess();
        }

        private void Before_Open_Addition_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new AdditionalParameterDTO()
            {
                CADDITIONAL_TYPE = "A",
                CREC_ID = loViewModel.loHeader.CREC_ID
            };
            eventArgs.TargetPageType = typeof(APT00131);
        }

        private void After_Open_Addition_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            loViewModel.Data.NADDITION = (decimal)eventArgs.Result;
            CalculateProcess();
        }

        #endregion

        #region OnChange

        private async Task DiscountAmountTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NSUMMARY_DISCOUNT = poParam;
                CalculateProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task AddOnTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NADD_ON = poParam;
                CalculateProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DiscountOptionRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CSUMMARY_DISC_TYPE = poParam;
                if (loViewModel.Data.CSUMMARY_DISC_TYPE == "P")
                {
                    loViewModel.Data.NSUMMARY_DISCOUNT = 0;
                    IsDiscountPercentEnable = true;
                    IsDiscountAmountEnable = false;
                }
                else if (loViewModel.Data.CSUMMARY_DISC_TYPE == "V")
                {
                    loViewModel.Data.NSUMMARY_DISC_PCT = 0;
                    IsDiscountPercentEnable = false;
                    IsDiscountAmountEnable = true;
                }
                CalculateProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private async Task SummaryDiscountCheckbox_ValueChanged(bool poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.LSUMMARY_DISCOUNT = poParam;
                if (loViewModel.Data.LSUMMARY_DISCOUNT)
                {
                    IsDiscountTypeEnable = true;
                }
                else
                {
                    loViewModel.Data.NSUMMARY_DISC_PCT = 0;
                    loViewModel.Data.NSUMMARY_DISCOUNT = 0;
                    IsDiscountTypeEnable = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DiscountPercentageTextBox_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NSUMMARY_DISC_PCT = poParam;
                loViewModel.Data.NSUMMARY_DISCOUNT = loViewModel.Data.NSUMMARY_DISC_PCT * (loViewModel.Data.NAMOUNT - loViewModel.Data.NDISCOUNT) * 0.01m;
                CalculateProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task DiscountTypeRadioButton_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CSUMMARY_DISC_TYPE = poParam;
                if (loViewModel.Data.CSUMMARY_DISC_TYPE == "P")
                {
                    IsDiscountPercentEnable = true;
                    loViewModel.Data.NSUMMARY_DISCOUNT = 0;
                    IsDiscountAmountEnable = false;
                }
                else if (loViewModel.Data.CSUMMARY_DISC_TYPE == "V")
                {
                    IsDiscountPercentEnable = false;
                    loViewModel.Data.NSUMMARY_DISCOUNT = 0;
                    IsDiscountAmountEnable = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}
