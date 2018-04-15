namespace Ordering.Services
{
    public class ServiceResponse<TData>
    {
        #region ctor

        public ServiceResponse()
        {
            Result = ServiceResponseResult.NotSet;
        }

        #endregion

        #region Properties

        public string ErrorMessage { get; set; }
        public TData Data { get; set; }
        public ServiceResponseResult Result { get; set; }

        #endregion
    }
}