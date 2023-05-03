using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Web.Administration;
using System.Collections.Generic;
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
        public ICommand ClearAppPoolProcessesCommand { get; }
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
            ClearAppPoolProcessesCommand = new DispatchedDelegateCommand(ClearAppPoolProcesses);
            MoveProcessUpCommand = new DispatchedDelegateCommand(MoveProcessUp, CanMoveProcessUp);
            MoveProcessDownCommand = new DispatchedDelegateCommand(MoveProcessDown, CanMoveProcessDown);
            SelectProcessCommand = new DispatchedDelegateCommand(SelectProcess);
        }

        private bool CanMoveProcessUp(object obj) => SelectedProcess != null && Processes.IndexOf(SelectedProcess) > 0;
        private bool CanMoveProcessDown(object obj) => SelectedProcess != null && Processes.IndexOf(SelectedProcess) < Processes.Count - 1;

        public event PropertyChangedEventHandler PropertyChanged;

        private void ImportAppPoolProcesses(object commandParameter)
        {
            try
            {
                using var serverManager = new ServerManager();

                var appPoolNames = serverManager.WorkerProcesses
                    .Select(p => p.AppPoolName)
                    .Distinct()
                    .ToList();

                RemoveMissingProcesses(appPoolNames);
                AddNewProcesses(appPoolNames);
            }
            catch (UnauthorizedAccessException)
            {
                ShowError("No permissions to get App Pools from IIS. Try launching VS as administrator.");
            }
            catch (Exception ex)
            {
                ShowError($"Unable to get App Pools from IIS: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            VS.MessageBox.ShowError(Resources.ExtensionName, message);
        }
        
        private void AddNewProcesses(List<string> existingAppPoolNames)
        {
            var configuredAppPoolNames = Processes
                .Select(p => p.AppPoolName)
                .ToHashSet();

            var processesToAdd = existingAppPoolNames
                .Where(name => !configuredAppPoolNames.Contains(name))
                .Select(name => new ProcessViewModel
                {
                    IsEnabled = true,
                    Name = name,
                    AppPoolName = name
                });

            foreach (var process in processesToAdd)
            {
                Processes.Add(process);
            }
        }

        private void RemoveMissingProcesses(List<string> existingAppPoolNames)
        {
            var names = existingAppPoolNames.ToHashSet();

            var processesToRemove = Processes
                .Where(process => !existingAppPoolNames.Contains(process.Name));

            foreach (var process in processesToRemove)
            {
                Processes.Remove(process);
            }
        }

        private void ClearAppPoolProcesses(object commandParameter)
        {
            if (Processes.Any())
            {
                var isConfirmed = VS.MessageBox.ShowConfirm(Resources.ExtensionName, "Are you sure you want to clear all configured processes?");

                if (isConfirmed)
                {
                    Processes.Clear();
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

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
