using System;

namespace AIDIMS.Core.Interfaces
{
    public interface ICoreService<T> : IReadService<T>, IWriteService<T>, IDeleteService<T>
        where T : class
    {
    }
}