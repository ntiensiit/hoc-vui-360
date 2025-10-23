namespace HocVui360.Lib.Settings;

public record DatabaseSettings
{
    public const string SectionName = "ConnectionStrings";

    public string? DefaultConnection { get; set; }
}
