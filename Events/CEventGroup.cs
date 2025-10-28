// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Events.CEventGroup
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Actions;
using RuntimeXNA.Application;
using RuntimeXNA.Conditions;

namespace RuntimeXNA.Events
{

    public class CEventGroup
    {
      public const ushort EVGFLAGS_ONCE = 1;
      public const ushort EVGFLAGS_NOTALWAYS = 2;
      public const ushort EVGFLAGS_REPEAT = 4;
      public const ushort EVGFLAGS_NOMORE = 8;
      public const ushort EVGFLAGS_SHUFFLE = 16 /*0x10*/;
      public const ushort EVGFLAGS_EDITORMARK = 32 /*0x20*/;
      public const ushort EVGFLAGS_UNDOMARK = 64 /*0x40*/;
      public const ushort EVGFLAGS_COMPLEXGROUP = 128 /*0x80*/;
      public const ushort EVGFLAGS_BREAKPOINT = 256 /*0x0100*/;
      public const ushort EVGFLAGS_ALWAYSCLEAN = 512 /*0x0200*/;
      public const ushort EVGFLAGS_ORINGROUP = 1024 /*0x0400*/;
      public const ushort EVGFLAGS_STOPINGROUP = 2048 /*0x0800*/;
      public const ushort EVGFLAGS_ORLOGICAL = 4096 /*0x1000*/;
      public const ushort EVGFLAGS_GROUPED = 8192 /*0x2000*/;
      public const ushort EVGFLAGS_INACTIVE = 16384 /*0x4000*/;
      public byte evgNCond;
      public byte evgNAct;
      public ushort evgFlags;
      public ushort evgInhibit;
      public short evgInhibitCpt;
      public ushort evgIdentifier;
      public CEvent[] evgEvents;
      public static ushort EVGFLAGS_NOGOOD = 32768 /*0x8000*/;
      public static readonly ushort EVGFLAGS_LIMITED = 30;
      public static readonly ushort EVGFLAGS_DEFAULTMASK = 8448;

      public static CEventGroup create(CRunApp app)
      {
        int filePointer = app.file.getFilePointer();
        short num1 = app.file.readAShort();
        CEventGroup ceventGroup = new CEventGroup();
        ceventGroup.evgNCond = app.file.readByte();
        ceventGroup.evgNAct = app.file.readByte();
        ceventGroup.evgFlags = (ushort) app.file.readAShort();
        ceventGroup.evgInhibit = (ushort) app.file.readAShort();
        ceventGroup.evgInhibitCpt = app.file.readAShort();
        ceventGroup.evgIdentifier = (ushort) app.file.readAShort();
        app.file.skipBytes(2);
        ceventGroup.evgEvents = new CEvent[(int) ceventGroup.evgNCond + (int) ceventGroup.evgNAct];
        int num2 = 0;
        for (int index = 0; index < (int) ceventGroup.evgNCond; ++index)
          ceventGroup.evgEvents[num2++] = (CEvent) CCnd.create(app);
        for (int index = 0; index < (int) ceventGroup.evgNAct; ++index)
          ceventGroup.evgEvents[num2++] = (CEvent) CAct.create(app);
        app.file.seek(filePointer - (int) num1);
        return ceventGroup;
      }
    }
}
