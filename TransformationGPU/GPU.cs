using System;
using System.Drawing;
using System.Drawing.Imaging;
using Cloo;
using ComponentContract;

namespace TransformationGPU
{
    public class Gpu : TransformationContract
    {
        private OpenClDeviceForm _openClDeviceForm;

        public Gpu()
        {
            ComponentName = "Grayscale [OpenCL]";
            ComponentDescription = "Image transformation OpenCL";
            ComponentAuthor = "Tomislav Štritof";

            _openClDeviceForm = new OpenClDeviceForm();
        }

        public override Bitmap ApplyTransformation(Bitmap image)
        {
            _openClDeviceForm.ShowDialog();

            var start = DateTime.Now;

            var newImg = TransformImageGrayscaleOpenCl(image);

            Duration = DateTime.Now.Subtract(start);
            return newImg;
        }


        #region OpenCL

        public Bitmap TransformImageGrayscaleOpenCl(Bitmap sourceImage)
        {
            var imgWidth = sourceImage.Width;
            var imgHeight = sourceImage.Height;
            var channels = 4; // you should beware of the pixel format of the final image. ARGB in our case so there are 4 bytes per pixel.
            var bufferSize = imgWidth * imgHeight * channels;

            //ask user for platform
            var platform = _openClDeviceForm.PlatformComboBox.SelectedValue as ComputePlatform;
            var device = _openClDeviceForm.DeviceComboBox.SelectedValue as ComputeDevice;

            var cpl = new ComputeContextPropertyList(platform);
            var context = new ComputeContext(ComputeDeviceTypes.All, cpl, null, IntPtr.Zero);
            var commands = new ComputeCommandQueue(context, device, ComputeCommandQueueFlags.None);

            var clBufferIn = new ComputeBuffer<byte>(context, ComputeMemoryFlags.ReadOnly, bufferSize);
            var clBufferOut = new ComputeBuffer<byte>(context, ComputeMemoryFlags.WriteOnly, bufferSize);

            //prepare prepare image for opencl
            BitmapData importBitmapData = sourceImage.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            commands.Write(clBufferIn, true, 0, clBufferIn.Size, importBitmapData.Scan0, null);
            sourceImage.UnlockBits(importBitmapData);

            string clKernel = @"
kernel void BitmapKernel(global read_only uchar4* arrayIn, global write_only uchar4* arrayOut)
{
    int gid = get_global_id(0);
// Even though the Bitmap format will be ARGB this array will be parsed as BGRA. So be careful with that. No, I don't know why it works like this. Ask Microsoft :)
uint gray =  arrayIn[gid][0] * 0.07 + arrayIn[gid][1]* 0.72 + arrayIn[gid][2]*0.21;
arrayOut[gid] = (uchar4)(gray,gray,gray, 255); // This is full red/half transparent color.
//int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
}";
            var program = new ComputeProgram(context, clKernel);
            program.Build(null, null, null, IntPtr.Zero);
            var kernel = program.CreateKernel("BitmapKernel");
            kernel.SetMemoryArgument(0, clBufferIn);
            kernel.SetMemoryArgument(1, clBufferOut);
            commands.Execute(kernel, null, new long[] { imgWidth * imgHeight }, null, null);
            
            //read image from opencl
            Bitmap exportImage = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);
            BitmapData exportBitmapData = exportImage.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            commands.Read(clBufferOut, true, 0, clBufferOut.Size, exportBitmapData.Scan0, null);
            exportImage.UnlockBits(exportBitmapData);

            return exportImage;
        }

        #endregion
    }
}
