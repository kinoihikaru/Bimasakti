using R_APICommonDTO;

namespace GLR00200COMMON
{
    public class GLR00200Result<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

}
