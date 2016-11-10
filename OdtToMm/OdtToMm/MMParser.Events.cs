using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdtToMm
{
    //EVENTS
    partial class MMParser
    {
        //EVENT DELEGATES
        public event EventHandler<NodeParseStepEventArgs> OnNodeParseStep = delegate { };

        public event EventHandler OnMMParseStart = delegate { };


        //EVENT CALLERS
        internal void NodeParseStep(int CurrentCount, int NodeCount) => OnEvent(OnNodeParseStep, new NodeParseStepEventArgs(CurrentCount, NodeCount));

        internal void MMParseStart() => OnEvent(OnMMParseStart);

        //MAIN CALLER
        private void OnEvent(EventHandler handler)
        {
            handler(this, EventArgs.Empty);
        }

        private void OnEvent<T>(EventHandler<T> handler, T eventArgs)
        {
            handler(this, eventArgs);
        }
    }


    public class NodeParseStepEventArgs : EventArgs
    {
        public int CurrentCount { get; private set; }
        public int NodeCount { get; private set; }

        public NodeParseStepEventArgs(int CurrentCount, int NodeCount)
        {
            this.CurrentCount = CurrentCount;
            this.NodeCount = NodeCount;
        }
    }
}
