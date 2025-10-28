// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Services.CFuncVal
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using System;

namespace RuntimeXNA.Services
{

    public class CFuncVal
    {
      public int intValue;
      public double doubleValue;

      public virtual int parse(string s)
      {
        try
        {
          if (s.Length >= 3)
          {
            if (s[0] == '0' && (s[1] == 'x' || s[2] == 'X'))
            {
              string str = s.Substring(2);
              try
              {
                this.intValue = Convert.ToInt32(str, 16 /*0x10*/);
              }
              catch (FormatException ex)
              {
                ex.GetType();
              }
              catch (ArgumentOutOfRangeException ex)
              {
                ex.GetType();
              }
              return 0;
            }
            if (s[0] == '0' && (s[1] == 'b' || s[2] == 'B'))
            {
              string str = s.Substring(2);
              try
              {
                this.intValue = Convert.ToInt32(str, 2);
              }
              catch (FormatException ex)
              {
                ex.GetType();
              }
              catch (ArgumentOutOfRangeException ex)
              {
                ex.GetType();
              }
              return 0;
            }
          }
          double a = 0.0;
          if (s.Length > 0)
          {
            try
            {
              a = double.Parse(s);
            }
            catch (FormatException ex)
            {
              ex.GetType();
            }
            catch (OverflowException ex)
            {
              ex.GetType();
            }
          }
          double num = Math.Round(a);
          if (a - num != 0.0)
          {
            this.doubleValue = a;
            return 1;
          }
          this.intValue = (int) a;
          return 0;
        }
        catch (FormatException ex)
        {
          ex.GetType();
        }
        this.intValue = 0;
        return 0;
      }
    }
}
