using System;

namespace AIDIMS.Core.Interfaces
{
    public interface ICoreRepository<T> : IReadRepository<T>, IWriteRepository<T>, IDeleteRepository<T>
        where T : class
    {
        // Đây là interface kết hợp, không cần khai báo thêm các phương thức
        // vì đã kế thừa tất cả từ các interfaces thành phần
    }
}