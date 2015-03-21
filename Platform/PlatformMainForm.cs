using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Platform
{
    
    public partial class PlatformMainForm : Form
    {
        #region Properties
        private readonly List<Bitmap> _imagesList = new List<Bitmap>();
        private int _selectedImageIndex;
        private CompositionHelper _compositionHelper;
        private OpenFileDialog _openFileDialog;
        #endregion

        public PlatformMainForm()
        {
            InitializeComponent();
        }

        #region Methods

        private bool LoadImage(Bitmap bitmapImage, int? atIndex = null)
        {   
            //check if image is already loaded
            if (_imagesList.Count > 0 && _imagesList.FindIndex(img => CompareMemCmp(img, bitmapImage)) != -1)
            {
                MessageBox.Show("Duplicate image.");
                return false;
            }

            //load image into the list
            if (atIndex.HasValue)
                _imagesList.Insert(atIndex.GetValueOrDefault(), bitmapImage);
            else 
                _imagesList.Add(bitmapImage);

            //show new image
            _selectedImageIndex = _imagesList.FindIndex(img => CompareMemCmp(img, bitmapImage));
            ShowImage(_selectedImageIndex);

            return true;
        }

        private void ShowImage(int pos)
        {
            ImageBoxMain.Image = _imagesList[pos];
            ImageNumberLabel.Text = (pos+1) + "/" + _imagesList.Count;
        }

        /// <summary>
        /// Compare two bitmaps, src: http://stackoverflow.com/a/2038515/1155121
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);
        public static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;
            if (b1 == b2) return true;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                var bd1scan0 = bd1.Scan0;
                var bd2scan0 = bd2.Scan0;

                var stride = bd1.Stride;
                var len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        #endregion

        #region Events

        private void PlatformMainForm_Load(object sender, EventArgs e)
        {
            //initialize open file dialog
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            _openFileDialog.Filter = "Image files|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|All files (*.*)|*.*";
            _openFileDialog.AutoUpgradeEnabled = false; //src: https://stackoverflow.com/questions/6751188/openfiledialog-c-slow-on-any-file-better-solution/29048890#29048890
            _openFileDialog.DereferenceLinks = false;
        }

        private void LoadComponentsButton_Click(object sender, EventArgs e)
        {
            //assemble components
            if (_compositionHelper == null)
                _compositionHelper = new CompositionHelper();
            _compositionHelper.AssembleComponents();

            //helpers
            var componentsAreFound = false;

            //hash table holding loaded components
            var componentsList = new Dictionary<ComponentContract.TransformationContract, string>();

            foreach (var transformation in _compositionHelper.AvailableTransformations)
            {
                componentsAreFound = true;
                System.Diagnostics.Debug.WriteLine(transformation.Value.ComponentName);
                componentsList.Add(transformation.Value, transformation.Value.ComponentName);
            }

            //show/hide UI elements
            TransformationsListBox.Visible = componentsAreFound; TransformationsListBox.Update();
            TransformationInfoLabel.Visible = componentsAreFound; TransformationInfoLabel.Update();
            TransformButton.Visible = componentsAreFound; TransformButton.Update();
            TransformationMetricLabel.Text = String.Empty; TransformationMetricLabel.Update();
            if (!componentsAreFound)
            {
                MessageBox.Show("Components not found");
                return;
            }

            //bind components to listbox
            TransformationsListBox.DataSource = new BindingSource(componentsList, null); //GC should clean this up on subsequent load
            TransformationsListBox.DisplayMember = "Value";
            TransformationsListBox.ValueMember = "Key";
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            //get current selected image
            if (_imagesList.Count == 0)
            {
                MessageBox.Show("Please load some image(s) first.");
                return;
            }
            var img = _imagesList[_selectedImageIndex];

            //get current selected transformation
            var selectedTransformation = (ComponentContract.TransformationContract) TransformationsListBox.SelectedValue;

            //transform
            TransformationMetricLabel.Text = "Transformation in progress...";
            TransformationMetricLabel.Update();
            var newImg = selectedTransformation.ApplyTransformation(img);

            //insert after current selected image (and show)
            var loadedOk = LoadImage(newImg,_selectedImageIndex+1);

            //show some metrics if transformation went OK (= image successfuly loaded)
            TransformationMetricLabel.Text = (loadedOk ? "Last trasformation took \n"+selectedTransformation.Duration.TotalMilliseconds + " ms" : String.Empty);
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            //load new image from file system
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(new Bitmap(_openFileDialog.FileName));
            }
        }

        private void PreviousImageButton_Click(object sender, EventArgs e)
        {
            //browse previous image in list
            if (_selectedImageIndex == 0)
                return;
            ShowImage(--_selectedImageIndex);
        }

        private void NextImageButton_Click(object sender, EventArgs e)
        {
            //browse next image in list
            if (_selectedImageIndex+1 >= _imagesList.Count)
                return;
            ShowImage(++_selectedImageIndex);
        }

        #endregion
    }
}
