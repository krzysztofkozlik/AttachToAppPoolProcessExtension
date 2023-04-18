using Microsoft.Internal.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace AttachToAppPoolProcessExtension.Options
{
    [Guid("00000000-0000-0000-0000-000000000000")]
    [ComVisible(true)]
    public class GeneralOptionsPage : UIElementDialogPage
    {
        //private string optionValue = "alpha";
        private GeneralOptions pageControl;
        private readonly GeneralOptionsViewModel viewModel = new GeneralOptionsViewModel();

        //public string OptionString
        //{
        //    get { return optionValue; }
        //    set { optionValue = value; }
        //}

        protected override System.Windows.UIElement Child
        {
            get
            {
                pageControl = new GeneralOptions(viewModel);
                //{
                //    generalOptionsPage = this
                //};

                //model.Processes = General.Instance.MyTextOption;

                //pageControl.Initialize(model);

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
            //General.Instance.MyOption = (bool)cbMyOption.IsChecked;
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

    //public class GeneralOptionsModel
    //{
    //    public string Processes { get; set; }
    //}
}