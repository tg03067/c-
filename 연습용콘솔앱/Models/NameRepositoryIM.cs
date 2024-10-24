using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 연습용콘솔앱.Models
{
    public class NameRepositoryIM : INameRepository
    {
        private Dictionary<int, Name> _namelist;

        public NameRepositoryIM() 
        { 
            _namelist = new Dictionary<int, Name>();
            new List<Name>
            {
                new Name(5, "aaaa"),
                new Name(6, "bbbb"),
                new Name(7, "cccc")
            }.ForEach(Name => Add(Name));
        }

        public Name this[int i] => _namelist.ContainsKey(i)? _namelist[i] : null;
        public IEnumerable<Name> Names => _namelist.Values;
        public Name Add(Name newName)
        {
            if (newName.id == 0)
            {
                int key = _namelist.Count;

                while (_namelist.ContainsKey(key)) key++;
                newName.id = key;
            }
            _namelist[newName.id] = newName;

            return newName;
        }
        public void Delete(int i) => _namelist.Remove(i);
        public Name Update(Name name) => Add(name);
    }
}
