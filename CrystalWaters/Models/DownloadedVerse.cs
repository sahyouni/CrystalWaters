using Realms;

namespace CrystalWaters.Models;

public class DownloadedVerse : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; } = string.Empty;
    public string VersionAbbr { get; set; } = string.Empty;
    public int BookOrd { get; set; }
    public int ChapterOrd { get; set; }
    public int VerseOrd { get; set; }
    public string VerseContent { get; set; } = string.Empty;
}