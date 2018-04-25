using System.IO;

namespace Data
{
    public class XML
    {
        public static string FileName => XmlResources.XmlFile;

        public static string GetPath()
        {
            string directory = Directory.GetCurrentDirectory().Replace("RaadHetWoordGit", "Data");
            return $"{directory}\\{FileName}";
        }

        public static string GetParent()
        {
            return Directory.GetCurrentDirectory().Replace("RaadHetWoordGit", "Data");
        }
    }
}
