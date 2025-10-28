// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.CParam
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.Params
{

    public abstract class CParam
    {
      public const short PARAM_EXPRESSION = 22;
      public short code;

      public static CParam create(CRunApp app)
      {
        long filePointer = (long) app.file.getFilePointer();
        CParam cparam = (CParam) null;
        short num1 = app.file.readAShort();
        short num2 = app.file.readAShort();
        switch (num2)
        {
          case 1:
            cparam = (CParam) new PARAM_OBJECT();
            break;
          case 2:
            cparam = (CParam) new PARAM_TIME();
            break;
          case 3:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 4:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 5:
            cparam = (CParam) new PARAM_INT();
            break;
          case 6:
            cparam = (CParam) new PARAM_SAMPLE();
            break;
          case 7:
            cparam = (CParam) new PARAM_SAMPLE();
            break;
          case 9:
            cparam = (CParam) new PARAM_CREATE();
            break;
          case 10:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 11:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 12:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 13:
            cparam = (CParam) new PARAM_EVERY();
            break;
          case 14:
            cparam = (CParam) new PARAM_KEY();
            break;
          case 15:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 16 /*0x10*/:
            cparam = (CParam) new PARAM_POSITION();
            break;
          case 17:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 18:
            cparam = (CParam) new PARAM_SHOOT();
            break;
          case 19:
            cparam = (CParam) new PARAM_ZONE();
            break;
          case 21:
            cparam = (CParam) new PARAM_CREATE();
            break;
          case 22:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 23:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 24:
            cparam = (CParam) new PARAM_COLOUR();
            break;
          case 25:
            cparam = (CParam) new PARAM_INT();
            break;
          case 26:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 27:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 28:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 29:
            cparam = (CParam) new PARAM_INT();
            break;
          case 31 /*0x1F*/:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 32 /*0x20*/:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 33:
            cparam = (CParam) new PARAM_PROGRAM();
            break;
          case 34:
            cparam = (CParam) new PARAM_INT();
            break;
          case 35:
            cparam = (CParam) new PARAM_SAMPLE();
            break;
          case 36:
            cparam = (CParam) new PARAM_SAMPLE();
            break;
          case 37:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 38:
            cparam = (CParam) new PARAM_GROUP();
            break;
          case 39:
            cparam = (CParam) new PARAM_GROUPOINTER();
            break;
          case 40:
            cparam = (CParam) new PARAM_STRING();
            break;
          case 41:
            cparam = (CParam) new PARAM_STRING();
            break;
          case 42:
            cparam = (CParam) new PARAM_CMPTIME();
            break;
          case 43:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 44:
            cparam = (CParam) new PARAM_KEY();
            break;
          case 45:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 46:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 47:
            cparam = (CParam) new PARAM_2SHORTS();
            break;
          case 48 /*0x30*/:
            cparam = (CParam) new PARAM_INT();
            break;
          case 49:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 50:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 51:
            cparam = (CParam) new PARAM_2SHORTS();
            break;
          case 52:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 53:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 54:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 55:
            cparam = (CParam) new PARAM_EXTENSION();
            break;
          case 56:
            cparam = (CParam) new PARAM_INT();
            break;
          case 57:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 58:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 59:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 60:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 61:
            cparam = (CParam) new PARAM_SHORT();
            break;
          case 62:
            cparam = (CParam) new RuntimeXNA.Params.PARAM_EXPRESSION();
            break;
          case 63 /*0x3F*/:
            cparam = (CParam) new PARAM_STRING();
            break;
          case 64 /*0x40*/:
            cparam = (CParam) new PARAM_EFFECT();
            break;
        }
        cparam.code = num2;
        cparam.load(app);
        app.file.seek((int) (filePointer + (long) num1));
        return cparam;
      }

      public abstract void load(CRunApp app);
    }
}
