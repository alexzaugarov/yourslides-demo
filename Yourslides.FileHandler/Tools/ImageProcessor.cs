using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Yourslides.FileHandler.Tools {
    public class ImageProcessor : IImageProcessor {
        private readonly int[] _heights = { 720 };
        private string OutputDir { get; set; }
        public void Process(string filepath) {
            Console.WriteLine(filepath);
            var filename = Path.GetFileName(filepath);
            using (var img = Image.FromFile(filepath)) {
                foreach (var height in _heights) {
                    ResizeHeight(img, height).SaveAndDispose(Path.Combine(OutputDir, height.ToString(), filename), ImageFormat.Png);
                }
                ResizeWidth(img, 160).SaveAndDispose(Path.Combine(OutputDir, "preview_small", filename), ImageFormat.Png);
                ResizeWidth(img, 320).SaveAndDispose(Path.Combine(OutputDir, "preview_big", filename), ImageFormat.Png);
            }

        }

        public void CreateSubdirectories(string outputdir) {
            OutputDir = outputdir;
            foreach (var width in _heights) {
                var dir = Path.Combine(OutputDir, width.ToString());
                Directory.CreateDirectory(dir);
            }
            Directory.CreateDirectory(Path.Combine(OutputDir, "preview_small"));
            Directory.CreateDirectory(Path.Combine(OutputDir, "preview_big"));
        }
        private Image ResizeHeight(Image srcImage, int height) {
            int newWidth = (int)Math.Ceiling((double)srcImage.Size.Width * height / srcImage.Size.Height);
            return Resize(srcImage, newWidth, height);
        }
        private Image ResizeWidth(Image srcImage, int width) {
            int newHeight = (int)Math.Ceiling((double)width * srcImage.Size.Height / srcImage.Size.Width);
            return Resize(srcImage, width, newHeight);
        }

        private Image Resize(Image srcImage, int width, int height) {
            Bitmap newImage = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(newImage)) {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(srcImage, new Rectangle(0, 0, width, height));
            }
            return newImage;
        }
    }
    static class ImageExtension {
        public static void SaveAndDispose(this Image image, string filename, ImageFormat format) {
            image.Save(filename, format);
            image.Dispose();
        }
    }
}