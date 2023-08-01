using System.Text;

namespace Package_System_CRUD.BusinessLogic.Utility
{
    public class FileUtils
    {
        public static string ReadDataFile(string filePath)
        {
            string readContents;
            using (var streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                readContents = streamReader.ReadToEnd();
            }

            return readContents;
        }

        public static void SaveDataFile(string filePath, string data)
        {
            try
            {
                using (var fs = File.Create(filePath))
                {
                    var info = new UTF8Encoding(true).GetBytes(data);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}