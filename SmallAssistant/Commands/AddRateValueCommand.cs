using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmallAssistant.DBModels;
using SmallAssistant.Models;
using SmallAssistant.ViewModels;

namespace SmallAssistant.Commands
{
    public class AddRateValueCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
           return true;
        }

        public void Execute(object parameter)
        {
            var ctx = SmallAssistantDBContext.GetContext();
            var data = parameter as SaveRateCommandParameter;
            data.RateValues.Add(new RateValueViewItem{RateId = data.Item.RateId, ActiveFrom = DateTime.Today, });
        }

        public event EventHandler CanExecuteChanged;
    }
}