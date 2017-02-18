using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Particle_Fever
{
    public class Timer
    {
        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        private long _start;
        private long _stop;
        private long _frequency;
        private Decimal _multiplier = new decimal(1.0e9);

        public Timer()
        {
            if(QueryPerformanceFrequency(out _frequency) == false)
            {
                throw new Win32Exception();
            }
        }

        public void start()
        {
            QueryPerformanceCounter(out _start);
        }

        public void stop()
        {
            QueryPerformanceCounter(out _stop);
        }

        public double duration(int iterations = 1)
        {
            return ((((double)(_stop - _start) * (double)_multiplier) / (double)_frequency) / iterations);
        }
    }
}
