using System;

namespace AIDIMS.Core.Interfaces
{
    public interface ICoreService<T, TCreateRequest, TUpdateRequest, TResponse> :
        IReadService<T, TResponse>,
    IWriteService<T, TCreateRequest, TUpdateRequest, TResponse>,
    IDeleteService<T>
    where T : class
    where TCreateRequest : class
        where TUpdateRequest : class
        where TResponse : class
    {
    }
}
