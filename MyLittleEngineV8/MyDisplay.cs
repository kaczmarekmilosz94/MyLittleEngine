using System.Windows.Forms;

namespace System.Windows.Forms
{
    public class MyDisplay : Panel
    {
        public MyDisplay()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }
}