using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ILogicPlayer
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }

        public static ILogicPlayer Create(string name, float x, float y, float speed)
        {
            return new LogicPlayer(name, x, y, speed);
        }
    }
}
