using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using GSM05000COMMON_FMC;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05020ViewModel : R_ViewModel<GSM05020DTO>
    {
        private GSM05020Model _GSM05020Model = new GSM05020Model();
        public ObservableCollection<GSM05020DTO> GridList = new ObservableCollection<GSM05020DTO>();

        public GSM05020DTO Entity = new GSM05020DTO();
        public GSM05000DTO HeaderEntity = new GSM05000DTO();
        public string TransactionCode { get; set; } = "";
        public async Task GetApprovalList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05020Model.GetApprovalListStreamAsync(TransactionCode);

                GridList = new ObservableCollection<GSM05020DTO>(loReturn.OrderBy(x => x.ISEQUENCE));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetEntityApproval(GSM05020DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GSM05020Model.R_ServiceGetRecordAsync(poEntity);

                Entity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public GSM05020DTO GenerateSequence(GSM05020DTO poNewEntity)
        {
            var loEx = new R_Exception();
            var lnSeq = 1;
            var loReturn = poNewEntity;
            try
            {
                // cek approval mode 
                if (HeaderEntity.CAPPROVAL_MODE != "1")
                {
                    if (GridList.Count >= 0)
                    {
                        var loApprover = GridList.OrderByDescending(x => x.CSEQUENCE).FirstOrDefault();
                        lnSeq = loApprover.ISEQUENCE + 1;
                    }

                    loReturn.ISEQUENCE = lnSeq;
                }
                else
                {
                    loReturn.ISEQUENCE = 0;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }
        public async Task<bool> CheckExistData(GSM05020ValidateDTO poEntity)
        {
            var loEx = new R_Exception();
            bool llReturn = false;

            try
            {
                var loResult = await _GSM05020Model.ValidateApprovalReplacementAsync(poEntity);
                llReturn = loResult is null ? false : true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return llReturn;
        }
        public async Task SaveEntity(GSM05020DTO poNewEntity, eCRUDMode peCrudMode)
        {
            var loEx = new R_Exception();
            try
            {
                Entity = await _GSM05020Model.R_ServiceSaveAsync(poNewEntity, peCrudMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task DeleteEntity(GSM05020DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05020Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Popup Copy
        public async Task CopyToApproval(GSM05020ParamFromToDeptDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05020Model.CopyToApprovalAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task CopyFromApproval(GSM05020ParamFromToDeptDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GSM05020Model.CopyFromApprovalAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Change Seq Popup
        public string DeptCode { get; set; } = "";
        public bool EnableUpBtn { get; set; } = false;
        public bool EnableDownBtn { get; set; } = false;
        public List<GSM05020DeptDTO> DeptList { get; set; } = new List<GSM05020DeptDTO>();
        public List<GSM05020DTO> SwapDataList = new List<GSM05020DTO>();
        public async Task GetDeptApprovalList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05020Model.GetDeptTransListStreamAsync(TransactionCode);

                DeptList = loReturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetApprovalByDeptList()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _GSM05020Model.GetApprovalListStreamAsync(TransactionCode, DeptCode);

                GridList = new ObservableCollection<GSM05020DTO>(loReturn.OrderBy(x => x.ISEQUENCE));
                SwapDataList = loReturn.OrderBy(x => x.ISEQUENCE).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public void GetEnableMethod(GSM05020DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData =  SwapDataList.Find(x => x.CUSER_ID == poParam.CUSER_ID);

                EnableDownBtn = loData.ISEQUENCE > loData.LOWEST;
                EnableUpBtn = loData.ISEQUENCE < loData.HIGHEST;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SwapUpSeqMethod(GetBtnClickUpOrDown poBtnClick, GSM05020DTO poSelectedData)
        {
            var loEx = new R_Exception();
            int liData1;
            int liData2;

            try
            {
                liData1 = SwapDataList.IndexOf(poSelectedData);
                liData2 = SwapDataList.Count;

                if (poBtnClick == GetBtnClickUpOrDown.Up)
                {
                    liData2 = liData1 + 1;
                }
                else if (poBtnClick == GetBtnClickUpOrDown.Down)
                {
                    liData2 = liData1 - 1;
                }

                // Swap the seq values
                int temp = SwapDataList[liData1].ISEQUENCE;
                SwapDataList[liData1].ISEQUENCE = SwapDataList[liData2].ISEQUENCE;
                SwapDataList[liData2].ISEQUENCE = temp;

                //Enable Up Down Btn
                GetEnableMethod(poSelectedData);

                //Change Index after swap
                SwapDataList = SwapDataList.OrderBy(x => x.ISEQUENCE).ToList();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveBacthSeqApproval()
        {
            var loEx = new R_Exception();
            
            try
            {
                var loParam = new GSM05000ParameterWithCRUDMode<GSM05020DTO>();
                loParam.Data = SwapDataList;
                loParam.poCRUDMode = eCRUDMode.EditMode; 
                
                await _GSM05020Model.BatchSeqApprovalAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }

    public enum GetBtnClickUpOrDown
    {
        Up,
        Down
    }

}

