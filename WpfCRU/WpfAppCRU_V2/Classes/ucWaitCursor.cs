using System;
using System.Windows.Input;

namespace WpfAppCRU_V2.Classes
{
    public class ucWaitCursor : IDisposable
    {
        private Cursor _previousCursor;

        public ucWaitCursor()
        {
            _previousCursor = Mouse.OverrideCursor;

            Mouse.OverrideCursor = Cursors.Wait;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Mouse.OverrideCursor = _previousCursor;
        }

        #endregion
    }

}
