// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CCndExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Input;
using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Conditions
{

    public class CCndExtension : CCnd
    {
      public override bool eva1(CRun rhPtr, CObject pHo)
      {
        if (pHo == null)
          return this.eva2(rhPtr);
        CExtension cextension = (CExtension) pHo;
        pHo.hoFlags |= (short) 2;
        int num = (int) -(short) (this.evtCode >> 16 /*0x10*/ & (int) ushort.MaxValue) - 80 /*0x50*/ - 1;
        if (!cextension.condition(num, this))
          return false;
        rhPtr.rhEvtProg.evt_AddCurrentObject(pHo);
        return true;
      }

      public override bool eva2(CRun rhPtr)
      {
        CObject cobject = rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
        int nselectedObjects = rhPtr.rhEvtProg.evtNSelectedObjects;
        int num = (int) -(short) (this.evtCode >> 16 /*0x10*/ & (int) ushort.MaxValue) - 80 /*0x50*/ - 1;
        for (; cobject != null; cobject = rhPtr.rhEvtProg.evt_NextObject())
        {
          CExtension cextension = (CExtension) cobject;
          cobject.hoFlags &= (short) -3;
          if (cextension.condition(num, this))
          {
            if (((int) this.evtFlags2 & 1) != 0)
            {
              --nselectedObjects;
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
            }
          }
          else if (((int) this.evtFlags2 & 1) == 0)
          {
            --nselectedObjects;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        return nselectedObjects != 0;
      }

      public virtual PARAM_OBJECT getParamObject(CRun rhPtr, int num)
      {
        return (PARAM_OBJECT) this.evtParams[num];
      }

      public virtual int getParamTime(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 2 ? ((PARAM_TIME) this.evtParams[num]).timer : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual short getParamBorder(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual short getParamAltValue(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual short getParamDirection(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual int getParamAnimation(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 10 ? (int) ((PARAM_SHORT) this.evtParams[num]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual short getParamPlayer(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual PARAM_EVERY getParamEvery(CRun rhPtr, int num)
      {
        return (PARAM_EVERY) this.evtParams[num];
      }

      public virtual Keys getParamKey(CRun rhPtr, int num) => ((PARAM_KEY) this.evtParams[num]).key;

      public virtual int getParamSpeed(CRun rhPtr, int num)
      {
        return rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual PARAM_POSITION getParamPosition(CRun rhPtr, int num)
      {
        return (PARAM_POSITION) this.evtParams[num];
      }

      public virtual short getParamJoyDirection(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual int getParamExpression(CRun rhPtr, int num)
      {
        return rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual int getParamColour(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 24 ? ((PARAM_COLOUR) this.evtParams[num]).color : CServices.swapRGB(rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]));
      }

      public virtual short getParamFrame(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual int getParamNewDirection(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 29 ? (int) ((PARAM_SHORT) this.evtParams[num]).value : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual short getParamClick(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual PARAM_PROGRAM getParamProgram(CRun rhPtr, int num)
      {
        return (PARAM_PROGRAM) this.evtParams[num];
      }

      public virtual string getParamFilename(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 40 ? ((PARAM_STRING) this.evtParams[num]).pString : rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[num]);
      }

      public virtual string getParamExpString(CRun rhPtr, int num)
      {
        return rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[num]);
      }

      public virtual string getParamFilename2(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 63 /*0x3F*/ ? ((PARAM_STRING) this.evtParams[num]).pString : rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[num]);
      }

      public virtual bool compareValues(CRun rhPtr, int num, CValue value_Renamed)
      {
        CValue eventExpressionAny = rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[num]);
        short comparaison = ((CParamExpression) this.evtParams[num]).comparaison;
        return CRun.compareTo(value_Renamed, eventExpressionAny, comparaison);
      }

      public virtual bool compareTime(CRun rhPtr, int num, int t)
      {
        PARAM_CMPTIME evtParam = (PARAM_CMPTIME) this.evtParams[num];
        CValue pValue2 = new CValue(evtParam.timer);
        short comparaison = evtParam.comparaison;
        return CRun.compareTo(new CValue(t), pValue2, comparaison);
      }
    }
}
