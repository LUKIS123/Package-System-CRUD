using Exception = System.Exception;
using Process = System.Diagnostics.Process;

namespace Package_System_CRUD.BusinessLogic.Utility
{
    public class DateBuffer
    {
        private readonly string _dataFilePath;

        public DateTime CurrentDateTime { get; private set; }

        public DateBuffer()
        {
            var sFile = System.IO.Path.Combine(
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ??
                throw new Exception("Path Error"), @".\date.txt"
            );
            _dataFilePath = Path.GetFullPath(sFile);

            this.RefreshCurrentDateTime();
        }

        public DateTime RefreshCurrentDateTime()
        {
            try
            {
                var data = Array
                    .ConvertAll(FileUtils.ReadDataFile(_dataFilePath).Split("."), int.Parse);
                CurrentDateTime = new DateTime(data[2], data[1], data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                CurrentDateTime = DateTime.Today;
                FileUtils.SaveDataFile(
                    _dataFilePath,
                    CurrentDateTime.Day + "." + CurrentDateTime.Month + "." + CurrentDateTime.Year
                );
            }

            return CurrentDateTime;
        }
    }
}