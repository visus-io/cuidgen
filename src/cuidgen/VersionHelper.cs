namespace Visus.Cuid.Generator;

using System.Globalization;
using System.Reflection;

internal static class VersionHelper
{
    public static string GetVersion(bool simple)
    {
        Version version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0, 0);

        return simple
                   ? string.Create(CultureInfo.InvariantCulture, $"{version.Major}.{version.Minor}.{version.Build}")
                   : version.ToString();
    }
}
