using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Data;
public class HospitalContext : DbContext
{
    public HospitalContext()
    {
    }
    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Ward> Wards { get; set; }
    public virtual DbSet<Admission> Admissions { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Bed> Beds { get; set; }
    public virtual DbSet<BedType> BedTypes { get; set; }
    public virtual DbSet<BedAssignment> BedAssignments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.Pesel).IsFixedLength();
        });
        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.Id).IsFixedLength();
        });
    }
}