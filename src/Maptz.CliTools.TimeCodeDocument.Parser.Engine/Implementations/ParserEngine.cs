using Maptz.Editing.TimeCodeDocuments;
using Maptz.Editing.TimeCodeDocuments.Converters.All;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Maptz.CliTools.TimeCodeDocument.Parser.Engine
{
    public class ParserEngine : IParserEngine
    {
        public ParserEngine(IConsoleInstance consoleInstance, IServiceProvider serviceProvider)
        {
            this.ConsoleInstance = consoleInstance;
            this.ServiceProvider = serviceProvider;
        }

        public async Task ParseAsync(string inputFilePath, string converterTypeStr)
        {
            string sourceText;
            var fileInfo = new FileInfo(inputFilePath);
            if (!fileInfo.Exists)
            {
                var error = $"No file found at path {inputFilePath}";
                this.ConsoleInstance.WriteError(error);
                throw new FileNotFoundException(inputFilePath);
            }

            using (var sr = fileInfo.OpenText())
            {
                sourceText = sr.ReadToEnd();
            }

            IStreamableResult result = null;
            switch (converterTypeStr?.ToLower())
            {
                case "avidds":
                    var avidParser = await TimeCodeDocumentConverters.GetAvidDSParserAsync(this.ServiceProvider);
                    result = avidParser.Parse(sourceText);
                    break;
                case "finalcutxml":
                    var fcpParser = await TimeCodeDocumentConverters.GetFinalCutParserAsync(this.ServiceProvider);
                    result = fcpParser.Parse(sourceText);
                    break;
                case "markdown":
                    var markdownParser = await TimeCodeDocumentConverters.GetMarkdownParserAsync(this.ServiceProvider);
                    result = markdownParser.Parse(sourceText);
                    break;
                case "smptett":
                    var smptettParser = await TimeCodeDocumentConverters.GetSMPTETTParserAsync(this.ServiceProvider);
                    result = smptettParser.Parse(sourceText);
                    break;
                case "srt":
                    var srtParser = await TimeCodeDocumentConverters.GetSRTParserAsync(this.ServiceProvider);
                    result = srtParser.Parse(sourceText);
                    break;
                default:
                    throw new NotSupportedException();
            }

            this.Save(inputFilePath, result);
        }

        private void Save(string inputFilePath, IStreamableResult result)
        {
            var outputFilePath = Path.Combine(Path.GetDirectoryName(inputFilePath), Path.GetFileNameWithoutExtension(inputFilePath) + result.DefaultFileExtension);
            var outputFileInfo = new FileInfo(outputFilePath);
            using (var fs = File.Create(outputFilePath))
            {
                var str = result.GetStream();
                str.CopyTo(fs);
                str.Dispose();
            }
        }

        public IConsoleInstance ConsoleInstance
        {
            get;
        }

        public IServiceProvider ServiceProvider
        {
            get;
        }
    }
}