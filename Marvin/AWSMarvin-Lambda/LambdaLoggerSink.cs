using Amazon.Lambda.Core;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSMarvin_Lambda
{
    public static class MySinkExtensions
    {
        public static LoggerConfiguration LambdaLoggerSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new LambdaLoggerSink(formatProvider));
        }
    }

    public class LambdaLoggerSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public LambdaLoggerSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            LambdaLogger.Log(message + Environment.NewLine);
        }
    }
}
