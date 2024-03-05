using System;

namespace APT00500COMMON
{
    public interface IAPT00500Init
    {
        APT00500SingleResult<APT00500InitTabListDTO> GetAllInitialProcessTabList();
        APT00500SingleResult<APT00500InitTabEntryDTO> GetAllInitialProcessTabEntry();
        APT00500SingleResult<APT00500InitPopupAllocDTO> GetAllInitialProcessPopupAlloc();
    }
}
