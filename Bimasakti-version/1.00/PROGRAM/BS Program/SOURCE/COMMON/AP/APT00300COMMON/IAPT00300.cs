using System;
using System.Collections.Generic;

namespace APT00300COMMON
{
    public interface IAPT00300
    {
        IAsyncEnumerable<APT00300DTO> GetDebitNoteListStream();
    }
}
