using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 연습용콘솔앱.Models
{
    public interface INameRepository
    {
        IEnumerable<Name> Names { get; }
        Name this[int i ] { get; }
        Name Add(Name newName);
        Name Update(Name name);
        void Delete(int i);
    }
}
