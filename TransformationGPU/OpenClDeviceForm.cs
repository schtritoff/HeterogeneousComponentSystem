using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Cloo;

namespace TransformationGPU
{
    public partial class OpenClDeviceForm : Form
    {
        private Dictionary<ComputeDevice, string> deviceList; 
        public OpenClDeviceForm()
        {
            InitializeComponent();

            //setup platforms
            var platformList = ComputePlatform.Platforms.ToDictionary(platform => platform,
                                                                      platform =>
                                                                      platform.Name + ", " + platform.Version);
            PlatformComboBox.DataSource = new BindingSource(platformList,null);
            PlatformComboBox.DisplayMember = "Value";
            PlatformComboBox.ValueMember = "Key";

            //setup devices
            deviceList = ((ComputePlatform) PlatformComboBox.SelectedValue).Devices.ToDictionary(device => device,
                                                                                                 device =>
                                                                                                 device.Name + ", " +
                                                                                                 device.
                                                                                                     OpenCLCVersionString);

            DeviceComboBox.DataSource = new BindingSource(deviceList, null);
            DeviceComboBox.DisplayMember = "Value";
            DeviceComboBox.ValueMember = "Key";
            PlatformComboBox.SelectedIndexChanged += PlatformComboBox_SelectedIndexChanged;
        }

        void PlatformComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            deviceList = ((ComputePlatform)PlatformComboBox.SelectedValue).Devices.ToDictionary(device => device,
                                                                                                 device =>
                                                                                                 device.Name + ", " +
                                                                                                 device.
                                                                                                     OpenCLCVersionString);
        }

        //multiple devices
        //public ICollection<ComputeDevice> GetSelectedDevices()
        //{
        //    var devices = DevicesListBox.CheckedIndices.OfType<ComputeDevice>().ToList();

        //    return new Collection<ComputeDevice>(devices);
        //}

        private void OkButton_Click(object sender, System.EventArgs e)
        {
            //multiple devices
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
