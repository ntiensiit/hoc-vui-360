namespace HocVui360.Entity;

public class File : Entity<string>
{
    public string Name { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public long Size { get; set; }
}
