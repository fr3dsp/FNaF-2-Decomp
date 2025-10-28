// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CND_CCOUNTER
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Conditions
{

    public class CND_CCOUNTER : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject hoPtr) => this.eva2(rhPtr);

      public override bool eva2(CRun rhPtr)
      {
        CObject cobject = rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
        int nselectedObjects = rhPtr.rhEvtProg.evtNSelectedObjects;
        CValue pValue1 = new CValue();
        for (; cobject != null; cobject = rhPtr.rhEvtProg.evt_NextObject())
        {
          pValue1.forceValue(((CCounter) cobject).cpt_GetValue());
          CValue eventExpressionAny = rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[0]);
          if (!CRun.compareTo(pValue1, eventExpressionAny, ((CParamExpression) this.evtParams[0]).comparaison))
          {
            --nselectedObjects;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        do
          ;
        while (cobject != null);
        return nselectedObjects != 0;
      }
    }
}
