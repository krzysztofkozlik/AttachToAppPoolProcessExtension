using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AttachToAppPoolProcessExtension.Options
{
    /// <summary>
    /// Interaction logic for GeneralOptions.xaml
    /// </summary>
    public partial class GeneralOptions : UserControl
    {
        internal GeneralOptionsPage generalOptionsPage;

        private GeneralOptionsModel model;

        public GeneralOptions()
        {
            InitializeComponent();
        }

        public void Initialize(GeneralOptionsModel model)
        {
            this.model = model;
            textbox.Text = model.Processes;

            cbMyOption.IsChecked = General.Instance.MyOption;
            //generalOptionsPage.
            //General.Instance.Save();
        }

        public void LoadOptions()
        {
            textbox.Text = model.Processes;
        }

        private void cbMyOption_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            General.Instance.MyOption = (bool)cbMyOption.IsChecked;
            //General.Instance.Save();
        }

        private void cbMyOption_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            General.Instance.MyOption = (bool)cbMyOption.IsChecked;
            //General.Instance.Save();
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (model != null)
                model.Processes = textbox.Text;
        }
    }
}
