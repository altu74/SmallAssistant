using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using SmallAssistant.ViewModels;

namespace SmallAssistant.Commands
{
    public class SaveMeterCommand: ICommand
    {
        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            var ctx = SmallAssistantDBContext.GetContext();
            var data = parameter as SaveMeterCommandParameter;
            var editedValue = data.IsNewItem ? new Meter() : ctx.Meters.First(p => p.MeterId == data.Item.MeterId);
            editedValue.MeterName = data.Item.MeterName;
            if (data.IsNewItem)
                ctx.Meters.Add(editedValue);
            ctx.SaveChanges();
            data.Navigation.PopAsync(true);
        }

        public event EventHandler CanExecuteChanged;
    }
}