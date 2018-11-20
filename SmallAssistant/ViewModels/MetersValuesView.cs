using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using SmallAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using static SmallAssistant.DBModels.SmallAssistantDBContext;

namespace SmallAssistant.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MetersValuesView : ContentPage
    {

        public ObservableCollection<GroupedTotalCollection<DateTime, decimal, MeterValueViewItem>> MeterValueItems
        {
            get => _meterValueItems;
            set {
                _meterValueItems = value;
                OnPropertyChanged();
            }
        }
        private MeterValueViewItem _selectedItem;
        private bool _itemAdded;
        private ObservableCollection<GroupedTotalCollection<DateTime, decimal, MeterValueViewItem>> _meterValueItems;
        public ICommand RefreshCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public MetersValuesView()
        {
            ReadDataFromDB();
            InitializeComponent();
            BindingContext = this;
            _itemAdded = false;
            RefreshCommand = new Command(ReadDataFromDB);
            AddCommand = new Command(AddMeterValue);
        }

        private void Target()
        {
            throw new NotImplementedException();
        }

        private void ReadDataFromDB()
        {
            var valuesList = new List<MeterValueViewItem>();
            var dbContext = GetContext();
            var limitDate = DateTime.Now.AddMonths(-6);
            var rawData = dbContext.MeterValues
                .Include(c => c.Meter)
                .Where(c => c.MeterDate >= limitDate)
                .OrderBy(c => c.MeterDate)
                .ThenBy(c => c.MeterId)
                .ToList();

            foreach (var meter in rawData)
            {
                var viewItem = new MeterValueViewItem()
                {
                    MeterId = meter.Meter.MeterId,
                    MeterName = meter.Meter.MeterName,
                    MeterValueDate = meter.MeterDate,
                    MeterCurrentValueId = meter.MeterValueId,
                    MeterCurrentValue = meter.Value
                };

                if (rawData.Any(x => x.MeterId == meter.MeterId && (x.MeterDate < meter.MeterDate)))
                {
                    var prevMeterDate = rawData.Where(x => x.MeterId == meter.MeterId && (x.MeterDate < meter.MeterDate)).Max(x => x.MeterDate);
                    var prevMeterValue = rawData.FirstOrDefault(r => r.MeterId == meter.MeterId && r.MeterDate == prevMeterDate)?.Value;
                    var prevMeterValueId = rawData.FirstOrDefault(r => r.MeterId == meter.MeterId && r.MeterDate == prevMeterDate)?.MeterValueId;
                    viewItem.MeterPrevValue = prevMeterValue ?? 0.00M;
                    viewItem.MeterPrevValueId = prevMeterValueId ?? -1;
                }

                CalculateAmount(viewItem);

                valuesList.Add(viewItem);
            }

            MeterValueItems = new ObservableCollection<GroupedTotalCollection<DateTime, decimal, MeterValueViewItem>>(valuesList.OrderByDescending(x => x.MeterValueDate).GroupBy(x => x.MeterValueDate)
                .Select(x => new GroupedTotalCollection<DateTime, decimal, MeterValueViewItem>(x.Key, 0, x)));
            MeterValueItems.ForEach(i => i.Total = i.Sum(t => t.Amount));
            if (Meters != null)
              Meters.IsRefreshing = false;
        }

        private void CalculateAmount(MeterValueViewItem viewItem)
        {
            var dbContext = GetContext();
            var currRates = from mr in dbContext.MeterRates
                join rv in dbContext.RateValues on mr.RateId equals rv.RateId
                join r in dbContext.Rates on rv.RateId equals r.RateId
                where (mr.MeterId == viewItem.MeterId) && (rv.ActiveFrom <= viewItem.MeterValueDate)
                group rv by rv.RateId
                into r
                select r.OrderByDescending(t => t.ActiveFrom);

            var delta = viewItem.MeterCurrentValue - (viewItem.MeterPrevValue ?? 0.00M);

            var rate = currRates.SelectMany(x => x.Select(t => t));
            if (!currRates.Any())
            {
                viewItem.Amount = 0;
            }
            else
            {
                var currRateId = rate.FirstOrDefault()?.RateId;
                if (dbContext.Rates.FirstOrDefault(r => r.RateId == currRateId)?.RateTypeId == 1)
                {
                    viewItem.Amount = delta * rate.FirstOrDefault()?.Value ?? 0.00M;
                }
            }
        }

        protected override void OnAppearing()
        {
           if (_itemAdded)
               ReadDataFromDB();
            else
               RefreshData();
            OnPropertyChanged("MeterValueItems");
        }

        private void RefreshData()
        {
            if (_selectedItem != null) 
               CalculateAmount(_selectedItem);
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            _itemAdded = false;
            _selectedItem = e.Item as MeterValueViewItem;
            Navigation.PushAsync(new MeterValueEdit(_selectedItem));
        }

        private void AddMeterValue()
        {
            _itemAdded = true;
            Navigation.PushAsync(new MeterValueEdit(new MeterValueViewItem { MeterValueDate = DateTime.Today, MeterId = -1 }));
        }

        private void DeleteItem_OnClicked(object sender, EventArgs e)
        {
            var mi = (MenuItem) sender;
            if (mi.CommandParameter != null)
            {
                var param = mi.CommandParameter as MeterValueViewItem;
                var ctx = GetContext();
                var meterValue = ctx.MeterValues.FirstOrDefault(t => t.MeterValueId == param.MeterCurrentValueId);
                if (meterValue != null)
                {
                    ctx.MeterValues.Remove(meterValue);
                    ctx.SaveChanges();
                }

                var meterValueViewItem = MeterValueItems.FirstOrDefault(t => t.FirstOrDefault(r => r.MeterCurrentValueId == param.MeterCurrentValueId) != null);
                MeterValueItems.Remove(meterValueViewItem);
                OnPropertyChanged(nameof(MeterValueItems));
            }

                DisplayAlert("ttttt", "ttttt", "ok");
            //_selectedItem = e.Item as MeterValueViewItem;
        }
    }
}
