using System.Windows;
using System.Windows.Controls;

namespace AttachToAppPoolProcessExtension.Options
{
    /// <summary>
    /// Interaction logic for GeneralOptions.xaml
    /// </summary>
    public partial class GeneralOptions : UserControl
    {
        public GeneralOptions(GeneralOptionsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

            // Dummy call to force the loading of Microsoft.Xaml.Behaviors
            var _ = new Microsoft.Xaml.Behaviors.DefaultTriggerAttribute(typeof(Trigger), typeof(Microsoft.Xaml.Behaviors.TriggerBase), null);
        }
    }
}
