using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using Model;
using static System.Net.Mime.MediaTypeNames;

namespace ViewModel
{
    public class ViewModel1 : INotifyPropertyChanged
    {
        //ViewList
        public ObservableCollection<Employee> Employees { set; get; }
        //Отдельный элемент из ViewList
        public Employee SelectedModel { get; set; }
        //Лист, который хранит записи из текстовых файлов
        private List<string> text { get; set; } = new List<string>();


        private Employee _newEmployee;
        public Employee NewEmployee
        {
            get
            {
                return _newEmployee;
            }
            set
            {
                _newEmployee = value;
                OnPropertyChanged("NewEmployee");
            }
        }

        public ViewModel1()
        {
            Employees = new ObservableCollection<Employee>();
            NewEmployee = new Employee();
            ListView1();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void AddEmployee() /*Добавление данных в ListView и текстовый файл*/
        {
            Employees.Add(NewEmployee);
            NewEmployee = new Employee();
            UploadingToAFile();           
        }

        public void RemoveEmployee() /*Удаление элемента из ListView*/
        {
            if (SelectedModel != null)
            {
                Employees.Remove(SelectedModel);
            }
            UploadingToAFile();
        }

        public void UploadingToAFile()
        {
            string path1 = @"C:\Users\User\Desktop\Laboratornaya2S5_1.txt";
            string path2 = @"C:\Users\User\Desktop\Laboratornaya2S5_2.txt";
            string path3 = @"C:\Users\User\Desktop\Laboratornaya2S5_3.txt";
            string path4 = @"C:\Users\User\Desktop\Laboratornaya2S5_4.txt";
            string path5 = @"C:\Users\User\Desktop\Laboratornaya2S5_5.txt";
            File.WriteAllText(path1, string.Empty);
            File.WriteAllText(path2, string.Empty);
            File.WriteAllText(path3, string.Empty);
            File.WriteAllText(path4, string.Empty);
            File.WriteAllText(path5, string.Empty);

            foreach (var employee in Employees) 
            {
                File.AppendAllText(path1, $"{employee.NameSubject}\n", Encoding.UTF8);
                File.AppendAllText(path2, $"{employee.Name}\n", Encoding.UTF8);
                File.AppendAllText(path3, $"{employee.Level}\n", Encoding.UTF8);
                File.AppendAllText(path4, $"{employee.Data}\n", Encoding.UTF8);
                File.AppendAllText(path5, $"{employee.StartData}\n", Encoding.UTF8);
            }
        }

        public string[] GetText() /*Чтение файлов и добавление их в массив*/
        {
            string path1 = @"C:\Users\User\Desktop\Laboratornaya2S5_1.txt";
            var srcEncoding = Encoding.GetEncoding("UTF-8");
            using (StreamReader reader = new StreamReader(path1, encoding: srcEncoding))
            {
                text.Add(reader.ReadToEnd());
            }

            string path2 = @"C:\Users\User\Desktop\Laboratornaya2S5_2.txt";
            using (StreamReader reader = new StreamReader(path2, encoding: srcEncoding))
            {
                text.Add(reader.ReadToEnd());
            }

            string path3 = @"C:\Users\User\Desktop\Laboratornaya2S5_3.txt";
            using (StreamReader reader = new StreamReader(path3, encoding: srcEncoding))
            {
                text.Add(reader.ReadToEnd());
            }

            string path4 = @"C:\Users\User\Desktop\Laboratornaya2S5_4.txt";
            using (StreamReader reader = new StreamReader(path4, encoding: srcEncoding))
            {
                text.Add(reader.ReadToEnd());
            }

            string path5 = @"C:\Users\User\Desktop\Laboratornaya2S5_5.txt";
            using (StreamReader reader = new StreamReader(path5, encoding: srcEncoding))
            {
                text.Add(reader.ReadToEnd());
            }

            int i = 0;
            string[] words = new string[text.Count];
            foreach (var word in text)
                words[i++] = word.Trim();
            return words;
        }

        public void ListView1() /*Хранение данных в ListView*/
        {
            Employees = new ObservableCollection<Employee>();

            for (int j = 0; j < GetText()[0].Split('\n').Length; j++)
            {
                Employee employee = new Employee()
                {
                    NameSubject = GetText()[0].Split('\n')[j],
                    Name = GetText()[1].Split('\n')[j],
                    Level = GetText()[2].Split('\n')[j],
                    Data = GetText()[3].Split('\n')[j],
                    StartData = GetText()[4].Split('\n')[j]
                };
                Employees.Add(employee);
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ?? (addCommand = new RelayCommand(AddEmployee));

        private ICommand addCommand1;
        public ICommand AddCommand1 => addCommand1 ?? (addCommand1 = new RelayCommand(RemoveEmployee));
    }
}
