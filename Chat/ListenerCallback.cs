using System;

namespace Chat
{
    public class ListenerCallback : IListenerCallback
    {
        #region IListenerCallback Members

        public void SayCallback()
        {
            Console.WriteLine(string.Concat("Post send - ", DateTime.Now.ToLongTimeString()));
        }

        public void FailureCallback(Exception ex)
        {
            Console.Error.WriteLine("Exception in Listener Callback: {0}", ex);
        }

        #endregion
    }
}