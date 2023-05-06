using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using AttachToAppPoolProcessExtension.Options;
using Microsoft;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.AppPoolProcessComboCommand)]
    internal sealed class AppPoolProcessComboCommand : BaseCommand<AppPoolProcessComboCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs eventArgs)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            IntPtr outValue = eventArgs.OutValue;

            if (outValue != IntPtr.Zero)
            {
                await HandleGetComboValueAsync(outValue);
            }
            else if (eventArgs.InValue is string inValue)
            {
                await HandleSetComboValueAsync(inValue);
            }
        }

        private async Task HandleSetComboValueAsync(string inValue)
        {
            var selectedProcess = GetAvailableProcesses().FirstOrDefault(p => p.Name == inValue);

            if (selectedProcess != null)
            {
                await SaveSelectedProcessAsync(selectedProcess);
            }
            else
            {
                await VS.MessageBox.ShowWarningAsync(Resources.ExtensionName, $"Cannot find '{inValue}' process");
            }
        }

        private async Task HandleGetComboValueAsync(IntPtr outValue)
        {
            var selectedAppPoolName = GetSelectedAppPoolName();

            var selectedProcess = selectedAppPoolName != null
                ? GetAvailableProcesses().FirstOrDefault(p => p.AppPoolName == selectedAppPoolName)
                : null;

            await ToggleAttachToProcessCommandAsync(selectedProcess != null);

            Marshal.GetNativeVariantForObject(selectedProcess?.Name, outValue);
        }

        private async Task ToggleAttachToProcessCommandAsync(bool enabled)
        {
            var oleMenuCommandService = await Package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;

            Assumes.Present(oleMenuCommandService);

            var commandId = new CommandID(PackageGuids.AttachToAppPoolProcessExtension, PackageIds.AttachToProcessCommand);
            var command = oleMenuCommandService.FindCommand(commandId);

            command.Enabled = enabled;
        }

        private string GetSelectedAppPoolName()
        {
            return GeneralOptions.Instance.SelectedAppPoolName;
        }

        private AppPoolProcess[] GetAvailableProcesses()
        {
            return GeneralOptions.Instance.Processes
                ?.Where(p => p.IsEnabled)
                .ToArray() ?? new AppPoolProcess[0];
        }

        private async Task SaveSelectedProcessAsync(AppPoolProcess process)
        {
            GeneralOptions.Instance.SelectedAppPoolName = process.AppPoolName;

            await GeneralOptions.Instance.SaveAsync();
        }
    }
}
