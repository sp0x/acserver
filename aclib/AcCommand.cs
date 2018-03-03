using System;

namespace AcController
{
    public class AcCommand
    {
        private int _temp;
        private bool _off;
        private byte _fanSpeed;
        private AcMode _mode;
        public AcCommand(){
        }
        public AcCommand SetTemperature(int temperature){
            this._temp = temperature;
            return this;
        }
        public AcCommand TurnOff() { this._off = true; return this;}
        public AcCommand SetFanSpeed(Ac ac, byte percentage){
            byte fval = (byte)Math.Ceiling(ac.FanSpeedMax * ((float)percentage/100.0f));
            _fanSpeed = fval;
            return this;
        }
        public AcCommand Cold(){ _mode = AcMode.Cold; return this;}
        public AcCommand Heat(){ _mode = AcMode.Heat; return this;}

        public override string ToString(){
            if(_off) return "off";
            string sMode = _mode == AcMode.Heat ? "H" : "C";
            var cmds = $"{sMode} {_temp} {_fanSpeed}";
            return cmds;
        }
    }
}