using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CnetSDK.OCR.Trial;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Attendence
{
    public partial class Form1 : Form
    {
        public static string DirectoryPath;

        private ScreenCapture objScreenCapture;
        private List<string> attended;

        public Form1()
        {
            InitializeComponent();

            menuStrip1.Renderer = new Renderers.WindowsVistaRenderer();

            objScreenCapture = new ScreenCapture();

            DirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            attended = new List<string>();

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(DirectoryPath + "Attendence.xml"))
            {
                XmlLoader xml = new XmlLoader(DirectoryPath + "Attendence.xml", true);

                checkBoxGrey.Checked = xml.Get("Greyscale", "false").ToLower() == "true";
                checkBoxSaveSS.Checked = xml.Get("SaveScreenshot", "true").ToLower() == "true";
            }
        }

        private void SaveSettings()
        {
            if (File.Exists(DirectoryPath + "Attendence.xml"))
            {
                XmlLoader xml = new XmlLoader(DirectoryPath + "Attendence.xml");

                xml.Set("Greyscale", checkBoxGrey.Checked.ToString());
                xml.Set("SaveScreenshot", checkBoxSaveSS.Checked.ToString());

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

                string date = DateTime.Now.ToString("ddd dd_MM");

                if (!Directory.Exists(DirectoryPath + "Screenshots\\" + date))
                {
                    Directory.CreateDirectory(DirectoryPath + "Screenshots\\" + date);
                }

                string time = DateTime.Now.ToString("hh_mm_ss");

                snap.Save(DirectoryPath + "Screenshots\\" + date + "\\" + time + ".png");
            }

            if(checkBoxGrey.Checked)
            {
                snap = GreyScaleFilter(snap);
            }

            pictureBoxSS.Image = snap;

            snap = ResizeImage(snap, snap.Width * 4, snap.Height * 4);

            if (snap != null)
            {
                 OcrEngine engine = new OcrEngine();

                engine.TessDataPath = DirectoryPath + "tessdata";
                engine.TextLanguage = "eng";
                engine.RecognizeArea = new Rectangle(0, 0, snap.Width, snap.Height);

                string ImageText;
                try
                {
                    ImageText = engine.PerformOCR(snap);
                }
                catch
                {
                    MessageBox.Show("Capture failed, please re-try.");
                    return;
                }

                if (ImageText.StartsWith("CnetSDK*"))
                {
                    string[] lines = ImageText.Remove(0, 8).Split('\n');

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

                            int min = 50;
                            string selected = "none";

                            foreach (string family in familys)
                            {

                                int i = LevenshteinDistance.Compute(n, family.ToLower());
                                if (i < min && i + 1 < family.Length && Math.Abs(family.Length - n.Length) <= 2)
                                {
                                    min = i;
                                    selected = family;

                                    if (i <= 1) break;
                                }
                            }

                            if (selected != "none")
                            {
                                familys.Remove(selected);
                                if (!attended.Contains(selected))
                                {
                                    attended.Add(selected);
                                    sb.AppendLine(selected);

                                    if (--max <= 0) break;
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
                }
            }
        }

        private void getCopyPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] familys = File.ReadAllLines(DirectoryPath + "Data\\FamilyNames.txt");

            StringBuilder sb = new StringBuilder();
            foreach (string f in familys)
            {
                sb.AppendLine(attended.Contains(f) ? "TRUE" : "FALSE");
                attended.Remove(f);
            }

            textBoxResult.Text = sb.ToString();
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
        }

        private void buttonOopsClear_Click(object sender, EventArgs e)
        {
            textBoxOops.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}
