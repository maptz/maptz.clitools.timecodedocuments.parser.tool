//using Maptz.CliTools.TimeCodeDocument.Parser.Tool.Engine
using Maptz.CliTools.TimeCodeDocument.Parser.Engine;
using Microsoft.Extensions.DependencyInjection;
using Maptz.Editing.TimeCodeDocuments.Converters.All;
using Maptz.Editing.TimeCodeDocuments.StringDocuments;
using Maptz.Editing.TimeCodeDocuments;
using Microsoft.Extensions.Logging;

namespace Maptz.CliTools.TimeCodeDocument.Parser.Tool
{
    class Program : CliProgramBase<AppSettings>
    {
        public static void Main(string[] args)
        {
            new Program(args);
        }

        public Program(string[] args): base(args)
        {
        }

        protected override void AddServices(IServiceCollection serviceCollection)
        {
            base.AddServices(serviceCollection);
            serviceCollection.AddTransient<ICliProgramRunner, CLIProgramRunner>();
            serviceCollection.AddTransient<IParserEngine, ParserEngine>();
            serviceCollection.AddOptions();
            serviceCollection.AddLogging();

            serviceCollection.AddLogging(loggingBuilder => 
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"))
                                           .AddConsole()
                                           .AddDebug());

            serviceCollection.AddTimeCodeDocumentConverters();
            serviceCollection.Configure<TimeCodeStringDocumentParserSettings>(settings =>
            {
                settings.FrameRate = SmpteFrameRate.Smpte25;
            }

            );
            serviceCollection.Configure<TimeCodeDocumentTimeValidatorSettings>(settings =>
            {
                settings.DefaultDurationFrames = 40;
            }

            );
        }
    }
}