using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace RealEstate.Common.Logs
{
    public class LogCommon
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LogCommon));

        public LogCommon()
        {
            //BasicConfigurator.Configure();
            XmlConfigurator.Configure();
        }

        public static void WriteLogInfo(string msg)
        {
            _logger.Info(msg);
        }

        public static void WriteLogError(string msg)
        {
            _logger.Error(msg);
        }

        public static void WriteLogFatal(string msg)
        {
            _logger.Fatal(msg);
        }

        public static void WriteLogDebug(string msg)
        {
            _logger.Debug(msg);
        }

        public static void WriteLogWarning(string msg)
        {
            _logger.Warn(msg);
        }
        public static string PhysicalPath;
        public static void WriteError(string errorMessage)
        {
            try
            {
                string folder = "C:\\Error\\";
                string path = folder + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    //string err = "Error Message:" + errorMessage;
                    //w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        public static void WriteError(string errorMessage, string FunctionName)
        {
            try
            {
                string path = "C:\\Error\\" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
                using (StreamWriter w = File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = " Function: " + FunctionName +
                                  ". Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }
    }
}
