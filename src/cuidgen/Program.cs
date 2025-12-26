#pragma warning disable VISLIB0001
#pragma warning disable CA1031

namespace Visus.Cuid.Generator;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

internal static class Program
{
    private static readonly string ApplicationName = AppDomain.CurrentDomain.FriendlyName;

    private static readonly CompositeFormat ErrorUnrecognizedOptionFormat =
        CompositeFormat.Parse(Resources.Resources.Error_UnrecognizedOption);

    private static readonly CompositeFormat HelpUsageFormat = CompositeFormat.Parse(Resources.Resources.Help_Usage);

    public static int Execute(string[] args, TextWriter outputWriter, TextWriter errorWriter)
    {
        short generation = 2;
        int length = 24;
        int number = 1;

        try
        {
            for ( int i = 0; i < args.Length; i++ )
            {
                int? result = ProcessArgument(args,
                    ref i,
                    ref generation,
                    ref length,
                    ref number,
                    outputWriter,
                    errorWriter);

                if ( result.HasValue )
                {
                    return result.Value;
                }
            }

            GenerateCuid(generation, length, number, outputWriter);
            return 0;
        }
        catch ( Exception )
        {
            ShowHelp(errorWriter);
            return -1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GenerateCuid(short generation, int length, int number, TextWriter outputWriter)
    {
        for ( int i = 0; i < number; i++ )
        {
            switch ( generation )
            {
                case 1:
                    outputWriter.WriteLine(Cuid.NewCuid());
                    break;
                case 2:
                    outputWriter.WriteLine(new Cuid2(length));
                    break;
            }
        }
    }

    [ExcludeFromCodeCoverage]
    private static int Main(string[] args)
    {
        return Execute(args, Console.Out, Console.Error);
    }

    private static int? ProcessArgument(string[] args,
                                        ref int index,
                                        ref short generation,
                                        ref int length,
                                        ref int number,
                                        TextWriter outputWriter,
                                        TextWriter errorWriter
    )
    {
        string arg = args[index];

        switch ( arg )
        {
            case "-v":
            case "--version":
                outputWriter.WriteLine(VersionHelper.GetVersion(true));
                return 0;

            case "-h":
            case "--help":
            case "-?":
                ShowHelp(outputWriter);
                return 0;

            case "-g":
            case "--generation":
                return TryParseGeneration(args, ref index, ref generation, errorWriter) ? null : -1;

            case "-l":
            case "--length":
                return TryParseLength(args, ref index, ref length, errorWriter) ? null : -1;

            case "-n":
            case "--number":
                return TryParseNumber(args, ref index, ref number, errorWriter) ? null : -1;

            default:
                errorWriter.WriteLine(string.Format(CultureInfo.CurrentCulture, ErrorUnrecognizedOptionFormat, arg));
                ShowHelp(errorWriter);
                return -1;
        }
    }

    private static void ShowHelp(TextWriter writer)
    {
        writer.WriteLine(VersionHelper.GetVersion(true));
        writer.WriteLine();
        writer.WriteLine(string.Format(CultureInfo.CurrentCulture, HelpUsageFormat, ApplicationName));
        writer.WriteLine();
        writer.WriteLine(Resources.Resources.Help_OptionsHeader);
        writer.WriteLine($"  -g, --generation <generation>  {Resources.Resources.HelpOpt_G_Descr}");
        writer.WriteLine($"                                 {Resources.Resources.HelpOpt_G_AllowedValues}");
        writer.WriteLine($"                                 {Resources.Resources.HelpOpt_G_DefaultValue}");
        writer.WriteLine($"  -l, --length <length>          {Resources.Resources.HelpOpt_L_Descr}");
        writer.WriteLine($"                                 {Resources.Resources.HelpOpt_L_DefaultValue}");
        writer.WriteLine($"  -n, --number <number>          {Resources.Resources.HelpOpt_N_Descr}");
        writer.WriteLine($"                                 {Resources.Resources.HelpOpt_N_DefaultValue}");
        writer.WriteLine($"  -v, --version                  {Resources.Resources.HelpOpt_V_Descr}");
        writer.WriteLine($"  -h, --help                     {Resources.Resources.HelpOpt_H_Descr}");
    }

    private static bool TryParseGeneration(string[] args, ref int index, ref short generation, TextWriter errorWriter)
    {
        if ( index + 1 >= args.Length )
        {
            errorWriter.WriteLine(Resources.Resources.Error_GenerationRequiresValue);
            ShowHelp(errorWriter);
            return false;
        }

        if ( short.TryParse(args[++index], CultureInfo.InvariantCulture, out generation) && generation is 1 or 2 )
        {
            return true;
        }

        errorWriter.WriteLine(Resources.Resources.Error_GenerationInvalidValue);
        ShowHelp(errorWriter);
        return false;
    }

    private static bool TryParseLength(string[] args, ref int index, ref int length, TextWriter errorWriter)
    {
        if ( index + 1 >= args.Length )
        {
            errorWriter.WriteLine(Resources.Resources.Error_LengthRequiresValue);
            ShowHelp(errorWriter);
            return false;
        }

        if ( int.TryParse(args[++index], CultureInfo.InvariantCulture, out length) && length is >= 4 and <= 32 )
        {
            return true;
        }

        errorWriter.WriteLine(Resources.Resources.Error_LengthOutOfRange);
        ShowHelp(errorWriter);
        return false;
    }

    private static bool TryParseNumber(string[] args, ref int index, ref int number, TextWriter errorWriter)
    {
        if ( index + 1 >= args.Length )
        {
            errorWriter.WriteLine(Resources.Resources.Error_NumberRequiresValue);
            ShowHelp(errorWriter);
            return false;
        }

        if ( int.TryParse(args[++index], CultureInfo.InvariantCulture, out number) && number is >= 1 and <= 1000 )
        {
            return true;
        }

        errorWriter.WriteLine(Resources.Resources.Error_NumberOutOfRange);
        ShowHelp(errorWriter);
        return false;
    }
}
