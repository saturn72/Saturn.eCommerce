namespace Ordering.Services
{
    public enum ServiceResponseResult
    {
        Success,
        BadOrMissingData,
        NotFound,
        NotSet,
        Created,
        InternalError
    }
}