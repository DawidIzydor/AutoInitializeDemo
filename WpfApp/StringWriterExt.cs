using System;
using System.ComponentModel;
using System.IO;

namespace WpfApp
{
    // This is only for console output logging 
    // imported from https://stackoverflow.com/questions/3708454/is-there-a-textwriter-child-class-that-fires-event-if-text-is-written/3710257#3710257
    public class StringWriterExt : StringWriter
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public delegate void FlushedEventHandler(object sender, EventArgs args);

        public StringWriterExt()
        {
        }

        public StringWriterExt(bool autoFlush)
        {
            AutoFlush = autoFlush;
        }

        public virtual bool AutoFlush { get; set; }
        public event FlushedEventHandler Flushed;

        protected void OnFlush()
        {
            var eh = Flushed;
            eh?.Invoke(this, EventArgs.Empty);
        }

        public override void Flush()
        {
            base.Flush();
            OnFlush();
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (AutoFlush)
            {
                Flush();
            }
        }

        public override void Write(string value)
        {
            base.Write(value);
            if (AutoFlush)
            {
                Flush();
            }
        }

        public override void Write(char[] buffer, int index, int count)
        {
            base.Write(buffer, index, count);
            if (AutoFlush)
            {
                Flush();
            }
        }
    }
}