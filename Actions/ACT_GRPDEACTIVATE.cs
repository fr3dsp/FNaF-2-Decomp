// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_GRPDEACTIVATE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_GRPDEACTIVATE : CAct
    {
      public override void execute(CRun rhPtr)
      {
        int pointer = (int) ((PARAM_GROUPOINTER) this.evtParams[0]).pointer;
        PARAM_GROUP evtParam = (PARAM_GROUP) rhPtr.rhEvtProg.events[pointer].evgEvents[0].evtParams[0];
        bool flag = ((int) evtParam.grpFlags & 8) == 0;
        evtParam.grpFlags |= (short) 8;
        if (!flag || ((int) evtParam.grpFlags & 4) != 0)
          return;
        this.grpDeactivate(rhPtr, pointer);
      }

      internal virtual int grpDeactivate(CRun rhPtr, int evg)
      {
        CEventGroup ceventGroup1 = rhPtr.rhEvtProg.events[evg];
        PARAM_GROUP evtParam1 = (PARAM_GROUP) ceventGroup1.evgEvents[0].evtParams[0];
        ceventGroup1.evgFlags |= (ushort) 16384 /*0x4000*/;
        ++evg;
        bool flag1 = false;
        int num = 1;
        while (true)
        {
          CEventGroup ceventGroup2 = rhPtr.rhEvtProg.events[evg];
          CEvent evgEvent = ceventGroup2.evgEvents[0];
          switch (evgEvent.evtCode)
          {
            case -655361:
              --num;
              if (num == 0)
              {
                ceventGroup2.evgFlags |= (ushort) 16384 /*0x4000*/;
                flag1 = true;
                ++evg;
                break;
              }
              break;
            case -589825:
              PARAM_GROUP evtParam2 = (PARAM_GROUP) evgEvent.evtParams[0];
              bool flag2 = ((int) evtParam2.grpFlags & 4) == 0;
              if (num == 1)
                evtParam2.grpFlags |= (short) 4;
              if (flag2 && ((int) evtParam2.grpFlags & 8) == 0)
              {
                evg = this.grpDeactivate(rhPtr, evg);
                continue;
              }
              ++num;
              break;
            default:
              if (num == 1)
              {
                ceventGroup2.evgFlags |= (ushort) 16384 /*0x4000*/;
                break;
              }
              break;
          }
          if (!flag1)
            ++evg;
          else
            break;
        }
        return evg;
      }
    }
}
