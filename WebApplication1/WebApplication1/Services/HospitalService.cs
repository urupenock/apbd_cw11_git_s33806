using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
namespace WebApplication1.Services;
public class HospitalService : IHospitalService
{
   private readonly HospitalContext _context;
   public HospitalService(HospitalContext context)
   {
       _context = context;
   }
   public async Task<IEnumerable<PatientGetDto>> GetPatientsAsync(string? search)
   {
       var query = _context.Patients
           .Include(p => p.Admissions).ThenInclude(a => a.Ward)
           .Include(p => p.BedAssignments).ThenInclude(ba => ba.Bed).ThenInclude(b => b.BedType)
           .Include(p => p.BedAssignments).ThenInclude(ba => ba.Bed).ThenInclude(b => b.Room).ThenInclude(r => r.Ward)
           .AsQueryable();
       if (!string.IsNullOrWhiteSpace(search))
       {
           var cleanSearch = search.Trim().ToLower();
           query = query.Where(p => p.FirstName.ToLower().Contains(cleanSearch)
                                 || p.LastName.ToLower().Contains(cleanSearch));
       }
       var patients = await query.ToListAsync();
       return patients.Select(p => new PatientGetDto
       {
           Pesel = p.Pesel,
           FirstName = p.FirstName,
           LastName = p.LastName,
           Age = p.Age,
           Sex = p.Sex ? "Male" : "Female", 
           Admissions = p.Admissions.Select(a => new AdmissionDto
           {
               Id = a.Id,
               AdmissionDate = a.AdmissionDate,
               DischargeDate = a.DischargeDate,
               Ward = new WardDto
               {
                   Id = a.Ward.Id,
                   Name = a.Ward.Name,
                   Description = a.Ward.Description
               }
           }).ToList(),
           BedAssignments = p.BedAssignments.Select(ba => new BedAssignmentDto
           {
               Id = ba.Id,
               From = ba.From,
               To = ba.To,
               Bed = new BedDto
               {
                   Id = ba.Bed.Id,
                   BedType = new BedTypeDto
                   {
                       Id = ba.Bed.BedType.Id,
                       Name = ba.Bed.BedType.Name,
                       Description = ba.Bed.BedType.Description
                   },
                   Room = new RoomDto
                   {
                       Id = ba.Bed.Room.Id,
                       HasTv = ba.Bed.Room.HasTv,
                       Ward = new WardDto
                       {
                           Id = ba.Bed.Room.Ward.Id,
                           Name = ba.Bed.Room.Ward.Name,
                           Description = ba.Bed.Room.Ward.Description
                       }
                   }
               }
           }).ToList()
       });
   }
   public async Task<bool> AssignBedAsync(string pesel, BedAssignmentPostDto dto)
   {
       var patientExists = await _context.Patients.AnyAsync(p => p.Pesel == pesel);
       if (!patientExists) return false;
       var availableBeds = await _context.Beds
           .Where(b => b.BedType.Name == dto.BedType && b.Room.Ward.Name == dto.Ward)
           .ToListAsync();
       Bed? targetBed = null;
       foreach (var bed in availableBeds)
       {
           var isOccupied = await _context.BedAssignments
               .Where(ba => ba.BedId == bed.Id)
               .AnyAsync(ba =>
                   (dto.To == null && (ba.To == null || ba.To > dto.From)) ||
                   (dto.To != null && ba.From < dto.To && (ba.To == null || ba.To > dto.From))
               );
           if (!isOccupied)
           {
               targetBed = bed;
               break; 
           }
       }
       if (targetBed == null) return false;
       var newAssignment = new BedAssignment
       {
           PatientPesel = pesel,
           BedId = targetBed.Id,
           From = dto.From,
           To = dto.To
       };
       await _context.BedAssignments.AddAsync(newAssignment);
       await _context.SaveChangesAsync();
       return true;
   }
}