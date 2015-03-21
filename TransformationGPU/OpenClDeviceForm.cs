using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Cloo;

namespace TransformationGPU
{
    public partial class OpenClDeviceForm : Form
    {
        private Dictionary<ComputeDevice, string> _deviceList;
        private Dictionary<string, string> _kernelList; 

        public OpenClDeviceForm()
        {
            InitializeComponent();

            // Setup platforms
            var platformList = ComputePlatform.Platforms.ToDictionary(
                platform => platform,
                platform => platform.Name + ", " + platform.Version);
            PlatformComboBox.DataSource = new BindingSource(platformList,null);
            PlatformComboBox.DisplayMember = "Value";
            PlatformComboBox.ValueMember = "Key";

            // Setup devices
            _deviceList = ((ComputePlatform) PlatformComboBox.SelectedValue).Devices.ToDictionary(
                device => device,
                device => device.Name + ", " + device.OpenCLCVersionString);

            DeviceComboBox.DataSource = new BindingSource(_deviceList, null);
            DeviceComboBox.DisplayMember = "Value";
            DeviceComboBox.ValueMember = "Key";
            PlatformComboBox.SelectedIndexChanged += PlatformComboBox_SelectedIndexChanged;

            // Setup kernels
            FillKernelsDictionary();
            KernelsComboBox.DataSource = new BindingSource(_kernelList, null);
            KernelsComboBox.DisplayMember = "Value";
            KernelsComboBox.ValueMember = "Key";
        }

        // On show, refresh kernel list
        public void ShowForm()
        {
            FillKernelsDictionary();
            ShowDialog();
        }

        // Populate kernel list from assembly directory
        private void FillKernelsDictionary()
        {
            var path = Gpu.AssemblyDirectory + @"\";
            const string pathFilter = "*.kernel.cl";
            _kernelList = Directory.GetFiles(path, pathFilter).ToDictionary(pathK => pathK, Path.GetFileName);
        }

        void PlatformComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            _deviceList = ((ComputePlatform)PlatformComboBox.SelectedValue).Devices.ToDictionary(
                device => device,
                device => device.Name + ", " + device.OpenCLCVersionString);
        }

        //multiple devices - not implemented
        //public ICollection<ComputeDevice> GetSelectedDevices()
        //{
        //    var devices = DevicesListBox.CheckedIndices.OfType<ComputeDevice>().ToList();

        //    return new Collection<ComputeDevice>(devices);
        //}

        private void OkButton_Click(object sender, System.EventArgs e)
        {
            //multiple devices - not implemented
            //if (GetSelectedDevices().Count == 0)
            //{
            //    if (MessageBox.Show("No device selected, continue?","",MessageBoxButtons.YesNo) == DialogResult.Yes) Hide();
            //}
            //else
            //{
            //    Hide();
            //}
            Hide();
        }
    }
}
