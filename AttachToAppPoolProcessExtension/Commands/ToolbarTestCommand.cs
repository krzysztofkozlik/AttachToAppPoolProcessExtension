using AttachToAppPoolProcessExtension.Options;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.ToolbarTestCommandId)]
    internal sealed class ToolbarTestCommand : BaseCommand<ToolbarTestCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            Package.ShowOptionPage(typeof(GeneralOptionsPage));
        }
    }
}
