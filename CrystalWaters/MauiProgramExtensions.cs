using CrystalWaters.Services;
using CrystalWaters.ViewModels;
using Microsoft.Extensions.Logging;

namespace CrystalWaters;

public static class MauiProgramExtensions
{
	public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
	{
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		// Add BibleDownloadManager to the DI Container
		builder.Services.AddSingleton<BibleDownloadManager>();

		// Add ViewModels to DI Container
		builder.Services.AddTransient<MainViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder;
	}
}
