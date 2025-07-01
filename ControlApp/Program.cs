using ControlApp.Subroutines;

namespace ControlApp;

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.Run(new MyCustomApplicationContext());
    }
}


public class MyCustomApplicationContext : ApplicationContext {
    private NotifyIcon trayIcon;
    private MainWindow mainWindow = new MainWindow();
    public MyCustomApplicationContext() {
        // Initialize Tray Icon
        trayIcon = new NotifyIcon();
        trayIcon.Icon = new Icon("App.ico");
        trayIcon.ContextMenuStrip = new ContextMenuStrip();
        trayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);
        trayIcon.ContextMenuStrip.Items.Add("Open", null, Open);
        trayIcon.ContextMenuStrip.Items.Add("Panic", null, Panic);
        trayIcon.ContextMenuStrip.Items.Add("Subliminal", null, Subliminal);
        trayIcon.MouseClick += TrayIcon_MouseClick;
        trayIcon.Visible = true;
    }

    private void TrayIcon_MouseClick(object? sender, MouseEventArgs e) {
        if (e.Button != MouseButtons.Left) return;
        mainWindow.Visible = true;
    }

    void Exit(object? sender, EventArgs e) {
        // Hide tray icon, otherwise it will remain shown until user mouses over it
        trayIcon.Visible = false;
        mainWindow.Dispose();

        Application.Exit();
    }
    void Open(object? sender, EventArgs e) {
        mainWindow.Show();
    }
    void Subliminal(object? sender, EventArgs e) {
        SubLoop? loop = (SubLoop?) Utils.GetForm(typeof(SubLoop));
        if (loop != null) {
            loop.Close();
        } else {
            new SubLoop().Show();
        }
    }
    void Panic(object? sender, EventArgs e) {
        foreach (Form fm in Application.OpenForms) {
            fm.Close();
        }
    }
}