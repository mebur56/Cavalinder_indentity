using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Caalinder.Observer
{
    public class ObserverModel
    {
        public string _email;
        public ObserverModel(string email)
        {
            this._email = email;
        }

        public void Update(int like)
        {
            if(like%5==0)
            {
                //send email
            }
        }
       
    }
}