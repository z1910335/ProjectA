

using System;

public class SimPendulum
{
    private double L;   // Pendulum Length
    private double g;   // grav field strength
    int n; 
    double[] x;     // array of states
    double[] xA;    // intermediate state
    double[][] f;   // array of array of slopes


    ///////////////////////////////////////////////////////////////////////
    // Constructor
    //////////////////////////////////////////////////////////////////////
    public SimPendulum()
    {
        L = 0.9;
        g = 9.81;

        n = 2;
        x = new double[n];
        xA = new double[n];
        f = new double[2][];
        f[0] = new double[n];
        f[1] = new double[n];

        x[0] = 1.0;     // default pendulum angle
        x[1] = 0.0;     // default rotation rate

        //Console.WriteLine("In Constructor"); // Constructor Initializes Things
    }

    ////////////////////////////////////////////////////////////////////
    /// StepEuler: completes one Euler step in solving equations of motion
    ///////////////////////////////////////////////////////////////////
    public void StepEuler(double time, double dt)
    {
        int i;

        RHSFuncPendulum(x, time, f[0]);
        for (i=0; i<n; ++i)
        {
            x[i] += f[0][i]*dt;
        }
    }

    ////////////////////////////////////////////////////////////////////
    /// StepRK2: completes one RK2 step in solving equations of motion
    ///////////////////////////////////////////////////////////////////
    public void StepRK2(double time, double dt)
    {
        int i;

        RHSFuncPendulum(x, time, f[0]);
        for (i=0; i<n; ++i)
        {
            xA[i] = x[i] + f[0][i]*dt;
        }

        RHSFuncPendulum(xA, time + dt, f[1]);
        for (i=0; i<n; ++i)
        {
            x[i] = x[i] + 0.5*(f[0][i] + f[1][i])*dt;
        }
    }




    ////////////////////////////////////////////////////////////////////
    /// RHSFuncPendulum
    ///////////////////////////////////////////////////////////////////

   private void RHSFuncPendulum(double[] xx, double t, double[] ff) 
    { //shield this function by making it private (protect from naive users!)
        ff[0] = xx[1];
        ff[1] = -g/L * Math.Sin(xx[0]);
    }

    public double Angle
    {
        get{
            return(x[0]);
        }
        set{
            x[0] = value;
        }
    }

    public void TestFunc()
    {
        //Console.WriteLine("Inside TestFunc");
    }
}