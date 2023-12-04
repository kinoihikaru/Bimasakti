using GSM01500COMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GSM01500MODEL.ViewModel
{
    public class GSM01500ViewModel : R_ViewModel<GSM01500DTO>
    {
        private GSM01500Model loModel = new GSM01500Model();

        public GSM01500DTO loCenter = null;

        public ObservableCollection<CopyFromProcessCompanyDTO> loCompanyList = new ObservableCollection<CopyFromProcessCompanyDTO>();

        public ObservableCollection<GSM01500DTO> loCenterList = new ObservableCollection<GSM01500DTO>();

        public GSM01500CenterListDTO loRtn = null;

        public CopyFromProcessCompanyListDTO loCompanyRtn = null;

        public ValidateCompanyDataResultDTO loValidateRtn = null;

        public string SelectedCopyFromCompanyId = "";

        public string SelectedActiveInactiveCenterCode = "";

        public bool SelectedActiveInactiveLACTIVE;

        public async Task GetCenterListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loRtn = await loModel.GetCenterListStreamAsync();
                loCenterList = new ObservableCollection<GSM01500DTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCompanyRtn = await loModel.GetCompanyListStreamAsync();
                loCompanyList = new ObservableCollection<CopyFromProcessCompanyDTO>(loCompanyRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ValidateCompanyDataAsync()
        {
            R_Exception loEx = new R_Exception();
            ValidateCompanyDataParameterDTO loParam = null;

            try
            {
                loParam = new ValidateCompanyDataParameterDTO()
                {
                    SELECTED_COMPANY_ID = SelectedCopyFromCompanyId
                };
                loValidateRtn = await loModel.ValidateCompanyDataAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCenterAsync(GSM01500DTO poEntity)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loResult = await loModel.R_ServiceGetRecordAsync(poEntity);

                loCenter = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void CenterValidation(GSM01500DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CCENTER_CODE);
                if (llCancel)
                {
                    loEx.Add("", "Center Code is required");
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CCENTER_NAME);
                if (llCancel)
                {
                    loEx.Add("", "Center Name is required");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveCenterAsync(GSM01500DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM01500DTO loResult = await loModel.R_ServiceSaveAsync(poEntity, peCRUDMode);

                loCenter = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }

        public async Task DeleteCenterAsync(GSM01500DTO poEntity)
        {
            R_Exception loException = new R_Exception();

            try
            {
                await loModel.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task CopyFromProcessAsync()
        {
            R_Exception loException = new R_Exception();
            CopyFromProcessParameter loParam = null;

            try
            {
                //R_FrontContext.R_SetContext(ContextConstant.COPY_FROM_COMPANY_ID_CONTEXT, SelectedCopyFromCompanyId);
                loParam = new CopyFromProcessParameter()
                {
                    CCOPY_FROM_COMPANY_ID = SelectedCopyFromCompanyId
                };
                await loModel.CopyFromProcessAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task ActiveInactiveProcessAsync()
        {
            R_Exception loException = new R_Exception();
            ActiveInactiveParameterDTO loParam = null;

            try
            {
                loParam = new ActiveInactiveParameterDTO()
                {
                    CCENTER_CODE = SelectedActiveInactiveCenterCode,
                    LACTIVE = SelectedActiveInactiveLACTIVE
                };
                //R_FrontContext.R_SetContext(ContextConstant.ACTIVE_INACTIVE_CENTER_CODE_CONTEXT, SelectedActiveInactiveCenterCode);
                //R_FrontContext.R_SetContext(ContextConstant.ACTIVE_INACTIVE_LACTIVE_CONTEXT, SelectedActiveInactiveLACTIVE);

                await loModel.RSP_GS_ACTIVE_INACTIVE_CenterMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Template
        public async Task<TemplateCenterDTO> DownloadTemplateCenterAsync()
        {
            var loEx = new R_Exception();
            TemplateCenterDTO loResult = null;

            try
            {
                loResult = await loModel.DownloadTemplateCenterAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #endregion

    }
}
