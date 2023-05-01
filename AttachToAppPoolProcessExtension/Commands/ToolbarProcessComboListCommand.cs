using System.Linq;
using System.Runtime.InteropServices;
using AttachToAppPoolProcessExtension.Options;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.ToolbarProcessComboListId)]
    internal sealed class ToolbarProcessComboListCommand : BaseCommand<ToolbarProcessComboListCommand>
    {
        protected override void Execute(object sender, EventArgs e)
        {
            if (e is OleMenuCmdEventArgs eventArgs)
            {
                IntPtr outValue = eventArgs.OutValue;

                if (outValue != IntPtr.Zero)
                {
                    // when out value is non-NULL, the IDE is requesting the current value for the combo
                    var processesNames = GetAvailableProcessesNames();

                    Marshal.GetNativeVariantForObject(processesNames, outValue);
                }
            }
        }

        private string[] GetAvailableProcessesNames()
        {
            return General.Instance.Processes
                .Where(p => p.IsEnabled)
                .Select(p => p.Name)
                .ToArray();
        }
    }
}
