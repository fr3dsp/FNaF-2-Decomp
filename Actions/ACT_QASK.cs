// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_QASK
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_QASK : CAct
    {
      public override void execute(CRun rhPtr)
      {
        if (this.evtOiList >= (short) 0)
        {
          this.qstCreate(rhPtr, this.evtOi);
        }
        else
        {
          if (this.evtOiList == (short) -1)
            return;
          CQualToOiList qualToOi = rhPtr.rhEvtProg.qualToOiList[(int) this.evtOiList & (int) short.MaxValue];
          for (int index = 0; index < qualToOi.qoiList.Length; index += 2)
            this.qstCreate(rhPtr, qualToOi.qoiList[index]);
        }
      }

      internal virtual void qstCreate(CRun rhPtr, short oi)
      {
        CCreate evtParam = (CCreate) this.evtParams[0];
        CPositionInfo pInfo = new CPositionInfo();
        if (!evtParam.read_Position(rhPtr, 16 /*0x10*/, pInfo))
          return;
        rhPtr.f_CreateObject(evtParam.cdpHFII, oi, pInfo.x, pInfo.y, pInfo.dir, (short) 0, rhPtr.rhFrame.nLayers - 1, -1);
      }
    }
}
