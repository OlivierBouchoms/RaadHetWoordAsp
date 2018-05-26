namespace Data
{
    internal class Xml
    {
        private static string FileName => XmlResources.XmlFile;

        internal static string GetPath()
        {
            return FileName;
        }
    }
}
