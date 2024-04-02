// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;
using System.Globalization;
using System;
using System.Management.Automation;
using System.Text;

namespace PoshSvn
{
    public class ArgumentToEncodingTransformationAttribute : ArgumentTransformationAttribute
    {
        public override object Transform(EngineIntrinsics engineIntrinsics, object inputData)
        {
            if (inputData is string stringName)
            {
                if (encodingMap.TryGetValue(stringName, out Encoding foundEncoding))
                {
                    return foundEncoding;
                }
                else
                {
                    return Encoding.GetEncoding(stringName);
                }
            }
            else if (inputData is int intName)
            {
                return Encoding.GetEncoding(intName);
            }
            else
            {
                return inputData;
            }
        }

        private static readonly Dictionary<string, Encoding> encodingMap = new Dictionary<string, Encoding>(StringComparer.OrdinalIgnoreCase)
        {
            { "ANSI", Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.ANSICodePage) },
            { "Ascii", Encoding.ASCII },
            { "BigEndianUnicode", Encoding.BigEndianUnicode },
            { "BigEndianUtf32", new UTF32Encoding(bigEndian: true, byteOrderMark: true) },
            { "Default", Encoding.Default },
            { "String", Encoding.Unicode },
            { "Unicode", Encoding.Unicode },
            { "Unknown", Encoding.Unicode },
            { "Utf7", Encoding.UTF7 },
            { "Utf8", Encoding.Default },
            { "Utf8Bom", Encoding.UTF8 },
            { "Utf8NoBom", Encoding.Default },
            { "Utf32", Encoding.UTF32 },
        };
    }
}
