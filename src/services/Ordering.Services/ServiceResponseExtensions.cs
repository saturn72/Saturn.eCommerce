namespace Ordering.Services
{
    public static class ServiceResponseExtensions
    {
        public static bool IsSuccessful<TData>(this ServiceResponse<TData> serviceResponse)
        {
            return serviceResponse.Result == ServiceResponseResult.Success
                   || serviceResponse.Result == ServiceResponseResult.Created;
        }
    }
}
