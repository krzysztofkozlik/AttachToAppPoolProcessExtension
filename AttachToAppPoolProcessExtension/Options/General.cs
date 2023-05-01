using System.ComponentModel;

namespace AttachToAppPoolProcessExtension.Options
{
    public class General : BaseOptionModel<General>
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

    public class AppPoolProcess
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string AppPoolName { get; set; }
    }
}
