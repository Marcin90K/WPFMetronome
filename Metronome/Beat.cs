using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Metronome
{
    class Beat : INotifyPropertyChanged
    {
        private double _rate;
        private double _perMsec;

        public double Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public double perMsec
        {
            get { return _perMsec; }
            set
            { 
                _perMsec = value;
                OnPropertyChanged("perMsec");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
