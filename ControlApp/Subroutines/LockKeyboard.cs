using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ControlApp.Subroutines;

internal class LockKeyboard {
    private bool locked;

    private delegate nint LowLevelKeyboardProc(int nCode, nint wParam, nint lParam);

    private const int WH_KEYBOARD_LL = 13;

    private static LowLevelKeyboardProc _proc = HookCallback;

    private static nint _hookID = IntPtr.Zero;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, nint hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(nint hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern nint GetModuleHandle(string lpModuleName);

    private static nint SetHook(LowLevelKeyboardProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;
        return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0u);
    }

    private static nint HookCallback(int nCode, nint wParam, nint lParam)
    {
        if (nCode >= 0)
        {
            return 1;
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    public void Lock() {
        locked = true;
        _hookID = SetHook(_proc);
    }

    public void Unlock() {
        locked = false;
        UnhookWindowsHookEx(_hookID);
    }

    public bool IsLocked() {
        return locked;
    }
}