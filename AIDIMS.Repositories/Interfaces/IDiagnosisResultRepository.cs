using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Repositories.Interfaces
{
    public interface IDiagnosisResultRepository : IGenericRepository<DiagnosisResult>
    {
        // Kế thừa từ IGenericRepository, không cần thêm phương thức khác
    }
}