using System.Windows.Forms;
using Ice;

namespace Chat.Engine
{
    public class ListenerI : ListenerDisp_
    {
        private readonly TextBox _output;
        private readonly JobQueue _jobQueue;

        public ListenerI(TextBox outputTxtBx, JobQueue jobQueue)
        {
            this._output = outputTxtBx;
            this._jobQueue = jobQueue;
        }

        public override void NotifyPost_async(AMD_Listener_NotifyPost cb, Post newPost, Current current)
        {
            lock (this)
            {
                this._jobQueue.Add(cb, newPost, this._output);
            }
        }
    }
}