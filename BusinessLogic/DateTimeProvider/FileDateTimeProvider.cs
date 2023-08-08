using System.Diagnostics;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Utility;

namespace Package_System_CRUD.BusinessLogic.DateTimeProvider
{
    public class FileDateTimeProvider : IDateTimeProvider
    {
        private DateTime _currentDateTime;
        private readonly string _dataFilePath;

        public FileDateTimeProvider(ConfigurationProperties config)
        {
            var sFile = System.IO.Path.Combine(
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ??
                throw new Exception("Path Error"), $@".\{config.FileDateTimeProviderSourcePath}"
            );
            _dataFilePath = Path.GetFullPath(sFile);

            RefreshCurrentDateTime();
        }

        public DateTime RefreshCurrentDateTime()
        {
            try
            {
                var data = Array
                    .ConvertAll(FileUtils.ReadDataFile(_dataFilePath).Split("."), int.Parse);
                _currentDateTime = new DateTime(data[2], data[1], data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _currentDateTime = DateTime.Today;
                FileUtils.SaveDataFile(
                    _dataFilePath,
                    _currentDateTime.Day + "." + _currentDateTime.Month + "." + _currentDateTime.Year
                );
            }

            return _currentDateTime;
        }

        public DateTime GetDateTime()
        {
            return _currentDateTime;
        }
    }
}