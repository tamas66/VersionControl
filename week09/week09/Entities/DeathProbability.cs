﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week09.Entities
{
    public class DeathProbability
    {
        public int Age { get; set; }
        public int NbrOfChildren { get; set; }
        public double Probability { get; set; }
        public Gender Gender { get; set; }
    }
}
