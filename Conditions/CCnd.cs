// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Conditions.CCnd
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Events;
using RuntimeXNA.Expressions;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using System;

namespace RuntimeXNA.Conditions
{

    public abstract class CCnd : CEvent
    {
      public short evtIdentifier;

      public static CCnd create(CRunApp app)
      {
        int filePointer = app.file.getFilePointer();
        short num1 = app.file.readAShort();
        int num2 = app.file.readAInt();
        CCnd ccnd;
        switch (num2)
        {
          case -5505015:
            ccnd = (CCnd) new CND_CCAISPAUSED();
            break;
          case -5439484:
            ccnd = (CCnd) new CND_QEQUAL();
            break;
          case -5439479:
            ccnd = (CCnd) new CND_CCAISVISIBLE();
            break;
          case -5373948:
            ccnd = (CCnd) new CND_QFALSE();
            break;
          case -5373943:
            ccnd = (CCnd) new CND_CCAAPPFINISHED();
            break;
          case -5308414:
            ccnd = (CCnd) new CND_SPRCLICK();
            break;
          case -5308412:
            ccnd = (CCnd) new CND_QEXACT();
            break;
          case -5308409:
            ccnd = (CCnd) new CND_CCOUNTER();
            break;
          case -5308407:
            ccnd = (CCnd) new CND_CCAFRAMECHANGED();
            break;
          case -1638401:
            ccnd = (CCnd) new CND_CHANCE();
            break;
          case -1572865:
            ccnd = (CCnd) new CND_ORLOGICAL();
            break;
          case -1507329:
            ccnd = (CCnd) new CND_OR();
            break;
          case -1441797:
            ccnd = (CCnd) new CND_CHOOSEALLINLINE();
            break;
          case -1441793:
            ccnd = (CCnd) new CND_GROUPSTART();
            break;
          case -1376261:
            ccnd = (CCnd) new CND_CHOOSEFLAGRESET();
            break;
          case -1310725:
            ccnd = (CCnd) new CND_CHOOSEFLAGSET();
            break;
          case -1310721:
            ccnd = (CCnd) new CND_ONCLOSE();
            break;
          case -1245189:
            ccnd = (CCnd) new CND_CHOOSEVALUE();
            break;
          case -1245185:
            ccnd = (CCnd) new CND_COMPAREGSTRING();
            break;
          case -1179653:
            ccnd = (CCnd) new CND_PICKFROMID();
            break;
          case -1114117:
            ccnd = (CCnd) new CND_CHOOSEALLINZONE();
            break;
          case -1048581:
            ccnd = (CCnd) new CND_CHOOSEALL();
            break;
          case -983045:
            ccnd = (CCnd) new CND_CHOOSEZONE();
            break;
          case -983041:
            ccnd = (CCnd) new CND_ONLOOP();
            break;
          case -917509:
            ccnd = (CCnd) new CND_NUMOFALLOBJECT();
            break;
          case -851973:
            ccnd = (CCnd) new CND_NUMOFALLZONE();
            break;
          case -786437:
            ccnd = (CCnd) new CND_NOMOREALLZONE();
            break;
          case -720902:
            ccnd = (CCnd) new CND_ONMOUSEWHEELDOWN();
            break;
          case -720901:
            ccnd = (CCnd) new CND_CHOOSEFLAGRESET_OLD();
            break;
          case -720897:
            ccnd = (CCnd) new CND_GROUPACTIVATED();
            break;
          case -655366:
            ccnd = (CCnd) new CND_ONMOUSEWHEELUP();
            break;
          case -655365:
            ccnd = (CCnd) new CND_CHOOSEFLAGSET_OLD();
            break;
          case -655361:
            ccnd = (CCnd) new CND_ENDGROUP();
            break;
          case -589830:
            ccnd = (CCnd) new CND_MOUSEON();
            break;
          case -589825:
            ccnd = (CCnd) new CND_GROUP();
            break;
          case -524294:
            ccnd = (CCnd) new CND_ANYKEY();
            break;
          case -524290:
            ccnd = (CCnd) new CND_SPCHANNELPAUSED();
            break;
          case -524289:
            ccnd = (CCnd) new CND_REMARK();
            break;
          case -458758:
            ccnd = (CCnd) new CND_MKEYDEPRESSED();
            break;
          case -458757:
            ccnd = (CCnd) new CND_CHOOSEVALUE_OLD();
            break;
          case -458756:
            ccnd = (CCnd) new CND_EVERY2();
            break;
          case -458755:
            ccnd = (CCnd) new CND_ENDOFPAUSE();
            break;
          case -458754:
            ccnd = (CCnd) new CND_NOSPCHANNELPLAYING();
            break;
          case -458753:
            ccnd = (CCnd) new CND_COMPAREG();
            break;
          case -393222:
            ccnd = (CCnd) new CND_MCLICKONOBJECT();
            break;
          case -393221:
            ccnd = (CCnd) new CND_PICKFROMID_OLD();
            break;
          case -393220:
            ccnd = (CCnd) new CND_TIMER2();
            break;
          case -393217:
            ccnd = (CCnd) new CND_NOTALWAYS();
            break;
          case -327687:
            ccnd = (CCnd) new CND_JOYPUSHED();
            break;
          case -327686 /*0xFFFAFFFA*/:
            ccnd = (CCnd) new CND_MCLICKINZONE();
            break;
          case -327685:
            ccnd = (CCnd) new CND_CHOOSEALLINZONE_OLD();
            break;
          case -327683:
            ccnd = (CCnd) new CND_ISLADDER();
            break;
          case -327682:
            ccnd = (CCnd) new CND_SPSAMPAUSED();
            break;
          case -327681:
            ccnd = (CCnd) new CND_ONCE();
            break;
          case -262151:
            ccnd = (CCnd) new CND_NOMORELIVE();
            break;
          case -262150:
            ccnd = (CCnd) new CND_MCLICK();
            break;
          case -262149 /*0xFFFBFFFB*/:
            ccnd = (CCnd) new CND_CHOOSEALL_OLD();
            break;
          case -262148:
            ccnd = (CCnd) new CND_TIMEOUT();
            break;
          case -262147:
            ccnd = (CCnd) new CND_ISOBSTACLE();
            break;
          case -262145:
            ccnd = (CCnd) new CND_REPEAT();
            break;
          case -196615:
            ccnd = (CCnd) new CND_JOYPRESSED();
            break;
          case -196614:
            ccnd = (CCnd) new CND_MONOBJECT();
            break;
          case -196613:
            ccnd = (CCnd) new CND_CHOOSEZONE_OLD();
            break;
          case -196612 /*0xFFFCFFFC*/:
            ccnd = (CCnd) new CND_EVERY();
            break;
          case -196611:
            ccnd = (CCnd) new CND_QUITAPPLICATION();
            break;
          case -196609:
            ccnd = (CCnd) new CND_NOMORE();
            break;
          case -131079:
            ccnd = (CCnd) new CND_LIVE();
            break;
          case -131078:
            ccnd = (CCnd) new CND_MINZONE();
            break;
          case -131077:
            ccnd = (CCnd) new CND_NUMOFALLOBJECT_OLD();
            break;
          case -131076:
            ccnd = (CCnd) new CND_TIMER();
            break;
          case -131075 /*0xFFFDFFFD*/:
            ccnd = (CCnd) new CND_LEVEL();
            break;
          case -131074:
            ccnd = (CCnd) new CND_NOSAMPLAYING();
            break;
          case -131073:
            ccnd = (CCnd) new CND_COMPARE();
            break;
          case -65543:
            ccnd = (CCnd) new CND_SCORE();
            break;
          case -65542:
            ccnd = (CCnd) new CND_KBKEYDEPRESSED();
            break;
          case -65541:
            ccnd = (CCnd) new CND_NUMOFALLZONE_OLD();
            break;
          case -65540:
            ccnd = (CCnd) new CND_TIMERINF();
            break;
          case -65539:
            ccnd = (CCnd) new CND_END();
            break;
          case -65537:
            ccnd = (CCnd) new CND_NEVER();
            break;
          case -7:
            ccnd = (CCnd) new CND_PLAYERPLAYING();
            break;
          case -6:
            ccnd = (CCnd) new CND_KBPRESSKEY();
            break;
          case -5:
            ccnd = (CCnd) new CND_NOMOREALLZONE_OLD();
            break;
          case -4:
            ccnd = (CCnd) new CND_TIMERSUP();
            break;
          case -3:
            ccnd = (CCnd) new CND_START();
            break;
          case -2:
            ccnd = (CCnd) new CND_NOSPSAMPLAYING();
            break;
          case -1:
            ccnd = (CCnd) new CND_ALWAYS();
            break;
          default:
            switch (num2 & -65536)
            {
              case -2490368:
                ccnd = (CCnd) new CND_EXTISITALIC();
                break;
              case -2424832:
                ccnd = (CCnd) new CND_EXTISBOLD();
                break;
              case -2359296:
                ccnd = (CCnd) new CND_EXTCMPVARSTRING();
                break;
              case -2293760:
                ccnd = (CCnd) new CND_EXTPATHNODENAME();
                break;
              case -2228224:
                ccnd = (CCnd) new CND_EXTCHOOSE();
                break;
              case -2162688:
                ccnd = (CCnd) new CND_EXTNOMOREOBJECT();
                break;
              case -2097152 /*0xFFE00000*/:
                ccnd = (CCnd) new CND_EXTNUMOFOBJECT();
                break;
              case -2031616:
                ccnd = (CCnd) new CND_EXTNOMOREZONE();
                break;
              case -1966080:
                ccnd = (CCnd) new CND_EXTNUMBERZONE();
                break;
              case -1900544:
                ccnd = (CCnd) new CND_EXTSHOWN();
                break;
              case -1835008:
                ccnd = (CCnd) new CND_EXTHIDDEN();
                break;
              case -1769472:
                ccnd = (CCnd) new CND_EXTCMPVAR();
                break;
              case -1703936:
                ccnd = (CCnd) new CND_EXTCMPVARFIXED();
                break;
              case -1638400:
                ccnd = (CCnd) new CND_EXTFLAGSET();
                break;
              case -1572864:
                ccnd = (CCnd) new CND_EXTFLAGRESET();
                break;
              case -1507328:
                ccnd = (CCnd) new CND_EXTISCOLBACK();
                break;
              case -1441792:
                ccnd = (CCnd) new CND_EXTNEARBORDERS();
                break;
              case -1376256:
                ccnd = (CCnd) new CND_EXTENDPATH();
                break;
              case -1310720:
                ccnd = (CCnd) new CND_EXTPATHNODE();
                break;
              case -1245184:
                ccnd = (CCnd) new CND_EXTCMPACC();
                break;
              case -1179648:
                ccnd = (CCnd) new CND_EXTCMPDEC();
                break;
              case -1114112:
                ccnd = (CCnd) new CND_EXTCMPX();
                break;
              case -1048576 /*0xFFF00000*/:
                ccnd = (CCnd) new CND_EXTCMPY();
                break;
              case -983040:
                ccnd = (CCnd) new CND_EXTCMPSPEED();
                break;
              case -917504:
                ccnd = (CCnd) new CND_EXTCOLLISION();
                break;
              case -851968:
                ccnd = (CCnd) new CND_EXTCOLBACK();
                break;
              case -786432:
                ccnd = (CCnd) new CND_EXTOUTPLAYFIELD();
                break;
              case -720896:
                ccnd = (CCnd) new CND_EXTINPLAYFIELD();
                break;
              case -655360:
                ccnd = (CCnd) new CND_EXTISOUT();
                break;
              case -589824:
                ccnd = (CCnd) new CND_EXTISIN();
                break;
              case -524288:
                ccnd = (CCnd) new CND_EXTFACING();
                break;
              case -458752:
                ccnd = (CCnd) new CND_EXTSTOPPED();
                break;
              case -393216:
                ccnd = (CCnd) new CND_EXTBOUNCING();
                break;
              case -327680:
                ccnd = (CCnd) new CND_EXTREVERSED();
                break;
              case -262144:
                ccnd = (CCnd) new CND_EXTISCOLLIDING();
                break;
              case -196608:
                ccnd = (CCnd) new CND_EXTANIMPLAYING();
                break;
              case -131072:
                ccnd = (CCnd) new CND_EXTANIMENDOF();
                break;
              case -65536:
                ccnd = (CCnd) new CND_EXTCMPFRAME();
                break;
              default:
                ccnd = (CCnd) new CCndExtension();
                break;
            }
            break;
        }
        if (ccnd != null)
        {
          ccnd.evtCode = num2;
          ccnd.evtOi = app.file.readAShort();
          ccnd.evtOiList = app.file.readAShort();
          ccnd.evtFlags = app.file.readByte();
          ccnd.evtFlags2 = app.file.readByte();
          ccnd.evtNParams = app.file.readByte();
          ccnd.evtDefType = app.file.readByte();
          ccnd.evtIdentifier = app.file.readAShort();
          if (ccnd.evtNParams > (byte) 0)
          {
            ccnd.evtParams = new CParam[(int) ccnd.evtNParams];
            for (int index = 0; index < (int) ccnd.evtNParams; ++index)
              ccnd.evtParams[index] = CParam.create(app);
          }
        }
        else
          Console.Out.WriteLine("*** Missing condition!");
        app.file.seek(filePointer + (int) num1);
        return ccnd;
      }

