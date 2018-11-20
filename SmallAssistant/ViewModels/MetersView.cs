using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmallAssistant.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MetersView : ContentPage
    {
        private MeterViewItem _selectedItem;
        private bool _itemAdded;

        public ObservableCollection<MeterViewItem> MeterItems { get; set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public MetersView()
        {
            ReadDataFromDB();
            InitializeComponent();
            BindingContext = this;
            RefreshCommand = new Command(ReadDataFromDB);
            AddCommand = new Command(AddMeter);

        }

        private void AddMeter(object obj)
        {
            _itemAdded = true;
            Navigation.PushAsync(new MeterEdit(new MeterViewItem {MeterId = -1}));
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            _itemAdded = false;
            _selectedItem = e.Item as MeterViewItem;
            Navigation.PushAsync(new MeterEdit(_selectedItem));
        }

        protected override void OnAppearing()
        {
        }

        private void ReadDataFromDB()
        {
            try
            {
                var context = SmallAssistantDBContext.GetContext();

                var meterRates = from mr in context.MeterRates
                    group mr by mr.MeterId
                    into r
                    select r.OrderByDescending(f => f.ActiveFrom).First();

                var rateValues = from v in context.RateValues
                    join rt in context.RateTypes on v.Rate.RateTypeId equals rt.RateTypeId
                    group v by v.RateId
                    into r
                    select r.OrderByDescending(g => g.ActiveFrom).FirstOrDefault();

                var tt = rateValues.ToList();
                //if (tt.Count == 0)
                //    Utils.ShowError("No values found");
                var tt1 = meterRates.ToList();

                var rates = from m in context.Meters
                    join mr in meterRates on m.MeterId equals mr.MeterId into mrv
                    from mrt in mrv.DefaultIfEmpty()
                    join rv in rateValues on mrt.RateId equals rv.RateId into res
                    from r in res.DefaultIfEmpty()
                    select new MeterViewItem
                    {
                        MeterId = m.MeterId,
                        MeterName = m.MeterName,
                        RateId = mrt == null ? null : (int?) mrt.Rate.RateId,
                        RateName = mrt == null ? string.Empty : mrt.Rate.RateName,
                        RateTypeName = mrt == null ? string.Empty : mrt.Rate.RateType.RateTypeName,
                        RateValue = r == null ? 0.00M : r.Value
                    };

                MeterItems = new ObservableCollection<MeterViewItem>(rates);

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }
    }
}