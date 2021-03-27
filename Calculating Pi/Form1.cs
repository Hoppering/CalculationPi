using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Calculating_Pi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int round = 0;
        static int count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            round = Convert.ToInt32(textBox1.Text);
            count = Convert.ToInt32(textBox2.Text);

            object lockObject = new object();

            long num_steps = long.Parse(textBox3.Text);
            Stopwatch timer = Stopwatch.StartNew();

            switch (num_steps)
            {
                case 1:
                    num_steps = 2;
                    break;
                case 2:
                    num_steps = 4;
                    break;
                case 3:
                    num_steps = 8;
                    break;
                case 4:
                    num_steps = 16;
                    break;
                case 5:
                    num_steps = 32;
                    break;
                case 6:
                    num_steps = 64;
                    break;
                case 7:
                    num_steps = 100;
                    break;
                case 8:
                    num_steps = 1000;
                    break;
                case 9:
                    num_steps = 10000;
                    break;
                case 10:
                    num_steps = 100000;
                    break;
                case 11:
                    num_steps = 1000000;
                    break;
                case 12:
                    num_steps = 10000000;
                    break;
                case 13:
                    num_steps = 100000000;
                    break;
                case 14:
                    num_steps = 1000000000;
                    break;

            }

            double step = 1.0 / num_steps;
            double sum = 0;

            Parallel.For(1, num_steps, new ParallelOptions { MaxDegreeOfParallelism = count }, () => 0.0, (i, loopState, partialResult) =>
            {
                var x = (i - 0.5) * step;
                return partialResult + 4.0 / (1.0 + x * x);
            },
            localPartialSum =>
            {
                lock (lockObject)
                {
                    sum += localPartialSum;
                }
            });

            var pi = step * sum;
            timer.Stop();
            pi = Math.Round(pi, round);
            label3.Text = pi.ToString();
            label4.Text = timer.ElapsedMilliseconds.ToString();
        }
    }
}
