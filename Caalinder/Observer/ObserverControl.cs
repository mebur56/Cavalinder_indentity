using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Observer
{
    public class Email
    {
        public string email { get; set; }
    }
    public class ObserverControl : IObservable<Email>
    {
        public List<IObserver<Email>> _user;
        public Email _email;

        public ObserverControl(Email email)
        {
            _user = new List<IObserver<Email>>();
            _email = email;
        }

        public IDisposable Subscribe(IObserver<Email> user)
        {
            if (!_user.Contains(user))
            {
                _user.Add(user);
            }
            return new Disposer(_user, user);
        }
    }
}