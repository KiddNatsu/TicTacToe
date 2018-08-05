using System;

namespace TTT.ViewModel
{
    public class WinLossOrDrawEventArgs : EventArgs
    {
        public string Result { get; set; }

        public WinLossOrDrawEventArgs(string result)
        {
            this.Result = result;
        }
    }
}