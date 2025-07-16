using ControlApp.Forms;
using ControlApp.Subroutines;

namespace ControlApp;

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
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
    private MainWindow mainWindow = new();
    public MyCustomApplicationContext() {
        // 1. Check for an existing token
        string storedToken = SecureTokenStorage.ReadToken();

        if (string.IsNullOrEmpty(storedToken))
        {
            // 2. No token found, show the login form
            using LoginForm loginForm = new();
            // If login is successful, LoginForm will return DialogResult.OK
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                // If login fails or is cancelled, exit the application
                Exit(null, EventArgs.Empty);
                return;
            }
        }
        // 3. Token exists or login was successful, proceed to the main application
        InitializeMainApp();
    }

    private void InitializeMainApp()
    {
        mainWindow = new MainWindow();
        trayIcon = new NotifyIcon();
        trayIcon.Icon = new Icon("App.ico");
        trayIcon.ContextMenuStrip = new ContextMenuStrip();
        trayIcon.ContextMenuStrip.Items.Add("Logout", null, Logout); // Added Logout
        trayIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);
        trayIcon.ContextMenuStrip.Items.Add("Open", null, Open);
        trayIcon.ContextMenuStrip.Items.Add("Panic", null, Panic);
        trayIcon.ContextMenuStrip.Items.Add("Subliminal", null, Subliminal);
        trayIcon.MouseClick += TrayIcon_MouseClick;
        trayIcon.Visible = true;
        
        // Show the main window now
        mainWindow.Show();
    }

    private void TrayIcon_MouseClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;
        mainWindow?.Show();
    }

    private void Logout(object? sender, EventArgs e)
    {
        // Delete the token
        SecureTokenStorage.DeleteToken();

        // Hide the tray icon and close the main window
        if (trayIcon != null)
        {
            trayIcon.Visible = false;
        }
        if (mainWindow != null)
        {
            // We need to use BeginInvoke to allow the current context menu to close before restarting
            mainWindow.BeginInvoke(new Action(() =>
            {
                mainWindow.Close();
                Application.Restart();
            }));
        } else {
             Application.Restart();
        }
    }

    private void Exit(object? sender, EventArgs e)
    {
        // Hide tray icon, otherwise it will remain shown until user mouses over it
        if(trayIcon != null) trayIcon.Visible = false;
        mainWindow.Dispose();
        Application.Exit();
    }

    private void Open(object? sender, EventArgs e) {
        mainWindow.Show();
    }

    private void Subliminal(object? sender, EventArgs e) {
        SubLoop? loop = (SubLoop?) Utils.GetForm(typeof(SubLoop));
        if (loop != null) {
            loop.Visible = !loop.Visible;
        } else {
            new SubLoop().Show();
        }
    }

    private void Panic(object? sender, EventArgs e) {
        foreach (Form fm in Application.OpenForms) {
            fm.Close();
        }
    }
}