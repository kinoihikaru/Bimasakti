using R_CommonFrontBackAPI.Log;

namespace GLR00200SERVICE
{
    public class GLR00200PrintLogKeyDTO<T>
    {
        public T poParam { get; set; }
        public R_NetCoreLogKeyDTO poLogKey { get; set; }
    }
}
