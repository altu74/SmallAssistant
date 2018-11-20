using System;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using SmallAssistant.ViewModels;

namespace SmallAssistant.Commands
{
    public class SaveMeterValueCommand: ICommand
    {
        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            var ctx = SmallAssistantDBContext.GetContext();
            var data = parameter as SaveMeterValueCommandParameter;
            var editedValue = data.IsNewItem ? new MeterValue() : ctx.MeterValues.First(p => p.MeterValueId == data.Item.MeterCurrentValueId);
            editedValue.MeterDate = data.Item.MeterValueDate;
            editedValue.MeterId = data.Item.MeterId;
            editedValue.Value = (decimal)data.Item.MeterCurrentValue;
            if (data.IsNewItem)
                ctx.MeterValues.Add(editedValue);
            ctx.SaveChanges();
            data.Navigation.PopAsync(true);
        }

        public event EventHandler CanExecuteChanged;
    }
}