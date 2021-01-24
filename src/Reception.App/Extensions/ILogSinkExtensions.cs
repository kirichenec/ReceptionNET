using Avalonia.Logging;
using Reception.Extension.Converters;
using System;
using System.Linq;
using System.Text;

namespace Reception.App.Extensions
{
    public static class ILogSinkExtensions
    {
        public static void LogException<TIn1>(this ILogSink logger, string areaMethodName, TIn1 source, Type errorType, params object[] areaMethodParams)
        {
            string messageTemplate = GetMessageTemplate(areaMethodParams);
            logger.Log(LogEventLevel.Error, areaMethodName, source, messageTemplate,
                       errorType.Name, string.Join(Environment.NewLine, areaMethodParams.ToJsonStrings()));
        }

        private static string GetMessageTemplate(object[] areaMethodParams)
        {
            StringBuilder messageTemplateBuilder = new StringBuilder("ExceptionType: {0}");
            switch (areaMethodParams?.Count())
            {
                case null:
                case 0:
                    break;
                case 1:
                    messageTemplateBuilder.Append("parameter: {1}");
                    break;
                default:
                    messageTemplateBuilder.Append("parameters: {1}");
                    break;
            }
            return messageTemplateBuilder.ToString();
        }
    }
}