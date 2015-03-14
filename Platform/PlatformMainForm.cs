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
        private readonly List<Bitmap> _imagesList = new List<Bitmap>(1);
        private int _selectedImageIndex;
        private CompositionHelper _compositionHelper;
        private OpenFileDialog openFileDialog;
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

            //select that image
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
        /// src: http://stackoverflow.com/a/2038515/1155121
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
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

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
            //initialize
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Image file|*.jpg;*.bmp";
            openFileDialog.AutoUpgradeEnabled = false; //src: https://stackoverflow.com/questions/6751188/openfiledialog-c-slow-on-any-file-better-solution/29048890#29048890
            openFileDialog.DereferenceLinks = false;
        }

        private void LoadComponentsButton_Click(object sender, EventArgs e)
        {
            //assemble components
            if (_compositionHelper == null)
                _compositionHelper = new CompositionHelper();
            _compositionHelper.AssembleComponents();

            //helpers
            var componentsAreFound = false;

            //hash array holding loaded components
            var componentsList = new Dictionary<ComponentContract.TransformationContract, string>();
                
            foreach (var transformation in _compositionHelper.AvailableTransformations)
            {
                componentsAreFound = true;
                System.Diagnostics.Debug.WriteLine(transformation.Value.ComponentName);
                componentsList.Add(transformation.Value, transformation.Value.ComponentName);
            }

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
            TransformationsListBox.DataSource = new BindingSource(componentsList, null);
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

            //show some metrics
            TransformationMetricLabel.Text = (loadedOk ? "Last trasformation took \n"+selectedTransformation.Metrics.TotalMilliseconds + " ms" : String.Empty);
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadImage(new Bitmap(openFileDialog.FileName));
            }
        }

        private void PreviousImageButton_Click(object sender, EventArgs e)
        {
            if (_selectedImageIndex == 0)
                return;
            ShowImage(--_selectedImageIndex);
        }

        private void NextImageButton_Click(object sender, EventArgs e)
        {
            if (_selectedImageIndex+1 >= _imagesList.Count)
                return;
            ShowImage(++_selectedImageIndex);
        }



        #endregion





    }
}
