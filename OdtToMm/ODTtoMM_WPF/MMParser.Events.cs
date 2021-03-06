﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODTtoMM_WPF
{
    //EVENTS
    partial class MMParser
    {
        //EVENT DELEGATES
        public event EventHandler<NodeParseStepEventArgs> OnNodeParseStep = delegate { };

        public event EventHandler OnMMParseStarted = delegate { };

        public event EventHandler<MMParseEndedEventArgs> OnMMParseEnded = delegate { };


        //EVENT CALLERS
        internal void NodeParseStep(int CurrentCount, int NodeCount) => OnEvent(OnNodeParseStep, new NodeParseStepEventArgs(CurrentCount, NodeCount));

        internal void MMParseStart() => OnEvent(OnMMParseStarted);

        internal void MMParseEnded(bool success) => OnEvent(OnMMParseEnded, new MMParseEndedEventArgs(success));



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

    /// <summary>
    /// NodeParseStepEventArgs
    /// </summary>
    public class NodeParseStepEventArgs : EventArgs
    {
        public int CurrentCount { get; private set; }
        public int NodeCount { get; private set; }
        public byte percentage { get; private set; }

        public NodeParseStepEventArgs(int CurrentCount, int NodeCount)
        {
            this.CurrentCount = CurrentCount;
            this.NodeCount = NodeCount;
            percentage = (byte)(CurrentCount / NodeCount * 100);
        }
    }
    /// <summary>
    /// MMParseEndedEventArgs
    /// </summary>
    public class MMParseEndedEventArgs : EventArgs
    {
        public bool successful { get; private set; }

        public MMParseEndedEventArgs(bool successful)
        {
            this.successful = successful;
        }
    }
}
