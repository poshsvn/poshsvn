// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PoshSvn
{
    public static class SecureStringExtensions
    {
        public static string AsPlainString(this SecureString secureString)
        {
            string result;
            IntPtr bstr = IntPtr.Zero;

            if (secureString == null)
            {
                return null;
            }
           
            if(secureString.Length == 0)
            {
                return "";
            }

            try
            {
                bstr = Marshal.SecureStringToBSTR(secureString);
                result = Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                if (bstr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(bstr);
                }
            }

            return result;
        }
    }
}
