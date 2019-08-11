using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiManager.WPF.UI.EventModels;

namespace WebApiManager.WPF.UI.ViewModels
{
    public class ShellViewModel : Conductor<Object>, IHandle<LogOnEvent>
    {        
        private IEventAggregator _events;

        private SimpleContainer _container;
        public ShellViewModel(IEventAggregator events, SimpleContainer container)
        {
            _events = events;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {               
            ActivateItem(_container.GetInstance<SalesViewModel>());
        }
    }
}
