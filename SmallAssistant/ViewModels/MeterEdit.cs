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
using Xamarin.Forms.Xaml;

namespace SmallAssistant.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeterEdit : ContentPage
    {
        public MeterViewItem Item
        {
            get { return Param.Item; }
            set
            {
                Param.Item = value;
                OnPropertyChanged("Param");
                OnPropertyChanged();
            }
        }

        public Rate SelectedRate { get; set; }
        public SaveMeterCommand SaveCommand { get; private set; }
        public ObservableCollection<Rate> Rates { get; private set; }
        public SaveMeterCommandParameter Param { get; set; }

        public MeterEdit(MeterViewItem item)
        {
            Param = new SaveMeterCommandParameter {Navigation = Navigation};
            Item = item;
            Param.IsNewItem = Item.MeterId < 0;
            BindingContext = this;
            GetDataDictionaries();
            SaveCommand = new SaveMeterCommand();
            InitializeComponent();
        }

        private void GetDataDictionaries()
        {
            var ctx = SmallAssistantDBContext.GetContext();
            Rates = new ObservableCollection<Rate>(ctx.Rates.Select(m => m).ToList());
            SelectedRate = Rates.FirstOrDefault(i => i.RateId == Item.MeterId);
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Item.RateId = SelectedRate.RateId;
        }
    }

    public class SaveMeterCommandParameter : INotifyPropertyChanged
    {
        private MeterViewItem _item;
        public bool IsNewItem { get; set; }

        public MeterViewItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
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