namespace HocVui360.Entity;

public class OpeningSchedule : Entity<string>
{
    public string FeaturedCourseId { get; set; }
    public DateTime OpeningDate { get; set; }
}
