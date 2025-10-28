// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_ISOBSTACLE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_ISOBSTACLE : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        int eventExpressionInt1 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
        int eventExpressionInt2 = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        return rhPtr.rhFrame.bkdCol_TestPoint(eventExpressionInt1 - rhPtr.rhWindowX, eventExpressionInt2 - rhPtr.rhWindowY, -1, 0) ? this.negaTRUE() : this.negaFALSE();
      }
    }
}
