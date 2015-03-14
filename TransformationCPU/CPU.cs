using System;
using System.Drawing;
using ComponentContract;

namespace TransformationCPU
{
    public class Cpu : TransformationContract
    {
        public Cpu()
        {
            ComponentName = "Grayscale [CPU]";
            ComponentDescription = "Image transformation CPU";
            ComponentAuthor = "Tomislav Štritof";
        }

        public override Bitmap ApplyTransformation(Bitmap image)
        {
            var start = DateTime.Now;

            var exportImage = MakeGrayscale3(image);

            Metrics = DateTime.Now.Subtract(start);

            return exportImage;
        }

        /// <summary>
        /// src: http://bobpowell.net/grayscale.aspx
        /// </summary>
        /// <param name="source"> </param>
        /// <returns></returns>
        private Bitmap MakeGrayscale3(Bitmap source)
        {
            var bm = new Bitmap(source.Width, source.Height);

            for (int y = 0; y < bm.Height; y++)
            {

                for (int x = 0; x < bm.Width; x++)
                {

                    var c = source.GetPixel(x, y);

                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);

                    bm.SetPixel(x, y, Color.FromArgb(luma, luma, luma));

                }

            }

            return bm;
        }
    }
}
