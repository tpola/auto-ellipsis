using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoEllipsis
{
    /// <summary>
    /// Functional reconstruction of the Label demo control described in
    /// the original CodeProject article. This file is not claimed to be
    /// the exact source from the lost ZIP.
    /// </summary>
    public class LabelEllipsis : Label
    {
        private string fullText = "";
        private bool internalTextChange;
        private EllipsisFormat ellipsisFormat = EllipsisFormat.End;
        private ToolTip toolTip;

        public LabelEllipsis()
        {
            AutoSize = false;
            toolTip = new ToolTip();
        }

        [DefaultValue(EllipsisFormat.End)]
        public EllipsisFormat EllipsisFormat
        {
            get { return ellipsisFormat; }
            set
            {
                ellipsisFormat = value;
                RefreshEllipsis();
            }
        }

        [Browsable(false)]
        public string FullText
        {
            get { return fullText; }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!internalTextChange)
            {
                fullText = base.Text;
                UpdateToolTip();
                RefreshEllipsis();
            }

            base.OnTextChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshEllipsis();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && toolTip != null)
            {
                toolTip.Dispose();
                toolTip = null;
            }

            base.Dispose(disposing);
        }

        private void UpdateToolTip()
        {
            if (toolTip != null)
                toolTip.SetToolTip(this, fullText);
        }

        private void RefreshEllipsis()
        {
            if (!IsHandleCreated || internalTextChange)
                return;

            string display = fullText;

            if (!string.IsNullOrEmpty(fullText))
                display = Ellipsis.Compact(fullText, this, ellipsisFormat);

            if (base.Text != display)
            {
                internalTextChange = true;
                try { base.Text = display; }
                finally { internalTextChange = false; }
            }
        }
    }
}
