using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Cloo;
using ComponentContract;

namespace TransformationGPU
{
    public class Gpu : TransformationContract
    {
        private readonly OpenClDeviceForm _openClDeviceForm;

        public Gpu()
        {
            ComponentName = "Grayscale [OpenCL]";
            ComponentDescription = "Image transformation OpenCL";
            ComponentAuthor = "Tomislav Štritof";

            _openClDeviceForm = new OpenClDeviceForm();
        }

        public override Bitmap ApplyTransformation(Bitmap image)
        {
            _openClDeviceForm.ShowForm();

            var start = DateTime.Now;

            var newImg = image; //return unchanged if transformation fails
            try
            {
                newImg = TransformImageOpenCl(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            Duration = DateTime.Now.Subtract(start);
            return newImg;
        }


        #region OpenCL

        public static string AssemblyDirectory
        {
            get
            {
                //src: http://stackoverflow.com/a/283917/1155121
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        //inspired by: http://sourceforge.net/p/cloo/discussion/1048266/thread/cb07b92b/#97c7
        public Bitmap TransformImageOpenCl(Bitmap sourceImage)
        {
            //set user chosen device and platform
            var platform = _openClDeviceForm.PlatformComboBox.SelectedValue as ComputePlatform;
            var device = _openClDeviceForm.DeviceComboBox.SelectedValue as ComputeDevice;

            //init context and command queue
            var cpl = new ComputeContextPropertyList(platform);
            var context = new ComputeContext(ComputeDeviceTypes.Default, cpl, null, IntPtr.Zero);
            var commands = new ComputeCommandQueue(context, device, ComputeCommandQueueFlags.None);

            //prepare vars for image buffers
            var imgWidth = sourceImage.Width;
            var imgHeight = sourceImage.Height;
            const int channels = 4; // pixel format (depth), ARGB => 4 bytes per pixel
            var bufferSize = imgWidth * imgHeight * channels;

            //init buffers for bitmap exchange between host and OpenCL device
            var clBufferIn = new ComputeBuffer<byte>(context, ComputeMemoryFlags.ReadOnly, bufferSize);
            var clBufferOut = new ComputeBuffer<byte>(context, ComputeMemoryFlags.WriteOnly, bufferSize);

            //write source image to buffer available to OpenCL device
            var importBitmapData = sourceImage.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            commands.Write(clBufferIn, true, 0, clBufferIn.Size, importBitmapData.Scan0, null);
            sourceImage.UnlockBits(importBitmapData);

            //set user chosen kernel
            //var clKernel = File.ReadAllText(AssemblyDirectory + @"\" + "TransformGPU.kernel.cl");
            var clKernel =  File.ReadAllText(_openClDeviceForm.KernelsComboBox.SelectedValue as string);
            
            //compile OpenCL kernel program
            var program = new ComputeProgram(context, clKernel);
            try
            {
                //program.Build(null, "-cl-std=CL1.2", null, IntPtr.Zero); //compiler directive: minimum OpenCL version
                program.Build(null, null, null, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                //catch and display compilation errors
                MessageBox.Show(program.GetBuildLog(device) + "\n" + program.GetBuildStatus(device));
                //catch and display OpenCL program errors
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                //return unchanged bitmap image, exit from method
                return sourceImage;
            }

            //instantiate kernel program
            var kernel = program.CreateKernel("img_trans");
            //set args for kernel program
            kernel.SetMemoryArgument(0, clBufferIn);
            kernel.SetMemoryArgument(1, clBufferOut);
            //fire kernel program
            commands.Execute(kernel, null, new long[] { imgWidth * imgHeight }, null, null);
            
            //read modified image from buffer where OpenCL stored exported image
            var exportImage = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);
            var exportBitmapData = exportImage.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            commands.Read(clBufferOut, true, 0, clBufferOut.Size, exportBitmapData.Scan0, null);
            exportImage.UnlockBits(exportBitmapData);

            //return modified bitmap image
            return exportImage;
        }

        #endregion
    }
}
