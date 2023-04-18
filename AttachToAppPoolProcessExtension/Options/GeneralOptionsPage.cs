using System.Linq;
using System.Runtime.InteropServices;

namespace AttachToAppPoolProcessExtension.Options
{
    [Guid("2EB56389-1EA2-4FA6-B7F9-EC81671876C7")]
    [ComVisible(true)]
    public class GeneralOptionsPage : UIElementDialogPage
    {
        private GeneralOptions pageControl;
        private readonly GeneralOptionsViewModel viewModel = new GeneralOptionsViewModel();

        protected override System.Windows.UIElement Child
        {
            get
            {
                pageControl ??= new GeneralOptions(viewModel);

                return pageControl;
            }
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            viewModel.Processes.Clear();
            
            foreach (var process in General.Instance.Processes)
            {
                viewModel.Processes.Add(new ProcessViewModel
                {
                    IsEnabled = process.IsEnabled,
                    Name = process.Name,
                    AppPoolName = process.AppPoolName
                });
            }
        }

        public override void ResetSettings()
        {
            base.ResetSettings();

            viewModel.Processes.Clear();
        }

        public override void SaveSettingsToStorage()
        {
            General.Instance.Processes = viewModel.Processes
                .Select(p => new AppPoolProcess
                { 
                    IsEnabled = p.IsEnabled,
                    Name = p.Name,
                    AppPoolName = p.AppPoolName
                })
                .ToArray();

            General.Instance.Save();

            base.SaveSettingsToStorage();
        }
    }
}