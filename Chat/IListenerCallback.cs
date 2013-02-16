namespace Chat
{
    public interface IListenerCallback
    {
        void SayCallback();
        void FailureCallback(System.Exception ex);
    }
}