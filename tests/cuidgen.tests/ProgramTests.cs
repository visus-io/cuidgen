namespace Visus.Cuid.Generator.Tests;

using AwesomeAssertions;

internal sealed class ProgramTests
{
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
    public void Program_MaximumLength_GeneratesCuid2WithLength32()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "32");

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(32);
    }

    [Test]
    public void Program_MinimumLength_GeneratesCuid2WithLength4()
    {
        ( int exitCode, string output, string _ ) = ExecuteProgram("-l", "4");

        exitCode.Should().Be(0);
        output.Trim().Should().HaveLength(4);
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
    public void Program_UnrecognizedOption_ReturnsErrorAndShowsHelp()
    {
        ( int exitCode, string _, string error ) = ExecuteProgram("--invalid-option");

        exitCode.Should().Be(-1);
        error.Should().Contain("Usage:");
    }

    private static (int ExitCode, string Output, string Error) ExecuteProgram(params string[] args)
    {
        using StringWriter outputWriter = new();
        using StringWriter errorWriter = new();

        int exitCode = Program.Execute(args, outputWriter, errorWriter);

        return ( exitCode, outputWriter.ToString(), errorWriter.ToString() );
    }
}
