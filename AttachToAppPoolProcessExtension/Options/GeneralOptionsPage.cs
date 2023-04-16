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
        private string optionValue = "alpha";
        private GeneralOptions pageControl;
        private GeneralOptionsModel model = new GeneralOptionsModel();

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        protected override System.Windows.UIElement Child
        {
            get
            {
                pageControl = new GeneralOptions
                {
                    generalOptionsPage = this
                };

                //model.Processes = General.Instance.MyTextOption;

                pageControl.Initialize(model);

                return pageControl;
            }
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();


            model.Processes = General.Instance.MyTextOption;

            pageControl?.LoadOptions();
        }

        public override void ResetSettings()
        {
            base.ResetSettings();
        }

        public override void SaveSettingsToStorage()
        {
            //General.Instance.MyOption = (bool)cbMyOption.IsChecked;
            General.Instance.MyTextOption = model.Processes;
            General.Instance.Save();

            base.SaveSettingsToStorage();
        }
    }

    public class GeneralOptionsModel
    {
        public string Processes { get; set; }
    }
}