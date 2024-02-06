﻿using BlazorClientHelper;
using GLT00100COMMON;
using GLT00100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System.Globalization;

namespace GLT00100FRONT
{
    public partial class GLT00120 : R_Page
    {
        private GLT00120ViewModel _viewModel = new();
        private R_Grid<GLT00100DTO> _gridRef;
        private R_ConductorGrid _conductorGridRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private string lcPeriodMonth = "";
        private string lcPeriodYear = "";
        private string lcStatusName = "Submitted";
        private GLT00100DTO _LoHeader = new GLT00100DTO();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetInitialVarData();

                var loParam = (GLT00100DTO)poParameter;
                lcPeriodMonth = loParam.CREF_PRD.Substring(4,2);
                lcPeriodYear = loParam.CREF_PRD.Substring(0,4);
                _LoHeader = loParam;

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Journal Batch
        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100ParamDTO>(_LoHeader);
                loParam.CPERIOD = _LoHeader.CREF_PRD;
                loParam.CSTATUS = "10";
                await _viewModel.GetJournalList(loParam);

                eventArgs.ListEntityResult = _viewModel.JournalGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task ServiceSaveBatchApprove(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loListData = (List<GLT00100DTO>)eventArgs.Data;
                var loSelectedData = loListData.Where(x => x.LSELECTED).Select(x => x.CREC_ID).ToList();
                if (loSelectedData.Count() !> 0)
                {
                    loEx.Add("", "Please select Journal to approve!");
                    goto EndBlock;
                }

                var loData = new GLT00100UpdateStatusDTO();
                loData.CREC_ID = string.Join(",", loSelectedData);
                loData.CNEW_STATUS = "20";
                loData.LAUTO_COMMIT = _viewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;

                await _viewModel.UpdateJournalStatus(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Button Process
        private async Task OnClose()
        {
            await Close(false, null);
        }
        private async Task OnChangeRapidApprove()
        {
            var loEx = new R_Exception();
            try
            {
                
                var res = await R_MessageBox.Show("", "Are you sure want to process selected Journal(s)?", R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.Yes)
                {
                    await _conductorGridRef.R_SaveBatch();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
