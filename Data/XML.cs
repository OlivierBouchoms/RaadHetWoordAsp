using System.IO;

namespace Data
{
    internal class XML
    {
        private static string FileName => XmlResources.XmlFile;

        internal static string GetPath()
        {
            var directory = Directory.GetCurrentDirectory().Replace("RaadHetWoordGit", "Data");
            return $"{directory}\\{FileName}";
        }
    }
}
