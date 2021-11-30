using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemProgrammingTask1.Command;

namespace SystemProgrammingTask1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainWindow MainView { get; set; }

        public RelayCommand AddBlackBoxCommand { get; set; }

        public RelayCommand CreateCommand { get; set; }


        public RelayCommand EndCommand { get; set; }


        private ObservableCollection<string> _processBlackBoxNames;

        public ObservableCollection<string> ProcessBlackBoxNames
        {
            get { return _processBlackBoxNames; }
            set { _processBlackBoxNames = value; OnPropertyChanged(); }
        }



        private ObservableCollection<string> _allProcess;
        public ObservableCollection<string> AllProcess
        {
            get { return _allProcess; }
            set { _allProcess = value; OnPropertyChanged(); }
        }



        public MainViewModel()
        {

            AddBlackBoxCommand = new RelayCommand((sender) =>
            {
                var process = Process.GetCurrentProcess();

                if (process.ProcessName==MainView.BlackBoxTxtBx.Text)
                {

                    process.Kill();
                }


            });

            CreateCommand = new RelayCommand(sender =>
            {
                var processes = Process.GetProcesses();
                foreach (var item in processes)
                {
                    AllProcess = new ObservableCollection<string>();
                    AllProcess.Add(item.ProcessName);
                }
            });


            EndCommand = new RelayCommand(sender => { });


        }
    }
}
