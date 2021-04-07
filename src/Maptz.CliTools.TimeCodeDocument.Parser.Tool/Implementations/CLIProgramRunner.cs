using Maptz.CliTools.TimeCodeDocument.Parser.Engine;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Maptz.CliTools.TimeCodeDocument.Parser.Tool
{
    public class CLIProgramRunner : ICliProgramRunner
    {
        /* #region Public Properties */
        public AppSettings AppSettings
        {
            get;
        }

        public IConsoleInstance ConsoleInstance
        {
            get;
        }

        public ILogger Logger
        {
            get;
        }

        public ILoggerFactory LoggerFactory
        {
            get;
        }

        public IServiceProvider ServiceProvider
        {
            get;
        }

        /* #endregion Public Properties */
        /* #region Public Constructors */
        public CLIProgramRunner(IOptions<AppSettings> appSettings, ILoggerFactory loggerFactory, IServiceProvider serviceProvider, IConsoleInstance consoleInstance, IParserEngine parserEngine)
        {
            this.AppSettings = appSettings.Value;
            this.LoggerFactory = loggerFactory;
            this.Logger = this.LoggerFactory.CreateLogger(this.GetType().Name);
            this.ServiceProvider = serviceProvider;
            this.ConsoleInstance = consoleInstance;
            this.ParserEngine = parserEngine;
        }

        /* #endregion Public Constructors */
        /* #region Public Methods */
        public async Task RunAsync(string[] args)
        {
            await Task.Run(() =>
            {
                CommandLineApplication cla = new CommandLineApplication(throwOnUnexpectedArg: false);
                cla.HelpOption("-?|-h|--help");
                cla.Description = "A tool for converting loose timecode documents into various forms of timecoded files.";
                /* #region get */
                cla.Command("convert", config =>
                {
                    var modeOption = config.Option("-m|--mode <conversionMode>", $"The conversion mode.", CommandOptionType.SingleValue);
                    var inputFilePathOption = config.Option("-i|--inputFilePath <inputFilePath>", $"The file to convert", CommandOptionType.SingleValue);
                    config.OnExecute(async () =>
                    {
                        var inputFilePath = inputFilePathOption.HasValue() ? inputFilePathOption.Value() : this.AppSettings.InputFilePath;
                        var mode = modeOption.HasValue() ? modeOption.Value() : this.AppSettings.Mode.ToString();
                        await this.ParserEngine.ParseAsync(inputFilePath, mode);
                        return 0;
                    }

                    );
                }

                );
                /* #endregion*/
                /* #region Default */
                //Just show the help text.
                cla.OnExecute(() =>
                {
                    cla.ShowHelp();
                    return 0;
                }
                //some s
                );
                /* #endregion*/
                cla.Execute(args); 
            }

            );
        }

        public IParserEngine ParserEngine
        {
            get;
        }
    /* #endregion Public Methods */
    }
}