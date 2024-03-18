﻿using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00200Common
{
    public class RecordResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
    public class ParemeterRecordWithCRUDModeResultDTO<T> 
    {
        public T data { get; set; }
        public eCRUDMode eCRUDMode { get; set; }
    }
    public class ListResultDTO<T> : R_APIResultBaseDTO
    {
        public List<T> data { get; set; }
    }


}