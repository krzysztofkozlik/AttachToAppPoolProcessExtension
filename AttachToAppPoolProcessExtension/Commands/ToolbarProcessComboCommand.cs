using System.Linq;
using System.Runtime.InteropServices;
using AttachToAppPoolProcessExtension.Options;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.ToolbarProcessComboId)]
    internal sealed class ToolbarProcessComboCommand : BaseCommand<ToolbarProcessComboCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs eventArgs)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IntPtr outValue = eventArgs.OutValue;

            var availableValues = GetAvailableProcesses();

            if (outValue != IntPtr.Zero)
            {
                // when outValue is non-NULL, the IDE is requesting the current value for the combo
                var selectedAppPoolName = GetSelectedAppPoolName();

                var selectedProcess = selectedAppPoolName != null 
                    ? GetAvailableProcesses().FirstOrDefault(p => p.AppPoolName == selectedAppPoolName)
                    : null;

                Marshal.GetNativeVariantForObject(selectedProcess?.Name, outValue);
            }
            else if (eventArgs.InValue is string inValue)
            {
                var selectedProcess = availableValues.FirstOrDefault(p => p.Name == inValue);

                if (selectedProcess != null)
                {
                    await SaveSelectedProcessAsync(selectedProcess);
                }
                else
                {
                    await VS.MessageBox.ShowWarningAsync("AttachToAppPoolProcessExtension", $"Cannot find '{inValue}' process");
                }
            }
        }

        private string GetSelectedAppPoolName()
        {
            return General.Instance.SelectedAppPoolName;
        }

        private AppPoolProcess[] GetAvailableProcesses()
        {
            return General.Instance.Processes
                .Where(p => p.IsEnabled)
                .ToArray();
        }

        private async Task SaveSelectedProcessAsync(AppPoolProcess process)
        {
            General.Instance.SelectedAppPoolName = process.AppPoolName;

            await General.Instance.SaveAsync();
        }
    }
}
