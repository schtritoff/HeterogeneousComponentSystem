using System;
using System.ComponentModel.Composition;
using System.Drawing;

namespace ComponentContract
{
    [InheritedExport]
    public abstract class TransformationContract : BaseComponent
    {
        /// <summary>
        /// Transforms image.
        /// </summary>
        /// <param name="image">Image for processing</param>
        /// <returns>Returns transformed image in Bitmap format</returns>
        public abstract Bitmap ApplyTransformation(Bitmap image);

        /// <summary>
        /// Transformation duration.
        /// </summary>
        public TimeSpan Duration { get; protected set; }
    }
}
