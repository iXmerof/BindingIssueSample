using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace XamlToolkitBindingIssue
{
    public class UsersViewModel : ViewModelBase
    {
        public override string Name { get; protected set; } = "UsersView";
        public override string FriendlyName { get; protected set; } = "Users View";
        public override string MenuItemName { get; protected set; } = "Users";

        private IProgress<int> _currentProgress { get; } = new Progress<int>();
        public int CurrentProgress { get; private set; } = 0;
        public string ProgressResult { get; private set; } = "Aww yeah";


        private RelayCommand _importDDLCommand;
        public ICommand ImportDDLComand
        {
            get
            {
                if (this._importDDLCommand == null)
                    this._importDDLCommand = new RelayCommand(y => this.ImportDDL(), x => true);

                return this._importDDLCommand;
            }
        }


        private RelayCommand _showDialogCommand;
        public ICommand ShowDialogCommand
        {
            get
            {
                if (this._showDialogCommand == null)
                    this._showDialogCommand = new RelayCommand(y => this.ShowDialog(y), x => true);

                return this._showDialogCommand;
            }
        }

        public async void ShowDialog(object binding)
        {
            await DialogHost.Show(binding, "RootDialog", this.ClosingEventHandler);
        }

        public async void ImportDDL()
        {
            this._currentProgress.Report(1);
            ((Progress<int>) this._currentProgress).ProgressChanged += this.OnProgressChanged;
            await ImportDDL(this._currentProgress);
        }

        public async Task<bool> ImportDDL(IProgress<int> currentProgress)
        {
            await new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    currentProgress.Report(i * 10);
                    Task.Delay(200);
                }
            });

            return true;
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("DDLProgressView closed");
        }

        private void OnProgressChanged(object sender, int i)
        {
            this.CurrentProgress = i;
        }
    }
}
