#pragma warning disable VISLIB0001

using McMaster.Extensions.CommandLineUtils;
using Visus.Cuid.Generator;
using Visus.Cuid.Generator.Resources;
using Visus.Cuid;

CommandLineApplication app = new();

CommandOption<short> generation = app
	.Option<short>("-g <generation>", Resources.HelpOpt_G_Descr, CommandOptionType.SingleOrNoValue)
	.Accepts(a => a.Values("1", "2"));

CommandOption<int> length = app.Option<int>("-l <length>", Resources.HelpOpt_L_Descr, CommandOptionType.SingleOrNoValue)
	.Accepts(a => a.Range(4, 32));

generation.DefaultValue = 2;
length.DefaultValue = 24;

app.HelpOption();
app.VersionOption("-v", VersionHelper.GetVersion(true), VersionHelper.GetVersion(false));

app.OnExecute(() =>
{
	switch ( generation.ParsedValue )
	{
		case 1:
			Console.WriteLine(Cuid.NewCuid());
			break;
		case 2:
			Console.WriteLine(new Cuid2(length.ParsedValue));
			break;
		default:
#pragma warning disable CA2208
			// ReSharper disable once LocalizableElement
			throw new ArgumentException("Unknown generation type", nameof(generation.ParsedValue));
#pragma warning restore CA2208
	}
});

return app.Execute(args);