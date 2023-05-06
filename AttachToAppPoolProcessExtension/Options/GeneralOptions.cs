using System.ComponentModel;

namespace AttachToAppPoolProcessExtension.Options
{
    public class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("General")]
        [DisplayName("Available processes")]
        [Description("An informative description.")]
        public AppPoolProcess[] Processes { get; set; }

        [Category("General")]
        [DisplayName("Selected App Pool")]
        [Description("An informative description.")]
        public string SelectedAppPoolName { get; set;  }
    }
}
