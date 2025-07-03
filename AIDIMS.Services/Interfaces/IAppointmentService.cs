using AIDIMS.Core.DTOs.Request;
using AIDIMS.Core.DTOs.Response;
using AIDIMS.Core.Interfaces;
using AIDIMS.Core.Models;

namespace AIDIMS.Services.Interfaces
{
    public interface IAppointmentService : ICoreService<Appointment, CreateAppointmentRequest, UpdateAppointmentRequest, AppointmentResponse>
    {
        Task<AppointmentResponse> UpdateStatusAsync(int id, UpdateAppointmentStatusRequest statusRequest);
    }
}