using IronPdf.Viewer.Maui;
using System.ComponentModel;
using System.Diagnostics;


namespace MauiApp2;

public partial class MainPage:ContentPage
{
    private readonly IronPdfView pdfView;
    public MainPage()
    {
        Process currentProcess = Process.GetCurrentProcess();

        var processes = Process.GetProcessesByName(currentProcess.ProcessName);

        if (processes.Length > 1)
        {
            foreach (var process in processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    try
                    {
                        if (!process.HasExited)
                        {
                            process.Kill();
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        Console.WriteLine($"Error killing process: {ex.Message}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Error killing process: {ex.Message}");
                    }
                }
            }
        }
        InitializeComponent();
        this.pdfView = new IronPdfView
        {
            Options = IronPdfViewOptions.Zoom | IronPdfViewOptions.FitWidth | IronPdfViewOptions.FitHeight | IronPdfViewOptions.RotateCw | IronPdfViewOptions.RotateCcw | IronPdfViewOptions.FileName
        };

        Content = this.pdfView;
        var args = Environment.GetCommandLineArgs();
        var filePath = args.Length > 1 ? args[1] : null;

        pdfView.Source = IronPdfViewSource.FromFile(filePath);
    }
}