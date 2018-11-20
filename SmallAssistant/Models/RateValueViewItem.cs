using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;
using SmallAssistant.Annotations;

namespace SmallAssistant.Models
{
    public class RateValueViewItem : INotifyPropertyChanged
    {
        private int _rateValueId;
        private int? _rateId;
        private decimal? _meterValueFrom;
        private decimal? _meterValueTo;
        private TimeSpan? _timeFrom;
        private TimeSpan? _timeTo;
        private DateTime _activeFrom;
        private decimal _value;

        public int RateValueId
        {
            get => _rateValueId;
            set
            {
                _rateValueId = value;
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

        public decimal? MeterValueFrom
        {
            get => _meterValueFrom;
            set
            {
                _meterValueFrom = value;
                OnPropertyChanged();
            }
        }

        public decimal? MeterValueTo
        {
            get => _meterValueTo;
            set
            {
                _meterValueTo = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan? TimeFrom
        {
            get => _timeFrom;
            set
            {
                _timeFrom = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan? TimeTo
        {
            get => _timeTo;
            set
            {
                _timeTo = value;
                OnPropertyChanged();
            }
        }

        public DateTime ActiveFrom
        {
            get => _activeFrom;
            set
            {
                _activeFrom = value;
                OnPropertyChanged();
            }
        }

        public decimal Value
        {
            get => _value;
            set
            {
                _value = value;
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