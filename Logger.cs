using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ResumeScrenner
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public static class Logger
    {
        private static readonly string LogDirectory = "Logs";
        private static readonly string LogFileName = "ResumeScreener.log";
        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private static bool _initialized = false;

        static Logger()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if (_initialized)
                return;

            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                _initialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to initialize logger: {ex.Message}");
            }
        }

        public static void Log(LogLevel level, string message)
        {
            string logFilePath = Path.Combine(LogDirectory, LogFileName);
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            try
            {
                _lock.EnterWriteLock();

                using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log: {ex.Message}");
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public static string[] GetRecentLogs(int count = 100)
        {
            string logFilePath = Path.Combine(LogDirectory, LogFileName);

            if (!File.Exists(logFilePath))
                return new string[0];

            try
            {
                _lock.EnterReadLock();

                string[] allLines = File.ReadAllLines(logFilePath);
                int startIndex = Math.Max(0, allLines.Length - count);
                int length = Math.Min(count, allLines.Length);

                string[] result = new string[length];
                Array.Copy(allLines, startIndex, result, 0, length);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading log: {ex.Message}");
                return new string[0];
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public static void ClearLogs()
        {
            string logFilePath = Path.Combine(LogDirectory, LogFileName);

            try
            {
                _lock.EnterWriteLock();

                if (File.Exists(logFilePath))
                {
                    File.WriteAllText(logFilePath, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing log: {ex.Message}");
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}