      public virtual bool negaTRUE() => ((int) this.evtFlags2 & 1) == 0;

      public virtual bool negaFALSE() => ((int) this.evtFlags2 & 1) != 0;

      public virtual bool compute_GlobalNoRepeat(CRun rhPtr)
      {
        CEventGroup rhEventGroup = rhPtr.rhEvtProg.rhEventGroup;
        int evgInhibit = (int) rhEventGroup.evgInhibit;
        rhEventGroup.evgInhibit = (ushort) rhPtr.rhLoopCount;
        int rhLoopCount = rhPtr.rhLoopCount;
        return rhLoopCount != evgInhibit && rhLoopCount - 1 != evgInhibit;
      }

      public bool compute_NoRepeatCol(int identifier, CObject pHo)
      {
        CArrayList carrayList = pHo.hoBaseNoRepeat;
        if (carrayList == null)
        {
          carrayList = new CArrayList();
          pHo.hoBaseNoRepeat = carrayList;
        }
        else
        {
          for (int index = 0; index < carrayList.size(); ++index)
          {
            if ((int) carrayList.get(index) == identifier)
              return false;
          }
        }
        int o = identifier;
        carrayList.add((object) o);
        CArrayList hoPrevNoRepeat = pHo.hoPrevNoRepeat;
        if (hoPrevNoRepeat == null)
          return true;
        for (int index = 0; index < hoPrevNoRepeat.size(); ++index)
        {
          if ((int) hoPrevNoRepeat.get(index) == identifier)
            return false;
        }
        return true;
      }

