using AttachToAppPoolProcessExtension.Options;
using System.Linq;
using System.Runtime.InteropServices;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.ToolbarProcessComboListId)]
    internal sealed class ToolbarProcessComboListCommand : BaseCommand<ToolbarProcessComboListCommand>
    {
        protected override void Execute(object sender, EventArgs e)
        {
            var eventArgs = e as OleMenuCmdEventArgs;
            if (eventArgs != null)
            {
                // Note: works only for dynamic- and dropdown- combos
                IntPtr outValue = eventArgs.OutValue;
                if (outValue != IntPtr.Zero)
                {
                    // when out value is non-NULL, the IDE is requesting the current value for the combo
                    SetCurrentValue(outValue);
                }
            }
        }

        private void SetCurrentValue(IntPtr outValue)
        {
            var processesNames = General.Instance.Processes
                .Where(p => p.IsEnabled)
                .Select(p => p.Name)
                .ToArray();

            Marshal.GetNativeVariantForObject(processesNames, outValue);
        }
    }
}
