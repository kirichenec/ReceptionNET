using Avalonia.Logging;
using Reception.Extension;
using Reception.Extension.Converters;
using System.Text;

namespace Reception.App.Extensions
{
    public static class ILogSinkExtensions
    {
        public static void LogException<TIn1>(this ILogSink logger, string areaMethodName,
            TIn1 source, Type errorType, params object[] areaMethodParams)
        {
            string messageTemplate = GetMessageTemplate(areaMethodParams);
            logger.Log(LogEventLevel.Error, areaMethodName, source, messageTemplate, errorType.Name,
                       areaMethodParams.ToJsonStrings().ToJoinString(Environment.NewLine));
        }

        private static string GetMessageTemplate(object[] areaMethodParams)
        {
            var messageTemplateBuilder = new StringBuilder("ExceptionType: {0}");
            switch (areaMethodParams?.Length)
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