      public virtual bool compute_NoRepeat(CObject pHo)
      {
        return this.compute_NoRepeatCol((int) this.evtIdentifier, pHo);
      }

      public virtual bool evaChooseValueOld(CRun rhPtr, IChooseValue pRoutine)
      {
        int num = 0;
        for (CObject pHo = rhPtr.rhEvtProg.evt_FirstObjectFromType((short) 2); pHo != null; pHo = rhPtr.rhEvtProg.evt_NextObjectFromType())
        {
          ++num;
          int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
          if (!pRoutine.evaluate(pHo, eventExpressionInt))
          {
            --num;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        return num != 0;
      }

      public virtual bool evaChooseValue(CRun rhPtr, IChooseValue pRoutine)
      {
        int num = 0;
        for (CObject pHo = rhPtr.rhEvtProg.evt_FirstObjectFromType((short) -1); pHo != null; pHo = rhPtr.rhEvtProg.evt_NextObjectFromType())
        {
          ++num;
          int eventExpressionInt = rhPtr.get_EventExpressionInt((CParamExpression) this.evtParams[0]);
          if (!pRoutine.evaluate(pHo, eventExpressionInt))
          {
            --num;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        return num != 0;
      }

      public virtual bool evaExpObject(CRun rhPtr, IEvaExpObject pRoutine)
      {
        CObject hoPtr = rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
        int nselectedObjects = rhPtr.rhEvtProg.evtNSelectedObjects;
        CParamExpression evtParam = (CParamExpression) this.evtParams[0];
        for (; hoPtr != null; hoPtr = rhPtr.rhEvtProg.evt_NextObject())
        {
          int eventExpressionInt = rhPtr.get_EventExpressionInt(evtParam);
          if (!pRoutine.evaExpRoutine(hoPtr, eventExpressionInt, evtParam.comparaison))
          {
            --nselectedObjects;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        return nselectedObjects != 0;
      }

      public virtual bool evaObject(CRun rhPtr, IEvaObject pRoutine)
      {
        CObject hoPtr = rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
        int nselectedObjects = rhPtr.rhEvtProg.evtNSelectedObjects;
        for (; hoPtr != null; hoPtr = rhPtr.rhEvtProg.evt_NextObject())
        {
          if (!pRoutine.evaObjectRoutine(hoPtr))
          {
            --nselectedObjects;
            rhPtr.rhEvtProg.evt_DeleteCurrentObject();
          }
        }
        return nselectedObjects != 0;
      }

      public virtual bool compareCondition(CRun rhPtr, int param, int v)
      {
        CValue eventExpressionAny = rhPtr.get_EventExpressionAny((CParamExpression) this.evtParams[param]);
        short comparaison = ((CParamExpression) this.evtParams[param]).comparaison;
        return CRun.compareTo(new CValue(v), eventExpressionAny, comparaison);
      }

      public virtual bool checkMark(CRun rhPtr, int mark)
      {
        return mark != 0 && (mark == rhPtr.rhLoopCount || mark == rhPtr.rhLoopCount - 1);
      }

      public bool isColliding(CRun rhPtr)
      {
        if (rhPtr.rhEvtProg.rh4ConditionsFalse)
        {
          rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
          rhPtr.rhEvtProg.evt_FirstObject(((PARAM_OBJECT) this.evtParams[0]).oiList);
          return false;
        }
        bool flag1 = false;
        if (((int) this.evtFlags2 & 1) != 0)
          flag1 = true;
        CObject pHo = rhPtr.rhEvtProg.evt_FirstObject(this.evtOiList);
        if (pHo == null)
          return this.negaFALSE();
        int nselectedObjects1 = rhPtr.rhEvtProg.evtNSelectedObjects;
        int num = nselectedObjects1;
        short oi = ((PARAM_OBJECT) this.evtParams[0]).oi;
        short[] pOiColList;
        if (oi >= (short) 0)
        {
          rhPtr.isColArray[0] = oi;
          rhPtr.isColArray[1] = ((PARAM_OBJECT) this.evtParams[0]).oiList;
          pOiColList = rhPtr.isColArray;
        }
        else
          pOiColList = rhPtr.rhEvtProg.qualToOiList[(int) ((PARAM_OBJECT) this.evtParams[0]).oiList & (int) short.MaxValue].qoiList;
        CArrayList carrayList1 = new CArrayList();
        do
        {
          CArrayList carrayList2 = rhPtr.objectAllCol_IXY(pHo, pHo.roc.rcImage, pHo.roc.rcAngle, pHo.roc.rcScaleX, pHo.roc.rcScaleY, pHo.hoX, pHo.hoY, pOiColList);
          if (carrayList2 == null)
          {
            if (!flag1)
            {
              --nselectedObjects1;
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
            }
          }
          else
          {
            bool flag2 = false;
            for (int index = 0; index < carrayList2.size(); ++index)
            {
              CObject o = (CObject) carrayList2.get(index);
              if (((int) o.hoFlags & 1) == 0)
              {
                carrayList1.add((object) o);
                flag2 = true;
              }
            }
            if (flag1)
            {
              if (flag2)
              {
                --nselectedObjects1;
                rhPtr.rhEvtProg.evt_DeleteCurrentObject();
              }
            }
            else if (!flag2)
            {
              --nselectedObjects1;
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
            }
          }
          pHo = rhPtr.rhEvtProg.evt_NextObject();
        }
        while (pHo != null);
        if (!flag1)
        {
          if (nselectedObjects1 == 0)
            return false;
        }
        else if (nselectedObjects1 < num)
          return false;
        CObject cobject1 = rhPtr.rhEvtProg.evt_FirstObject(((PARAM_OBJECT) this.evtParams[0]).oiList);
        if (cobject1 == null)
          return false;
        int nselectedObjects2 = rhPtr.rhEvtProg.evtNSelectedObjects;
        if (!flag1)
        {
          do
          {
            int index;
            for (index = 0; index < carrayList1.size(); ++index)
            {
              CObject cobject2 = (CObject) carrayList1.get(index);
              if (cobject1 == cobject2)
                break;
            }
            if (index == carrayList1.size())
            {
              --nselectedObjects2;
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
            }
            cobject1 = rhPtr.rhEvtProg.evt_NextObject();
          }
          while (cobject1 != null);
          return nselectedObjects2 != 0;
        }
        do
        {
          for (int index = 0; index < carrayList1.size(); ++index)
          {
            CObject cobject3 = (CObject) carrayList1.get(index);
            if (cobject1 == cobject3)
            {
              --nselectedObjects2;
              rhPtr.rhEvtProg.evt_DeleteCurrentObject();
              break;
            }
          }
          cobject1 = rhPtr.rhEvtProg.evt_NextObject();
        }
        while (cobject1 != null);
        return nselectedObjects2 != 0;
      }

      public abstract bool eva1(CRun rhPtr, CObject hoPtr);

      public abstract bool eva2(CRun rhPtr);
    }
}
