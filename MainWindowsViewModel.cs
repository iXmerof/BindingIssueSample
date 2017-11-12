using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlToolkitBindingIssue
{
    public class MainWindowsViewModel
    {
        public ViewModelBase CurrentView { get; } = new UsersViewModel();
    }
}
