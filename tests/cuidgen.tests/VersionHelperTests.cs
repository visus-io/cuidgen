namespace Visus.Cuid.Generator.Tests;

using System.Globalization;
using AwesomeAssertions;

internal sealed class VersionHelperTests
{
    [Test]
    public void GetVersion_FullFormat_ContainsSimpleFormat()
    {
        string simpleVersion = VersionHelper.GetVersion(true);
        string fullVersion = VersionHelper.GetVersion(false);

        fullVersion.Should().StartWith(simpleVersion);
    }

    [Test]
    public void GetVersion_SimpleFormat_MatchesPattern()
    {
        string result = VersionHelper.GetVersion(true);

        string[] parts = result.Split('.');
        parts.Should().HaveCount(3);

        foreach ( string part in parts )
        {
            int.TryParse(part, CultureInfo.InvariantCulture, out _).Should().BeTrue();
        }
    }

    [Test]
    public void GetVersion_WithSimpleFalse_ReturnsFullVersion()
    {
        string result = VersionHelper.GetVersion(false);

        result.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void GetVersion_WithSimpleTrue_ReturnsSimpleVersion()
    {
        string result = VersionHelper.GetVersion(true);

        result.Should().NotBeNullOrEmpty();
        result.Split('.').Should().HaveCount(3);
    }
}
