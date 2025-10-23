namespace HocVui360.Entity;

public class TrainingProgram : Entity<string>
{
    public string TrainingProgramName { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Course> Courses { get; set; }
}
