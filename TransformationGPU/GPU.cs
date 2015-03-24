using System;
using System.Drawing;
using System.Windows.Forms;
using Cloo;
using ComponentContract;
using OpenClCommon;

namespace TransformationGPU
{
    public class Gpu : TransformationContract
    {
        private readonly OpenClTransformation _transformation = new OpenClTransformation();

        public Gpu()
        {
            // Inherited from BaseComponent
            ComponentName = "Transformation [Graphics]";
            ComponentDescription = "Image transformation";
            ComponentAuthor = "Tomislav Štritof";
        }

        // Inherited from TransformationContract
        public override Bitmap ApplyTransformation(Bitmap image)
        {
            // Set OpenCL platform, device and kernel
            _transformation.ChooseDevices(ComputeDeviceTypes.Gpu);

            var start = DateTime.Now;

            // Return unchanged image if transformation fails
            var newImg = image;

            try
            {
                // Transform image using OpenCL
                newImg = _transformation.TransformImageOpenCl(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            Duration = DateTime.Now.Subtract(start);
            return newImg;
        }
    }
}
