using WebApplication1.DTOs;
namespace WebApplication1.Services;
public interface IHospitalService
{
    Task<IEnumerable<PatientGetDto>> GetPatientsAsync(string? search);
    Task<bool> AssignBedAsync(string pesel, BedAssignmentPostDto dto);
}