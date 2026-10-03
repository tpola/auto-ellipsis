using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoEllipsis
{
    /// <summary>
    /// Recreated demonstration form for the archived article.
    /// </summary>
    public class MainForm : Form
    {
        private readonly string samplePath =
            @"C:\Documents and Settings\TPOL\My Documents\Visual Studio 2005\Projects\MyProject1\Program.cs";

        public MainForm()
        {
            Text = "Auto Ellipsis — reconstructed demo";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(760, 390);
            MinimumSize = new Size(500, 350);

            Label intro = new Label();
            intro.AutoSize = true;
            intro.Location = new Point(16, 16);
            intro.Text = "Resize the window to see the ellipsis modes.";

            Label pathTitle = new Label();
            pathTitle.AutoSize = true;
            pathTitle.Location = new Point(16, 55);
            pathTitle.Text = "Path + Middle:";

            LabelEllipsis pathLabel = new LabelEllipsis();
            pathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathLabel.Location = new Point(16, 78);
            pathLabel.Size = new Size(720, 24);
            pathLabel.BorderStyle = BorderStyle.FixedSingle;
            pathLabel.EllipsisFormat = EllipsisFormat.Middle | EllipsisFormat.Path;
            pathLabel.Text = samplePath;

            Label endTitle = new Label();
            endTitle.AutoSize = true;
            endTitle.Location = new Point(16, 120);
            endTitle.Text = "End + Word:";

            LabelEllipsis wordLabel = new LabelEllipsis();
            wordLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            wordLabel.Location = new Point(16, 143);
            wordLabel.Size = new Size(720, 24);
            wordLabel.BorderStyle = BorderStyle.FixedSingle;
            wordLabel.EllipsisFormat = EllipsisFormat.End | EllipsisFormat.Word;
            wordLabel.Text =
                "The quick brown fox jumps over the lazy dog while demonstrating word-boundary ellipsis.";

            Label textBoxTitle = new Label();
            textBoxTitle.AutoSize = true;
            textBoxTitle.Location = new Point(16, 190);
            textBoxTitle.Text =
                "TextBoxEllipsis (full text while focused; ellipsis when focus is lost):";

            TextBoxEllipsis textBox = new TextBoxEllipsis();
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox.Location = new Point(16, 215);
            textBox.Size = new Size(720, 20);
            textBox.EllipsisFormat = EllipsisFormat.Middle | EllipsisFormat.Path;
            textBox.Text = samplePath;

            Label modesTitle = new Label();
            modesTitle.AutoSize = true;
            modesTitle.Location = new Point(16, 270);
            modesTitle.Text = "Alignment examples:";

            LabelEllipsis start = MakeDemoLabel(new Point(16, 295), EllipsisFormat.Start,
                "This is a long text used to demonstrate START ellipsis.");
            LabelEllipsis middle = MakeDemoLabel(new Point(16, 323), EllipsisFormat.Middle,
                "This is a long text used to demonstrate MIDDLE ellipsis.");
            LabelEllipsis end = MakeDemoLabel(new Point(16, 351), EllipsisFormat.End,
                "This is a long text used to demonstrate END ellipsis.");

            Controls.Add(intro);
            Controls.Add(pathTitle);
            Controls.Add(pathLabel);
            Controls.Add(endTitle);
            Controls.Add(wordLabel);
            Controls.Add(textBoxTitle);
            Controls.Add(textBox);
            Controls.Add(modesTitle);
            Controls.Add(start);
            Controls.Add(middle);
            Controls.Add(end);
        }

        private LabelEllipsis MakeDemoLabel(Point location, EllipsisFormat format, string text)
        {
            LabelEllipsis label = new LabelEllipsis();
            label.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label.Location = location;
            label.Size = new Size(720, 22);
            label.BorderStyle = BorderStyle.FixedSingle;
            label.EllipsisFormat = format;
            label.Text = text;
            return label;
        }
    }
}
