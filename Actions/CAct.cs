// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Actions.CAct
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Events;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using System;

namespace RuntimeXNA.Actions
{

    public abstract class CAct : CEvent
    {
      public const byte ACTFLAGS_REPEAT = 1;

      public static CAct create(CRunApp app)
      {
        int filePointer = app.file.getFilePointer();
        short num1 = app.file.readAShort();
        int num2 = app.file.readAInt();
        CAct cact;
        switch (num2)
        {
          case 65529:
            cact = (CAct) new ACT_SETSCORE();
            break;
          case 65530:
            cact = (CAct) new ACT_HIDECURSOR();
            break;
          case 65531:
            cact = (CAct) new ACT_CREATE();
            break;
          case 65532:
            cact = (CAct) new ACT_SETTIMER();
            break;
          case 65533:
            cact = (CAct) new ACT_NEXTLEVEL();
            break;
          case 65534:
            cact = (CAct) new ACT_PLAYSAMPLE();
            break;
          case (int) ushort.MaxValue:
            cact = (CAct) new ACT_SKIP();
            break;
          case 131065:
            cact = (CAct) new ACT_SETLIVES();
            break;
          case 131066:
            cact = (CAct) new ACT_SHOWCURSOR();
            break;
          case 131069:
            cact = (CAct) new ACT_PREVLEVEL();
            break;
          case 131070:
            cact = (CAct) new ACT_STOPSAMPLE();
            break;
          case 196601:
            cact = (CAct) new ACT_NOINPUT();
            break;
          case 196605:
            cact = (CAct) new ACT_GOLEVEL();
            break;
          case 262137:
            cact = (CAct) new ACT_RESTINPUT();
            break;
          case 262141:
            cact = (CAct) new ACT_PAUSE();
            break;
          case 262143 /*0x03FFFF*/:
            cact = (CAct) new ACT_SETVARG();
            break;
          case 327673:
            cact = (CAct) new ACT_ADDSCORE();
            break;
          case 327677:
            cact = (CAct) new ACT_ENDGAME();
            break;
          case 327678:
            cact = (CAct) new ACT_PLAYLOOPSAMPLE();
            break;
          case 327679 /*0x04FFFF*/:
            cact = (CAct) new ACT_SUBVARG();
            break;
          case 393209:
            cact = (CAct) new ACT_ADDLIVES();
            break;
          case 393213:
            cact = (CAct) new ACT_RESTARTGAME();
            break;
          case 393215 /*0x05FFFF*/:
            cact = (CAct) new ACT_ADDVARG();
            break;
          case 458745:
            cact = (CAct) new ACT_SUBSCORE();
            break;
          case 458749:
            cact = (CAct) new ACT_RESTARTLEVEL();
            break;
          case 458750:
            cact = (CAct) new ACT_STOPSPESAMPLE();
            break;
          case 458751 /*0x06FFFF*/:
            cact = (CAct) new ACT_GRPACTIVATE();
            break;
          case 524281:
            cact = (CAct) new ACT_SUBLIVES();
            break;
          case 524285:
            cact = (CAct) new ACT_CDISPLAY();
            break;
          case 524286:
            cact = (CAct) new ACT_PAUSESAMPLE();
            break;
          case 524287 /*0x07FFFF*/:
            cact = (CAct) new ACT_GRPDEACTIVATE();
            break;
          case 589817:
            cact = (CAct) new ACT_SETINPUT();
            break;
          case 589821:
            cact = (CAct) new ACT_CDISPLAYX();
            break;
          case 589822:
            cact = (CAct) new ACT_RESUMESAMPLE();
            break;
          case 655353:
            cact = (CAct) new ACT_SETINPUTKEY();
            break;
          case 655357:
            cact = (CAct) new ACT_CDISPLAYY();
            break;
          case 720889:
            cact = (CAct) new ACT_SETPLAYERNAME();
            break;
          case 786430:
            cact = (CAct) new ACT_PLAYCHANNEL();
            break;
          case 851966:
            cact = (CAct) new ACT_PLAYLOOPCHANNEL();
            break;
          case 917502:
            cact = (CAct) new ACT_PAUSECHANNEL();
            break;
          case 983037:
            cact = (CAct) new ACT_FULLSCREENMODE();
            break;
          case 983038:
            cact = (CAct) new ACT_RESUMECHANNEL();
            break;
          case 983039 /*0x0EFFFF*/:
            cact = (CAct) new ACT_STARTLOOP();
            break;
          case 1048573:
            cact = (CAct) new ACT_WINDOWEDMODE();
            break;
          case 1048574:
            cact = (CAct) new ACT_STOPCHANNEL();
            break;
          case 1048575 /*0x0FFFFF*/:
            cact = (CAct) new ACT_STOPLOOP();
            break;
          case 1114109:
            cact = (CAct) new ACT_SETFRAMERATE();
            break;
          case 1114111:
            cact = (CAct) new ACT_SETLOOPINDEX();
            break;
          case 1179645:
            cact = (CAct) new ACT_PAUSEKEY();
            break;
          case 1179646:
            cact = (CAct) new ACT_SETCHANNELVOL();
            break;
          case 1179647:
            cact = (CAct) new ACT_RANDOMIZE();
            break;
          case 1245181:
            cact = (CAct) new ACT_PAUSEANYKEY();
            break;
          case 1245182:
            cact = (CAct) new ACT_SETCHANNELPAN();
            break;
          case 1310719:
            cact = (CAct) new ACT_SETGLOBALSTRING();
            break;
          case 1376254:
            cact = (CAct) new ACT_SETSAMPLEMAINVOL();
            break;
          case 1441789:
            cact = (CAct) new ACT_SETVIRTUALWIDTH();
            break;
          case 1441790:
            cact = (CAct) new ACT_SETSAMPLEVOL();
            break;
          case 1507325:
            cact = (CAct) new ACT_SETVIRTUALHEIGHT();
            break;
          case 1507326:
            cact = (CAct) new ACT_SETSAMPLEMALNPAN();
            break;
          case 1572861:
            cact = (CAct) new ACT_SETFRAMEBDKCOLOR();
            break;
          case 1572862:
            cact = (CAct) new ACT_SETSAMPLEPAN();
            break;
          case 1572863:
            cact = (CAct) new ACT_OPENDEBUGGER();
            break;
          case 1638397:
            cact = (CAct) new ACT_DELCREATEDBKDAT();
            break;
          case 1638398:
            cact = (CAct) new ACT_PAUSEALLCHANNELS();
            break;
          case 1638399:
            cact = (CAct) new ACT_PAUSEDEBUGGER();
            break;
          case 1703933:
            cact = (CAct) new ACT_DELALLCREATEDBKD();
            break;
          case 1703934:
            cact = (CAct) new ACT_RESUMEALLCHANNELS();
            break;
          case 1769469:
            cact = (CAct) new ACT_SETFRAMEWIDTH();
            break;
          case 1835005:
            cact = (CAct) new ACT_SETFRAMEHEIGHT();
            break;
          case 2031614:
            cact = (CAct) new ACT_LOCKCHANNEL();
            break;
          case 2097150:
            cact = (CAct) new ACT_UNLOCKCHANNEL();
            break;
          case 2162685:
            cact = (CAct) new ACT_SKIP();
            break;
          case 2162686:
            cact = (CAct) new ACT_SETCHANNELFREQ();
            break;
          case 2228221:
            cact = (CAct) new ACT_SKIP();
            break;
          case 2228222:
            cact = (CAct) new ACT_SETSAMPLEFREQ();
            break;
          case 2293757:
            cact = (CAct) new ACT_SKIP();
            break;
          case 2359293:
            cact = (CAct) new ACT_SKIP();
            break;
          case 2424829:
            cact = (CAct) new ACT_SKIP();
            break;
          case 5242882 /*0x500002*/:
            cact = (CAct) new ACT_SPRPASTE();
            break;
          case 5242883 /*0x500003*/:
            cact = (CAct) new ACT_STRDESTROY();
            break;
          case 5242884 /*0x500004*/:
            cact = (CAct) new ACT_QASK();
            break;
          case 5242887 /*0x500007*/:
            cact = (CAct) new ACT_CSETVALUE();
            break;
          case 5242889 /*0x500009*/:
            cact = (CAct) new ACT_CCARESTARTAPP();
            break;
          case 5308418:
            cact = (CAct) new ACT_SPRFRONT();
            break;
          case 5308419:
            cact = (CAct) new ACT_STRDISPLAY();
            break;
          case 5308423:
            cact = (CAct) new ACT_CADDVALUE();
            break;
          case 5308425:
            cact = (CAct) new ACT_CCARESTARTFRAME();
            break;
          case 5373954:
            cact = (CAct) new ACT_SPRBACK();
            break;
          case 5373955:
            cact = (CAct) new ACT_STRDISPLAYDURING();
            break;
          case 5373959:
            cact = (CAct) new ACT_CSUBVALUE();
            break;
          case 5373961:
            cact = (CAct) new ACT_CCANEXTFRAME();
            break;
          case 5439490:
            cact = (CAct) new ACT_SPRADDBKD();
            break;
          case 5439491:
            cact = (CAct) new ACT_STRSETCOLOUR();
            break;
          case 5439495:
            cact = (CAct) new ACT_CSETMIN();
            break;
          case 5439497:
            cact = (CAct) new ACT_CCAPREVIOUSFRAME();
            break;
          case 5505026:
            cact = (CAct) new ACT_SPRREPLACECOLOR();
            break;
          case 5505027:
            cact = (CAct) new ACT_STRSET();
            break;
          case 5505031:
            cact = (CAct) new ACT_CSETMAX();
            break;
          case 5505033:
            cact = (CAct) new ACT_CCAENDAPP();
            break;
          case 5570562:
            cact = (CAct) new ACT_SPRSETSCALE();
            break;
          case 5570563:
            cact = (CAct) new ACT_STRPREV();
            break;
          case 5570567:
            cact = (CAct) new ACT_CSETCOLOR1();
            break;
          case 5636098:
            cact = (CAct) new ACT_SPRSETSCALEX();
            break;
          case 5636099:
            cact = (CAct) new ACT_STRNEXT();
            break;
          case 5636103:
            cact = (CAct) new ACT_CSETCOLOR2();
            break;
          case 5636105:
            cact = (CAct) new ACT_CCAJUMPFRAME();
            break;
          case 5701634:
            cact = (CAct) new ACT_SPRSETSCALEY();
            break;
          case 5701635:
            cact = (CAct) new ACT_STRDISPLAYSTRING();
            break;
          case 5701641:
            cact = (CAct) new ACT_CCASETGLOBALVALUE();
            break;
          case 5767170:
            cact = (CAct) new ACT_SPRSETANGLE();
            break;
          case 5767171:
            cact = (CAct) new ACT_STRSETSTRING();
            break;
          case 5767177:
            cact = (CAct) new ACT_CCASHOW();
            break;
          case 5832713:
            cact = (CAct) new ACT_CCAHIDE();
            break;
          case 5898249:
            cact = (CAct) new ACT_CCASETGLOBALSTRING();
            break;
          case 5963785:
            cact = (CAct) new ACT_CCAPAUSEAPP();
            break;
          case 6029321:
            cact = (CAct) new ACT_CCARESUMEAPP();
            break;
          default:
            switch (num2 & -65536)
            {
              case 65536 /*0x010000*/:
                cact = (CAct) new ACT_EXTSETPOS();
                break;
              case 131072 /*0x020000*/:
                cact = (CAct) new ACT_EXTSETX();
                break;
              case 196608 /*0x030000*/:
                cact = (CAct) new ACT_EXTSETY();
                break;
              case 262144 /*0x040000*/:
                cact = (CAct) new ACT_EXTSTOP();
                break;
              case 327680 /*0x050000*/:
                cact = (CAct) new ACT_EXTSTART();
                break;
              case 393216 /*0x060000*/:
                cact = (CAct) new ACT_EXTSPEED();
                break;
              case 458752 /*0x070000*/:
                cact = (CAct) new ACT_EXTMAXSPEED();
                break;
              case 524288 /*0x080000*/:
                cact = (CAct) new ACT_EXTWRAP();
                break;
              case 589824 /*0x090000*/:
                cact = (CAct) new ACT_EXTBOUNCE();
                break;
              case 655360 /*0x0A0000*/:
                cact = (CAct) new ACT_EXTREVERSE();
                break;
              case 720896 /*0x0B0000*/:
                cact = (CAct) new ACT_EXTNEXTMOVE();
                break;
              case 786432 /*0x0C0000*/:
                cact = (CAct) new ACT_EXTPREVMOVE();
                break;
              case 851968 /*0x0D0000*/:
                cact = (CAct) new ACT_EXTSELMOVE();
                break;
              case 917504 /*0x0E0000*/:
                cact = (CAct) new ACT_EXTLOOKAT();
                break;
              case 983040 /*0x0F0000*/:
                cact = (CAct) new ACT_EXTSTOPANIM();
                break;
              case 1048576 /*0x100000*/:
                cact = (CAct) new ACT_EXTSTARTANIM();
                break;
              case 1114112 /*0x110000*/:
                cact = (CAct) new ACT_EXTFORCEANIM();
                break;
              case 1179648 /*0x120000*/:
                cact = (CAct) new ACT_EXTFORCEDIR();
                break;
              case 1245184 /*0x130000*/:
                cact = (CAct) new ACT_EXTFORCESPEED();
                break;
              case 1310720 /*0x140000*/:
                cact = (CAct) new ACT_EXTRESTANIM();
                break;
              case 1376256 /*0x150000*/:
                cact = (CAct) new ACT_EXTRESTDIR();
                break;
              case 1441792 /*0x160000*/:
                cact = (CAct) new ACT_EXTRESTSPEED();
                break;
              case 1507328 /*0x170000*/:
                cact = (CAct) new ACT_EXTSETDIR();
                break;
              case 1572864 /*0x180000*/:
                cact = (CAct) new ACT_EXTDESTROY();
                break;
              case 1638400 /*0x190000*/:
                cact = (CAct) new ACT_EXTSHUFFLE();
                break;
              case 1703936 /*0x1A0000*/:
                cact = (CAct) new ACT_EXTHIDE();
                break;
              case 1769472 /*0x1B0000*/:
                cact = (CAct) new ACT_EXTSHOW();
                break;
              case 1835008 /*0x1C0000*/:
                cact = (CAct) new ACT_EXTDISPLAYDURING();
                break;
              case 1900544 /*0x1D0000*/:
                cact = (CAct) new ACT_EXTSHOOT();
                break;
              case 1966080 /*0x1E0000*/:
                cact = (CAct) new ACT_EXTSHOOTTOWARD();
                break;
              case 2031616 /*0x1F0000*/:
                cact = (CAct) new ACT_EXTSETVAR();
                break;
              case 2097152 /*0x200000*/:
                cact = (CAct) new ACT_EXTADDVAR();
                break;
              case 2162688 /*0x210000*/:
                cact = (CAct) new ACT_EXTSUBVAR();
                break;
              case 2228224 /*0x220000*/:
                cact = (CAct) new ACT_EXTDISPATCHVAR();
                break;
              case 2293760 /*0x230000*/:
                cact = (CAct) new ACT_EXTSETFLAG();
                break;
              case 2359296 /*0x240000*/:
                cact = (CAct) new ACT_EXTCLRFLAG();
                break;
              case 2424832 /*0x250000*/:
                cact = (CAct) new ACT_EXTCHGFLAG();
                break;
              case 2490368 /*0x260000*/:
                cact = (CAct) new ACT_EXTINKEFFECT();
                break;
              case 2555904 /*0x270000*/:
                cact = (CAct) new ACT_EXTSETSEMITRANSPARENCY();
                break;
              case 2621440 /*0x280000*/:
                cact = (CAct) new ACT_EXTFORCEFRAME();
                break;
              case 2686976 /*0x290000*/:
                cact = (CAct) new ACT_EXTRESTFRAME();
                break;
              case 2752512 /*0x2A0000*/:
                cact = (CAct) new ACT_EXTSETACCELERATION();
                break;
              case 2818048 /*0x2B0000*/:
                cact = (CAct) new ACT_EXTSETDECELERATION();
                break;
              case 2883584 /*0x2C0000*/:
                cact = (CAct) new ACT_EXTSETROTATINGSPEED();
                break;
              case 2949120 /*0x2D0000*/:
                cact = (CAct) new ACT_EXTSETDIRECTIONS();
                break;
              case 3014656 /*0x2E0000*/:
                cact = (CAct) new ACT_EXTBRANCHNODE();
                break;
              case 3080192 /*0x2F0000*/:
                cact = (CAct) new ACT_EXTSETGRAVITY();
                break;
              case 3145728 /*0x300000*/:
                cact = (CAct) new ACT_EXTGOTONODE();
                break;
              case 3211264 /*0x310000*/:
                cact = (CAct) new ACT_EXTSETVARSTRING();
                break;
              case 3276800 /*0x320000*/:
                cact = (CAct) new ACT_EXTSETFONTNAME();
                break;
              case 3342336 /*0x330000*/:
                cact = (CAct) new ACT_EXTSETFONTSIZE();
                break;
              case 3407872 /*0x340000*/:
                cact = (CAct) new ACT_EXTSETBOLD();
                break;
              case 3670016 /*0x380000*/:
                cact = (CAct) new ACT_EXTSETTEXTCOLOR();
                break;
              case 3735552 /*0x390000*/:
                cact = (CAct) new ACT_EXTSPRFRONT();
                break;
              case 3801088 /*0x3A0000*/:
                cact = (CAct) new ACT_EXTSPRBACK();
                break;
              case 3866624 /*0x3B0000*/:
                cact = (CAct) new ACT_EXTMOVEBEFORE();
                break;
              case 3932160 /*0x3C0000*/:
                cact = (CAct) new ACT_EXTMOVEAFTER();
                break;
              case 3997696 /*0x3D0000*/:
                cact = (CAct) new ACT_EXTMOVETOLAYER();
                break;
              case 4063232 /*0x3E0000*/:
                cact = (CAct) new ACT_EXTADDTODEBUGGER();
                break;
              case 4128768 /*0x3F0000*/:
                cact = (CAct) new ACT_EXTSETEFFECT();
                break;
              case 4194304 /*0x400000*/:
                cact = (CAct) new ACT_EXTSETEFFECTPARAM();
                break;
              case 4259840 /*0x410000*/:
                cact = (CAct) new ACT_EXTSETALPHACOEF();
                break;
              case 4325376 /*0x420000*/:
                cact = (CAct) new ACT_EXTSETRGBCOEF();
                break;
              case 4390912 /*0x430000*/:
                cact = (CAct) new ACT_EXTSETEFFECTPARAMTEXTURE();
                break;
              default:
                cact = (CAct) new CActExtension();
                break;
            }
            break;
        }
        if (cact != null)
        {
          cact.evtCode = num2;
          cact.evtOi = app.file.readAShort();
          cact.evtOiList = app.file.readAShort();
          cact.evtFlags = app.file.readByte();
          cact.evtFlags2 = app.file.readByte();
          cact.evtNParams = app.file.readByte();
          cact.evtDefType = app.file.readByte();
          if (cact.evtNParams > (byte) 0)
          {
            cact.evtParams = new CParam[(int) cact.evtNParams];
            for (int index = 0; index < (int) cact.evtNParams; ++index)
              cact.evtParams[index] = CParam.create(app);
          }
        }
        else
          Console.Out.WriteLine("*** Missing action!");
        app.file.seek(filePointer + (int) num1);
        return cact;
      }

      public abstract void execute(CRun rhPtr);
    }
}
