// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.ACT_STARTLOOP
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Events;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Actions
{

    public class ACT_STARTLOOP : CAct
    {
      public override void execute(CRun rhPtr)
      {
        string expressionString = rhPtr.get_EventExpressionString((CParamExpression) this.evtParams[0]);
        if (expressionString.Length == 0)
          return;
        int num = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[1]);
        int index = 0;
        while (index < rhPtr.rh4FastLoops.size() && string.Compare(((CLoop) rhPtr.rh4FastLoops.get(index)).name, expressionString, StringComparison.OrdinalIgnoreCase) != 0)
          ++index;
        if (index == rhPtr.rh4FastLoops.size())
        {
          CLoop o = new CLoop();
          rhPtr.rh4FastLoops.add((object) o);
          index = rhPtr.rh4FastLoops.size() - 1;
          o.name = expressionString;
          o.flags = (short) 0;
        }
        CLoop cloop = (CLoop) rhPtr.rh4FastLoops.get(index);
        cloop.flags &= (short) -2;
        bool flag = false;
        if (num < 0)
        {
          flag = true;
          num = 10;
        }
        string rh4CurrentFastLoop = rhPtr.rh4CurrentFastLoop;
        bool rh2ActionLoop = rhPtr.rhEvtProg.rh2ActionLoop;
        int rh2ActionLoopCount = rhPtr.rhEvtProg.rh2ActionLoopCount;
        CEventGroup rhEventGroup = rhPtr.rhEvtProg.rhEventGroup;
        for (cloop.index = 0; cloop.index < num; ++cloop.index)
        {
          rhPtr.rh4CurrentFastLoop = cloop.name;
          rhPtr.rhEvtProg.rh2ActionOn = false;
          rhPtr.rhEvtProg.handle_GlobalEvents(-983041);
          if (((int) cloop.flags & 1) == 0)
          {
            if (flag)
              num = cloop.index + 10;
          }
          else
            break;
        }
        rhPtr.rhEvtProg.rhEventGroup = rhEventGroup;
        rhPtr.rhEvtProg.rh2ActionLoopCount = rh2ActionLoopCount;
        rhPtr.rhEvtProg.rh2ActionLoop = rh2ActionLoop;
        rhPtr.rh4CurrentFastLoop = rh4CurrentFastLoop;
        rhPtr.rhEvtProg.rh2ActionOn = true;
        rhPtr.rh4FastLoops.remove(index);
      }
    }
}
