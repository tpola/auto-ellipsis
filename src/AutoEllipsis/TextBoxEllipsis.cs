using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AutoEllipsis
{
    /// <summary>
    /// Functional reconstruction of TextBoxEllipsis from the article:
    /// it displays the full text while focused and returns to ellipsis
    /// mode when focus is lost.
    ///
    /// This file is reconstructed from documented behavior and is not
    /// claimed to be the exact source from the lost ZIP.
    /// </summary>
    public class TextBoxEllipsis : TextBox
    {
        private string fullText = "";
        private bool internalTextChange;
        private EllipsisFormat ellipsisFormat = EllipsisFormat.Middle;
        private ToolTip toolTip;

        public TextBoxEllipsis()
        {
            toolTip = new ToolTip();
        }

        [DefaultValue(EllipsisFormat.Middle)]
        public EllipsisFormat EllipsisFormat
        {
            get { return ellipsisFormat; }
            set
            {
                ellipsisFormat = value;
                if (!Focused)
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

                if (!Focused)
                    RefreshEllipsis();
            }

            base.OnTextChanged(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            ShowFullText();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!internalTextChange)
                fullText = base.Text;

            RefreshEllipsis();
            UpdateToolTip();
            base.OnLeave(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!Focused)
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

        private void ShowFullText()
        {
            if (base.Text == fullText)
                return;

            internalTextChange = true;
            try
            {
                base.Text = fullText;
                SelectionStart = base.Text.Length;
            }
            finally
            {
                internalTextChange = false;
            }
        }

        private void UpdateToolTip()
        {
            if (toolTip != null)
                toolTip.SetToolTip(this, fullText);
        }

        private void RefreshEllipsis()
        {
            if (!IsHandleCreated || internalTextChange || Focused)
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
