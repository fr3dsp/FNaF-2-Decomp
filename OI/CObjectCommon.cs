// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.OI.CObjectCommon
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Animations;
using RuntimeXNA.Banks;
using RuntimeXNA.Movements;
using RuntimeXNA.Services;
using RuntimeXNA.Values;

namespace RuntimeXNA.OI
{

    public class CObjectCommon : COC
    {
      public const int OEFLAG_DISPLAYINFRONT = 1;
      public const int OEFLAG_BACKGROUND = 2;
      public const int OEFLAG_BACKSAVE = 4;
      public const int OEFLAG_RUNBEFOREFADEIN = 8;
      public const int OEFLAG_MOVEMENTS = 16 /*0x10*/;
      public const int OEFLAG_ANIMATIONS = 32 /*0x20*/;
      public const int OEFLAG_TABSTOP = 64 /*0x40*/;
      public const int OEFLAG_WINDOWPROC = 128 /*0x80*/;
      public const int OEFLAG_VALUES = 256 /*0x0100*/;
      public const int OEFLAG_SPRITES = 512 /*0x0200*/;
      public const int OEFLAG_INTERNALBACKSAVE = 1024 /*0x0400*/;
      public const int OEFLAG_SCROLLINGINDEPENDANT = 2048 /*0x0800*/;
      public const int OEFLAG_QUICKDISPLAY = 4096 /*0x1000*/;
      public const int OEFLAG_NEVERKILL = 8192 /*0x2000*/;
      public const int OEFLAG_NEVERSLEEP = 16384 /*0x4000*/;
      public const int OEFLAG_MANUALSLEEP = 32768 /*0x8000*/;
      public const int OEFLAG_TEXT = 65536 /*0x010000*/;
      public const int OEFLAG_DONTCREATEATSTART = 131072 /*0x020000*/;
      public const short OCFLAGS2_DONTSAVEBKD = 1;
      public const short OCFLAGS2_SOLIDBKD = 2;
      public const short OCFLAGS2_COLBOX = 4;
      public const short OCFLAGS2_VISIBLEATSTART = 8;
      public const short OCFLAGS2_OBSTACLESHIFT = 4;
      public const short OCFLAGS2_OBSTACLEMASK = 48 /*0x30*/;
      public const short OCFLAGS2_OBSTACLE_SOLID = 16 /*0x10*/;
      public const short OCFLAGS2_OBSTACLE_PLATFORM = 32 /*0x20*/;
      public const short OCFLAGS2_OBSTACLE_LADDER = 48 /*0x30*/;
      public const short OCFLAGS2_AUTOMATICROTATION = 64 /*0x40*/;
      public const short OEPREFS_BACKSAVE = 1;
      public const short OEPREFS_SCROLLINGINDEPENDANT = 2;
      public const short OEPREFS_QUICKDISPLAY = 4;
      public const short OEPREFS_SLEEP = 8;
      public const short OEPREFS_LOADONCALL = 16 /*0x10*/;
      public const short OEPREFS_GLOBAL = 32 /*0x20*/;
      public const short OEPREFS_BACKEFFECTS = 64 /*0x40*/;
      public const short OEPREFS_KILL = 128 /*0x80*/;
      public const short OEPREFS_INKEFFECTS = 256 /*0x0100*/;
      public const short OEPREFS_TRANSITIONS = 512 /*0x0200*/;
      public const short OEPREFS_FINECOLLISIONS = 1024 /*0x0400*/;
      public int ocOEFlags;
      public short[] ocQualifiers;
      public short ocFlags2;
      public short ocOEPrefs;
      public int ocIdentifier;
      public int ocBackColor;
      public CRect ocFadeIn;
      public CRect ocFadeOut;
      public CMoveDefList ocMovements;
      public CDefValues ocValues;
      public CDefStrings ocStrings;
      public CAnimHeader ocAnimations;
      public CDefCounters ocCounters;
      public CDefObject ocObject;
      public byte[] ocExtension;
      public int ocVersion;
      public int ocID;
      public int ocPrivate;
      public int ocFadeInLength;
      public int ocFadeOutLength;

      public override void load(CFile file, short type)
      {
        int filePointer = file.getFilePointer();
        this.ocQualifiers = new short[8];
        file.skipBytes(4);
        int num1 = (int) file.readAShort();
        int num2 = (int) file.readAShort();
        file.skipBytes(2);
        int num3 = (int) file.readAShort();
        int num4 = (int) file.readAShort();
        file.skipBytes(2);
        this.ocOEFlags = file.readAInt();
        for (int index = 0; index < 8; ++index)
          this.ocQualifiers[index] = file.readAShort();
        int num5 = (int) file.readAShort();
        int num6 = (int) file.readAShort();
        int num7 = (int) file.readAShort();
        this.ocFlags2 = file.readAShort();
        this.ocOEPrefs = file.readAShort();
        this.ocIdentifier = file.readAInt();
        this.ocBackColor = file.readAColor();
        int num8 = file.readAInt();
        int num9 = file.readAInt();
        if (num1 != 0)
        {
          file.seek(filePointer + num1);
          this.ocMovements = new CMoveDefList();
          this.ocMovements.load(file);
        }
        if (num6 != 0)
        {
          file.seek(filePointer + num6);
          this.ocValues = new CDefValues();
          this.ocValues.load(file);
        }
        if (num7 != 0)
        {
          file.seek(filePointer + num7);
          this.ocStrings = new CDefStrings();
          this.ocStrings.load(file);
        }
        if (num2 != 0)
        {
          file.seek(filePointer + num2);
          this.ocAnimations = new CAnimHeader();
          this.ocAnimations.load(file);
        }
        if (num3 != 0)
        {
          file.seek(filePointer + num3);
          this.ocObject = (CDefObject) new CDefCounter();
          this.ocObject.load(file);
        }
        if (num5 != 0)
        {
          file.seek(filePointer + num5);
          int num10 = file.readAInt();
          file.skipBytes(4);
          this.ocVersion = file.readAInt();
          this.ocID = file.readAInt();
          this.ocPrivate = file.readAInt();
          int length = num10 - 20;
          if (length != 0)
          {
            this.ocExtension = new byte[length];
            file.read(this.ocExtension);
          }
        }
        if (num8 != 0)
        {
          file.seek(filePointer + num8);
          file.skipBytes(8);
          this.ocFadeInLength = file.readAInt();
        }
        if (num9 != 0)
        {
          file.seek(filePointer + num9);
          file.skipBytes(8);
          this.ocFadeOutLength = file.readAInt();
        }
        if (num4 == 0)
          return;
        file.seek(filePointer + num4);
        switch (type)
        {
          case 3:
          case 4:
            this.ocObject = (CDefObject) new CDefTexts();
            this.ocObject.load(file);
            break;
          case 5:
          case 6:
          case 7:
            this.ocCounters = new CDefCounters();
            this.ocCounters.load(file);
            break;
          case 9:
            this.ocObject = (CDefObject) new CDefCCA();
            this.ocObject.load(file);
            break;
        }
      }

      public override void enumElements(IEnum enumImages, IEnum enumFonts)
      {
        if (this.ocAnimations != null)
          this.ocAnimations.enumElements(enumImages);
        if (this.ocObject != null)
          this.ocObject.enumElements(enumImages, enumFonts);
        if (this.ocCounters == null)
          return;
        this.ocCounters.enumElements(enumImages, enumFonts);
      }
    }
}
