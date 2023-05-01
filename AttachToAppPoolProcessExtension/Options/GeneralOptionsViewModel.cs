using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Web.Administration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AttachToAppPoolProcessExtension.Options
{
    public class GeneralOptionsViewModel : INotifyPropertyChanged
    {
        private ProcessViewModel selectedProcess;

        public ObservableCollection<ProcessViewModel> Processes { get; } = new();

        public ICommand ImportAppPoolProcessesCommand { get; }
        public ICommand MoveProcessUpCommand { get; }
        public ICommand MoveProcessDownCommand { get; }
        public ICommand SelectProcessCommand { get; }

        public ProcessViewModel SelectedProcess 
        { 
            get => selectedProcess;
            set
            {
                selectedProcess = value;
                RaisePropertyChanged();
            }
        }

        public GeneralOptionsViewModel()
        {
            ImportAppPoolProcessesCommand = new DispatchedDelegateCommand(ImportAppPoolProcesses);
            MoveProcessUpCommand = new DispatchedDelegateCommand(MoveProcessUp, CanMoveProcessUp);
            MoveProcessDownCommand = new DispatchedDelegateCommand(MoveProcessDown, CanMoveProcessDown);
            SelectProcessCommand = new DispatchedDelegateCommand(SelectProcess);
        }

        private bool CanMoveProcessUp(object obj) => SelectedProcess != null && Processes.IndexOf(SelectedProcess) > 0;
        private bool CanMoveProcessDown(object obj) => SelectedProcess != null && Processes.IndexOf(SelectedProcess) < Processes.Count - 1;

        public event PropertyChangedEventHandler PropertyChanged;

        private void ImportAppPoolProcesses(object commandParameter)
        {
            var existingAppPoolNames = Processes.Select(p => p.AppPoolName).ToHashSet();

            using (var serverManager = new ServerManager())
            {
                var appPoolNames = serverManager.WorkerProcesses
                    .Select(p => p.AppPoolName)
                    .Where(name => !existingAppPoolNames.Contains(name));

                foreach (var name in appPoolNames)
                {
                    var process = new ProcessViewModel
                    {
                        IsEnabled = true,
                        Name = name,
                        AppPoolName = name
                    };

                    Processes.Add(process);
                }
            }
        }

        private void MoveProcessUp(object commandParameter)
        {
            var process = SelectedProcess;

            if (process != null)
            {
                var index = Processes.IndexOf(process);

                if (index > 0)
                {
                    Processes.RemoveAt(index);
                    Processes.Insert(index - 1, process);

                    SelectedProcess = process;
                }
            }
        }

        private void MoveProcessDown(object commandParameter)
        {
            var process = SelectedProcess;

            if (process != null)
            {
                var index = Processes.IndexOf(process);

                if (index < Processes.Count - 1)
                {
                    Processes.RemoveAt(index);
                    Processes.Insert(index + 1, process);

                    SelectedProcess = process;
                }
            }
        }

        private void SelectProcess(object commandParameter)
        {
            if (commandParameter is ProcessViewModel process)
            {
                SelectedProcess = process;
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
