using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Observer
{
    public class ObserverModel : IObserver<Email>
    {
        private IDisposable _disposer;
        public ObserverModel(IObservable<Email> controladorEmail)
        {
            _disposer = controladorEmail.Subscribe(this);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Email value)
        { 
            
      }
        public void Dispose()
        {
            _disposer.Dispose();
        }
    }
}