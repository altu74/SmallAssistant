using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmallAssistant.Annotations;
using SmallAssistant.Commands;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SmallAssistant.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateEdit : ContentPage
    {
        private ControlTemplate _sigleValueEdit;
        private ControlTemplate _multiValueEdit;
        private ObservableCollection<RateValueViewItem> _rateValues;

        public RateViewItem Item
        {
            get { return Param.Item; }
            set
            {
                Param.Item = value;
                OnPropertyChanged("Param");
                OnPropertyChanged();
            }
        }

        public  ObservableCollection<RateValueViewItem> RateValues
        {
            get
            {
                return _rateValues;

            }
            set
            {
                _rateValues = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RateType> RateTypes { get; set; }

        public RateType SelectedRateType { get; set; }
        public SaveRateCommand SaveCommand { get; private set; }
        public AddRateValueCommand AddCommand { get; private set; }
        public ObservableCollection<Meter> Meters { get; private set; }
        public SaveRateCommandParameter Param { get; set; }

        public RateEdit(RateViewItem value)
        {
            try
            {
                Param = new SaveRateCommandParameter {Navigation = Navigation};
                Item = value;
                Param.IsNewItem = Item.RateId < 0;
                BindingContext = this;
                GetDataDictionaries();
                SaveCommand = new SaveRateCommand();
                AddCommand = new AddRateValueCommand();
                InitializeComponent();
                _sigleValueEdit = (ControlTemplate) Application.Current.Resources["SingleValueEdit"];
                _multiValueEdit = (ControlTemplate)Application.Current.Resources["MultiValueEdit"];
                SelectedRateType = RateTypes.FirstOrDefault(i => i.RateTypeId == Item.RateTypeId);
                OnPropertyChanged("SelectedRateType");
                RateValues.Add(new RateValueViewItem{RateValueId = 0, RateId = 1, MeterValueFrom = 0, MeterValueTo = 100, ActiveFrom = DateTime.Now, Value = 10});
                RateValues.Add(new RateValueViewItem { RateValueId = 2, RateId = 1, MeterValueFrom = 101, MeterValueTo = 999999, ActiveFrom = DateTime.Now, Value = 20 });
                RateValues.Add(new RateValueViewItem { RateValueId = 3, RateId = 1, ActiveFrom = DateTime.Today, Value = 20 });
                RateValues.Add(new RateValueViewItem { RateValueId = 4, RateId = 1, ActiveFrom = DateTime.Today, Value = 30 });
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }

        protected sealed override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        private void GetDataDictionaries()
        {
            var ctx = SmallAssistantDBContext.GetContext();
            var values = ctx.RateValues.Where(v => v.RateId == Item.RateId);
            var valuesList = new List<RateValueViewItem>();
            values.ForEach(i => valuesList.Add(new RateValueViewItem
            {
                RateValueId = i.RateValueId,
                RateId = i.RateId,
                MeterValueFrom = i.MeterValueFrom,
                MeterValueTo = i.MeterValueTo,
                TimeFrom = i.TimeFrom,
                TimeTo = i.TimeTo,
                ActiveFrom = i.ActiveFrom,
                Value = i.Value
            }));
            RateValues = new ObservableCollection<RateValueViewItem>(valuesList);
            RateTypes = new ObservableCollection<RateType>(ctx.RateTypes);
            Param.RateValues = RateValues;
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Item.RateTypeId = SelectedRateType.RateTypeId;
            //RateValues.Clear();
            switch (Item.RateTypeId)
            {
                case 1: CreateSingleValueEntry();
                    break;
                case 2: CreateMultiValueEntry();
                    break;
                case 3: CreateTimespecificEntry();
                    break;

            }
        }

        private void CreateTimespecificEntry()
        {
            throw new NotImplementedException();
        }

        private void CreateMultiValueEntry()
        {
            EditView.ControlTemplate = _multiValueEdit;
            EditView.ControlTemplate.CreateContent();
        }

        private void CreateSingleValueEntry()
        {
            EditView.ControlTemplate = _sigleValueEdit;
        }
    }

    public class SaveRateCommandParameter : INotifyPropertyChanged
    {
        private RateViewItem _item;
        private ObservableCollection<RateValueViewItem> _rateValues;

        public bool IsNewItem { get; set; }

        public RateViewItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RateValueViewItem> RateValues
        {
            get { return _rateValues; }
            set
            {
                _rateValues = value;
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
