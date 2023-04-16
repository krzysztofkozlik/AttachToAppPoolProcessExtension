using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.Web.Administration;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AttachToAppPoolProcessExtension.Options
{
    public partial class GenearlOptionsControl : UserControl
    {
        public GenearlOptionsControl()
        {
            InitializeComponent();
        }

        internal GeneralOptionsPage optionsPage;

        public void Initialize()
        {
            textBoxName.Text = optionsPage.OptionString;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            optionsPage.OptionString = textBoxName.Text;
        }

        private void listViewProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedItem();

            if (selectedItem != null)
            {
                buttonUpdate.Enabled = true;
                buttonRemove.Enabled = true;

                textBoxName.Text = selectedItem.SubItems[0].Text;
                textBoxAppPool.Text = selectedItem.SubItems[1].Text;
            }
            else
            {
                buttonUpdate.Enabled = false;
                buttonRemove.Enabled = false;

                textBoxName.Text = string.Empty;
                textBoxAppPool.Text = string.Empty;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            var selectedItem = GetSelectedItem();

            selectedItem.SubItems[0].Text = textBoxName.Text;
            selectedItem.SubItems[1].Text = textBoxAppPool.Text;
        }

        private ListViewItem GetSelectedItem()
        {
            return listViewProcesses.SelectedItems.Count > 0
                ? listViewProcesses.SelectedItems[0]
                : null;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewProcesses.SelectedIndices.Count > 0)
            {
                listViewProcesses.Items.RemoveAt(listViewProcesses.SelectedIndices[0]);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var name = $"Name_{Guid.NewGuid()}";
            var appPool = $"AppPool_{Guid.NewGuid()}";

            var item = new ListViewItem(name, appPool)
            {
                Selected = true,
            };

            listViewProcesses.Items.Add(item);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (var serverManager = new ServerManager())
            {
                var appPoolNames = serverManager.WorkerProcesses.Select(p => p.AppPoolName);

                foreach (var name in appPoolNames)
                {
                    var item = new ListViewItem(name, name);

                    listViewProcesses.Items.Add(item);
                }
            }
        }

        private void SaveSettings()
        {
            //var settingsManager = ExtensionManager.ShellSettingsManager;
            //var userSettingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            //var processes = new List<AppPoolProcess>();

            //foreach (ListViewItem item in listViewProcesses.Items)
            //{
            //    processes.Add(new AppPoolProcess
            //    {
            //        Name = item.Text,
            //        AppPoolName = item.SubItems[1].Text
            //    });
            //}

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(processes);

            //userSettingsStore.SetString("AttachToAppPoolProcessExtensionPackage", "Processes", json);
        }
    }

    public class AppPoolProcess
    {
        public string Name { get; set; }
        public string AppPoolName { get; set; }
    }
}
