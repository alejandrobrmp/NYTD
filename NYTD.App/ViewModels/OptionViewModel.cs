using Caliburn.Micro;
using NYTD.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.ViewModels
{
    public class OptionViewModel : PropertyChangedBase
    {
        private readonly Option option;
        public string ToolTip => option.ToolTip;
        public object Content => option.Content;

        public bool CanMakeClick => canClick?.Invoke() ?? true;
        public void MakeClick() => click?.Invoke();

        private Func<bool> canClick;
        private System.Action click;

        public OptionViewModel(Option option, Func<bool> canClick, System.Action click)
        {
            this.option = option;
            this.canClick = canClick;
            this.click = click;
        }
    }
}
