using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Curves
{
    class BezierCurve
    {

        public void Bezier2D(double[] b, int cpts, double[] p)
        {
            if (b.Length == 8)
            {
                int icount;
                double step, t;
                icount = 0;
                t = 0.0;
                step = (double)1.0 / (cpts);

                for (int k = 0; k != cpts; k++)
                {
                    if ((1.0 - t) < 5e-6)
                        t = 1.0;
                    p[icount] = Math.Pow((1 - t), 3) * b[0] + 3 * Math.Pow((1 - t), 2) * t * b[2] + 3 * Math.Pow(t, 2) * (1 - t) * b[4] + Math.Pow(t, 3) * b[6];
                    p[icount + 1] = Math.Pow((1 - t), 3) * b[1] + 3 * Math.Pow((1 - t), 2) * t * b[3] + 3 * Math.Pow(t, 2) * (1 - t) * b[5] + Math.Pow(t, 3) * b[7];
                    icount = icount + 2;
                    t += step;
                }
            }
            else
            {
                if (b.Length == 6)
                {
                    int icount;
                    double step, t;
                    icount = 0;
                    t = 0.0;
                    step = (double)1.0 / (cpts);
                    for (int k = 0; k != cpts; k++)
                    {
                        if ((1.0 - t) < 5e-6)
                            t = 1.0;
                        p[icount] = Math.Pow((1 - t), 2) * b[0] + 2 * (1 - t) * t * b[2] + Math.Pow(t, 2) * b[4];
                        p[icount + 1] = Math.Pow((1 - t), 2) * b[1] + 2 * (1 - t) * t * b[3] + Math.Pow(t, 2) * b[5];
                        icount = icount + 2;
                        t += step;
                    }
                }
                else
                {
                    if (b.Length == 4)
                    {
                        int icount;
                        double step, t;
                        icount = 0;
                        t = 0.0;
                        step = (double)1.0 / (cpts);
                        for (int k = 0; k != cpts; k++)
                        {
                            if ((1.0 - t) < 5e-6)
                                t = 1.0;
                            p[icount] = (1 - t) * b[0] + t * b[2];
                            p[icount + 1] = (1 - t) * b[1] + t * b[3];
                            icount = icount + 2;
                            t += step;
                        }
                    }
                }

            }

        }
    }
}
