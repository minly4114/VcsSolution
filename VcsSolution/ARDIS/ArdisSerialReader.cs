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
        string message;
        bool needSend = false;

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
                    if (sPort.BytesToRead > 0)
                    { 
                        string msg = sPort.ReadLine();
                        writeLog.BeginInvoke(msg, null, null);
                    }
                    if (needSend)
                    {
                        sPort.WriteLine(message);
                        needSend = false;
                    }
                    Thread.Sleep(10);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                sPort.Close();
            }
        }

        public void SendMessage(string msg)
        {
            message = msg;
            needSend = true;
        }
    }
}
