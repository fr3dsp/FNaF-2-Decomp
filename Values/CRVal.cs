// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Values.CRVal
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.OI;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Values
{

    public class CRVal
    {
      public const int VALUES_NUMBEROF_ALTERABLE = 26;
      public const int STRINGS_NUMBEROF_ALTERABLE = 10;
      public int rvValueFlags;
      public CValue[] rvValues;
      public string[] rvStrings;

      public void init(CObject ho, CObjectCommon ocPtr, CCreateObjectInfo cob)
      {
        this.rvValueFlags = 0;
        this.rvValues = new CValue[26];
        this.rvStrings = new string[10];
        for (int index = 0; index < 26; ++index)
          this.rvValues[index] = (CValue) null;
        for (int index = 0; index < 10; ++index)
          this.rvStrings[index] = (string) null;
        if (ocPtr.ocValues != null)
        {
          for (int n = 0; n < (int) ocPtr.ocValues.nValues; ++n)
            this.getValue(n).forceInt(ocPtr.ocValues.values[n]);
        }
        if (ocPtr.ocStrings == null)
          return;
        for (int index = 0; index < (int) ocPtr.ocStrings.nStrings; ++index)
          this.rvStrings[index] = ocPtr.ocStrings.strings[index];
      }

      public void kill(bool bFast)
      {
        for (int index = 0; index < 26; ++index)
          this.rvValues[index] = (CValue) null;
        for (int index = 0; index < 10; ++index)
          this.rvStrings[index] = (string) null;
      }

      public CValue getValue(int n)
      {
        if (this.rvValues[n] == null)
          this.rvValues[n] = new CValue();
        return this.rvValues[n];
      }

      public string getString(int n)
      {
        if (this.rvStrings[n] == null)
          this.rvStrings[n] = "";
        return this.rvStrings[n];
      }

      public void setString(int n, string s) => this.rvStrings[n] = s;
    }
}
