using System;
using System.IO.Ports;

namespace aclib
{
    public class SerialPortDriver : IAcDriver, IDisposable
    {
        private string _portValue;
        private int _baud;
        private SerialPort _port;

        public SerialPortDriver(string comPort, int baudRate = 9600){
            _portValue = comPort;
            _baud = baudRate;
        }

        public void Connect()
        {
            _port = new SerialPort(_portValue, _baud);
            _port.Open();
            // while (true)
            // {
            //     String s=Console.ReadLine();
            //     if (s.Equals("exit"))
            //     {
            //         break;
            //     }
            //     port.Write(s+'\n');
            // }
            // port.Close();
        }

        //TODO: Implement multiplexing or queueing.
        public void Send(string value){
            _port.Write(value + "\n");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_port!=null){
                        _port.Close();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SerialPortDriver() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}