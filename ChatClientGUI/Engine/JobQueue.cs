using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Chat.Engine
{
    public class JobQueue
    {
        private readonly List<ListenerJob> _jobs = new List<ListenerJob>();
        private bool _done;
        private Thread _thread;

        public void Join()
        {
            this._thread.Join();
        }

        public void Start()
        {
            this._thread = new Thread(this.Run);
            this._thread.Start();
        }

        private void Run()
        {
            lock(this)
            {
                while(!_done)
                {
                    if(this._jobs.Count == 0)
                    {
                        Monitor.Wait(this);
                    }

                    if (this._jobs.Count == 0) 
                        continue;
                    
                    ListenerJob entry = this._jobs[0];

                    Monitor.Wait(this, 100);

                    if (this._done) 
                        continue;

                    this._jobs.RemoveAt(0);
                    entry.Execute();
                }

                foreach (ListenerJob job in this._jobs)
                    job.AmdListenerNotifyPost.ice_exception(new IChatException("Request cancelled"));
            }
        }

        public void Add(AMD_Listener_NotifyPost amdListenerNotifyPost, Post post, TextBox output)
        {
            lock(this)
            {
                if(!this._done)
                {
                    if (this._jobs.Count == 0)
                        Monitor.Pulse(this);
                    this._jobs.Add(new ListenerJob(amdListenerNotifyPost, post, output));
                }
                else
                {
                    amdListenerNotifyPost.ice_exception(new IChatException("Queue is destroyed"));
                }
            }
        }

        public void Destroy()
        {
            lock(this)
            {
                this._done = true;
                Monitor.Pulse(this);
            }
        }

        private class ListenerJob
        {
            private readonly AMD_Listener_NotifyPost _amdListenerNotifyPost;
            private readonly Post _post;
            private readonly TextBox _output;

            public ListenerJob(AMD_Listener_NotifyPost amdListenerNotifyPost, Post post, TextBox output)
            {
                this._amdListenerNotifyPost = amdListenerNotifyPost;
                this._post = post;
                this._output = output;
            }

            public AMD_Listener_NotifyPost AmdListenerNotifyPost
            {
                get { return this._amdListenerNotifyPost; }
            }

            public void Execute()
            {
                lock (this._output)
                {
                    try
                    {
                        if (this._post.time == 0)
                        {
                            this._output.Text += string.Format("=========={0}=========={1}", this._post.Author, Environment.NewLine);
                        }
                        else
                        {
                            DateTime dateTime = new DateTime(this._post.time);
                            this._output.Text += string.Format("=========={0} ({1})=========={2}", this._post.Author, dateTime.ToLongTimeString(), Environment.NewLine);
                        }

                        this._output.Text += string.Format("{0}{1}", this._post.Message, Environment.NewLine);
                        this._output.SelectionStart = this._output.Text.Length;
                        this._output.ScrollToCaret();
                    }
                    catch (Exception e)
                    {
                        this._amdListenerNotifyPost.ice_exception(e);
                        return;
                    }

                    this._amdListenerNotifyPost.ice_response();
                }
            }
        }
    }
}