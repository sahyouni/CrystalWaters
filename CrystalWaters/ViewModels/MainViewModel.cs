using CrystalWaters.Services;

namespace CrystalWaters.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainViewModel : ObservableObject
{
    private readonly BibleDownloadManager _downloadManager;

    public MainViewModel()
    {
        _downloadManager = new BibleDownloadManager();
    }

    [ObservableProperty]
    private string _downloadUrl;   // Notice the use of _ prefix

    [ObservableProperty]
    private string _statusMessage; // Notice the use of _ prefix

    [RelayCommand]
    public async Task DownloadBibleAsync()
    {
        try
        {
            StatusMessage = "Downloading...";
            var result =  _downloadManager.GetAllDownloadedVersions();
            StatusMessage = "Download Completed Successfully";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    public void ImportBibleFromXML(string filePath)
    {
        try
        {
            _downloadManager.GetAllDownloadedVersions();
            StatusMessage = "XML Import Completed Successfully";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }
}