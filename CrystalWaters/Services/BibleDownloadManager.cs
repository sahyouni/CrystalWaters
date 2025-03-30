using CrystalWaters.Models;
using Realms;

namespace CrystalWaters.Services;

public class BibleDownloadManager
{
    private readonly Realm _realm;
    private readonly List<DownloadedVerse> _batchVerses;
    private const int BatchSize = 1000;

    public BibleDownloadManager()
    {
        _realm = Realm.GetInstance();
        _batchVerses = new List<DownloadedVerse>();
    }

    public async Task SaveVerseToRealmAsync(DownloadedVerse verse)
    {
        _batchVerses.Add(verse);

        if (_batchVerses.Count >= BatchSize)
        {
            await WriteBatchToRealmAsync();
        }
    }

    private async Task WriteBatchToRealmAsync()
    {
        var versesToSave = _batchVerses.ToList();
        _batchVerses.Clear();

        await Task.Run(() =>
        {
            try
            {
                using var transaction = _realm.BeginWrite();
                foreach (var verse in versesToSave)
                {
                    _realm.Add(verse, update: true);
                }
                transaction.Commit();
                Console.WriteLine($"✅ Saved {versesToSave.Count} verses to Realm");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving batch: {ex.Message}");
            }
        });
    }

    public async Task FindMismatchedDownloadedVersesAsync()
    {
        var allVerses = _realm.All<DownloadedVerse>().ToList();

        var mismatchedGroups = allVerses
            .GroupBy(v => new { v.BookOrd, v.ChapterOrd, v.VerseOrd })
            .Where(g => g.Count() != 2)
            .ToList();

        if (mismatchedGroups.Count > 0)
        {
            Console.WriteLine("Verses with count NOT equal to 2:");
            foreach (var group in mismatchedGroups)
            {
                Console.WriteLine($"{group.Key.BookOrd}-{group.Key.ChapterOrd}-{group.Key.VerseOrd} appears {group.Count()} times");
            }
        }
        else
        {
            Console.WriteLine("✅ All verses are consistent.");
        }
    }

    public async Task SaveRemainingVersesAsync()
    {
        if (_batchVerses.Any())
        {
            Console.WriteLine($"Saving last {_batchVerses.Count} remaining verses...");
            await WriteBatchToRealmAsync();
        }
    }

    public HashSet<string> GetAllDownloadedVersions()
    {
        var versions = _realm.All<BibleVersion>().ToList();
        return new HashSet<string>(versions.Select(v => v.Abbreviation));
    }
}