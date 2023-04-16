using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttachToAppPoolProcessExtension.Options
{
    [Guid("00000000-0000-0000-0000-000000000000")]
    [ComVisible(true)]
    public class GeneralOptionsPage : DialogPage
    {
        private string optionValue = "alpha";

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        protected override IWin32Window Window
        {
            get
            {
                var page = new GenearlOptionsControl();
                page.optionsPage = this;
                page.Initialize();
                return page;
            }
        }
    }
}
