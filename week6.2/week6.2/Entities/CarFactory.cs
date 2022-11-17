using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week6._2.Abstractions;

namespace week6._2.Entities
{
    internal class CarFactory: IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
