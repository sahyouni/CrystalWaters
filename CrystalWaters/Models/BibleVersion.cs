using Realms;

namespace CrystalWaters.Models;

public class BibleVersion : RealmObject
{
    [PrimaryKey]
    public string Name { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string? QueryURL { get; set; }
    public string? DownloadURL { get; set; }
    public bool IsDownloadable { get; set; } = false;
    public string? LocalFileName { get; set; }
    public int LocalFileFormat { get; set; } = (int)BibleDataFormat.Unknown;
}