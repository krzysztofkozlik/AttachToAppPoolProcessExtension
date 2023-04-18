using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AttachToAppPoolProcessExtension.Options
{
    internal partial class OptionsProvider
    {
        // Register the options with these attributes on your package class:
        // [ProvideOptionPage(typeof(OptionsProvider.GeneralOptions), "MyExtension", "General", 0, 0, true)]
        // [ProvideProfile(typeof(OptionsProvider.GeneralOptions), "MyExtension", "General", 0, 0, true)]
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> 
        {
            public override void LoadSettingsFromStorage()
            {
                base.LoadSettingsFromStorage();
            }

            public override void ResetSettings()
            {
                base.ResetSettings();
            }

            public override void SaveSettingsToStorage()
            {
                base.SaveSettingsToStorage();
            }
        }
    }

    public class General : BaseOptionModel<General>
    {
        [Category("My category")]
        [DisplayName("My Option")]
        [Description("An informative description.")]
        [DefaultValue(true)]
        public bool MyOption { get; set; } = true;

        [Category("My category")]
        [DisplayName("My Option")]
        [Description("An informative description.")]
        [DefaultValue("")]
        public string MyTextOption { get; set; } = "";

        [Category("My category")]
        [DisplayName("My Option")]
        [Description("An informative description.")]
        public AppPoolProcess[] Processes { get; set; }
    }

    public class AppPoolProcess
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public string AppPoolName { get; set; }
    }
}
