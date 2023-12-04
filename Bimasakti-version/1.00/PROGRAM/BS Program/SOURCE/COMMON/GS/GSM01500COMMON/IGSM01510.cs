using GSM01500COMMON.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500COMMON
{
    public interface IGSM01510
    {
        IAsyncEnumerable<GSM01510DepartmentDTO> GetCenterDepartmentList();
    }
}
