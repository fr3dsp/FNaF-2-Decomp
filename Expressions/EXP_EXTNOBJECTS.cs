// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.EXP_EXTNOBJECTS
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Expressions
{

    public class EXP_EXTNOBJECTS : CExpOi
    {
      public override void evaluate(CRun rhPtr)
      {
        short oiList = this.oiList;
        if (oiList >= (short) 0)
        {
          CObjInfo rhOi = rhPtr.rhOiList[(int) oiList];
          rhPtr.getCurrentResult().forceInt(rhOi.oilNObjects);
        }
        else
        {
          int num = 0;
          if (oiList != (short) -1)
          {
            CQualToOiList qualToOi = rhPtr.rhEvtProg.qualToOiList[(int) oiList & (int) short.MaxValue];
            for (int index = 0; index < qualToOi.qoiList.Length; index += 2)
            {
              CObjInfo rhOi = rhPtr.rhOiList[(int) qualToOi.qoiList[index + 1]];
              num += rhOi.oilNObjects;
            }
          }
          rhPtr.getCurrentResult().forceInt(num);
        }
      }
    }
}
