namespace GLB09900COMMON
{
    public interface IGLB09900
    {
        GLB09900InitialDTO GetInitialVar();
        GLB09900GLSystemParamDTO GetSystemParam();
        GLB09900ValidateDTO GetValidateResultCloseGlPeriod(GLB09900ValidateDTO poParam);
        GLB09900DTO GetResultCloseGlPeriod(GLB09900DTO poParam);
    }
}
