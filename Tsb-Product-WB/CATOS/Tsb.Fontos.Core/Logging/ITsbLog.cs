using System;

using log4net;
using log4net.Config;

namespace Tsb.Fontos.Core.Logging
{
    public interface ITsbLog : ILog
    {
        new void Debug(object message);
        new void Debug(object message, Exception t);

        new void Info(object message);
        new void Info(object message, Exception t);

        new void Warn(object message);
        new void Warn(object message, Exception t);

        new void Error(object message);
        new void Error(object message, Exception t);

        new void Fatal(object message);
        new void Fatal(object message, Exception t);
    }
}