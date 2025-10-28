// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_CHOOSEVALUE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_CHOOSEVALUE : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        int num = 0;
        for (CObject cobject = rhPtr.rhEvtProg.evt_FirstObjectFromType((short) -1); cobject != null; cobject = rhPtr.rhEvtProg.evt_NextObjectFromType())
        {
          ++num;
          int n = this.evtParams[0].code != (short) 53 ? (int) ((PARAM_SHORT) this.evtParams[0]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
          CValue eventExpressionAny = rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[1]);
          if (cobject.rov != null)
          {
            CValue pValue1 = new CValue(cobject.rov.getValue(n));
            short comparaison = ((CParamExpression) this.evtParams[1]).comparaison;
            if (!CRun.compareTo(pValue1, eventExpressionAny, comparaison))
            {
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
              --num;
            }
          }
        }
        return num != 0;
      }
    }
}
