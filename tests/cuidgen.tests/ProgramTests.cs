namespace Visus.Cuid.Generator.Tests;

using AwesomeAssertions;

internal sealed class ProgramTests
{
    private const string UsageText = "Usage:";

    [Test]
    public void Program_CustomLength_GeneratesCuid2WithSpecifiedLength()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "10");

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(10);
    }

    [Test]
    public void Program_Generation1_GeneratesCuid1()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-g", "1");

        exitCode.Should().Be(0);
        string cuid = output.Trim();
        cuid.Should().StartWith("c");
        cuid.Should().HaveLength(25);
    }

    [Test]
    [Arguments("-g")]
    [Arguments("--generation")]
    public void Program_GenerationWithoutValue_ReturnsError(string generationFlag)
    {
        ( int exitCode, string _, string error ) = ExecuteProgram(generationFlag);

        exitCode.Should().Be(-1);
        error.Should().Contain(UsageText);
    }

    [Test]
    [Arguments("--help")]
    [Arguments("-h")]
    [Arguments("-?")]
    public void Program_Help_DisplaysHelpAndExitsSuccessfully(string helpFlag)
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram(helpFlag);

        exitCode.Should().Be(0);
        output.Should().Contain(UsageText);
    }

    [Test]
    public void Program_InvalidGeneration_ReturnsError()
    {
        ( int exitCode, string _, string _ ) = ExecuteProgram("-g", "3");

        exitCode.Should().NotBe(0);
    }

    [Test]
    public void Program_LengthTooLarge_ReturnsError()
    {
        ( int exitCode, string _, string _ ) = ExecuteProgram("-l", "33");

        exitCode.Should().NotBe(0);
    }

    [Test]
    public void Program_LengthTooSmall_ReturnsError()
    {
        ( int exitCode, string _, string _ ) = ExecuteProgram("-l", "3");

        exitCode.Should().NotBe(0);
    }

    [Test]
    [Arguments("-l")]
    [Arguments("--length")]
    public void Program_LengthWithoutValue_ReturnsError(string lengthFlag)
    {
        ( int exitCode, string _, string error ) = ExecuteProgram(lengthFlag);

        exitCode.Should().Be(-1);
        error.Should().Contain(UsageText);
    }

    [Test]
    public void Program_MaximumLength_GeneratesCuid2WithLength32()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "32");

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(32);
    }

    [Test]
    public void Program_MaximumNumber_Generates1000Cuids()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-n", "1000");

        exitCode.Should().Be(0);
        string[] lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines.Should().HaveCount(1000);
    }

    [Test]
    public void Program_MinimumLength_GeneratesCuid2WithLength4()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "4");

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(4);
    }

    [Test]
    public void Program_MinimumNumber_GeneratesSingleCuid()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-n", "1");

        exitCode.Should().Be(0);
        string[] lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines.Should().ContainSingle();
        lines[0].Should().HaveLength(24);
    }

    [Test]
    public void Program_MultipleInvocations_GenerateUniqueCuids()
    {
        ( int _, string output1, string _ ) = ExecuteProgram();
        ( int _, string output2, string _ ) = ExecuteProgram();
        ( int _, string output3, string _ ) = ExecuteProgram();

        string cuid1 = output1.Trim();
        string cuid2 = output2.Trim();
        string cuid3 = output3.Trim();

        cuid1.Should().NotBe(cuid2);
        cuid1.Should().NotBe(cuid3);
        cuid2.Should().NotBe(cuid3);
    }

    [Test]
    public void Program_NoArguments_GeneratesCuid2WithDefaultLength()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram();

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(24);
    }

    [Test]
    [Arguments("-n", "5")]
    [Arguments("--number", "5")]
    public void Program_Number_GeneratesSpecifiedNumberOfCuids(string numberFlag, string count)
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram(numberFlag, count);

        exitCode.Should().Be(0);
        string[] lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines.Should().HaveCount(5);

        foreach ( string line in lines )
        {
            line.Should().HaveLength(24);
        }
    }

    [Test]
    public void Program_NumberTooLarge_ReturnsError()
    {
        ( int exitCode, string _, string error ) = ExecuteProgram("-n", "1001");

        exitCode.Should().Be(-1);
        error.Should().Contain("Number must be between 1 and 1000");
    }

    [Test]
    public void Program_NumberTooSmall_ReturnsError()
    {
        ( int exitCode, string _, string error ) = ExecuteProgram("-n", "0");

        exitCode.Should().Be(-1);
        error.Should().Contain("Number must be between 1 and 1000");
    }

    [Test]
    public void Program_NumberWithCustomLength_GeneratesMultipleCuidsWithSpecifiedLength()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "16", "-n", "4");

        exitCode.Should().Be(0);
        string[] lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines.Should().HaveCount(4);

        foreach ( string line in lines )
        {
            line.Should().HaveLength(16);
        }
    }

    [Test]
    public void Program_NumberWithGeneration1_GeneratesMultipleCuid1s()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-g", "1", "-n", "3");

        exitCode.Should().Be(0);
        string[] lines = output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines.Should().HaveCount(3);

        foreach ( string line in lines )
        {
            line.Should().StartWith("c");
            line.Should().HaveLength(25);
        }
    }

    [Test]
    [Arguments("-n")]
    [Arguments("--number")]
    public void Program_NumberWithoutValue_ReturnsError(string numberFlag)
    {
        ( int exitCode, string _, string error ) = ExecuteProgram(numberFlag);

        exitCode.Should().Be(-1);
        error.Should().Contain(UsageText);
    }

    [Test]
    public void Program_UnrecognizedOption_ReturnsErrorAndShowsHelp()
    {
        ( int exitCode, string _, string error ) = ExecuteProgram("--invalid-option");

        exitCode.Should().Be(-1);
        error.Should().Contain(UsageText);
    }

    [Test]
    [Arguments("--version")]
    [Arguments("-v")]
    public void Program_Version_DisplaysVersionAndExitsSuccessfully(string versionFlag)
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram(versionFlag);

        exitCode.Should().Be(0);
        output.Should().NotBeEmpty();
    }

    private static (int ExitCode, string Output, string Error) ExecuteProgram(params string[] args)
    {
        using StringWriter outputWriter = new();
        using StringWriter errorWriter = new();

        int exitCode = Program.Execute(args, outputWriter, errorWriter);

        return ( exitCode, outputWriter.ToString(), errorWriter.ToString() );
    }
}
