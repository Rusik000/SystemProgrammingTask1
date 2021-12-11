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


        private Process _process;

        public Process Process
        {
            get { return _process; }
            set { _process = value; OnPropertyChanged(); }
        }



        private ObservableCollection<Process> _allProcess;
        public ObservableCollection<Process> AllProcess
        {
            get { return _allProcess; }
            set { _allProcess = value; OnPropertyChanged(); }
        }


        public MainViewModel()
        {


            AllProcess = new ObservableCollection<Process>(Process.GetProcesses());



            AddBlackBoxCommand = new RelayCommand((sender) =>
            {
                MainView.BlackBoxListBx.Items.Add(MainView.BlackBoxTxtBx.Text);
                try
                {
                    foreach (var item in MainView.BlackBoxListBx.Items)
                    {
                        var processes = AllProcess.Where(p => p.ProcessName == item.ToString());

                        if (processes != null)
                        {
                            foreach (var item2 in processes)
                            {
                                Console.WriteLine($"{item2.Id}  {item2.ProcessName}");
                                var process = AllProcess.FirstOrDefault(p => p.ProcessName == item.ToString());

                                if (item2.ProcessName == process.ProcessName)
                                {
                                    if (!item2.WaitForExit(1000))
                                    {
                                        MessageBox.Show(item2.StartTime.ToLongTimeString());
                                        for (int J = 0; J < 100; J++)
                                        {
                                            if (!item2.HasExited) item2.Kill();

                                        }

                                    }
                                }

                            }

                            AllProcess = new ObservableCollection<Process>(Process.GetProcesses());

                        }


                    }

                }
                catch (Exception)
                {


                }
            });

            CreateCommand = new RelayCommand(sender =>
            {
                if (MainView.CreateTxtBx.Text != string.Empty)
                {
                    try
                    {
                        bool opening = true;
                        foreach (var item in MainView.BlackBoxListBx.Items)
                        {

                            if (item.ToString() == MainView.CreateTxtBx.Text)
                            {
                                MessageBox.Show("You can not create this exe");
                                opening = false;
                            }

                        }

                        if (opening == true)
                        {
                            Process.Start($"{MainView.CreateTxtBx.Text}.exe");
                        }
                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You must fill correct");
                    }
                }
                else
                {
                    MessageBox.Show("You must fill the text");
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
