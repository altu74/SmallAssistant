using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmallAssistant.Annotations;

namespace SmallAssistant.Models
{
    public class MeterValueViewItem : INotifyPropertyChanged
    {
        private decimal _meterCurrentValue;
        private int _meterId;
        private string _meterName;
        private DateTime _meterValueDate;
        private decimal _amount;

        public int MeterId { get => _meterId;
            set
            {
                _meterId = value;
                OnPropertyChanged();
            }
        }
        public string MeterName
        {
            get => _meterName;
            set
            {
                _meterName = value;
                OnPropertyChanged();
            } }
        public DateTime MeterValueDate { get => _meterValueDate;
            set
            {
                _meterValueDate = value;
                OnPropertyChanged();
            }
        }
        public long MeterPrevValueId { get; set; }
        public decimal? MeterPrevValue { get; set; }
        public long MeterCurrentValueId { get; set; }
        public decimal MeterCurrentValue
        {
            get => _meterCurrentValue;
            set
            {
                _meterCurrentValue = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(Delta));
            }
        }

        public decimal? Delta => MeterCurrentValue - (MeterPrevValue ?? 0.00M);
        public decimal Amount { get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}