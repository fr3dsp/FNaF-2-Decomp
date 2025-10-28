// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.CActExtension
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework.Input;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;

namespace RuntimeXNA.Actions
{

    public class CActExtension : CAct
    {
      public override void execute(CRun rhPtr)
      {
        CObject actionObjects = rhPtr.rhEvtProg.get_ActionObjects((CAct) this);
        if (actionObjects == null)
          return;
        int num = (this.evtCode >> 16 /*0x10*/ & (int) ushort.MaxValue) - 80 /*0x50*/;
        ((CExtension) actionObjects).action(num, this);
      }

      public virtual CObject getParamObject(CRun rhPtr, int num)
      {
        return rhPtr.rhEvtProg.get_ParamActionObjects(((PARAM_OBJECT) this.evtParams[num]).oiList, (CAct) this);
      }

      public virtual int getParamTime(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 2 ? ((PARAM_TIME) this.evtParams[num]).timer : rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[num]);
      }

      public virtual short getParamBorder(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual short getParamShort(CRun rhPtr, int num)
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

      public virtual PARAM_CREATE getParamCreate(CRun rhPtr, int num)
      {
        return (PARAM_CREATE) this.evtParams[num];
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

      public virtual CPositionInfo getParamPosition(CRun rhPtr, int num)
      {
        CPosition evtParam = (CPosition) this.evtParams[num];
        CPositionInfo pInfo = new CPositionInfo();
        evtParam.read_Position(rhPtr, 0, pInfo);
        return pInfo;
      }

      public virtual short getParamJoyDirection(CRun rhPtr, int num)
      {
        return ((PARAM_SHORT) this.evtParams[num]).value;
      }

      public virtual PARAM_SHOOT getParamShoot(CRun rhPtr, int num)
      {
        return (PARAM_SHOOT) this.evtParams[num];
      }

      public virtual PARAM_ZONE getParamZone(CRun rhPtr, int num) => (PARAM_ZONE) this.evtParams[num];

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

      public virtual double getParamExpDouble(CRun rhPtr, int num)
      {
        return rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[num]).getDouble();
      }

      public virtual string getParamFilename2(CRun rhPtr, int num)
      {
        return this.evtParams[num].code == (short) 63 /*0x3F*/ ? ((PARAM_STRING) this.evtParams[num]).pString : rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[num]);
      }

      public virtual CFile getParamExtension(CRun rhPtr, int num) => (CFile) null;
    }
}
