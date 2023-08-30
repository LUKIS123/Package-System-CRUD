using System.Diagnostics;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Utility;

namespace Package_System_CRUD.BusinessLogic.DateTimeProvider
{
    public class FileDateTimeProvider : IDateTimeProvider
    {
        private readonly string _dataFilePath;

        public FileDateTimeProvider(ConfigurationProperties config)
        {
            var sFile = System.IO.Path.Combine(
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ??
                throw new Exception("Path Error"), $@".\{config.FileDateTimeProviderSourcePath}"
            );
            _dataFilePath = Path.GetFullPath(sFile);
        }

        private DateTime RefreshCurrentDateTime()
        {
            DateTime currentDateTime;
            try
            {
                var data = Array
                    .ConvertAll(FileUtils.ReadDataFile(_dataFilePath).Split("."), int.Parse);
                currentDateTime = new DateTime(data[2], data[1], data[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                currentDateTime = DateTime.Today;
                FileUtils.SaveDataFile(
                    _dataFilePath,
                    currentDateTime.Day + "." + currentDateTime.Month + "." + currentDateTime.Year
                );
            }

            return currentDateTime;
        }

        public DateTime GetDateTime()
        {
            return RefreshCurrentDateTime();
        }
    }
}