using System;
using System.Linq;
using System.Windows.Input;
using SmallAssistant.DBModels;
using SmallAssistant.ViewModels;

namespace SmallAssistant.Commands
{
    public class SaveRateCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            var ctx = SmallAssistantDBContext.GetContext();
            var data = parameter as SaveRateCommandParameter;
            var editedValue = data.IsNewItem ? new Rate() : ctx.Rates.First(p => p.RateId == data.Item.RateId);
            editedValue.RateName = data.Item.RateName;
            editedValue.RateTypeId = data.Item.RateTypeId;
            if (data.IsNewItem)
                ctx.Rates.Add(editedValue);
            ctx.SaveChanges();
            data.Navigation.PopAsync(true);
        }

        public event EventHandler CanExecuteChanged;
    }
}