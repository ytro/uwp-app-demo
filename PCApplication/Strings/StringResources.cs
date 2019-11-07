using Windows.ApplicationModel.Resources;

namespace PCApplication
{
    /// <summary>
    /// A class to manage string resources, such as mocked JSON objects. See the "Resources.resw" file.
    /// </summary>
    public class StringResources
    {
        private static ResourceLoader _loader;

        static StringResources()
        {
            _loader = ResourceLoader.GetForViewIndependentUse("Resources");
        }

        // Grabs a string from the Resources.resw file using a key
        public static string GetString(string key)
        {
            var text = _loader.GetString(key);
            if (text.Length > 0)
            {
                return text;
            }
            return key;
        }
    }
}
