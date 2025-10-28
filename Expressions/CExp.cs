// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Expressions.CExp
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Expressions
{

    public abstract class CExp
    {
      public int code;

      public static CExp create(CFile file)
      {
        int filePointer = file.getFilePointer();
        int num1 = file.readAInt();
        CExp cexp;
        switch (num1)
        {
          case -131073:
            cexp = (CExp) new EXP_VIRGULE();
            break;
          case -65537:
            cexp = (CExp) new EXP_PARENTH2();
            break;
          case -1:
            cexp = (CExp) new EXP_PARENTH1();
            break;
          case 0:
            cexp = (CExp) new EXP_END();
            break;
          case 65529:
            cexp = (CExp) new EXP_PLASCORE();
            break;
          case 65530:
            cexp = (CExp) new EXP_XMOUSE();
            break;
          case 65531:
            cexp = (CExp) new EXP_CRENUMBERALL();
            break;
          case 65532:
            cexp = (CExp) new EXP_TIMVALUE();
            break;
          case 65533:
            cexp = (CExp) new EXP_GAMLEVEL();
            break;
          case 65534:
            cexp = (CExp) new EXP_GETSAMPLEMAINVOL();
            break;
          case (int) ushort.MaxValue:
            cexp = (CExp) new EXP_LONG();
            break;
          case 131065:
            cexp = (CExp) new EXP_PLALIVES();
            break;
          case 131066:
            cexp = (CExp) new EXP_YMOUSE();
            break;
          case 131068:
            cexp = (CExp) new EXP_TIMCENT();
            break;
          case 131069:
            cexp = (CExp) new EXP_GAMNPLAYER();
            break;
          case 131070:
            cexp = (CExp) new EXP_GETSAMPLEVOL();
            break;
          case 131071 /*0x01FFFF*/:
            cexp = (CExp) new EXP_RANDOM();
            break;
          case 131072 /*0x020000*/:
            cexp = (CExp) new EXP_PLUS();
            break;
          case 196601:
            cexp = (CExp) new EXP_GETINPUT();
            break;
          case 196602:
            cexp = (CExp) new EXP_MOUSEWHEELDELTA();
            break;
          case 196604:
            cexp = (CExp) new EXP_TIMSECONDS();
            break;
          case 196605:
            cexp = (CExp) new EXP_PLAYXLEFT();
            break;
          case 196606:
            cexp = (CExp) new EXP_GETCHANNELVOL();
            break;
          case 196607 /*0x02FFFF*/:
            cexp = (CExp) new EXP_VARGLO();
            break;
          case 262137:
            cexp = (CExp) new EXP_GETINPUTKEY();
            break;
          case 262140:
            cexp = (CExp) new EXP_TIMHOURS();
            break;
          case 262141:
            cexp = (CExp) new EXP_PLAYXRIGHT();
            break;
          case 262142:
            cexp = (CExp) new EXP_GETSAMPLEMAINPAN();
            break;
          case 262143 /*0x03FFFF*/:
            cexp = (CExp) new EXP_STRING();
            break;
          case 262144 /*0x040000*/:
            cexp = (CExp) new EXP_MINUS();
            break;
          case 327673:
            cexp = (CExp) new EXP_GETPLAYERNAME();
            break;
          case 327676:
            cexp = (CExp) new EXP_TIMMINITS();
            break;
          case 327677:
            cexp = (CExp) new EXP_PLAYYTOP();
            break;
          case 327678:
            cexp = (CExp) new EXP_GETSAMPLEPAN();
            break;
          case 327679 /*0x04FFFF*/:
            cexp = (CExp) new EXP_STR();
            break;
          case 393213:
            cexp = (CExp) new EXP_PLAYYBOTTOM();
            break;
          case 393214:
            cexp = (CExp) new EXP_GETCHANNELPAN();
            break;
          case 393215 /*0x05FFFF*/:
            cexp = (CExp) new EXP_VAL();
            break;
          case 393216 /*0x060000*/:
            cexp = (CExp) new EXP_MULT();
            break;
          case 458749:
            cexp = (CExp) new EXP_PLAYWIDTH();
            break;
          case 458751 /*0x06FFFF*/:
            cexp = (CExp) new EXP_PATH();
            break;
          case 524285:
            cexp = (CExp) new EXP_PLAYHEIGHT();
            break;
          case 524287 /*0x07FFFF*/:
            cexp = (CExp) new EXP_PATH();
            break;
          case 524288 /*0x080000*/:
            cexp = (CExp) new EXP_DIV();
            break;
          case 589821:
            cexp = (CExp) new EXP_GAMLEVELNEW();
            break;
          case 589822:
            cexp = (CExp) new EXP_GETSAMPLEDUR();
            break;
          case 589823 /*0x08FFFF*/:
            cexp = (CExp) new EXP_PATH();
            break;
          case 655357:
            cexp = (CExp) new EXP_GETCOLLISIONMASK();
            break;
          case 655358:
            cexp = (CExp) new EXP_GETCHANNELDUR();
            break;
          case 655360 /*0x0A0000*/:
            cexp = (CExp) new EXP_MOD();
            break;
          case 720893:
            cexp = (CExp) new EXP_FRAMERATE();
            break;
          case 720894:
            cexp = (CExp) new EXP_GETSAMPLEFREQ();
            break;
          case 720895 /*0x0AFFFF*/:
            cexp = (CExp) new EXP_SIN();
            break;
          case 786429:
            cexp = (CExp) new EXP_GETVIRTUALWIDTH();
            break;
          case 786430:
            cexp = (CExp) new EXP_GETCHANNELFREQ();
            break;
          case 786431 /*0x0BFFFF*/:
            cexp = (CExp) new EXP_COS();
            break;
          case 786432 /*0x0C0000*/:
            cexp = (CExp) new EXP_POW();
            break;
          case 851965:
            cexp = (CExp) new EXP_GETVIRTUALHEIGHT();
            break;
          case 851967 /*0x0CFFFF*/:
            cexp = (CExp) new EXP_TAN();
            break;
          case 917501:
            cexp = (CExp) new EXP_GETFRAMEBKDCOLOR();
            break;
          case 917503 /*0x0DFFFF*/:
            cexp = (CExp) new EXP_SQR();
            break;
          case 917504 /*0x0E0000*/:
            cexp = (CExp) new EXP_AND();
            break;
          case 983037:
            cexp = (CExp) new EXP_ZERO();
            break;
          case 983039 /*0x0EFFFF*/:
            cexp = (CExp) new EXP_LOG();
            break;
          case 1048573:
            cexp = (CExp) new EXP_ZERO();
            break;
          case 1048575 /*0x0FFFFF*/:
            cexp = (CExp) new EXP_LN();
            break;
          case 1048576 /*0x100000*/:
            cexp = (CExp) new EXP_OR();
            break;
          case 1114109:
            cexp = (CExp) new EXP_ZERO();
            break;
          case 1114111:
            cexp = (CExp) new EXP_HEX();
            break;
          case 1179645:
            cexp = (CExp) new EXP_FRAMERGBCOEF();
            break;
          case 1179647:
            cexp = (CExp) new EXP_BIN();
            break;
          case 1179648 /*0x120000*/:
            cexp = (CExp) new EXP_XOR();
            break;
          case 1245181:
            cexp = (CExp) new EXP_ZERO();
            break;
          case 1245183:
            cexp = (CExp) new EXP_EXP();
            break;
          case 1310719:
            cexp = (CExp) new EXP_LEFT();
            break;
          case 1376255:
            cexp = (CExp) new EXP_RIGHT();
            break;
          case 1441791:
            cexp = (CExp) new EXP_MID();
            break;
          case 1507327:
            cexp = (CExp) new EXP_LEN();
            break;
          case 1572863:
            cexp = (CExp) new EXP_DOUBLE();
            break;
          case 1638399:
            cexp = (CExp) new EXP_VARGLONAMED();
            break;
          case 1900543:
            cexp = (CExp) new EXP_INT();
            break;
          case 1966079:
            cexp = (CExp) new EXP_ABS();
            break;
          case 2031615:
            cexp = (CExp) new EXP_CEIL();
            break;
          case 2097151 /*0x1FFFFF*/:
            cexp = (CExp) new EXP_FLOOR();
            break;
          case 2162687:
            cexp = (CExp) new EXP_ACOS();
            break;
          case 2228223:
            cexp = (CExp) new EXP_ASIN();
            break;
          case 2293759:
            cexp = (CExp) new EXP_ATAN();
            break;
          case 2359295:
            cexp = (CExp) new EXP_NOT();
            break;
          case 2686975:
            cexp = (CExp) new EXP_MIN();
            break;
          case 2752511:
            cexp = (CExp) new EXP_MAX();
            break;
          case 2818047:
            cexp = (CExp) new EXP_GETRGB();
            break;
          case 2883583:
            cexp = (CExp) new EXP_GETRED();
            break;
          case 2949119:
            cexp = (CExp) new EXP_GETGREEN();
            break;
          case 3014655:
            cexp = (CExp) new EXP_GETBLUE();
            break;
          case 3080191:
            cexp = (CExp) new EXP_LOOPINDEX();
            break;
          case 3145727 /*0x2FFFFF*/:
            cexp = (CExp) new EXP_NEWLINE();
            break;
          case 3211263:
            cexp = (CExp) new EXP_ROUND();
            break;
          case 3276799:
            cexp = (CExp) new EXP_STRINGGLO();
            break;
          case 3342335:
            cexp = (CExp) new EXP_STRINGGLONAMED();
            break;
          case 3407871:
            cexp = (CExp) new EXP_LOWER();
            break;
          case 3473407:
            cexp = (CExp) new EXP_UPPER();
            break;
          case 3538943:
            cexp = (CExp) new EXP_FIND();
            break;
          case 3604479:
            cexp = (CExp) new EXP_REVERSEFIND();
            break;
          case 3866623:
            cexp = (CExp) new EXP_FLOATTOSTRING();
            break;
          case 3932159:
            cexp = (CExp) new EXP_ATAN2();
            break;
          case 3997695:
            cexp = (CExp) new EXP_ZERO();
            break;
          case 4063231:
            cexp = (CExp) new EXP_EMPTY();
            break;
          case 5242882 /*0x500002*/:
            cexp = (CExp) new EXP_GETRGBAT();
            break;
          case 5242883 /*0x500003*/:
            cexp = (CExp) new EXP_STRNUMBER();
            break;
          case 5242887 /*0x500007*/:
            cexp = (CExp) new EXP_CVALUE();
            break;
          case 5242889 /*0x500009*/:
            cexp = (CExp) new EXP_CCAGETFRAMENUMBER();
            break;
          case 5308418:
            cexp = (CExp) new EXP_GETSCALEX();
            break;
          case 5308419:
            cexp = (CExp) new EXP_STRGETCURRENT();
            break;
          case 5308423:
            cexp = (CExp) new EXP_CGETMIN();
            break;
          case 5308425:
            cexp = (CExp) new EXP_CCAGETGLOBALVALUE();
            break;
          case 5373954:
            cexp = (CExp) new EXP_GETSCALEY();
            break;
          case 5373955:
            cexp = (CExp) new EXP_STRGETNUMBER();
            break;
          case 5373959:
            cexp = (CExp) new EXP_CGETMAX();
            break;
          case 5373961:
            cexp = (CExp) new EXP_CCAGETGLOBALSTRING();
            break;
          case 5439490:
            cexp = (CExp) new EXP_GETANGLE();
            break;
          case 5439491:
            cexp = (CExp) new EXP_STRGETNUMERIC();
            break;
          case 5439495:
            cexp = (CExp) new EXP_CGETCOLOR1();
            break;
          case 5505027:
            cexp = (CExp) new EXP_STRGETNPARA();
            break;
          case 5505031:
            cexp = (CExp) new EXP_CGETCOLOR2();
            break;
          default:
            switch (num1 & -65536)
            {
              case 65536 /*0x010000*/:
                cexp = (CExp) new EXP_EXTYSPR();
                break;
              case 131072 /*0x020000*/:
                cexp = (CExp) new EXP_EXTISPR();
                break;
              case 196608 /*0x030000*/:
                cexp = (CExp) new EXP_EXTSPEED();
                break;
              case 262144 /*0x040000*/:
                cexp = (CExp) new EXP_EXTACC();
                break;
              case 327680 /*0x050000*/:
                cexp = (CExp) new EXP_EXTDEC();
                break;
              case 393216 /*0x060000*/:
                cexp = (CExp) new EXP_EXTDIR();
                break;
              case 458752 /*0x070000*/:
                cexp = (CExp) new EXP_EXTXLEFT();
                break;
              case 524288 /*0x080000*/:
                cexp = (CExp) new EXP_EXTXRIGHT();
                break;
              case 589824 /*0x090000*/:
                cexp = (CExp) new EXP_EXTYTOP();
                break;
              case 655360 /*0x0A0000*/:
                cexp = (CExp) new EXP_EXTYBOTTOM();
                break;
              case 720896 /*0x0B0000*/:
                cexp = (CExp) new EXP_EXTXSPR();
                break;
              case 786432 /*0x0C0000*/:
                cexp = (CExp) new EXP_EXTIDENTIFIER();
                break;
              case 851968 /*0x0D0000*/:
                cexp = (CExp) new EXP_EXTFLAG();
                break;
              case 917504 /*0x0E0000*/:
                cexp = (CExp) new EXP_EXTNANI();
                break;
              case 983040 /*0x0F0000*/:
                cexp = (CExp) new EXP_EXTNOBJECTS();
                break;
              case 1048576 /*0x100000*/:
                cexp = (CExp) new EXP_EXTVAR();
                break;
              case 1114112 /*0x110000*/:
                cexp = (CExp) new EXP_EXTGETSEMITRANSPARENCY();
                break;
              case 1179648 /*0x120000*/:
                cexp = (CExp) new EXP_EXTNMOVE();
                break;
              case 1245184 /*0x130000*/:
                cexp = (CExp) new EXP_EXTVARSTRING();
                break;
              case 1310720 /*0x140000*/:
                cexp = (CExp) new EXP_EXTGETFONTNAME();
                break;
              case 1376256 /*0x150000*/:
                cexp = (CExp) new EXP_EXTGETFONTSIZE();
                break;
              case 1441792 /*0x160000*/:
                cexp = (CExp) new EXP_EXTGETFONTCOLOR();
                break;
              case 1507328 /*0x170000*/:
                cexp = (CExp) new EXP_EXTGETLAYER();
                break;
              case 1572864 /*0x180000*/:
                cexp = (CExp) new EXP_EXTGETGRAVITY();
                break;
              case 1638400 /*0x190000*/:
                cexp = (CExp) new EXP_EXTXAP();
                break;
              case 1703936 /*0x1A0000*/:
                cexp = (CExp) new EXP_EXTYAP();
                break;
              case 1769472 /*0x1B0000*/:
                cexp = (CExp) new EXP_EXTALPHACOEF();
                break;
              case 1835008 /*0x1C0000*/:
                cexp = (CExp) new EXP_EXTRGBCOEF();
                break;
              case 1900544 /*0x1D0000*/:
                cexp = (CExp) new EXP_ZERO();
                break;
              case 1966080 /*0x1E0000*/:
                cexp = (CExp) new EXP_EXTVARBYINDEX();
                break;
              case 2031616 /*0x1F0000*/:
                cexp = (CExp) new EXP_EXTVARSTRINGBYINDEX();
                break;
              default:
                cexp = (CExp) new CExpExtension();
                break;
            }
            break;
        }
        if (cexp != null)
        {
          cexp.code = num1;
          if (num1 != 0)
          {
            short num2 = file.readAShort();
            switch (num1)
            {
              case (int) ushort.MaxValue:
                ((EXP_LONG) cexp).value = file.readAInt();
                break;
              case 262143 /*0x03FFFF*/:
                ((EXP_STRING) cexp).pString = file.readAString();
                break;
              case 1572863:
                ((EXP_DOUBLE) cexp).value = file.readADouble();
                break;
              case 1638399:
                file.skipBytes(4);
                ((EXP_VARGLONAMED) cexp).number = file.readAShort();
                break;
              case 3342335:
                file.skipBytes(4);
                ((EXP_STRINGGLONAMED) cexp).number = file.readAShort();
                break;
              default:
                short num3 = (short) num1;
                if (num3 >= (short) 2 || num3 == (short) -7)
                {
                  CExpOi cexpOi = (CExpOi) cexp;
                  cexpOi.oi = file.readAShort();
                  cexpOi.oiList = file.readAShort();
                  switch (num1 & -65536)
                  {
                    case 1048576 /*0x100000*/:
                      ((EXP_EXTVAR) cexp).number = file.readAShort();
                      break;
                    case 1245184 /*0x130000*/:
                      ((EXP_EXTVARSTRING) cexp).number = file.readAShort();
                      break;
                  }
                }
                else
                  break;
                break;
            }
            file.seek(filePointer + (int) num2);
          }
        }
        else
          Console.Out.WriteLine("*** Missing expression!");
        return cexp;
      }

      public abstract void evaluate(CRun rhPtr);
    }
}
