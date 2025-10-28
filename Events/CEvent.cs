// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Events.CEvent
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Params;

namespace RuntimeXNA.Events
{

    public abstract class CEvent
    {
      public const byte EVFLAGS_REPEAT = 1;
      public const byte EVFLAGS_DONE = 2;
      public const byte EVFLAGS_DEFAULT = 4;
      public const byte EVFLAGS_DONEBEFOREFADEIN = 8;
      public const byte EVFLAGS_NOTDONEINSTART = 16 /*0x10*/;
      public const byte EVFLAGS_ALWAYS = 32 /*0x20*/;
      public const byte EVFLAGS_BAD = 64 /*0x40*/;
      public const byte EVFLAG2_NOT = 1;
      public int evtCode;
      public short evtOi;
      public short evtOiList;
      public byte evtFlags;
      public byte evtFlags2;
      public byte evtDefType;
      public byte evtNParams;
      public CParam[] evtParams;
      public static byte EVFLAGS_BADOBJECT = 128 /*0x80*/;
      public static readonly byte EVFLAGS_DEFAULTMASK = 61;
    }
}
