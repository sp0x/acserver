namespace aclib
{
    public interface IAcDriver
    {
        void Connect();
        void Send(string command);
    }
}