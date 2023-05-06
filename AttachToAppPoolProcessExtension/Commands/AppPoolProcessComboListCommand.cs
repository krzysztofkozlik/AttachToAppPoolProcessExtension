using System.Linq;
using System.Runtime.InteropServices;
using AttachToAppPoolProcessExtension.Options;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.AppPoolProcessComboListCommand)]
    internal sealed class AppPoolProcessComboListCommand : BaseCommand<AppPoolProcessComboListCommand>
    {
        protected override void Execute(object sender, EventArgs e)
        {
            if (e is OleMenuCmdEventArgs eventArgs)
            {
                IntPtr outValue = eventArgs.OutValue;

                if (outValue != IntPtr.Zero)
                {
                    var processesNames = GetAvailableProcessesNames();

                    Marshal.GetNativeVariantForObject(processesNames, outValue);
                }
            }
        }

        private string[] GetAvailableProcessesNames()
        {
            return GeneralOptions.Instance.Processes
                ?.Where(p => p.IsEnabled)
                .Select(p => p.Name)
                .ToArray() ?? new string[0];
        }
    }
}
