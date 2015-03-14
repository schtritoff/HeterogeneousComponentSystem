using System;
using System.Drawing;
using ComponentContract;

namespace TransformationGPU
{
    public class Gpu : TransformationContract
    {
        public Gpu()
        {
            ComponentName = "Grayscale [OpenCL]";
            ComponentDescription = "Image transformation OpenCL";
            ComponentAuthor = "Tomislav Štritof";
        }

        public override Bitmap ApplyTransformation(Bitmap image)
        {
            var start = DateTime.Now;
            
            Test2();

            Metrics = DateTime.Now.Subtract(start);
            return image;
        }


        #region OpenCL.NET


        private void Test2()
        {
            

        }

        #endregion
    }
}
