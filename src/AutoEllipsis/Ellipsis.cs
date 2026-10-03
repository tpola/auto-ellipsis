using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoEllipsis
{
    /// <summary>
    /// Specifies ellipsis format and alignment.
    /// </summary>
    [Flags]
    public enum EllipsisFormat
    {
        None = 0,
        End = 1,
        Start = 2,
        Middle = 3,
        Path = 4,
        Word = 8
    }

    /// <summary>
    /// Flexible auto-ellipsis helper.
    ///
    /// Reconstructed from source fragments printed in Thomas Polaert's
    /// 2009 CodeProject article "Auto Ellipsis".
    /// </summary>
    public static class Ellipsis
    {
        public static readonly string EllipsisChars = "...";

        private static Regex prevWord = new Regex(@"\W*\w*$");
        private static Regex nextWord = new Regex(@"\w*\W*");

        public static string Compact(string text, Control ctrl, EllipsisFormat options)
        {
            using (Graphics dc = ctrl.CreateGraphics())
            {
                Size s = TextRenderer.MeasureText(dc, text, ctrl.Font);

                if (s.Width <= ctrl.Width)
                    return text;

                string pre = "";
                string mid = text;
                string post = "";

                bool isPath = (EllipsisFormat.Path & options) != 0;

                if (isPath)
                {
                    pre = Path.GetPathRoot(text);
                    mid = Path.GetDirectoryName(text).Substring(pre.Length);
                    post = Path.GetFileName(text);
                }

                int len = 0;
                int seg = mid.Length;
                string fit = "";

                while (seg > 1)
                {
                    seg -= seg / 2;

                    int left = len + seg;
                    int right = mid.Length;

                    if (left > right)
                        continue;

                    if ((EllipsisFormat.Middle & options) == EllipsisFormat.Middle)
                    {
                        right -= left / 2;
                        left -= left / 2;
                    }
                    else if ((EllipsisFormat.Start & options) != 0)
                    {
                        right -= left;
                        left = 0;
                    }

                    if ((EllipsisFormat.Word & options) != 0)
                    {
                        if ((EllipsisFormat.End & options) != 0)
                            left -= prevWord.Match(mid, 0, left).Length;

                        if ((EllipsisFormat.Start & options) != 0)
                            right += nextWord.Match(mid, right).Length;
                    }

                    string tst = mid.Substring(0, left) +
                        EllipsisChars + mid.Substring(right);

                    if (isPath)
                        tst = Path.Combine(Path.Combine(pre, tst), post);

                    s = TextRenderer.MeasureText(dc, tst, ctrl.Font);

                    if (s.Width <= ctrl.Width)
                    {
                        len += seg;
                        fit = tst;
                    }
                }

                if (len == 0)
                {
                    if (!isPath)
                        return EllipsisChars;

                    if (pre.Length == 0 && mid.Length == 0)
                        return post;

                    fit = Path.Combine(Path.Combine(pre, EllipsisChars), post);
                    s = TextRenderer.MeasureText(dc, fit, ctrl.Font);

                    if (s.Width > ctrl.Width)
                        fit = Path.Combine(EllipsisChars, post);
                }

                return fit;
            }
        }
    }
}
