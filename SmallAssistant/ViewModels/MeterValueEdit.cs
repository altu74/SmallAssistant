using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmallAssistant.Annotations;
using SmallAssistant.Commands;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmallAssistant.ViewModels
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeterValueEdit : ContentPage
	{
      public MeterValueViewItem Item
	    {
	        get { return Param.Item; }
	        set
	        {
	            Param.Item = value;
	            OnPropertyChanged("Param");
	            OnPropertyChanged();
	        }
        }

      public Meter SelectedMeter { get; set; }
      public SaveMeterValueCommand SaveCommand { get; private set; }
      public ObservableCollection<Meter> Meters { get; private set; }
      public SaveMeterValueCommandParameter Param { get; set; }

	    public MeterValueEdit(MeterValueViewItem value)
	    {
            try
            {
                Param = new SaveMeterValueCommandParameter{Navigation = Navigation};
                Item = value;
                Param.IsNewItem = Item.MeterId < 0;
                BindingContext = this;
                GetDataDictionaries();
                SaveCommand = new SaveMeterValueCommand();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
	    }

      private void GetDataDictionaries()
      {
          var ctx = SmallAssistantDBContext.GetContext();
          Meters = new ObservableCollection<Meter>(ctx.Meters.Select(m => m).ToList());
          SelectedMeter = Meters.FirstOrDefault(i => i.MeterId == Item.MeterId);
      }

	    private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
	    {
	        Item.MeterId = SelectedMeter.MeterId;
	    }
	}

    public class SaveMeterValueCommandParameter:INotifyPropertyChanged
    {
        private MeterValueViewItem _item;
        public bool IsNewItem { get; set; }
        public MeterValueViewItem Item
        {
            get { return _item;}
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