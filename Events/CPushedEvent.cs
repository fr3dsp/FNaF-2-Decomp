// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Events.CPushedEvent
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;

namespace RuntimeXNA.Events
{

    public class CPushedEvent
    {
      public int routine;
      public int code;
      public int param;
      public CObject pHo;
      public short oi;

      public CPushedEvent()
      {
      }

      public CPushedEvent(int r, int c, int p, CObject hoPtr, short o)
      {
        this.routine = r;
        this.code = c;
        this.param = p;
        this.pHo = hoPtr;
        this.oi = o;
      }
    }
}
