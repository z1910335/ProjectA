//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;
using System.IO;
using System.Reflection.PortableExecutable;

public class PendSim : Simulator
{
    double L;   // original pendulum length
    double l;   // new pendulum length
    double m;   // mass
    double k;   // spring constant
    double KE;  // Kinetic Energy
    double PE;  // Potential Energy
    double Tot; // Total Energy
    double V;   // Velocity
    double F;   // Spring Force Magnitude
    
    public PendSim() : base(6) //changed from 2 to 6.. went from 2 to 6 ICs.
    {

        // In a world... where angles mattered...
        // x[0] = 1.0;     // default pendulum angle
        // x[1] = 0.0;     // default rotation rate

        // ICs
        x[0] = -0.7;  // this is x. 
        x[1] = 0.0;   // this is x prime.
        x[2] = -0.2;  // this is y
        x[3] = 0.0;   // this is y prime
        x[4] = 0.5;   // this is z
        x[5] = 0.9;   // this is z prime

        SetRHSFunc(RHSFuncPendulum);
    }

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------
    private void RHSFuncPendulum(double[] xx, double t, double[] ff)
    {   //Make our lives easier:
        double X = xx[0];
        double Xdot = xx[1];
        double Y = xx[2];
        double Ydot = xx[3];
        double Z = xx[4];
        double Zdot = xx[5];

        //Establishing some variable values
        l = Math.Sqrt((X*X) + (Y*Y) + (Z*Z));
        F = k * (L - l);
        L = 0.9;
        k = 90;
        m = 1.4;

        //Establishing the Unit Vector
        double xUnit = X / l;
        double yUnit = Y / l;
        double zUnit = Z / l;

        //Establishing the Force Vector
        double Fx = xUnit * F;
        double Fy = yUnit * F;
        double Fz = zUnit * F;

        //Equations of Motion
        ff[0] = Xdot;
        ff[1] = (Fx / m);
        ff[2] = Ydot;
        ff[3] = (Fy / m) - g;
        ff[4] = Zdot;
        ff[5] = (Fz / m);

        // Mass Velocity
        double V = Math.Sqrt((Xdot*Xdot) + (Ydot*Ydot) + (Zdot*Zdot));
        //KE, PE, and Tot
        KE = (0.5) * m * V * V;
        PE = ((0.5) * k * (L - l)*(L - l)) + (m * g * Y);
        Tot = KE + PE;

    }

    //--------------------------------------------------------------------
    // Getters & Setters
    //--------------------------------------------------------------------

        public double X
    {
        get{
            return(x[0]);
        }

        set{
            x[0] = value;
        }
    }
        public double Y
    {
        get{
            return(x[2]);
        }

        set{
            x[2] = value;
        }
    }
        public double Z
    {
        get{
            return(x[4]);
        }

        set{
            x[4] = value;
        }
    }
        public double Xdot
    {
        get{
            return(x[1]);
        }

        set{
            x[1] = value;
        }
    }
        public double Ydot
    {
        get{
            return(x[3]);
        }

        set{
            x[3] = value;
        }
    }
        public double Zdot
    {
        get{
            return(x[5]);
        }

        set{
            x[5] = value;
        }
    }
    public double KEGet
    {
        get{
            return(KE);
        }
        set{
            KE = value;
        }
    }
    public double PEGet
    {
        get{
            return(PE);
        }
        set{
            PE = value;
        }
    }
        public double TotGet
    {
        get{
            return(Tot);
        }
        set{
            Tot = value;
        }
    }

}
