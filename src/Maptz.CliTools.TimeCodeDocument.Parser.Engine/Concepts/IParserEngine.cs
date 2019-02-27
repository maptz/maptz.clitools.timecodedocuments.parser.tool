using System;
using System.IO;
using System.Threading.Tasks;

namespace Maptz.CliTools.TimeCodeDocument.Parser.Engine
{

    public interface IParserEngine
    {
        Task ParseAsync(string inputFilePath, string converterTypeStr);
    }
}