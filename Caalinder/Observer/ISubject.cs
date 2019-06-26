using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caalinder.Observer
{
    public interface ISubject
    {
        void Registrar(IObserver observer);
        void Remover(IObserver observer);
        void EnviarEmail();
    }

    public interface IObserver
    {
        void ReceberEmail();
    }
}
