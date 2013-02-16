namespace Chat.Engine
{
    public interface IMessageCallback
    {
        void SayCallback();
        void FailureCallback(System.Exception ex);
    }
}