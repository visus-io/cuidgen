namespace Visus.Cuid.Generator;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

[ExcludeFromCodeCoverage]
internal static class VersionHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetVersion(bool simple)
	{
		Version? version = Assembly.GetExecutingAssembly().GetName().Version;
		if ( version is null )
		{
			return string.Empty;
		}

		return simple ? $"{version.Major}.{version.Minor}.{version.Build}" : version.ToString();
	}
}