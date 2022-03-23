using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBShoppingCart.Utility.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger nLogger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => nLogger.Debug(message);

        public void LogError(string message) => nLogger.Error(message);

        public void LogInfo(string message) => nLogger.Info(message);

        public void LogWarn(string message) => nLogger.Warn(message);
    }
}
