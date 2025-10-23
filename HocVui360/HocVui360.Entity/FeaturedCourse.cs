namespace HocVui360.Entity;

public class FeaturedCourse : Entity<string>
{
    public string FeaturedCourseName { get; set; }
    public ICollection<Course> Courses { get; set; }
    public bool IsActive { get; set; }
}
