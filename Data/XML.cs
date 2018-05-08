namespace Data
{
    internal class XML
    {
        private static string FileName => XmlResources.XmlFile;

        internal static string GetPath()
        {
            return FileName;
        }
    }
}
