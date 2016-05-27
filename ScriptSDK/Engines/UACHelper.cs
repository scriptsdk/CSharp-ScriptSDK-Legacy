using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32;
using ScriptSDK.Data;

namespace ScriptSDK.Engines
{
    /// <summary>
    /// Supporting class for applications which requires admin rights.
    /// Code has been taken from http://stackoverflow.com/questions/1220213/detect-if-running-as-administrator-with-or-without-elevated-privileges 
    /// and been updated to be clean code.
    /// </summary>
    public static class UacHelper
    {
        private const string uacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string uacRegistryValue = "EnableLUA";
        private const uint STANDARD_RIGHTS_READ = 0x00020000;
        private const uint TOKEN_QUERY = 0x0008;
        private const uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        /// <summary>
        /// Stores if UAC-Control on operating system is enabled. See more: https://en.wikipedia.org/wiki/User_Account_Control .
        /// </summary>
        public static bool IsUacEnabled
        {
            get
            {
                using (var uacKey = Registry.LocalMachine.OpenSubKey(uacRegistryKey, false))
                {
                    var result = uacKey != null && uacKey.GetValue(uacRegistryValue).Equals(1);
                    return result;
                }
            }
        }

        /// <summary>
        /// Stores if the application who calls this function has been executed with admin rights.
        /// </summary>
        public static bool IsProcessElevated
        {
            get
            {
                if (IsUacEnabled)
                {
                    IntPtr tokenHandle;
                    if (!OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out tokenHandle))
                    {
                        throw new ApplicationException("Could not get process token.  Win32 Error Code: " +
                                                       Marshal.GetLastWin32Error());
                    }

                    try
                    {
                        var elevationResult = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;

                        var elevationResultSize = Marshal.SizeOf((int) elevationResult);

                        var elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                        try
                        {
                            uint returnedSize;
                            var success = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenElevationType,
                                elevationTypePtr, (uint) elevationResultSize,
                                out returnedSize);
                            if (success)
                            {
                                elevationResult = (TOKEN_ELEVATION_TYPE) Marshal.ReadInt32(elevationTypePtr);
                                var isProcessAdmin = elevationResult == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull;
                                return isProcessAdmin;
                            }
                            else
                            {
                                throw new ApplicationException("Unable to determine the current elevation.");
                            }
                        }
                        finally
                        {
                            if (elevationTypePtr != IntPtr.Zero)
                                Marshal.FreeHGlobal(elevationTypePtr);
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                            CloseHandle(tokenHandle);
                    }
                }
                var identity = WindowsIdentity.GetCurrent();
                if (identity == null) return false;
                var principal = new WindowsPrincipal(identity);
                var result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                             || principal.IsInRole(0x200); //Domain Administrator
                return result;
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);
    }
}