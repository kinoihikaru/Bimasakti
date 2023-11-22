using LMM03500COMMON;
using LMM03500COMMON.DTOs;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03504;
using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON.DTOs.LMM03507;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LMM03500MODEL.ViewModel
{
    public class LMM03507ViewModel : R_ViewModel<LMM03507DetailDTO>
    {
        private LMM03500Model loSharedModel = new LMM03500Model();

        private LMM03507Model loModel = new LMM03507Model();

        public LMM03507DetailDTO loMembers = null;

        public ObservableCollection<LMM03507DTO> loMembersList = new ObservableCollection<LMM03507DTO>();

        public LMM03507ResultDTO loRtn = null;

        public TabParameterDTO loTabParameter = new TabParameterDTO();

        public TenantDTO loTenant = new TenantDTO();

        public TenantResultDTO loTenantRtn = null;

        public GetIdTypeDTO loIdType = null;

        public List<GetIdTypeDTO> loIdTypeList = null;

        public GetIdTypeResultDTO loIdTypeRtn = null;

        public async Task GetMembersListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03507_PROPERTY_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.LMM03507_TENANT_ID_STREAMING_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                loRtn = await loModel.GetMembersListStreamAsync();
                loMembersList = new ObservableCollection<LMM03507DTO>(loRtn.Data);
                foreach (var item in loRtn.Data)
                {
                    item.DDATE_BIRTH = DateTime.ParseExact(item.CDATE_BIRTH, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public void MembersValidation(LMM03507DetailDTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CMEMBER_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Full Name is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CGENDER);
                if (llCancel)
                {
                    loEx.Add("", "Gender is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDATE_BIRTH);
                if (llCancel)
                {
                    loEx.Add("", "Birth Date is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CID_TYPE);
                if (llCancel)
                {
                    loEx.Add("", "ID Type is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CID_NO);
                if (llCancel)
                {
                    loEx.Add("", "ID No is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetIdTypeListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loIdTypeRtn = await loModel.GetIdTypeListStreamAsync();
                loIdTypeList = new List<GetIdTypeDTO>(loIdTypeRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetTenantAsync()
        {
            R_Exception loException = new R_Exception();
            TenantParameterDTO loParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_TENANT_ID_CONTEXT, loTabParameter.CSELECTED_TENANT_ID);
                //R_FrontContext.R_SetContext(ContextConstant.LMM03500_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new TenantParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_TENANT_ID = loTabParameter.CSELECTED_TENANT_ID
                };

                loTenantRtn = await loSharedModel.GetTenantAsync(loParam);
                loTenant = loTenantRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetMembersAsync(LMM03507DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM03507ParameterDTO loParam = null;
            LMM03507ParameterDTO loResult = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new LMM03507ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                };

                loResult = await loModel.R_ServiceGetRecordAsync(loParam);
                loMembers = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task SaveMembersAsync(LMM03507DetailDTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();
            LMM03507ParameterDTO loParam = null;
            LMM03507ParameterDTO loResult = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new LMM03507ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                };

                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);
                loMembers = loResult.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteMembersAsync(LMM03507DetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LMM03507ParameterDTO loParam = null;
            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT, loTabParameter.CSELECTED_PROPERTY_ID);
                loParam = new LMM03507ParameterDTO()
                {
                    Data = poEntity,
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID
                };

                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
    }
}
