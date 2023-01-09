#pragma warning disable XAELIB0001

using McMaster.Extensions.CommandLineUtils;
using Xaevik.Cuid;
using Xaevik.Cuid.Generator;
using Xaevik.Cuid.Generator.Resources;

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
	}
});

return app.Execute(args);