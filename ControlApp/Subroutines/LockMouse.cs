using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ControlApp.Subroutines;

internal class LockMouse {
    private bool locked;

    private delegate nint LowLevelMouseProc(int nCode, nint wParam, nint lParam);

    private const int WH_MOUSE_LL = 14;

    private static LowLevelMouseProc _mouseProc = MouseHookCallback;

    private static nint _mouseHookID = IntPtr.Zero;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    private static nint SetMouseHook(LowLevelMouseProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;
        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0u);
    }

    private static nint MouseHookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            return 1;
        }
        return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
    }

    public void Lock() {
        locked = true;
        _mouseHookID = SetMouseHook(_mouseProc);
    }

    public void Unlock() {
        locked = false;
        UnhookWindowsHookEx(_mouseHookID);
    }

    public bool IsLocked() {
        return locked;
    }
}