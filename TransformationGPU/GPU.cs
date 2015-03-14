using System;
using System.Drawing;
using ComponentContract;
using ManOCL;

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

            var newImg = TransformImageGrayscaleOpenCl(image);

            Metrics = DateTime.Now.Subtract(start);
            return image;
        }


        #region OpenCL

        private const string kernel = @"
__kernel void imaging1(__read_only  image2d_t srcImg,
                       __write_only image2d_t dstImg)
{
  const sampler_t smp = CLK_NORMALIZED_COORDS_FALSE | //Natural coordinates
    CLK_ADDRESS_CLAMP_TO_EDGE | //Clamp to zeros
    CLK_FILTER_LINEAR;
  int2 coord = (int2)(get_global_id(0), get_global_id(1));
  uint4 bgra = read_imageui(srcImg, smp, coord); //The byte order is BGRA
  float4 bgrafloat = convert_float4(bgra) / 255.0f; //Convert to normalized [0..1] float
  //Convert RGB to luminance (make the image grayscale).
  float luminance =  sqrt(0.241f * bgrafloat.z * bgrafloat.z + 0.691f * 
                      bgrafloat.y * bgrafloat.y + 0.068f * bgrafloat.x * bgrafloat.x);
  bgra.x = bgra.y = bgra.z = (uint) (luminance * 255.0f);
  bgra.w = 255;
  write_imageui(dstImg, coord, bgra);
}
";

        private static Bitmap TransformImageGrayscaleOpenCl_old(Bitmap sourceImage)
        {
            var exportImage = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);

            //Initializes OpenCL Platforms and Devices and sets everything up
            CLCalc.InitCL(ComputeDeviceTypes.Gpu,0);

            //list devices to debug console
            foreach (var computeDevice in CLCalc.CLDevices)
            {
                System.Diagnostics.Debug.WriteLine(computeDevice.Platform.Name + ", " + computeDevice.Name + ", " + computeDevice.OpenCLCVersionString);
            }

            //kernel code for grayscale transformation, src: http://www.codeproject.com/Articles/502829/GPGPU-image-processing-basics-using-OpenCL-NET
            const string kernel = @"
__kernel void imaging1(__read_only  image2d_t srcImg,
                       __write_only image2d_t dstImg)
{
  const sampler_t smp = CLK_NORMALIZED_COORDS_FALSE | //Natural coordinates
    CLK_ADDRESS_CLAMP_TO_EDGE | //Clamp to zeros
    CLK_FILTER_LINEAR;
  int2 coord = (int2)(get_global_id(0), get_global_id(1));
  uint4 bgra = read_imageui(srcImg, smp, coord); //The byte order is BGRA
  float4 bgrafloat = convert_float4(bgra) / 255.0f; //Convert to normalized [0..1] float
  //Convert RGB to luminance (make the image grayscale).
  float luminance =  sqrt(0.241f * bgrafloat.z * bgrafloat.z + 0.691f * 
                      bgrafloat.y * bgrafloat.y + 0.068f * bgrafloat.x * bgrafloat.x);
  bgra.x = bgra.y = bgra.z = (uint) (luminance * 255.0f);
  bgra.w = 255;
  write_imageui(dstImg, coord, bgra);
}
";
            //compile kernel program
            CLCalc.Program.Compile(kernel);
            var kernelGrayscale = new CLCalc.Program.Kernel("imaging1");

            var imgIn = new CLCalc.Program.Image2D(sourceImage);
            var imgOut = new CLCalc.Program.Image2D(sourceImage);

            imgIn.WriteBitmap(sourceImage);

            kernelGrayscale.Execute(new CLCalc.Program.MemoryObject[] { imgIn, imgOut }, new[] { sourceImage.Width - 6, sourceImage.Height - 6 });

            return imgOut.ReadBitmap();
        }

        private static Bitmap TransformImageGrayscaleOpenCl(Bitmap sourceImage)
        {
            var exportImage = new Bitmap(sourceImage.Width, sourceImage.Height, sourceImage.PixelFormat);

            Kernel 

            return exportImage;
        }

        #endregion
    }
}
