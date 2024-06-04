using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;


namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .ConfigureIronPdfView()
            .ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(windows =>
                {
                    windows.OnWindowCreated(window =>
                    {
                        var nativeWindow = window as Microsoft.UI.Xaml.Window;
                        if (nativeWindow != null)
                        {
                            nativeWindow.Activate();
                            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);

                            MaximizeWindow(windowHandle);
                        }
                    });
                });
#endif
            });

            return builder.Build();
        }

#if WINDOWS
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_MAXIMIZE = 3;

        private static void MaximizeWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_MAXIMIZE);
        }
#endif
    }
}