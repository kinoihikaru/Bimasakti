using LMM03700MODEL;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMM03700COMMON;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace LMM03700FRONT
{
    public partial class LMM03711 : R_Page
    {
        private LMM03711ViewModel _viewModelTenantClass = new();//viewModel TenantClass
        private R_Grid<LMM03711DTO> _gridAssignTenantRef; //gridref assign Tenant 
        private R_Grid<LMM03711DTO> _gridTenantRef; //gridref Tenant 

        private R_ConductorGrid _conAssignTenant;
        private R_ConductorGrid _conTenant;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM03711DTO>(poParameter);
                await _gridAssignTenantRef.R_RefreshGrid(loParam);
                await _gridTenantRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }

        #region TenantClass
        private async Task TenantClass_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM03711DTO)eventArgs.Parameter;
                await _viewModelTenantClass.GetTenantClassTenantList(loParam);
                eventArgs.ListEntityResult = _viewModelTenantClass.TenantClassTenantGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task R_ServiceSaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loListData = (List<LMM03711DTO>)eventArgs.Data;

                List<string> idList = loListData.Select(x => x.CTENANT_ID).ToList();
                string idString = string.Join(",", idList);

                var loData = R_FrontUtility.ConvertObjectToObject<LMM03711AssignTenantDTO>(loListData.FirstOrDefault());
                loData.CTENANT_LIST = idString;
                await _viewModelTenantClass.AssignTenantClass(loData);

                await R_MessageBox.Show("", "Tenant Class updated successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region AssignTenantClass
        private async Task AssignTenantClass_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (LMM03711DTO)eventArgs.Parameter;
                await _viewModelTenantClass.GetAssignTenantClassList(loParam);
                eventArgs.ListEntityResult = _viewModelTenantClass.AssignTenantClassGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        #endregion

        #region Mover
        private string label1 = "<<";
        private string label2 = "<";
        private void R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private bool HasMove = false;
        private void R_GridRowBeforeDrop(R_GridRowBeforeDropEventArgs eventArgs)
        {
            HasMove = true;
        }

        private void R_GridRowAfterDrop(R_GridRowAfterDropEventArgs eventArgs)
        {
            HasMove = true;
        }
        private void BtnMoveRight()
        {
            HasMove = true;
            _gridAssignTenantRef.R_MoveToTargetGrid();
        }

        private void BtnAllMoveRight()
        {
            HasMove = true;
            _gridAssignTenantRef.R_MoveAllToTargetGrid();
        }

        private void BtnAllMoveLeft()
        {
            HasMove = true;
            _gridTenantRef.R_MoveAllToTargetGrid();
        }

        private void BtnMoveLeft()
        {
            HasMove = true;
            _gridTenantRef.R_MoveToTargetGrid();
        }
        #endregion

        #region Button
        private bool ProsessMove = false;
        private async Task BtnProcess()
        {
            var loEx = new R_Exception();

            try
            {
                ProsessMove = true;
                await _conTenant.R_SaveBatch();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task BtnClose()
        {
            var loEx = new R_Exception();

            try
            {
                if (HasMove && !ProsessMove)
                {
                    var Discard = await R_MessageBox.Show("", "Discard changes? ", R_eMessageBoxButtonType.YesNo);
                    if (Discard == R_eMessageBoxResult.Yes)
                    {
                        await this.Close(true, false);
                    }
                }
                else
                {
                    await this.Close(true, false);
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
