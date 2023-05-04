using System.Linq;
using AttachToAppPoolProcessExtension.Options;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.Web.Administration;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.AttachToProcessCommand)]
    internal sealed class AttachToProcessCommand : BaseCommand<AttachToProcessCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var selectedAppPool = General.Instance.SelectedAppPoolName;

            if (selectedAppPool == null)
            {
                ShowStatusMessage("No process selected");
                return;
            }

            var processId = GetProcessId(selectedAppPool);

            if (!processId.HasValue)
            {
                ShowStatusMessage($"App Pool {selectedAppPool} process not found");
                return;
            }

            var dte = (DTE2)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
            Assumes.Present(dte);

            var debugger = dte.Debugger as Debugger2;
            var process = debugger.LocalProcesses.Cast<Process2>().FirstOrDefault(p => p.ProcessID == processId);

            if (process == null)
            {
                ShowStatusMessage($"Process {processId} not found");
                return;
            }

            Transport transport = debugger.Transports.Item("default");
            Engine[] engines = { transport.Engines.Item("Managed") };

            process.Attach2(engines);

            ShowStatusMessage($"Attached to {process.Name} [{process.ProcessID}]");
        }

        private static void ShowStatusMessage(string message)
        {
            VS.StatusBar.ShowMessageAsync(message).FireAndForget();
        }

        private int? GetProcessId(string appPoolName)
        {
            using var serverManager = new ServerManager();

            var workerProcess = serverManager
                .WorkerProcesses
                .FirstOrDefault(p => p.AppPoolName.Equals(appPoolName, StringComparison.OrdinalIgnoreCase));

            return workerProcess?.ProcessId;
        }
    }
}
