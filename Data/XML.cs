using System.IO;

namespace Data
{
    internal class XML
    {
        private static string FileName => XmlResources.XmlFile;

        internal static string GetPath()
        {
            var directory = Directory.GetCurrentDirectory().Replace("RaadHetWoordGit", "Data");
            directory = directory.Replace("Tests", "Data");
            for (int i = 0; i < 3; i++)
            {
                directory = directory.Remove(directory.LastIndexOf("\\"));
            }
            return $"{directory}\\{FileName}";
        }
    }
}
