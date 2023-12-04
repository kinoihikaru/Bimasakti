using GSM01500COMMON.DTOs;
using GSM01500MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_CommonFrontBackAPI;
using System.Xml.Linq;
using System.Data;

namespace GSM01500FRONT
{
    public partial class CopyFromModal : R_Page
    {
        private GSM01500ViewModel CenterViewModel = new GSM01500ViewModel();

        private R_ConductorGrid _conGridCompanyRef;

        private R_Grid<CopyFromProcessCompanyDTO> _gridRef;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await CenterViewModel.GetCompanyListStreamAsync(); //mendapatkan list company untuk ditampilkan di grid
                eventArgs.ListEntityResult = CenterViewModel.loCompanyList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnProcess() //jalan ketika menekan tombol Process
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loData = (CopyFromProcessCompanyDTO)_gridRef.GetCurrentData();
                CenterViewModel.SelectedCopyFromCompanyId = loData.CCOMPANY_ID;

                //Validasi apakah terdapat Center pada Company Id yang dipilih
                //begin
                await CenterViewModel.ValidateCompanyDataAsync();
                if (CenterViewModel.loValidateRtn.Data.Result == false) //Jika tidak terdapat Center di Company yang dipilih
                {
                    await R_MessageBox.Show("", "There’s no center to copy, please select another Company", R_eMessageBoxButtonType.OK);
                    return;
                }
                //end
                
                await CenterViewModel.CopyFromProcessAsync(); //melakukan proses Copy From dari Company yang dipilih
                await this.Close(true, true);
            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task OnClose() //jalan ketika menekan tombol Close
        {
            await this.Close(true, false);
        }
    }
}
