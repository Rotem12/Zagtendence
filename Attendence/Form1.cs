using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Tesseract;

namespace Attendence
{
    public partial class Form1 : Form
    {
        public static string DirectoryPath;

        private ScreenCapture objScreenCapture;
        private Dictionary<string, string> attended;

        private bool fullList = false;

        public Form1()
        {
            InitializeComponent();

            menuStrip1.Renderer = new Renderers.WindowsVistaRenderer();

            objScreenCapture = new ScreenCapture();

            DirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            attended = new Dictionary<string, string>();

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(DirectoryPath + "Attendence.xml"))
            {
                XmlLoader xml = new XmlLoader(DirectoryPath + "Attendence.xml", true);

                checkBoxGrey.Checked = xml.Get("Greyscale", "false").ToLower() == "true";
                checkBoxSaveSS.Checked = xml.Get("SaveScreenshot", "true").ToLower() == "true";
                numTolerance.Value = int.Parse(xml.Get("Tolerance", "8"));
                checkBoxScores.Checked = xml.Get("Scores", "false").ToLower() == "true";
            }
        }

        private void SaveSettings()
        {
            if (File.Exists(DirectoryPath + "Attendence.xml"))
            {
                XmlLoader xml = new XmlLoader(DirectoryPath + "Attendence.xml");

                xml.Set("Greyscale", checkBoxGrey.Checked.ToString());
                xml.Set("SaveScreenshot", checkBoxSaveSS.Checked.ToString());
                xml.Set("Tolerance", numTolerance.Value.ToString());
                xml.Set("Scores", checkBoxScores.Checked.ToString());

                xml.Save();
            }
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var snap = objScreenCapture.SetCanvasAndGetSnap();

            if(checkBoxSaveSS.Checked)
            {
                if(!Directory.Exists(DirectoryPath + "Screenshots"))
                {
                    Directory.CreateDirectory(DirectoryPath + "Screenshots");
                }

                string date = DateTime.Now.ToString("dd_MM ddd");

                if (!Directory.Exists(DirectoryPath + "Screenshots\\" + date))
                {
                    Directory.CreateDirectory(DirectoryPath + "Screenshots\\" + date);
                }

                string time = DateTime.Now.ToString("hh_mm_ss");

                snap.Save(DirectoryPath + "Screenshots\\" + date + "\\" + time + ".png");
            }

            snap = ResizeImage(snap, snap.Width * 4, snap.Height * 4);

            if (checkBoxGrey.Checked)
            {
                snap = GreyScaleFilter(snap);
            }

            snap = AdjustContrast(snap, 100);

            pictureBoxSS.Image = snap;

            if (snap != null)
            {
                TesseractEngine engine = new TesseractEngine(DirectoryPath + "tessdata", "eng", EngineMode.Default);
                engine.DefaultPageSegMode = PageSegMode.SingleBlock;

                string ImageText;
                try
                {
                    Page page = engine.Process(snap);
                    ImageText = page.GetText();
                }
                catch
                {
                    MessageBox.Show("Capture failed, please re-try.");
                    return;
                }

                if (ImageText != "" && ImageText.Length > 0)
                {
                    string[] lines = ImageText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> familys = File.ReadAllLines(DirectoryPath + "Data\\FamilyNames.txt").ToList();

                    StringBuilder sb = new StringBuilder();

                    int max = 21;

                    foreach (string line in lines)
                    {
                        try
                        {
                            if (line.Length < 3) continue;

                            string fixedLine = line.Replace('[', '(').Replace('{', '(');

                            string[] split = fixedLine.Split('(');

                            if (split.Length == 1)
                            {
                                split = fixedLine.Split(' ');
                            }
                            else
                            {
                                split[0] = split[0].Remove(split[0].Length - 1, 1);
                            }

                            string n;
                            if (split.Length == 1)
                            {
                                n = split[0].Substring(0, split[0].Length / 2).ToLower();
                            }
                            else
                            {
                                int index = split.Length > 2 ? split.Length / 2 : 0;
                                n = split[index].ToLower();
                            }

                            if (n[0] == '2') n = "z" + n.Remove(0,1);
                            n = n.Replace("|", "l");

                            int min = 50;
                            string selected = "none";

                            foreach (string family in familys)
                            {

                                int i = LevenshteinDistance.Compute(n, family.ToLower());
                                int diff = Math.Abs(family.Length - n.Length);
                                if (i < min && i + 2 < family.Length && diff <= 2 && i < numTolerance.Value)
                                {
                                    min = i;
                                    selected = family;

                                    if (i <= 1) break;
                                }
                            }

                            if (selected != "none")
                            {
                                familys.Remove(selected);
                                if (!attended.ContainsKey(selected))
                                {
                                    if (checkBoxScores.Checked)
                                    {
                                        string[] scores = fixedLine.Split(')');
                                        if (scores.Length > 1)
                                        {
                                            attended.Add(selected, scores[1].Trim().ToLower().Replace("o", "0").Replace("s", "5").Replace(" ", "\t"));
                                        }
                                        else
                                        {
                                            attended.Add(selected, "0\t0\t0\t0\t0\t0\t0\t0");
                                        }
                                    }
                                    else
                                    {
                                        attended.Add(selected, "0\t0\t0\t0\t0\t0\t0\t0");
                                    }
                                    sb.AppendLine(selected);

                                    if (--max <= 0) break;
                                }
                                else
                                {
                                    textBoxOops.AppendText("Already in: " + line + Environment.NewLine);
                                }
                            }
                            else
                            {
                                textBoxOops.AppendText(line + Environment.NewLine);
                            }
                        }
                        catch { continue; }
                    }

                    labelCount.Text = attended.Count.ToString();
                    textBoxResult.Text = sb.ToString();
                    UpdateFull();
                }
            }
        }

