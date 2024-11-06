using Core.Utilities.Results;

namespace Core
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
        
    }
}