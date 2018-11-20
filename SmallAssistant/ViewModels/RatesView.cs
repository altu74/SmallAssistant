using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmallAssistant.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatesView: ContentPage
    {
        private RateViewItem _selectedItem;
        private bool _itemAdded;

        public ObservableCollection<RateViewItem> RateItems { get; set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand AddCommand { get; private set; }

        public RatesView()
        {
            ReadDataFromDB();
            InitializeComponent();
            BindingContext = this;
            RefreshCommand = new Command(ReadDataFromDB);
            AddCommand = new Command(AddRate);

        }

        private void AddRate(object obj)
        {
            _itemAdded = true;
            Navigation.PushAsync(new RateEdit(new RateViewItem {RateId = -1, ActiveFrom = DateTime.Today, RateTypeId = 2}));
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            _itemAdded = false;
            _selectedItem = e.Item as RateViewItem;
            Navigation.PushAsync(new RateEdit(_selectedItem));
        }

        protected override void OnAppearing()
        {
        }

        private void ReadDataFromDB()
        {
            try
            {
                var context = SmallAssistantDBContext.GetContext();

                //var meterRates = from mr in context.MeterRates
                //                 group mr by mr.MeterId
                //    into r
                //                 select r.OrderByDescending(f => f.ActiveFrom).First();

                //var rateValues = from v in context.RateValues
                //                 join rt in context.RateTypes on v.Rate.RateTypeId equals rt.RateTypeId
                //                 group v by v.RateId
                //    into r
                //                 select r.OrderByDescending(g => g.ActiveFrom).FirstOrDefault();

                //var tt = rateValues.ToList();
                ////if (tt.Count == 0)
                ////    Utils.ShowError("No values found");
                //var tt1 = meterRates.ToList();

                //var rates = from m in context.Meters
                //            join mr in meterRates on m.MeterId equals mr.MeterId into mrv
                //            from mrt in mrv.DefaultIfEmpty()
                //            join rv in rateValues on mrt.RateId equals rv.RateId into res
                //            from r in res.DefaultIfEmpty()
                //            select new MeterViewItem
                //            {
                //                MeterId = m.MeterId,
                //                MeterName = m.MeterName,
                //                RateId = mrt == null ? null : (int?)mrt.Rate.RateId,
                //                RateName = mrt == null ? string.Empty : mrt.Rate.RateName,
                //                RateTypeName = mrt == null ? string.Empty : mrt.Rate.RateType.RateTypeName,
                //                RateValue = r == null ? 0.00M : r.Value
                //            };

                //RateItems = new ObservableCollection<RateViewItem>(rates);

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }

        private void DeleteItem_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}