        private void getCopyPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] familys = File.ReadAllLines(DirectoryPath + "Data\\FamilyNames.txt");

            StringBuilder sb = new StringBuilder();
            foreach (string f in familys)
            {
                sb.AppendLine(attended.ContainsKey(f) ? "TRUE" : "FALSE");
            }

            textBoxResult.Text = sb.ToString();

            buttonToggleList.Text = "Recent";
            textBoxResult.BringToFront();
            fullList = false;
        }

        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
        }

        public Bitmap GreyScaleFilter(Bitmap image)
        {
            Bitmap greyScale = new Bitmap(image.Width, image.Height);

            for (Int32 y = 0; y < greyScale.Height; y++)
                for (Int32 x = 0; x < greyScale.Width; x++)
                {
                    Color c = image.GetPixel(x, y);

                    int average = (c.R + c.G + c.B) / 3;
                    if(average > 40)
                    {
                        greyScale.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        greyScale.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }

                    //     Int32 gs = (Int32)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);

                    //     greyScale.SetPixel(x, y, Color.FromArgb(gs, gs, gs));
                }
            return greyScale;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void buttonClipboard_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text != "")
            {
                Clipboard.SetText(textBoxResult.Text);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            attended.Clear();
            labelCount.Text = "0";
            textBoxResult.Clear();
            textBoxFull.Clear();
        }

        private void buttonOopsClear_Click(object sender, EventArgs e)
        {
            textBoxOops.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void buttonToggleList_Click(object sender, EventArgs e)
        {
            if (fullList)
            {
                buttonToggleList.Text = "Recent";
                textBoxResult.BringToFront();
            }
            else
            {
                buttonToggleList.Text = "Full";
                textBoxFull.BringToFront();
            }

            fullList = !fullList;
        }

        private void UpdateFull()
        {
            textBoxFull.Clear();
            textBoxFull.Text = string.Join(Environment.NewLine, attended.Keys);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                TextBox txt = sender as TextBox;
                int charIndex = txt.GetFirstCharIndexOfCurrentLine();
                int lineNo = txt.GetLineFromCharIndex(charIndex);

                string name = txt.Lines[lineNo];
                if (attended.ContainsKey(name))
                {
                    attended.Remove(name);
                    txt.Text = txt.Text.Substring(0, charIndex) + txt.Text.Substring(charIndex + name.Length);
                    txt.Text = txt.Text.Replace("\r\n\r\n", "\r\n");

                    labelCount.Text = attended.Count.ToString();

                    if(txt != textBoxFull)
                    {
                        int i = textBoxFull.Text.IndexOf(name);
                        if(i != -1)
                        {
                            textBoxFull.Text = textBoxFull.Text.Substring(0, i) + textBoxFull.Text.Substring(i + name.Length);
                            textBoxFull.Text = textBoxFull.Text.Replace("\r\n\r\n", "\r\n");
                        }
                    }
                    else
                    {
                        int i = textBoxResult.Text.IndexOf(name);
                        if (i != -1)
                        {
                            textBoxResult.Text = textBoxResult.Text.Substring(0, i) + textBoxResult.Text.Substring(i + name.Length);
                            textBoxResult.Text = textBoxResult.Text.Replace("\r\n\r\n", "\r\n");
                        }
                    }
                }
            }
        }

        private void copy2ColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);

                string[] familys = clipboardText.Replace("\r", "").Split('\n');

                foreach (string f in familys)
                {
                    attended.Add(f, "0\t0\t0\t0\t0\t0\t0\t0");
                }

                labelCount.Text = attended.Count.ToString();
                textBoxResult.Text = clipboardText;
                UpdateFull();
            }
        }

        private void scoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] familys = File.ReadAllLines(DirectoryPath + "Data\\FamilyNames.txt");

            StringBuilder sb = new StringBuilder();
            foreach (string f in familys)
            {
                if(attended.ContainsKey(f))
                {
                    sb.AppendLine(attended[f]);
                }
                else
                {
                    sb.AppendLine("0\t0\t0\t0\t0\t0\t0\t0");
                }
            }

            textBoxResult.Text = sb.ToString();

            buttonToggleList.Text = "Recent";
            textBoxResult.BringToFront();
            fullList = false;
        }
    }
}
