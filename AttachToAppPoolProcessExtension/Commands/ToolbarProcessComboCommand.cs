using AttachToAppPoolProcessExtension.Options;
using System.Linq;
using System.Runtime.InteropServices;

namespace AttachToAppPoolProcessExtension
{
    [Command(PackageIds.ToolbarProcessComboId)]
    internal sealed class ToolbarProcessComboCommand : BaseCommand<ToolbarProcessComboCommand>
    {
        //private string currentDropDownComboChoice = "One";
        //private string[] dropDownComboChoices = new[] { "One", "Two", "Three" };

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs eventArgs)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (eventArgs == null)
            {
                // We should never get here; EventArgs are required.
                throw new ArgumentException("EventArgsRequired"); // force an exception to be thrown
            }

            string newChoice = eventArgs.InValue as string;
            IntPtr vOut = eventArgs.OutValue;
            
            var dropDownComboChoices = GetAvailableProcesses();

            if (vOut != IntPtr.Zero)
            {
                // when vOut is non-NULL, the IDE is requesting the current value for the combo
                var currentValue = GetCurrentValue();

                var selected = currentValue != null 
                    ? GetAvailableProcesses().FirstOrDefault(p => p.AppPoolName == currentValue)
                    : null;

                Marshal.GetNativeVariantForObject(selected?.Name, vOut);
            }
            else if (newChoice != null)
            {
                // new value was selected or typed in
                // see if it is one of our items
                var selected = dropDownComboChoices.FirstOrDefault(p => p.Name == newChoice);

                if (selected != null)
                {
                    General.Instance.SelectedAppPoolName = selected.AppPoolName;
                    await General.Instance.SaveAsync();
                }
                else
                {
                    throw (new ArgumentException()); // force an exception to be thrown
                }

                //bool validInput = false;
                //int indexInput;


                //for (indexInput = 0; indexInput < dropDownComboChoices.Length; indexInput++)
                //{
                //    if (string.Compare(dropDownComboChoices[indexInput], newChoice, StringComparison.CurrentCultureIgnoreCase) == 0)
                //    {
                //        validInput = true;
                //        break;
                //    }
                //}

                //if (validInput)
                //{
                //    currentDropDownComboChoice = dropDownComboChoices[indexInput];
                //    await VS.MessageBox.ShowWarningAsync("AttachToAppPoolProcessExtension", "ToolbarProcessComboCommand " + currentDropDownComboChoice);
                //}
                //else
                //{
                //    throw (new ArgumentException()); // force an exception to be thrown
                //}
            }
        }

        private string GetCurrentValue()
        {
            return General.Instance.SelectedAppPoolName;
        }

        private AppPoolProcess[] GetAvailableProcesses()
        {
            return General.Instance.Processes
                .Where(p => p.IsEnabled)
                .ToArray();
        }
    }
}
