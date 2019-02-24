﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog.Sinks.File
{
    /// <summary>Interface of types that provide rolling log-file filename information.</summary>
    public interface IRollingFilePathProvider
    {
        /// <summary>The configured interval period.</summary>
        RollingInterval Interval { get; }

        /// <summary>Returns a Windows-compatible glob pattern for matching file-names in a directory. May return null if generated file-names will not match a glob pattern. Non-log files may match the glob pattern. If not returning null, it must return file-names without directory path names (so if the user wants files in a directory-per-interval, it would only match the expected filename WITHIN that interval directory).</summary>
        string DirectorySearchPattern { get; }

        /// <summary>Gets a path to a file (either absolute, or relative to the current file log directory) for the specified interval and point-in-time.</summary>
        string GetRollingLogFilePath(DateTime instant, Int32? sequenceNumber);

        /// <summary>Given a file-name (FileInfo) to a log file, returns <c>true</c> if the file matches the file-name pattern for that interval, and also returns the interval's period start encoded in the filename and the sequence number, if any.</summary>
        bool MatchRollingLogFilePath(FileInfo file, out DateTime? periodStart, out int? sequenceNumber);
    }

    internal static class PathUtility
    {
        public static string GetFilePathWithoutExtension(string filePath)
        {
            string ext = Path.GetExtension( filePath );
            if( String.IsNullOrEmpty( ext ) ) return filePath;

            return filePath.Substring( 0, filePath.Length - ext.Length );
        }
    }
}
