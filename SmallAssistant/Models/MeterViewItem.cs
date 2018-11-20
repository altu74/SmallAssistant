using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;
using SmallAssistant.Annotations;

namespace SmallAssistant.Models
{
    public class MeterViewItem: INotifyPropertyChanged
    {
        private string _meterName;
        private int _meterId;
        private int? _rateId;
        private string _rateName;
        private string _rateTypeName;
        private decimal _rateValue;

        public int MeterId
        {
            get => _meterId;
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
            }
        }
        public int? RateId
        {
            get => _rateId;
            set
            {
                _rateId = value;
                OnPropertyChanged();
            }
        }

        public string RateName
        {
            get => _rateName;
            set
            {
                _rateName = value;
                OnPropertyChanged();
            }
        }

        public string RateTypeName
        {
            get => _rateTypeName;
            set
            {
                _rateTypeName = value;
                OnPropertyChanged();
            }
        }

        public decimal RateValue
        {
            get => _rateValue;
            set
            {
                _rateValue = value;
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