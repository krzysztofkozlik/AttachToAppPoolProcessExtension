using AttachToAppPoolProcessExtension.Options;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.OpenOptionsCommand)]
    internal sealed class OpenOptionsCommand : BaseCommand<OpenOptionsCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            Package.ShowOptionPage(typeof(GeneralOptionsPage));
        }
    }
}
