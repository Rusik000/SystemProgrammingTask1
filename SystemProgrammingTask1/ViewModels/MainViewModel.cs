using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

            var processes = Process.GetProcesses();
            AllProcess = new ObservableCollection<string>();
            foreach (var item in processes)
            {
                AllProcess.Add(item.ProcessName);
            }



            AddBlackBoxCommand = new RelayCommand((sender) =>
            {
                if (MainView.BlackBoxTxtBx.Text != null)
                {
                    if (AllProcess != null)
                    {
                        foreach (var item in AllProcess)
                        {
                            if (item==MainView.BlackBoxTxtBx.Text)
                            {
                                AllProcess.Remove(item);
                                ProcessBlackBoxNames.Add(item);
                                MainView.BlackBoxListBx.ItemsSource = ProcessBlackBoxNames;
                            }
                        }
                    }
                }
            });

            CreateCommand = new RelayCommand(sender =>
            {
                MainView.AllProcessListBx.ItemsSource = AllProcess;
                if (MainView.CreateTxtBx.Text != null)
                {
                    Process.Start($"{MainView.CreateTxtBx.Text}.exe");
                }
                else
                {
                    MessageBox.Show("You must fill textbox");
                }

            });


            EndCommand = new RelayCommand(sender =>
            {
                var process = Process.GetProcesses().Where(p => p.ProcessName == MainView.CreateTxtBx.Text);
                foreach (var item in process)
                {
                    item.Kill();
                }
            });


        }
    }
}
