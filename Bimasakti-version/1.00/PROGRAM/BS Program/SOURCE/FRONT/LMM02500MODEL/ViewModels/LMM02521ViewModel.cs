using LMM02500COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM02500MODEL
{
    public class LMM02521ViewModel
    {

        private LMM02500Model _LMM02500Model = new LMM02500Model();

        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }
        public ObservableCollection<LMM02500TenantDTO> TenantMoveGrid { get; set; } = new ObservableCollection<LMM02500TenantDTO>();
        public List<LMM02500DTO> TenantGrpComboBox { get; set; } = new List<LMM02500DTO>();
        public LMM02500ParameterMoveTenantDTO HeaderMove = new LMM02500ParameterMoveTenantDTO();

        public async Task GetTenantGrpList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500Model.GetTenantGrpistAsync(HeaderMove.CPROPERTY_ID);

                TenantGrpComboBox = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetTenantList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM02500Model.GetTenantListAsync(HeaderMove.CPROPERTY_ID, HeaderMove.CTO_TENANT_GROUP);

                TenantMoveGrid = new ObservableCollection<LMM02500TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task MoveTenant(List<LMM02500TenantDTO> poNewEntity, string poCompanyId, string poUserId)
        {
            var loEx = new R_Exception();
            R_BatchParameter loUploadPar;
            R_ProcessAndUploadClient loCls;
            List<R_KeyValue> loUserParameneters;
            R_IProcessProgressStatus loProgressStatus;

            try
            {
                //set Data
                var loObjectData = new LMM02500MoveTenantDTO();
                loObjectData.Header = HeaderMove;
                loObjectData.Detail = poNewEntity;

                //preapare Batch Parameter
                loUploadPar = new R_BatchParameter();
                loUploadPar.COMPANY_ID = poCompanyId;
                loUploadPar.USER_ID = poUserId;
                loUploadPar.UserParameters = new List<R_KeyValue>();
                loUploadPar.ClassName = "LMM02500BACK.LMM02500Cls";
                loUploadPar.BigObject = loObjectData;

                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "LM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlLM");

                var loKeyGuid = await loCls.R_BatchProcess<LMM02500MoveTenantDTO>(loUploadPar, 1);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


    }

}
