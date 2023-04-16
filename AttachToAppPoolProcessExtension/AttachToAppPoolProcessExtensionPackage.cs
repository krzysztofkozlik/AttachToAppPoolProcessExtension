global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using AttachToAppPoolProcessExtension.Options;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System.Runtime.InteropServices;
using System.Threading;

namespace AttachToAppPoolProcessExtension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.AttachToAppPoolProcessExtensionString)]
    [ProvideOptionPage(typeof(GeneralOptionsPage), "Attach To App Pool Process", "Processes", 0, 0, true)]
    public sealed class AttachToAppPoolProcessExtensionPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            ExtensionManager.ShellSettingsManager = new ShellSettingsManager(this);

            await this.RegisterCommandsAsync();
        }
    }
}