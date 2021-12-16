using log4net;

namespace Froala.Web
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        public static ILog Log
        {
            get { return Logger.log; }
        }
    }
}