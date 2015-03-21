using System;
using System.Drawing;
using ComponentContract;

namespace TransformationCPU
{
    public class Cpu : TransformationContract
    {
        // Inherited from BaseComponent
        public Cpu()
        {
            ComponentName = "Grayscale [CPU]";
            ComponentDescription = "Image transformation CPU";
            ComponentAuthor = "Tomislav Štritof";
        }

        // Inherited from TransformationContract
        public override Bitmap ApplyTransformation(Bitmap image)
        {
            var start = DateTime.Now;
            var exportImage = MakeGrayscale3(image);
            Duration = DateTime.Now.Subtract(start);
            return exportImage;
        }

        // Grayscale image, src: http://bobpowell.net/grayscale.aspx
        private Bitmap MakeGrayscale3(Bitmap source)
        {
            var bm = new Bitmap(source.Width, source.Height);
            for (var y = 0; y < bm.Height; y++)
            {
                for (var x = 0; x < bm.Width; x++)
                {
                    var c = source.GetPixel(x, y);
                    var luma = (int)(c.R * 0.21 + c.G * 0.72 + c.B * 0.07);
                    bm.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
            return bm;
        }
    }
}