using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;
using SmallAssistant.Annotations;

namespace SmallAssistant.Models
{
    public class RateViewItem: INotifyPropertyChanged
    {
        private int? _rateId;
        private string _rateName;
        private int _rateTypeId;
        private string _rateTypeName;
        private decimal _rateValue;
        private DateTime _activeFrom;

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

        public int RateTypeId
        {
            get => _rateTypeId;
            set
            {
                _rateTypeId = value;
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

        public DateTime ActiveFrom
        {
            get => _activeFrom;
            set
            {
                _activeFrom = value;
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