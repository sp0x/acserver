namespace AcController
{
    public class AcManager
    {
        private IAcDriver _acDriver;

        public AcManager(IAcDriver acDriver){
            _acDriver = acDriver;
        }

        public Ac CreateAc(string model)
        {
            Ac newAc = new Ac(_acDriver, model, "Unnamed Ac");
            return newAc;
        }

        public void SetDriver(IAcDriver acDriver)
        {
            _acDriver = acDriver;
        }
    }
}