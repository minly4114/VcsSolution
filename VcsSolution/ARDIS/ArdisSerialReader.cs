using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace ARDIS
{
    public class ArdisSerialReader
    {
        SerialPort sPort;
        public WriteLog writeLog;
        public delegate void WriteLog(string log);

        public ArdisSerialReader(SerialPort port)
        {
            sPort = port;
        }

        public Task StartReadingAsync(WriteLog wLog)
        {
            var task = new Task(ReadSerial);
            writeLog = wLog;
            task.Start();
            return task;
        }

        private void ReadSerial()
        {
            try
            {
                sPort.Open();
                while (true)
                {
                    string msg = sPort.ReadLine();
                    writeLog.BeginInvoke(msg, null, null);
                    Thread.Sleep(50);
                }
            } catch
            {
                throw;
            } finally
            {
                sPort.Close();
            }
        }
    }
}
