using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employee : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _nameSubject;
        public string NameSubject
        {
            get
            {
                return _nameSubject;
            }
            set
            {
                _nameSubject = value;
                OnPropertyChanged("NameSubject");
            }
        }

        private string _level;
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }

        private string _data;
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private string _startData;
        public string StartData
        {
            get
            {
                if (Level == "Простой")
                    _startData = "Времени на подготовку не надо";
                else if (Level == "Средний")
                    _startData = DateTime.Parse(Data, CultureInfo.InvariantCulture).AddDays(-14).ToString("MM'/'dd'/'yyyy");
                else if (Level == "Сложный")
                    _startData = DateTime.Parse(Data, CultureInfo.InvariantCulture).AddMonths(-1).ToString("MM'/'dd'/'yyyy");
                return _startData;
            }
            set
            {
                _startData = value;
                OnPropertyChanged("StartData");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
