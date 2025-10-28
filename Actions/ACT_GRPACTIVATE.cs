// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_GRPACTIVATE
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Actions
{

    public class ACT_GRPACTIVATE : CAct
    {
      public override void execute(CRun rhPtr)
      {
        int pointer = (int) ((PARAM_GROUPOINTER) this.evtParams[0]).pointer;
        PARAM_GROUP evtParam = (PARAM_GROUP) rhPtr.rhEvtProg.events[pointer].evgEvents[0].evtParams[0];
        bool flag = ((int) evtParam.grpFlags & 8) != 0;
        evtParam.grpFlags &= (short) -9;
        if (!flag)
          return;
        this.grpActivate(rhPtr, pointer);
      }

      internal virtual int grpActivate(CRun rhPtr, int evg)
      {
        CEventGroup ceventGroup1 = rhPtr.rhEvtProg.events[evg];
        if (((int) ((PARAM_GROUP) ceventGroup1.evgEvents[0].evtParams[0]).grpFlags & 4) == 0)
        {
          ceventGroup1.evgFlags &= (ushort) 49151 /*0xBFFF*/;
          ++evg;
          bool flag = false;
          int num = 1;
          while (true)
          {
            CEventGroup ceventGroup2 = rhPtr.rhEvtProg.events[evg];
            CEvent evgEvent = ceventGroup2.evgEvents[0];
            switch (evgEvent.evtCode)
            {
              case -1441793:
                if (num == 1)
                {
                  ceventGroup2.evgFlags &= (ushort) 49151 /*0xBFFF*/;
                  ceventGroup2.evgFlags &= (ushort) 65534;
                  break;
                }
                break;
              case -655361:
                --num;
                if (num == 0)
                {
                  ceventGroup2.evgFlags &= (ushort) 49151 /*0xBFFF*/;
                  flag = true;
                  ++evg;
                  break;
                }
                break;
              case -589825:
                PARAM_GROUP evtParam = (PARAM_GROUP) evgEvent.evtParams[0];
                if (num == 1)
                  evtParam.grpFlags &= (short) -5;
                if (((int) evtParam.grpFlags & 8) == 0)
                {
                  evg = this.grpActivate(rhPtr, evg);
                  continue;
                }
                ++num;
                break;
              default:
                if (num == 1)
                {
                  ceventGroup2.evgFlags &= (ushort) 49151 /*0xBFFF*/;
                  break;
                }
                break;
            }
            if (!flag)
              ++evg;
            else
              break;
          }
        }
        else
        {
          ++evg;
          bool flag = false;
          int num = 1;
          while (true)
          {
            switch (rhPtr.rhEvtProg.events[evg].evgEvents[0].evtCode)
            {
              case -655361:
                --num;
                if (num == 0)
                {
                  flag = true;
                  ++evg;
                  break;
                }
                break;
              case -589825:
                ++num;
                break;
            }
            if (!flag)
              ++evg;
            else
              break;
          }
        }
        return evg;
      }
    }
}
