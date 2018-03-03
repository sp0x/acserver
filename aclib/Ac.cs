namespace aclib
{
    public class Ac
    {
        private string _model;
        private IAcDriver _driver;
        public Ac(IAcDriver driver, string model, string name)
        {
            _model = model;
            this.Name = name;
            _driver = driver;
        }
        public string Model => _model;
        public int FanSpeedMax => 5;
        public byte MinTemp => 10;
        public byte MaxTemp => 30;
        public Ac(string name)
        {
            this.Name = name;

        }
        public string Name { get; set; }
        public void Execute(AcCommand command)
        {
            var sCommand = SerializeCommand(command);
            _driver.Send(sCommand);
        }
        private string SerializeCommand(AcCommand command)
        {
            return command.ToString();
        }
    }
}