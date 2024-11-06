namespace Web_UI.Models.Utilities.RequestResults
{
    public interface IRequestDataResult<T> : IRequestResult
    {
        T Data { get; }

    }
}