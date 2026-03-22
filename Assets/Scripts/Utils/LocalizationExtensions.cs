using Platformer.Model.Definitions.Localization;

namespace Platformer.Utils
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            return LocalizationManager.I.Localize(key);
        }
    }
}
