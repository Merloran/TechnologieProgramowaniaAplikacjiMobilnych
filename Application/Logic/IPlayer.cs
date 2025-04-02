using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ILogicPlayer
    {
        public string Name { get; }
        public float X { get; }
        public float Y { get; }
    }
}
