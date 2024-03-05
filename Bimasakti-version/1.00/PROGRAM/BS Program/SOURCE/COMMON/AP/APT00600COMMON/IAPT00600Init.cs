using System;

namespace APT00600COMMON
{
    public interface IAPT00600Init
    {
        APT00600SingleResult<APT00600InitTabListDTO> GetAllInitialProcessTabList();
        APT00600SingleResult<APT00600InitTabEntryDTO> GetAllInitialProcessTabEntry();
        APT00600SingleResult<APT00600InitPopupAllocDTO> GetAllInitialProcessPopupAlloc();
    }
}
