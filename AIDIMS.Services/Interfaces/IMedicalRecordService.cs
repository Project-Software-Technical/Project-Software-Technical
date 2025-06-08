using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IMedicalRecordService : ICoreService<MedicalRecord>
    {
        // Kế thừa từ ICoreService, không cần thêm phương thức khác
    }
}