// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// RuntimeXNA.Events.CEventProgram
using System;
using Microsoft.Xna.Framework.Input;
using RuntimeXNA.Actions;
using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Conditions;
using RuntimeXNA.Events;
using RuntimeXNA.Expressions;
using RuntimeXNA.Frame;
using RuntimeXNA.Objects;
using RuntimeXNA.Params;
using RuntimeXNA.RunLoop;
using RuntimeXNA.Services;
using RuntimeXNA.Sprites;

public class CEventProgram
{
    public const short EVENTS_EXTBASE = 80;

    public const int PARAMCLICK_DOUBLE = 256;

    public CRun rhPtr;

    public short maxObjects;

    public short maxOi;

    public short nPlayers;

    public short[] nConditions = new short[17];

    public short nQualifiers;

    public int nEvents;

    public CLoadQualifiers[] qualifiers;

    public CEventGroup[] events;

    public CQualToOiList[] qualToOiList;

    public int[] listPointers;

    public CEventGroup[] eventPointersGroup;

    public sbyte[] eventPointersCnd;

    public short[] limitBuffer;

    public int[] rhEvents = new int[8];

    public bool rhEventAlways;

    public int rh4TimerEventsBase;

    internal short[] colBuffer;

    internal int qualOilPtr;

    internal int qualOilPos;

    internal int qualOilPtr2;

    internal int qualOilPos2;

    public bool rh4CheckDoneInstart;

    public CEventGroup rhEventGroup;

    public int rhCurCode;

    public int[] rh4PickFlags = new int[4];

    public bool rh2ActionLoop;

    public bool rh2ActionOn;

    public bool rh2EnablePick;

    public int rh2EventCount;

    public int rh2ActionCount;

    public int rh2ActionLoopCount;

    public int rh4EventCountOR;

    public bool rh4ConditionsFalse;

    public bool rh3DoStop;

    public CQualToOiList rh2EventQualPos;

    public int rh2EventQualPosNum;

    public CObject rh2EventPos;

    public int rh2EventPosOiList;

    public CObject rh2EventPrev;

    public CObjInfo rh2EventPrevOiList;

    public int evtNSelectedObjects;

    public bool repeatFlag;

    public short rh2EventType;

    public short rhCurOi;

    public int rhCurParam0;

    public int rhCurParam1;

    public int rh3CurrentMenu;

    public short rh2CurrentClick;

    public CObject rh4_2ndObject;

    public bool bReady;

    public CArrayList rh2ShuffleBuffer;

    public short rhCurObjectNumber;

    public short rh1stObjectNumber;

    public CArrayList rh2PushedEvents;

    public bool rh2ActionEndRoutine;

    public bool bTestAllKeys;

    internal virtual bool bts(int[] array, int index)
    {
        int num = index / 32;
        int num2 = 1 << (index & 0x1F);
        bool result = (array[num] & num2) != 0;
        array[num] |= num2;
        return result;
    }

    public virtual CObject evt_FirstObjectFromType(short nType)
    {
        rh2EventType = nType;
        if (nType == -1)
        {
            CObject cObject = null;
            bool bStore = true;
            for (int i = 0; i < rhPtr.rhOiList.Length; i++)
            {
                CObjInfo cObjInfo = rhPtr.rhOiList[i];
                if (!bts(rh4PickFlags, cObjInfo.oilType))
                {
                    CObject cObject2 = evt_SelectAllFromType(cObjInfo.oilType, bStore);
                    if (cObject2 != null)
                    {
                        cObject = cObject2;
                        bStore = false;
                    }
                }
            }
            if (cObject != null)
            {
                return cObject;
            }
        }
        else if (!bts(rh4PickFlags, nType))
        {
            return evt_SelectAllFromType(nType, true);
        }
        int num = 0;
        do
        {
            CObjInfo cObjInfo2 = rhPtr.rhOiList[num];
            if (cObjInfo2.oilType == nType && cObjInfo2.oilListSelected >= 0)
            {
                CObject result = rhPtr.rhObjectList[cObjInfo2.oilListSelected];
                rh2EventPrev = null;
                rh2EventPrevOiList = cObjInfo2;
                rh2EventPos = result;
                rh2EventPosOiList = num;
                return result;
            }
            num++;
        }
        while (num < rhPtr.rhMaxOI);
        return null;
    }

    public virtual CObject evt_NextObjectFromType()
    {
        CObject cObject = rh2EventPos;
        if (cObject == null)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[rh2EventPosOiList];
            if (cObjInfo.oilListSelected >= 0)
            {
                cObject = rhPtr.rhObjectList[cObjInfo.oilListSelected];
                rh2EventPrev = null;
                rh2EventPrevOiList = cObjInfo;
                rh2EventPos = cObject;
                return cObject;
            }
        }
        if (cObject != null && cObject.hoNextSelected >= 0)
        {
            rh2EventPrev = cObject;
            rh2EventPrevOiList = null;
            return rh2EventPos = rhPtr.rhObjectList[cObject.hoNextSelected];
        }
        int num = rh2EventPosOiList;
        short oilType = rhPtr.rhOiList[num].oilType;
        for (num++; num < rhPtr.rhMaxOI; num++)
        {
            if (((rh2EventType != -1 && rhPtr.rhOiList[num].oilType == oilType) || rh2EventType == -1) && rhPtr.rhOiList[num].oilListSelected >= 0)
            {
                cObject = rhPtr.rhObjectList[rhPtr.rhOiList[num].oilListSelected];
                rh2EventPrev = null;
                rh2EventPrevOiList = rhPtr.rhOiList[num];
                rh2EventPos = cObject;
                rh2EventPosOiList = num;
                return cObject;
            }
        }
        return null;
    }

    public virtual CObject evt_SelectAllFromType(short nType, bool bStore)
    {
        int num = -1;
        int num2 = rh2EventCount;
        int num3 = 0;
        do
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[num3];
            if (cObjInfo.oilType == nType && cObjInfo.oilEventCount != num2)
            {
                cObjInfo.oilEventCount = num2;
                if (rh4ConditionsFalse)
                {
                    cObjInfo.oilListSelected = -1;
                    cObjInfo.oilNumOfSelected = 0;
                }
                else
                {
                    cObjInfo.oilNumOfSelected = cObjInfo.oilNObjects;
                    short num4 = cObjInfo.oilObject;
                    if (num4 >= 0)
                    {
                        if (num == -1 && bStore)
                        {
                            num = num4;
                            rh2EventPrev = null;
                            rh2EventPrevOiList = cObjInfo;
                            rh2EventPosOiList = num3;
                        }
                        do
                        {
                            CObject cObject = rhPtr.rhObjectList[num4];
                            cObject.hoNextSelected = cObject.hoNumNext;
                            num4 = cObject.hoNumNext;
                        }
                        while (num4 >= 0);
                        num4 = cObjInfo.oilObject;
                    }
                    cObjInfo.oilListSelected = num4;
                }
            }
            num3++;
        }
        while (num3 < rhPtr.rhMaxOI);
        if (!bStore)
        {
            return null;
        }
        if (num < 0)
        {
            return null;
        }
        return rh2EventPos = rhPtr.rhObjectList[num];
    }

    public virtual CObject evt_FirstObject(short sEvtOiList)
    {
        evtNSelectedObjects = 0;
        rh2EventQualPos = null;
        rh2EventQualPosNum = -1;
        if (sEvtOiList < 0)
        {
            if (sEvtOiList == -1)
            {
                return null;
            }
            return qualProc(sEvtOiList);
        }
        CObjInfo cObjInfo = rhPtr.rhOiList[sEvtOiList];
        CObject result;
        if (cObjInfo.oilEventCount == rh2EventCount)
        {
            if (cObjInfo.oilListSelected < 0)
            {
                return null;
            }
            result = rhPtr.rhObjectList[cObjInfo.oilListSelected];
            rh2EventPrev = null;
            rh2EventPrevOiList = cObjInfo;
            rh2EventPos = result;
            rh2EventPosOiList = sEvtOiList;
            evtNSelectedObjects = cObjInfo.oilNumOfSelected;
            return result;
        }
        cObjInfo.oilEventCount = rh2EventCount;
        if (rh4ConditionsFalse)
        {
            cObjInfo.oilListSelected = -1;
            cObjInfo.oilNumOfSelected = 0;
            return null;
        }
        cObjInfo.oilListSelected = cObjInfo.oilObject;
        if (cObjInfo.oilObject < 0)
        {
            cObjInfo.oilNumOfSelected = 0;
            return null;
        }
        short num = cObjInfo.oilObject;
        do
        {
            result = rhPtr.rhObjectList[num];
            num = (result.hoNextSelected = result.hoNumNext);
        }
        while (num >= 0);
        result = rhPtr.rhObjectList[cObjInfo.oilObject];
        rh2EventPrev = null;
        rh2EventPrevOiList = cObjInfo;
        rh2EventPos = result;
        rh2EventPosOiList = sEvtOiList;
        cObjInfo.oilNumOfSelected = cObjInfo.oilNObjects;
        evtNSelectedObjects = cObjInfo.oilNumOfSelected;
        return result;
    }

    internal virtual CObject qualProc(short sEvtOiList)
    {
        int num = 0;
        int i = 0;
        CQualToOiList cQualToOiList;
        for (cQualToOiList = qualToOiList[sEvtOiList & 0x7FFF]; i < cQualToOiList.qoiList.Length; i += 2)
        {
            short num2 = cQualToOiList.qoiList[i + 1];
            CObjInfo cObjInfo = rhPtr.rhOiList[num2];
            int num3;
            if (cObjInfo.oilEventCount == rh2EventCount)
            {
                num3 = 0;
                if (cObjInfo.oilListSelected >= 0)
                {
                    num3 = cObjInfo.oilNumOfSelected;
                    if (rh2EventQualPos == null)
                    {
                        rh2EventQualPos = cQualToOiList;
                        rh2EventQualPosNum = i;
                    }
                }
            }
            else
            {
                num3 = 0;
                cObjInfo.oilEventCount = rh2EventCount;
                if (rh4ConditionsFalse)
                {
                    cObjInfo.oilListSelected = -1;
                    cObjInfo.oilNumOfSelected = 0;
                }
                else
                {
                    cObjInfo.oilListSelected = cObjInfo.oilObject;
                    if (cObjInfo.oilObject < 0)
                    {
                        cObjInfo.oilNumOfSelected = 0;
                    }
                    else
                    {
                        if (rh2EventQualPos == null)
                        {
                            rh2EventQualPos = cQualToOiList;
                            rh2EventQualPosNum = i;
                        }
                        short num4 = cObjInfo.oilObject;
                        do
                        {
                            CObject cObject = rhPtr.rhObjectList[num4];
                            cObject.hoNextSelected = cObject.hoNumNext;
                            num4 = cObject.hoNumNext;
                        }
                        while (num4 >= 0);
                        cObjInfo.oilNumOfSelected = cObjInfo.oilNObjects;
                        num3 = cObjInfo.oilNObjects;
                    }
                }
            }
            num += num3;
        }
        cQualToOiList = rh2EventQualPos;
        if (cQualToOiList != null)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[cQualToOiList.qoiList[rh2EventQualPosNum + 1]];
            rh2EventPrev = null;
            rh2EventPrevOiList = cObjInfo;
            CObject cObject = (rh2EventPos = rhPtr.rhObjectList[cObjInfo.oilListSelected]);
            rh2EventPosOiList = cQualToOiList.qoiList[rh2EventQualPosNum + 1];
            evtNSelectedObjects = num;
            return cObject;
        }
        return null;
    }

    public virtual CObject evt_NextObject()
    {
        CObject cObject = rh2EventPos;
        CObjInfo cObjInfo;
        if (cObject == null)
        {
            cObjInfo = rhPtr.rhOiList[rh2EventPosOiList];
            if (cObjInfo.oilListSelected >= 0)
            {
                cObject = rhPtr.rhObjectList[cObjInfo.oilListSelected];
                rh2EventPrev = null;
                rh2EventPrevOiList = cObjInfo;
                rh2EventPos = cObject;
                return cObject;
            }
        }
        if (cObject != null && cObject.hoNextSelected >= 0)
        {
            rh2EventPrev = cObject;
            rh2EventPrevOiList = null;
            return rh2EventPos = rhPtr.rhObjectList[cObject.hoNextSelected];
        }
        if (rh2EventQualPos == null)
        {
            return null;
        }
        do
        {
            rh2EventQualPosNum += 2;
            if (rh2EventQualPosNum >= rh2EventQualPos.qoiList.Length)
            {
                return null;
            }
            cObjInfo = rhPtr.rhOiList[rh2EventQualPos.qoiList[rh2EventQualPosNum + 1]];
        }
        while (cObjInfo.oilListSelected < 0);
        rh2EventPrev = null;
        rh2EventPrevOiList = cObjInfo;
        cObject = (rh2EventPos = rhPtr.rhObjectList[cObjInfo.oilListSelected]);
        rh2EventPosOiList = rh2EventQualPos.qoiList[rh2EventQualPosNum + 1];
        return cObject;
    }

    public virtual void evt_AddCurrentQualifier(short qual)
    {
        CQualToOiList cQualToOiList = qualToOiList[qual & 0x7FFF];
        for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[cQualToOiList.qoiList[i + 1]];
            if (cObjInfo.oilEventCount != rh2EventCount)
            {
                cObjInfo.oilEventCount = rh2EventCount;
                cObjInfo.oilNumOfSelected = 0;
                cObjInfo.oilListSelected = -1;
            }
        }
    }

    public virtual void evt_DeleteCurrentObject()
    {
        rh2EventPos.hoOiList.oilNumOfSelected--;
        if (rh2EventPrev != null)
        {
            rh2EventPrev.hoNextSelected = rh2EventPos.hoNextSelected;
            rh2EventPos = rh2EventPrev;
        }
        else
        {
            rh2EventPrevOiList.oilListSelected = rh2EventPos.hoNextSelected;
            rh2EventPos = null;
        }
    }

    public virtual void evt_AddCurrentObject(CObject pHo)
    {
        CObjInfo hoOiList = pHo.hoOiList;
        if (hoOiList.oilEventCount != rh2EventCount)
        {
            hoOiList.oilEventCount = rh2EventCount;
            hoOiList.oilListSelected = pHo.hoNumber;
            hoOiList.oilNumOfSelected = 1;
            pHo.hoNextSelected = -1;
            return;
        }
        short num = hoOiList.oilListSelected;
        if (num < 0)
        {
            hoOiList.oilListSelected = pHo.hoNumber;
            hoOiList.oilNumOfSelected++;
            pHo.hoNextSelected = -1;
            return;
        }
        CObject cObject;
        do
        {
            if (pHo.hoNumber == num)
            {
                return;
            }
            cObject = rhPtr.rhObjectList[num];
            num = cObject.hoNextSelected;
        }
        while (num >= 0);
        cObject.hoNextSelected = pHo.hoNumber;
        pHo.hoNextSelected = -1;
        pHo.hoOiList.oilNumOfSelected++;
    }

    public virtual void evt_ForceOneObject(CObject pHo)
    {
        pHo.hoNextSelected = -1;
        pHo.hoOiList.oilListSelected = pHo.hoNumber;
        pHo.hoOiList.oilNumOfSelected = 1;
        pHo.hoOiList.oilEventCount = rh2EventCount;
    }

    public virtual void evt_DeleteCurrentType(short nType)
    {
        bts(rh4PickFlags, nType);
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            if (cObjInfo.oilType == nType)
            {
                cObjInfo.oilEventCount = rh2EventCount;
                cObjInfo.oilListSelected = -1;
                cObjInfo.oilNumOfSelected = 0;
            }
        }
    }

    public virtual void evt_DeleteCurrent()
    {
        rh4PickFlags[0] = -1;
        rh4PickFlags[1] = -1;
        rh4PickFlags[2] = -1;
        rh4PickFlags[3] = -1;
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            cObjInfo.oilEventCount = rh2EventCount;
            cObjInfo.oilListSelected = -1;
            cObjInfo.oilNumOfSelected = 0;
        }
    }

    internal virtual void evt_MarkSelectedObjects()
    {
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            if (cObjInfo.oilEventCount != rh2EventCount)
            {
                continue;
            }
            short num;
            if (cObjInfo.oilEventCountOR != rh4EventCountOR)
            {
                cObjInfo.oilEventCountOR = rh4EventCountOR;
                num = cObjInfo.oilObject;
                while (num >= 0)
                {
                    CObject cObject = rhPtr.rhObjectList[num];
                    cObject.hoSelectedInOR = 0;
                    num = cObject.hoNumNext;
                }
            }
            num = cObjInfo.oilListSelected;
            while (num >= 0)
            {
                CObject cObject = rhPtr.rhObjectList[num];
                cObject.hoSelectedInOR = 1;
                num = cObject.hoNextSelected;
            }
        }
    }

    internal virtual void evt_BranchSelectedObjects()
    {
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            if (cObjInfo.oilEventCountOR != rh4EventCountOR)
            {
                continue;
            }
            cObjInfo.oilEventCount = rh2EventCount;
            short num = cObjInfo.oilObject;
            CObject cObject = null;
            while (num >= 0)
            {
                CObject cObject2 = rhPtr.rhObjectList[num];
                if (cObject2.hoSelectedInOR != 0)
                {
                    if (cObject != null)
                    {
                        cObject.hoNextSelected = num;
                    }
                    else
                    {
                        cObjInfo.oilListSelected = num;
                    }
                    cObject2.hoNextSelected = -1;
                    cObject = cObject2;
                }
                num = cObject2.hoNumNext;
            }
        }
    }

    public virtual CObject get_ExpressionObjects(short expoi)
    {
        if (rh2ActionOn)
        {
            rh2EnablePick = false;
            return get_CurrentObjects(expoi);
        }
        if (expoi >= 0)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[expoi];
            if (cObjInfo.oilEventCount == rh2EventCount)
            {
                if (cObjInfo.oilListSelected >= 0)
                {
                    return rhPtr.rhObjectList[cObjInfo.oilListSelected];
                }
                if (cObjInfo.oilObject >= 0)
                {
                    return rhPtr.rhObjectList[cObjInfo.oilObject];
                }
                return null;
            }
            if (cObjInfo.oilObject >= 0)
            {
                return rhPtr.rhObjectList[cObjInfo.oilObject];
            }
            return null;
        }
        CQualToOiList cQualToOiList = qualToOiList[expoi & 0x7FFF];
        int num = 0;
        if (num >= cQualToOiList.qoiList.Length)
        {
            return null;
        }
        do
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[cQualToOiList.qoiList[num + 1]];
            if (cObjInfo.oilEventCount == rh2EventCount && cObjInfo.oilListSelected >= 0)
            {
                return rhPtr.rhObjectList[cObjInfo.oilListSelected];
            }
            num += 2;
        }
        while (num < cQualToOiList.qoiList.Length);
        num = 0;
        do
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[cQualToOiList.qoiList[num + 1]];
            if (cObjInfo.oilObject >= 0)
            {
                return rhPtr.rhObjectList[cObjInfo.oilObject];
            }
            num += 2;
        }
        while (num < cQualToOiList.qoiList.Length);
        return null;
    }

    public virtual CObject get_ParamActionObjects(short qoil, CAct pAction)
    {
        rh2EnablePick = true;
        CObject cObject = get_CurrentObjects(qoil);
        if (cObject != null)
        {
            if (!repeatFlag)
            {
                pAction.evtFlags &= 1;
                return cObject;
            }
            pAction.evtFlags |= 1;
            rh2ActionLoop = true;
            return cObject;
        }
        pAction.evtFlags &= 254;
        pAction.evtFlags |= 16;
        return cObject;
    }

    public virtual CObject get_ActionObjects(CAct pAction)
    {
        pAction.evtFlags &= 239;
        rh2EnablePick = true;
        short evtOiList = pAction.evtOiList;
        CObject cObject = get_CurrentObjects(evtOiList);
        if (cObject != null)
        {
            if (!repeatFlag)
            {
                pAction.evtFlags &= 254;
                return cObject;
            }
            pAction.evtFlags |= 1;
            rh2ActionLoop = true;
            return cObject;
        }
        pAction.evtFlags &= 254;
        pAction.evtFlags |= 16;
        return cObject;
    }

    public virtual CObject get_CurrentObjects(short qoil)
    {
        if (qoil >= 0)
        {
            return get_CurrentObject(qoil);
        }
        return get_CurrentObjectQualifier(qoil);
    }

    public virtual CObject get_CurrentObject(short qoil)
    {
        CObjInfo cObjInfo = rhPtr.rhOiList[qoil];
        CObject cObject;
        if (cObjInfo.oilActionCount != rh2ActionCount)
        {
            cObjInfo.oilActionCount = rh2ActionCount;
            cObjInfo.oilActionLoopCount = rh2ActionLoopCount;
            if (cObjInfo.oilEventCount == rh2EventCount && cObjInfo.oilListSelected >= 0)
            {
                cObjInfo.oilCurrentOi = cObjInfo.oilListSelected;
                cObject = rhPtr.rhObjectList[cObjInfo.oilListSelected];
                cObjInfo.oilNext = cObject.hoNextSelected;
                if (cObject.hoNextSelected < 0)
                {
                    cObjInfo.oilNextFlag = false;
                    cObjInfo.oilCurrentRoutine = 1;
                    repeatFlag = false;
                    return cObject;
                }
                cObjInfo.oilNextFlag = true;
                cObjInfo.oilCurrentRoutine = 2;
                repeatFlag = true;
                return cObject;
            }
            if (rh2EnablePick && cObjInfo.oilEventCount == rh2EventCount)
            {
                cObjInfo.oilCurrentRoutine = 0;
                cObjInfo.oilCurrentOi = -1;
                return null;
            }
            if (cObjInfo.oilObject >= 0)
            {
                cObjInfo.oilCurrentOi = cObjInfo.oilObject;
                cObject = rhPtr.rhObjectList[cObjInfo.oilObject];
                if (cObject == null)
                {
                    cObjInfo.oilCurrentRoutine = 0;
                    cObjInfo.oilCurrentOi = -1;
                    return null;
                }
                if (cObject.hoNumNext >= 0)
                {
                    cObjInfo.oilNext = cObject.hoNumNext;
                    cObjInfo.oilNextFlag = true;
                    cObjInfo.oilCurrentRoutine = 3;
                    repeatFlag = true;
                    return cObject;
                }
                cObjInfo.oilNextFlag = false;
                cObjInfo.oilCurrentRoutine = 1;
                repeatFlag = false;
                return cObject;
            }
            cObjInfo.oilCurrentRoutine = 0;
            cObjInfo.oilCurrentOi = -1;
            return null;
        }
        if (cObjInfo.oilActionLoopCount != rh2ActionLoopCount)
        {
            cObjInfo.oilActionLoopCount = rh2ActionLoopCount;
            switch (cObjInfo.oilCurrentRoutine)
            {
                case 0:
                    repeatFlag = cObjInfo.oilNextFlag;
                    return null;
                case 1:
                    cObject = rhPtr.rhObjectList[cObjInfo.oilCurrentOi];
                    repeatFlag = cObjInfo.oilNextFlag;
                    return cObject;
                case 2:
                    {
                        cObjInfo.oilCurrentOi = cObjInfo.oilNext;
                        cObject = rhPtr.rhObjectList[cObjInfo.oilNext];
                        if (cObject == null)
                        {
                            return null;
                        }
                        short num = cObject.hoNextSelected;
                        if (num < 0)
                        {
                            cObjInfo.oilNextFlag = false;
                            num = cObjInfo.oilListSelected;
                        }
                        cObjInfo.oilNext = num;
                        repeatFlag = cObjInfo.oilNextFlag;
                        return cObject;
                    }
                case 3:
                    {
                        cObjInfo.oilCurrentOi = cObjInfo.oilNext;
                        cObject = rhPtr.rhObjectList[cObjInfo.oilNext];
                        if (cObject == null)
                        {
                            return null;
                        }
                        short num = cObject.hoNumNext;
                        if (num < 0)
                        {
                            cObjInfo.oilNextFlag = false;
                            num = cObjInfo.oilObject;
                        }
                        cObjInfo.oilNext = num;
                        repeatFlag = cObjInfo.oilNextFlag;
                        return cObject;
                    }
            }
        }
        if (cObjInfo.oilCurrentOi < 0)
        {
            return null;
        }
        cObject = rhPtr.rhObjectList[cObjInfo.oilCurrentOi];
        repeatFlag = cObjInfo.oilNextFlag;
        return cObject;
    }

    public virtual CObject get_CurrentObjectQualifier(short qoil)
    {
        CQualToOiList cQualToOiList = qualToOiList[qoil & 0x7FFF];
        CObject cObject;
        if (cQualToOiList.qoiActionCount != rh2ActionCount)
        {
            cQualToOiList.qoiActionCount = rh2ActionCount;
            cQualToOiList.qoiActionLoopCount = rh2ActionLoopCount;
            short num = qoi_GetFirstListSelected(cQualToOiList);
            if (num >= 0)
            {
                cQualToOiList.qoiCurrentOi = num;
                cObject = rhPtr.rhObjectList[num];
                if (cObject == null)
                {
                    cQualToOiList.qoiCurrentRoutine = 0;
                    cQualToOiList.qoiCurrentOi = -1;
                    return null;
                }
                short num2 = cObject.hoNextSelected;
                if (num2 < 0)
                {
                    num2 = qoi_GetNextListSelected(cQualToOiList);
                    if (num2 < 0)
                    {
                        cQualToOiList.qoiCurrentRoutine = 1;
                        cQualToOiList.qoiNextFlag = false;
                        repeatFlag = false;
                        return cObject;
                    }
                }
                cQualToOiList.qoiNext = num2;
                cQualToOiList.qoiCurrentRoutine = 2;
                cQualToOiList.qoiNextFlag = true;
                repeatFlag = true;
                return cObject;
            }
            if (rh2EnablePick && cQualToOiList.qoiSelectedFlag)
            {
                cQualToOiList.qoiCurrentRoutine = 0;
                cQualToOiList.qoiCurrentOi = -1;
                return null;
            }
            num = qoi_GetFirstList(cQualToOiList);
            if (num >= 0)
            {
                cQualToOiList.qoiCurrentOi = num;
                cObject = rhPtr.rhObjectList[num];
                if (cObject != null)
                {
                    num = cObject.hoNumNext;
                    if (num < 0)
                    {
                        num = qoi_GetNextList(cQualToOiList);
                        if (num < 0)
                        {
                            cQualToOiList.qoiCurrentRoutine = 1;
                            cQualToOiList.qoiNextFlag = false;
                            repeatFlag = false;
                            return cObject;
                        }
                    }
                    cQualToOiList.qoiNext = num;
                    cQualToOiList.qoiCurrentRoutine = 3;
                    cQualToOiList.qoiNextFlag = true;
                    repeatFlag = true;
                    return cObject;
                }
            }
            cQualToOiList.qoiCurrentRoutine = 0;
            cQualToOiList.qoiCurrentOi = -1;
            return null;
        }
        if (cQualToOiList.qoiActionLoopCount != rh2ActionLoopCount)
        {
            cQualToOiList.qoiActionLoopCount = rh2ActionLoopCount;
            switch (cQualToOiList.qoiCurrentRoutine)
            {
                case 0:
                    repeatFlag = cQualToOiList.qoiNextFlag;
                    return null;
                case 1:
                    cObject = rhPtr.rhObjectList[cQualToOiList.qoiCurrentOi];
                    repeatFlag = cQualToOiList.qoiNextFlag;
                    return cObject;
                case 2:
                    cQualToOiList.qoiCurrentOi = cQualToOiList.qoiNext;
                    cObject = rhPtr.rhObjectList[cQualToOiList.qoiNext];
                    if (cObject != null)
                    {
                        short num2 = cObject.hoNextSelected;
                        if (num2 < 0)
                        {
                            num2 = qoi_GetNextListSelected(cQualToOiList);
                            if (num2 < 0)
                            {
                                cQualToOiList.qoiNextFlag = false;
                                num2 = qoi_GetFirstListSelected(cQualToOiList);
                            }
                        }
                        cQualToOiList.qoiNext = num2;
                    }
                    repeatFlag = cQualToOiList.qoiNextFlag;
                    return cObject;
                case 3:
                    cQualToOiList.qoiCurrentOi = cQualToOiList.qoiNext;
                    cObject = rhPtr.rhObjectList[cQualToOiList.qoiNext];
                    if (cObject != null)
                    {
                        short num2 = cObject.hoNumNext;
                        if (num2 < 0)
                        {
                            num2 = qoi_GetNextList(cQualToOiList);
                            if (num2 < 0)
                            {
                                cQualToOiList.qoiNextFlag = false;
                                num2 = qoi_GetFirstList(cQualToOiList);
                            }
                        }
                        cQualToOiList.qoiNext = num2;
                    }
                    repeatFlag = cQualToOiList.qoiNextFlag;
                    return cObject;
            }
        }
        if (cQualToOiList.qoiCurrentOi < 0)
        {
            return null;
        }
        cObject = rhPtr.rhObjectList[cQualToOiList.qoiCurrentOi];
        repeatFlag = cQualToOiList.qoiNextFlag;
        return cObject;
    }

    internal virtual short qoi_GetNextListSelected(CQualToOiList pqoi)
    {
        for (int i = pqoi.qoiActionPos; i < pqoi.qoiList.Length; i += 2)
        {
            short num = pqoi.qoiList[i + 1];
            CObjInfo cObjInfo = rhPtr.rhOiList[num];
            if (cObjInfo.oilEventCount == rh2EventCount)
            {
                pqoi.qoiSelectedFlag = true;
                if (cObjInfo.oilListSelected >= 0)
                {
                    pqoi.qoiActionPos = (short)(i + 2);
                    return cObjInfo.oilListSelected;
                }
            }
        }
        return -1;
    }

    internal virtual short qoi_GetFirstListSelected(CQualToOiList pqoi)
    {
        pqoi.qoiActionPos = 0;
        pqoi.qoiSelectedFlag = false;
        return qoi_GetNextListSelected(pqoi);
    }

    internal virtual short qoi_GetNextList(CQualToOiList pqoi)
    {
        for (int i = pqoi.qoiActionPos; i < pqoi.qoiList.Length; i += 2)
        {
            short num = pqoi.qoiList[i + 1];
            CObjInfo cObjInfo = rhPtr.rhOiList[num];
            if (cObjInfo.oilObject >= 0)
            {
                pqoi.qoiActionPos = (short)(i + 2);
                return cObjInfo.oilObject;
            }
        }
        return -1;
    }

    internal virtual short qoi_GetFirstList(CQualToOiList pqoi)
    {
        pqoi.qoiActionPos = 0;
        return qoi_GetNextList(pqoi);
    }

    public virtual void handle_GlobalEvents(int code)
    {
        int num = -(short)(code & 0xFFFF);
        int num2 = -(short)((code >> 16) & 0xFFFF);
        int num3 = listPointers[rhEvents[num] + num2];
        if (num3 != 0)
        {
            computeEventList(num3, null);
        }
    }

    public virtual bool handle_Event(CObject pHo, int code)
    {
        rhCurCode = code;
        int num = -(short)((code >> 16) & 0xFFFF);
        int num2 = listPointers[pHo.hoEvents + num];
        if (num2 != 0)
        {
            computeEventList(num2, pHo);
            return true;
        }
        return false;
    }

    public virtual void compute_TimerEvents()
    {
        int num;
        if ((rhPtr.rhGameFlags & 0x10) != 0)
        {
            num = listPointers[rhEvents[3] + 1];
            if (num != 0)
            {
                listPointers[rhEvents[3] + 1] = -1;
                computeEventList(num, null);
                rh4CheckDoneInstart = true;
            }
            return;
        }
        num = listPointers[rhEvents[4] + 3];
        if (num != 0)
        {
            computeEventList(num, null);
        }
        num = listPointers[rhEvents[3] + 1];
        if (num != 0)
        {
            if (rh4CheckDoneInstart)
            {
                CEventGroup cEventGroup = null;
                int num2 = num;
                do
                {
                    CEventGroup cEventGroup2 = eventPointersGroup[num2];
                    if (cEventGroup2 != cEventGroup)
                    {
                        cEventGroup = cEventGroup2;
                        for (int i = cEventGroup2.evgNCond; i < cEventGroup2.evgNCond + cEventGroup2.evgNAct; i++)
                        {
                            CEvent cEvent = cEventGroup2.evgEvents[i];
                            if ((cEvent.evtFlags & 0x10) == 0)
                            {
                                cEvent.evtFlags |= 8;
                            }
                        }
                    }
                    num2++;
                }
                while (eventPointersGroup[num2] != null);
            }
            computeEventList(num, null);
            listPointers[rhEvents[3] + 1] = 0;
            if (rh4CheckDoneInstart)
            {
                CEventGroup cEventGroup = null;
                int num2 = num;
                do
                {
                    CEventGroup cEventGroup2 = eventPointersGroup[num2];
                    if (cEventGroup2 != cEventGroup)
                    {
                        cEventGroup = cEventGroup2;
                        for (int i = cEventGroup2.evgNCond; i < cEventGroup2.evgNCond + cEventGroup2.evgNAct; i++)
                        {
                            CEvent cEvent = cEventGroup2.evgEvents[i];
                            cEvent.evtFlags &= 247;
                        }
                    }
                    num2++;
                }
                while (eventPointersGroup[num2] != null);
                rh4CheckDoneInstart = false;
            }
        }
        num = listPointers[rhEvents[4] + 2];
        if (num != 0)
        {
            computeEventList(num, null);
        }
        num = listPointers[rhEvents[4] + 1];
        if (num != 0)
        {
            computeEventList(num, null);
        }
    }

    public virtual void restartTimerEvents()
    {
        int num = (int)rhPtr.rhTimer;
        int num2 = listPointers[rhEvents[4] + 3];
        if (num2 == 0)
        {
            return;
        }
        do
        {
            CEventGroup cEventGroup = eventPointersGroup[num2];
            CEvent cEvent = cEventGroup.evgEvents[eventPointersCnd[num2]];
            cEvent.evtFlags |= 2;
            PARAM_TIME pARAM_TIME = (PARAM_TIME)cEvent.evtParams[0];
            if (pARAM_TIME.timer > num)
            {
                cEvent.evtFlags &= 253;
            }
            num2++;
        }
        while (eventPointersGroup[num2] != null);
    }

    public virtual void computeEventList(int num, CObject pHo)
    {
        rh3DoStop = false;
        do
        {
            CEventGroup cEventGroup = eventPointersGroup[num];
            if ((cEventGroup.evgFlags & 0x4000) == 0)
            {
                rhEventGroup = cEventGroup;
                rh4PickFlags[0] = 0;
                rh4PickFlags[1] = 0;
                rh4PickFlags[2] = 0;
                rh4PickFlags[3] = 0;
                if ((cEventGroup.evgFlags & 0x400) == 0)
                {
                    rh2EventCount++;
                    rh4ConditionsFalse = false;
                    int i = 0;
                    if (((CCnd)cEventGroup.evgEvents[i]).eva1(rhPtr, pHo))
                    {
                        for (i++; i < cEventGroup.evgNCond && ((CCnd)cEventGroup.evgEvents[i]).eva2(rhPtr); i++)
                        {
                        }
                    }
                    if (i == cEventGroup.evgNCond)
                    {
                        if (rh3DoStop)
                        {
                            if (pHo != null)
                            {
                                call_Stops(pHo);
                            }
                        }
                        else
                        {
                            call_Actions(cEventGroup);
                        }
                    }
                    num++;
                    continue;
                }
                rh4EventCountOR++;
                bool flag;
                if ((cEventGroup.evgFlags & 0x1000) == 0)
                {
                    flag = false;
                    do
                    {
                        rh2EventCount++;
                        rh4ConditionsFalse = false;
                        int i = eventPointersCnd[num];
                        if (!((CCnd)cEventGroup.evgEvents[i]).eva1(rhPtr, pHo))
                        {
                            rh4ConditionsFalse = true;
                        }
                        for (i++; i < cEventGroup.evgNCond && cEventGroup.evgEvents[i].evtCode != -1507329; i++)
                        {
                            if (!((CCnd)cEventGroup.evgEvents[i]).eva2(rhPtr))
                            {
                                rh4ConditionsFalse = true;
                            }
                        }
                        evt_MarkSelectedObjects();
                        if (!rh4ConditionsFalse)
                        {
                            flag = true;
                        }
                        num++;
                        cEventGroup = eventPointersGroup[num];
                    }
                    while (cEventGroup != null && cEventGroup == rhEventGroup);
                    if (flag)
                    {
                        rh2EventCount++;
                        evt_BranchSelectedObjects();
                        call_Actions(rhEventGroup);
                    }
                    continue;
                }
                rh4ConditionsFalse = false;
                flag = false;
                do
                {
                    rh2EventCount++;
                    bool flag2 = false;
                    int i = eventPointersCnd[num];
                    if (((CCnd)cEventGroup.evgEvents[i]).eva1(rhPtr, pHo))
                    {
                        for (i++; i < cEventGroup.evgNCond && cEventGroup.evgEvents[i].evtCode != -1572865; i++)
                        {
                            if (!((CCnd)cEventGroup.evgEvents[i]).eva2(rhPtr))
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag2 = true;
                    }
                    if (!flag2)
                    {
                        evt_MarkSelectedObjects();
                        flag = true;
                    }
                    num++;
                    cEventGroup = eventPointersGroup[num];
                }
                while (cEventGroup != null && cEventGroup == rhEventGroup);
                if (flag)
                {
                    rh2EventCount++;
                    evt_BranchSelectedObjects();
                    call_Actions(rhEventGroup);
                }
                continue;
            }
            num++;
            if (eventPointersGroup[num] == null)
            {
                continue;
            }
            for (CEventGroup cEventGroup2 = eventPointersGroup[num]; cEventGroup2 == cEventGroup; cEventGroup2 = eventPointersGroup[num])
            {
                num++;
                if (eventPointersGroup[num] == null)
                {
                    break;
                }
            }
        }
        while (eventPointersGroup[num] != null);
    }

    internal virtual void call_Actions(CEventGroup pEvg)
    {
        if ((pEvg.evgFlags & CEventGroup.EVGFLAGS_LIMITED) != 0)
        {
            if ((pEvg.evgFlags & 0x10) != 0)
            {
                rh2ShuffleBuffer = new CArrayList();
            }
            if ((pEvg.evgFlags & 2) != 0)
            {
                ushort num = (ushort)rhPtr.rhLoopCount;
                ushort evgInhibit = pEvg.evgInhibit;
                pEvg.evgInhibit = num;
                if (num == evgInhibit)
                {
                    return;
                }
                num--;
                if (num == evgInhibit)
                {
                    return;
                }
            }
            if ((pEvg.evgFlags & 4) != 0)
            {
                if (pEvg.evgInhibitCpt == 0)
                {
                    return;
                }
                pEvg.evgInhibitCpt--;
            }
            if ((pEvg.evgFlags & 8) != 0)
            {
                int num2 = (int)rhPtr.rhTimer / 10;
                int num3 = ((pEvg.evgInhibitCpt < 0) ? (65536 - pEvg.evgInhibitCpt) : pEvg.evgInhibitCpt);
                if (num3 != 0 && num2 < num3)
                {
                    return;
                }
                pEvg.evgInhibitCpt = (short)(num2 + pEvg.evgInhibit);
            }
        }
        rh2ActionCount++;
        rh2ActionLoop = false;
        rh2ActionLoopCount = 0;
        rh2ActionOn = true;
        int num4 = 0;
        do
        {
            CAct cAct = (CAct)pEvg.evgEvents[num4 + pEvg.evgNCond];
            if ((cAct.evtFlags & (CEvent.EVFLAGS_BADOBJECT | 8)) == 0)
            {
                cAct.execute(rhPtr);
            }
            num4++;
        }
        while (num4 < pEvg.evgNAct);
        if (rh2ActionLoop)
        {
            do
            {
                rh2ActionLoop = false;
                rh2ActionLoopCount++;
                num4 = 0;
                do
                {
                    CAct cAct = (CAct)pEvg.evgEvents[num4 + pEvg.evgNCond];
                    if ((cAct.evtFlags & 1) != 0)
                    {
                        cAct.execute(rhPtr);
                    }
                    num4++;
                }
                while (num4 < pEvg.evgNAct);
            }
            while (rh2ActionLoop);
        }
        rh2ActionOn = false;
        if (rh2ShuffleBuffer != null)
        {
            endShuffle();
        }
    }

    internal virtual void call_Stops(CObject pHo)
    {
        short hoOi = pHo.hoOi;
        rh2EventCount++;
        evt_AddCurrentObject(pHo);
        rh2ActionCount++;
        rh2ActionLoop = false;
        rh2ActionLoopCount = 0;
        rh2ActionOn = true;
        int num = 0;
        do
        {
            CAct cAct = (CAct)rhEventGroup.evgEvents[rhEventGroup.evgNCond + num];
            int num2 = cAct.evtCode & -65536;
            if (num2 == 262144 || num2 == 589824)
            {
                if (hoOi == cAct.evtOi)
                {
                    cAct.execute(rhPtr);
                }
                else
                {
                    short evtOiList = cAct.evtOiList;
                    if ((evtOiList & 0x8000) != 0)
                    {
                        CQualToOiList cQualToOiList = qualToOiList[evtOiList & 0x7FFF];
                        for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
                        {
                            if (cQualToOiList.qoiList[i] == hoOi)
                            {
                                cAct.execute(rhPtr);
                                break;
                            }
                        }
                    }
                }
            }
            num++;
        }
        while (num < rhEventGroup.evgNAct);
        rh2ActionOn = false;
    }

    internal virtual void endShuffle()
    {
        if (rh2ShuffleBuffer.size() > 1)
        {
            int num = rhPtr.random((short)rh2ShuffleBuffer.size());
            int num2;
            do
            {
                num2 = rhPtr.random((short)rh2ShuffleBuffer.size());
            }
            while (num == num2);
            CObject cObject = (CObject)rh2ShuffleBuffer.get(num);
            CObject cObject2 = (CObject)rh2ShuffleBuffer.get(num2);
            int hoX = cObject.hoX;
            int hoY = cObject.hoY;
            int hoX2 = cObject2.hoX;
            int hoY2 = cObject2.hoY;
            CRun.setXPosition(cObject, hoX2);
            CRun.setYPosition(cObject, hoY2);
            CRun.setXPosition(cObject2, hoX);
            CRun.setYPosition(cObject2, hoY);
            rh2ShuffleBuffer = null;
        }
    }

    public virtual void onMouseButton(int b, int nClicks)
    {
        if (rhPtr == null || rhPtr.rh2PauseCompteur != 0 || !bReady)
        {
            return;
        }
        int num = b;
        if (nClicks == 2)
        {
            num += 256;
        }
        rhPtr.rh4TimeOut = 0L;
        if (rhPtr.rhMouseUsed != 0)
        {
            return;
        }
        rhCurParam0 = num;
        rh2CurrentClick = (short)num;
        handle_GlobalEvents(-262150);
        handle_GlobalEvents(-327686);
        int num2 = rhPtr.rh2MouseX - rhPtr.rhWindowX;
        int yp = rhPtr.rh2MouseY - rhPtr.rhWindowY;
        CSprite cSprite = null;
        CArrayList cArrayList = new CArrayList();
        while (true)
        {
            cSprite = rhPtr.rhApp.spriteGen.spriteCol_TestPoint(cSprite, -1, num2, yp, 0);
            if (cSprite == null)
            {
                break;
            }
            cArrayList.add(cSprite);
        }
        int i;
        for (i = 0; i < cArrayList.size(); i++)
        {
            cSprite = (CSprite)cArrayList.get(i);
            CObject sprExtraInfo = cSprite.sprExtraInfo;
            if ((sprExtraInfo.hoFlags & 1) == 0)
            {
                rhCurParam1 = sprExtraInfo.hoOi;
                rh4_2ndObject = sprExtraInfo;
                handle_GlobalEvents(-393222);
            }
        }
        i = 0;
        for (int j = 0; j < rhPtr.rhNObjects; j++)
        {
            for (; rhPtr.rhObjectList[i] == null; i++)
            {
            }
            CObject sprExtraInfo = rhPtr.rhObjectList[i];
            i++;
            if ((sprExtraInfo.hoFlags & 0x24) != 0)
            {
                continue;
            }
            int num3 = sprExtraInfo.hoX - sprExtraInfo.hoImgXSpot;
            if (rhPtr.rh2MouseX <= num2 && num3 + sprExtraInfo.hoImgWidth > rhPtr.rh2MouseX)
            {
                int num4 = sprExtraInfo.hoY - sprExtraInfo.hoImgYSpot;
                if (num4 <= rhPtr.rh2MouseY && num4 + sprExtraInfo.hoImgHeight > rhPtr.rh2MouseY && (sprExtraInfo.hoFlags & 1) == 0)
                {
                    rhCurParam1 = sprExtraInfo.hoOi;
                    rh4_2ndObject = sprExtraInfo;
                    handle_GlobalEvents(-393222);
                }
            }
        }
    }

    public virtual void onKeyDown(Keys vk)
    {
        if (rhPtr == null || !bReady)
        {
            return;
        }
        if (rhPtr.rh2PauseCompteur != 0 && rhPtr.bCheckResume)
        {
            if (rhPtr.rh4PauseKey == Keys.None)
            {
                rhPtr.resume();
                rhPtr.rh4EndOfPause = rhPtr.rhLoopCount;
                handle_GlobalEvents(-458755);
            }
            if (rhPtr.rh4PauseKey != Keys.None && rhPtr.rh4PauseKey == vk)
            {
                rhPtr.resume();
                rhPtr.rh4EndOfPause = rhPtr.rhLoopCount;
                handle_GlobalEvents(-458755);
            }
        }
        else
        {
            rhPtr.rh4TimeOut = 0L;
            handle_GlobalEvents(-524294);
        }
    }

    public virtual void onMouseWheel(int units)
    {
        if (rhPtr != null && bReady && rhPtr.rh2PauseCompteur == 0)
        {
            if (units < 0)
            {
                handle_GlobalEvents(-655366);
            }
            else
            {
                handle_GlobalEvents(-720902);
            }
        }
    }

    public virtual void onMouseMove()
    {
        if (rhPtr != null && bReady && rhPtr.rh2PauseCompteur == 0)
        {
            rhPtr.rh4TimeOut = 0L;
        }
    }

    internal virtual bool ctoCompare(PARAM_ZONE pZone, CObject pHo)
    {
        if (pHo.hoImgWidth == 0 || pHo.hoImgHeight == 0)
        {
            return false;
        }
        if (pHo.hoX < pZone.x1 || pHo.hoX >= pZone.x2)
        {
            return false;
        }
        if (pHo.hoY < pZone.y1 || pHo.hoY >= pZone.y2)
        {
            return false;
        }
        return true;
    }

    public virtual CObject count_ZoneTypeObjects(PARAM_ZONE pZone, int stop, short type)
    {
        stop++;
        evtNSelectedObjects = 0;
        int num = 0;
        CObjInfo cObjInfo = null;
        while (true)
        {
            if (num < rhPtr.rhOiList.Length)
            {
                cObjInfo = rhPtr.rhOiList[num];
                if (type != 0 && (type == 0 || type != cObjInfo.oilType))
                {
                    num++;
                    continue;
                }
            }
            if (num == rhPtr.rhOiList.Length)
            {
                break;
            }
            CObjInfo cObjInfo2 = cObjInfo;
            num++;
            if (cObjInfo2.oilEventCount != rh2EventCount)
            {
                if (rh4ConditionsFalse)
                {
                    continue;
                }
                short num2 = cObjInfo2.oilObject;
                while (num2 >= 0)
                {
                    CObject cObject = rhPtr.rhObjectList[num2];
                    if (cObject == null)
                    {
                        return null;
                    }
                    if ((cObject.hoFlags & 1) == 0 && ctoCompare(pZone, cObject))
                    {
                        evtNSelectedObjects++;
                        if (evtNSelectedObjects == stop)
                        {
                            return cObject;
                        }
                    }
                    num2 = cObject.hoNumNext;
                }
                continue;
            }
            short num3 = cObjInfo2.oilListSelected;
            while (num3 >= 0)
            {
                CObject cObject = rhPtr.rhObjectList[num3];
                if (cObject == null)
                {
                    return null;
                }
                if ((cObject.hoFlags & 1) == 0 && ctoCompare(pZone, cObject))
                {
                    evtNSelectedObjects++;
                    if (evtNSelectedObjects == stop)
                    {
                        return cObject;
                    }
                }
                num3 = cObject.hoNextSelected;
            }
        }
        return null;
    }

    public virtual CObject count_ObjectsFromType(short type, int stop)
    {
        stop++;
        evtNSelectedObjects = 0;
        int num = 0;
        CObjInfo cObjInfo = null;
        while (true)
        {
            if (num < rhPtr.rhOiList.Length)
            {
                cObjInfo = rhPtr.rhOiList[num];
                if (type != 0 && (type == 0 || type != cObjInfo.oilType))
                {
                    num++;
                    continue;
                }
            }
            if (num == rhPtr.rhOiList.Length)
            {
                break;
            }
            CObjInfo cObjInfo2 = cObjInfo;
            num++;
            if (cObjInfo2.oilEventCount != rh2EventCount)
            {
                if (rh4ConditionsFalse)
                {
                    continue;
                }
                short num2 = cObjInfo2.oilObject;
                while (num2 >= 0)
                {
                    CObject cObject = rhPtr.rhObjectList[num2];
                    if (cObject == null)
                    {
                        return null;
                    }
                    if ((cObject.hoFlags & 1) == 0)
                    {
                        evtNSelectedObjects++;
                        if (evtNSelectedObjects == stop)
                        {
                            return cObject;
                        }
                    }
                    num2 = cObject.hoNumNext;
                }
                continue;
            }
            short num3 = cObjInfo2.oilListSelected;
            while (num3 >= 0)
            {
                CObject cObject = rhPtr.rhObjectList[num3];
                if (cObject == null)
                {
                    return null;
                }
                if ((cObject.hoFlags & 1) == 0)
                {
                    evtNSelectedObjects++;
                    if (evtNSelectedObjects == stop)
                    {
                        return cObject;
                    }
                }
                num3 = cObject.hoNextSelected;
            }
        }
        return null;
    }

    internal virtual bool czaCompare(PARAM_ZONE pZone, CObject pHo)
    {
        if (pHo.hoX < pZone.x1 || pHo.hoX >= pZone.x2)
        {
            return false;
        }
        if (pHo.hoY < pZone.y1 || pHo.hoY >= pZone.y2)
        {
            return false;
        }
        return true;
    }

    public virtual int select_ZoneTypeObjects(PARAM_ZONE p, short type)
    {
        int num = 0;
        int num2 = 0;
        CObjInfo cObjInfo = null;
        while (true)
        {
            if (num2 < rhPtr.rhOiList.Length)
            {
                cObjInfo = rhPtr.rhOiList[num2];
                if (type != 0 && (type == 0 || type != cObjInfo.oilType))
                {
                    num2++;
                    continue;
                }
            }
            if (num2 == rhPtr.rhOiList.Length)
            {
                break;
            }
            CObjInfo cObjInfo2 = cObjInfo;
            num2++;
            CObject cObject;
            short num3;
            if (cObjInfo2.oilEventCount != rh2EventCount)
            {
                cObject = null;
                cObjInfo2.oilNumOfSelected = 0;
                cObjInfo2.oilEventCount = rh2EventCount;
                cObjInfo2.oilListSelected = -1;
                if (rh4ConditionsFalse)
                {
                    continue;
                }
                num3 = cObjInfo2.oilObject;
                while (num3 >= 0)
                {
                    CObject cObject2 = rhPtr.rhObjectList[num3];
                    if (cObject2 == null)
                    {
                        break;
                    }
                    if ((cObject2.hoFlags & 1) == 0 && czaCompare(p, cObject2))
                    {
                        num++;
                        cObjInfo2.oilNumOfSelected++;
                        cObject2.hoNextSelected = -1;
                        if (cObject == null)
                        {
                            cObjInfo2.oilListSelected = cObject2.hoNumber;
                        }
                        else
                        {
                            cObject.hoNextSelected = cObject2.hoNumber;
                        }
                        cObject = cObject2;
                    }
                    num3 = cObject2.hoNumNext;
                }
                continue;
            }
            cObject = null;
            num3 = cObjInfo2.oilListSelected;
            while (num3 >= 0)
            {
                CObject cObject2 = rhPtr.rhObjectList[num3];
                if (cObject2 == null)
                {
                    break;
                }
                if ((cObject2.hoFlags & 1) == 0)
                {
                    if (!czaCompare(p, cObject2))
                    {
                        cObjInfo2.oilNumOfSelected--;
                        if (cObject == null)
                        {
                            cObjInfo2.oilListSelected = cObject2.hoNextSelected;
                        }
                        else
                        {
                            cObject.hoNextSelected = cObject2.hoNextSelected;
                        }
                    }
                    else
                    {
                        num++;
                        cObject = cObject2;
                    }
                }
                num3 = cObject2.hoNextSelected;
            }
        }
        return num;
    }

    internal virtual bool losCompare(double x1, double y1, double x2, double y2, CObject pHo)
    {
        int num = pHo.hoX - pHo.hoImgXSpot;
        int num2 = num + pHo.hoImgWidth;
        int num3 = pHo.hoY - pHo.hoImgYSpot;
        int num4 = num3 + pHo.hoImgHeight;
        double num5;
        if (x2 - x1 > y2 - y1)
        {
            num5 = (y2 - y1) / (x2 - x1);
            if (x2 > x1)
            {
                if ((double)num2 < x1 || (double)num >= x2)
                {
                    return false;
                }
            }
            else if ((double)num2 < x2 || (double)num >= x1)
            {
                return false;
            }
            int num6 = (int)(num5 * ((double)num - x1) + y1);
            if (num6 >= num3 && num6 < num4)
            {
                return true;
            }
            num6 = (int)(num5 * ((double)num2 - x1) + y1);
            if (num6 >= num3 && num6 < num4)
            {
                return true;
            }
            return false;
        }
        num5 = (x2 - x1) / (y2 - y1);
        if (y2 > y1)
        {
            if ((double)num4 < y1 || (double)num3 >= y2)
            {
                return false;
            }
        }
        else if ((double)num4 < y2 || (double)num3 >= y1)
        {
            return false;
        }
        int num7 = (int)(num5 * ((double)num3 - y1) + x1);
        if (num7 >= num && num7 < num2)
        {
            return true;
        }
        num7 = (int)(num5 * ((double)num3 - y1) + x1);
        if (num7 >= num && num7 < num2)
        {
            return true;
        }
        return false;
    }

    public virtual int select_LineOfSight(int x1, int y1, int x2, int y2)
    {
        int num = 0;
        for (int i = 0; i < rhPtr.rhOiList.Length; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            CObject cObject;
            short num2;
            if (cObjInfo.oilEventCount != rh2EventCount)
            {
                cObject = null;
                cObjInfo.oilNumOfSelected = 0;
                cObjInfo.oilEventCount = rh2EventCount;
                cObjInfo.oilListSelected = -1;
                if (rh4ConditionsFalse)
                {
                    continue;
                }
                num2 = cObjInfo.oilObject;
                while (num2 >= 0)
                {
                    CObject cObject2 = rhPtr.rhObjectList[num2];
                    if (cObject2 == null)
                    {
                        break;
                    }
                    if ((cObject2.hoFlags & 1) == 0 && losCompare(x1, y1, x2, y2, cObject2))
                    {
                        num++;
                        cObjInfo.oilNumOfSelected++;
                        cObject2.hoNextSelected = -1;
                        if (cObject == null)
                        {
                            cObjInfo.oilListSelected = cObject2.hoNumber;
                        }
                        else
                        {
                            cObject.hoNextSelected = cObject2.hoNumber;
                        }
                        cObject = cObject2;
                    }
                    num2 = cObject2.hoNumNext;
                }
                continue;
            }
            cObject = null;
            num2 = cObjInfo.oilListSelected;
            while (num2 >= 0)
            {
                CObject cObject2 = rhPtr.rhObjectList[num2];
                if (cObject2 == null)
                {
                    break;
                }
                if ((cObject2.hoFlags & 1) == 0)
                {
                    if (!losCompare(x1, y1, x2, y2, cObject2))
                    {
                        cObjInfo.oilNumOfSelected--;
                        if (cObject == null)
                        {
                            cObjInfo.oilListSelected = cObject2.hoNextSelected;
                        }
                        else
                        {
                            cObject.hoNextSelected = cObject2.hoNextSelected;
                        }
                    }
                    else
                    {
                        num++;
                        cObject = cObject2;
                    }
                }
                num2 = cObject2.hoNextSelected;
            }
        }
        return num;
    }

    public virtual int czoCountThem(short oil, PARAM_ZONE pZone)
    {
        int num = 0;
        CObjInfo cObjInfo = rhPtr.rhOiList[oil];
        if (cObjInfo.oilEventCount != rh2EventCount)
        {
            if (!rh4ConditionsFalse)
            {
                short num2 = cObjInfo.oilObject;
                while (num2 >= 0)
                {
                    CObject cObject = rhPtr.rhObjectList[num2];
                    if (cObject == null)
                    {
                        return 0;
                    }
                    if ((cObject.hoFlags & 1) == 0 && czaCompare(pZone, cObject))
                    {
                        num++;
                    }
                    num2 = cObject.hoNumNext;
                }
            }
            return num;
        }
        short num3 = cObjInfo.oilListSelected;
        while (num3 >= 0)
        {
            CObject cObject = rhPtr.rhObjectList[num3];
            if (cObject == null)
            {
                return 0;
            }
            if ((cObject.hoFlags & 1) == 0 && czaCompare(pZone, cObject))
            {
                num++;
            }
            num3 = cObject.hoNextSelected;
        }
        return num;
    }

    public virtual int count_ZoneOneObject(short oil, PARAM_ZONE pZone)
    {
        if (oil >= 0)
        {
            return czoCountThem(oil, pZone);
        }
        if (oil == -1)
        {
            return 0;
        }
        CQualToOiList cQualToOiList = qualToOiList[oil & 0x7FFF];
        int num = 0;
        for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
        {
            num += czoCountThem(cQualToOiList.qoiList[i + 1], pZone);
        }
        return num;
    }

    internal virtual CObject countThem(short oil, int stop)
    {
        CObjInfo cObjInfo = rhPtr.rhOiList[oil];
        if (cObjInfo.oilEventCount != rh2EventCount)
        {
            if (rh4ConditionsFalse)
            {
                evtNSelectedObjects = 0;
                return null;
            }
            short num = cObjInfo.oilObject;
            while (num >= 0)
            {
                CObject cObject = rhPtr.rhObjectList[num];
                if (cObject == null)
                {
                    return null;
                }
                if ((cObject.hoFlags & 1) == 0)
                {
                    evtNSelectedObjects++;
                    if (evtNSelectedObjects == stop)
                    {
                        return cObject;
                    }
                }
                num = cObject.hoNumNext;
            }
            return null;
        }
        short num2 = cObjInfo.oilListSelected;
        while (num2 >= 0)
        {
            CObject cObject = rhPtr.rhObjectList[num2];
            if (cObject == null)
            {
                return null;
            }
            if ((cObject.hoFlags & 1) == 0)
            {
                evtNSelectedObjects++;
                if (evtNSelectedObjects == stop)
                {
                    return cObject;
                }
            }
            num2 = cObject.hoNextSelected;
        }
        return null;
    }

    public virtual CObject count_ObjectsFromOiList(short oil, int stop)
    {
        stop++;
        evtNSelectedObjects = 0;
        if (oil >= 0)
        {
            return countThem(oil, stop);
        }
        if (oil == -1)
        {
            return null;
        }
        CQualToOiList cQualToOiList = qualToOiList[oil & 0x7FFF];
        for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
        {
            CObject cObject = countThem(cQualToOiList.qoiList[i + 1], stop);
            if (cObject != null)
            {
                return cObject;
            }
        }
        return null;
    }

    public virtual bool pickFromId(int val)
    {
        int num = val & 0xFFFF;
        if (num > rhPtr.rhMaxObjects)
        {
            return false;
        }
        CObject cObject = rhPtr.rhObjectList[num];
        if (cObject == null)
        {
            return false;
        }
        int num2 = (val >> 16) & 0xFFFF;
        if (num2 != cObject.hoCreationId)
        {
            return false;
        }
        CObjInfo hoOiList = cObject.hoOiList;
        if (hoOiList.oilEventCount == rh2EventCount)
        {
            short num3 = hoOiList.oilListSelected;
            CObject cObject2 = null;
            while (num3 >= 0)
            {
                cObject2 = rhPtr.rhObjectList[num3];
                if (cObject == cObject2)
                {
                    break;
                }
                num3 = cObject2.hoNextSelected;
            }
            if (cObject != cObject2)
            {
                return false;
            }
        }
        hoOiList.oilEventCount = rh2EventCount;
        hoOiList.oilListSelected = -1;
        hoOiList.oilNumOfSelected = 0;
        cObject.hoNextSelected = -1;
        evt_AddCurrentObject(cObject);
        return true;
    }

    public virtual void push_Event(int routine, int code, int lParam, CObject pHo, short oi)
    {
        CPushedEvent o = new CPushedEvent(routine, code, lParam, pHo, oi);
        if (rh2PushedEvents == null)
        {
            rh2PushedEvents = new CArrayList();
        }
        rh2PushedEvents.add(o);
    }

    public virtual void handle_PushedEvents()
    {
        if (rh2PushedEvents == null)
        {
            return;
        }
        for (int i = 0; i < rh2PushedEvents.size(); i++)
        {
            CPushedEvent cPushedEvent = (CPushedEvent)rh2PushedEvents.get(i);
            if (cPushedEvent != null && cPushedEvent.code != 0)
            {
                rhCurParam0 = cPushedEvent.param;
                rhCurOi = cPushedEvent.oi;
                switch (cPushedEvent.routine)
                {
                    case 0:
                        handle_GlobalEvents(cPushedEvent.code);
                        break;
                    case 1:
                        handle_Event(cPushedEvent.pHo, cPushedEvent.code);
                        break;
                }
            }
        }
        rh2PushedEvents.clear();
    }

    public virtual void load(CRunApp app)
    {
        byte[] array = new byte[4];
        int num = 0;
        while (true)
        {
            app.file.read(array);
            if (array[0] == 69 && array[1] == 82 && array[2] == 62 && array[3] == 62)
            {
                maxObjects = app.file.readAShort();
                if (maxObjects < 300)
                {
                    maxObjects = 300;
                }
                maxOi = app.file.readAShort();
                nPlayers = app.file.readAShort();
                for (int i = 0; i < 17; i++)
                {
                    nConditions[i] = app.file.readAShort();
                }
                nQualifiers = app.file.readAShort();
                if (nQualifiers > 0)
                {
                    qualifiers = new CLoadQualifiers[nQualifiers];
                    for (int i = 0; i < nQualifiers; i++)
                    {
                        qualifiers[i] = new CLoadQualifiers();
                        qualifiers[i].qOi = app.file.readAShort();
                        qualifiers[i].qType = app.file.readAShort();
                    }
                }
            }
            else if (array[0] == 69 && array[1] == 82 && array[2] == 101 && array[3] == 115)
            {
                app.file.readAInt();
                nEvents = app.file.readAInt();
                events = new CEventGroup[nEvents];
                num = 0;
            }
            else if (array[0] == 69 && array[1] == 82 && array[2] == 101 && array[3] == 118)
            {
                app.file.readAInt();
                int num2 = app.file.readAInt();
                for (int i = 0; i < num2; i++)
                {
                    events[num] = CEventGroup.create(app);
                    num++;
                }
            }
            else if (array[0] == 60 && array[1] == 60 && array[2] == 69 && array[3] == 82)
            {
                break;
            }
        }
    }

    internal virtual int inactiveGroup(int evg)
    {
        CEventGroup cEventGroup = events[evg];
        cEventGroup.evgFlags &= CEventGroup.EVGFLAGS_DEFAULTMASK;
        cEventGroup.evgFlags |= 16384;
        evg++;
        bool flag = false;
        while (true)
        {
            cEventGroup = events[evg];
            cEventGroup.evgFlags &= CEventGroup.EVGFLAGS_DEFAULTMASK;
            cEventGroup.evgFlags |= 16384;
            CEvent cEvent = cEventGroup.evgEvents[0];
            switch (cEvent.evtCode)
            {
                case -589825:
                    {
                        PARAM_GROUP pARAM_GROUP = (PARAM_GROUP)cEvent.evtParams[0];
                        pARAM_GROUP.grpFlags |= 4;
                        evg = inactiveGroup(evg);
                        continue;
                    }
                case -655361:
                    flag = true;
                    evg++;
                    break;
            }
            if (flag)
            {
                break;
            }
            evg++;
        }
        return evg;
    }

    public virtual void prepareProgram()
    {
        bTestAllKeys = false;
        CArrayList cArrayList = new CArrayList();
        int i;
        for (i = 0; i < events.Length; i++)
        {
            CEventGroup cEventGroup = events[i];
            cEventGroup.evgFlags &= CEventGroup.EVGFLAGS_DEFAULTMASK;
            CEvent cEvent = cEventGroup.evgEvents[0];
            if (cEvent.evtCode == -589825)
            {
                PARAM_GROUP pARAM_GROUP = (PARAM_GROUP)cEvent.evtParams[0];
                CGroupFind cGroupFind = new CGroupFind();
                cGroupFind.id = pARAM_GROUP.grpId;
                cGroupFind.evg = i;
                cArrayList.add(cGroupFind);
                pARAM_GROUP.grpFlags &= -13;
                if ((pARAM_GROUP.grpFlags & 1) != 0)
                {
                    pARAM_GROUP.grpFlags |= 8;
                }
            }
            else if (cEvent.evtCode == -262148 || cEvent.evtCode == -524294)
            {
                bTestAllKeys = true;
            }
        }
        i = 0;
        while (i < events.Length)
        {
            CEventGroup cEventGroup = events[i];
            cEventGroup.evgFlags &= CEventGroup.EVGFLAGS_DEFAULTMASK;
            CEvent cEvent = cEventGroup.evgEvents[0];
            if (cEvent.evtCode == -589825)
            {
                PARAM_GROUP pARAM_GROUP = (PARAM_GROUP)cEvent.evtParams[0];
                pARAM_GROUP.grpFlags &= -5;
                if ((pARAM_GROUP.grpFlags & 8) != 0)
                {
                    i = inactiveGroup(i);
                    continue;
                }
            }
            i++;
        }
        for (i = 0; i < events.Length; i++)
        {
            CEventGroup cEventGroup = events[i];
            CEvent cEvent = cEventGroup.evgEvents[0];
            int evtCode = cEvent.evtCode;
            if (evtCode == -655361 || evtCode == -589825)
            {
                continue;
            }
            cEventGroup.evgInhibit = 0;
            cEventGroup.evgInhibitCpt = 0;
            for (int j = 0; j < cEventGroup.evgNCond + cEventGroup.evgNAct; j++)
            {
                cEvent = cEventGroup.evgEvents[j];
                if (cEvent.evtCode < 0)
                {
                    cEvent.evtFlags &= CEvent.EVFLAGS_DEFAULTMASK;
                }
                else
                {
                    cEvent.evtFlags &= 238;
                }
                if (cEvent.evtNParams == 0)
                {
                    continue;
                }
                for (int k = 0; k < cEvent.evtNParams; k++)
                {
                    CParam cParam = cEvent.evtParams[k];
                    switch (cParam.code)
                    {
                        case 13:
                            {
                                PARAM_EVERY pARAM_EVERY = (PARAM_EVERY)cParam;
                                pARAM_EVERY.compteur = pARAM_EVERY.delay;
                                break;
                            }
                        case 39:
                            {
                                PARAM_GROUPOINTER pARAM_GROUPOINTER = (PARAM_GROUPOINTER)cParam;
                                for (int l = 0; l < cArrayList.size(); l++)
                                {
                                    CGroupFind cGroupFind = (CGroupFind)cArrayList.get(l);
                                    if (cGroupFind.id == pARAM_GROUPOINTER.id)
                                    {
                                        pARAM_GROUPOINTER.pointer = (short)cGroupFind.evg;
                                        break;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
        }
    }

    public virtual void assemblePrograms(CRun run)
    {
        rhPtr = run;
        rh2ActionCount = 0;
        int num = 0;
        int num2 = 0;
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            if (rhPtr.rhOiList[i].oilOi != -1)
            {
                rhPtr.rhOiList[i].oilActionCount = -1;
                rhPtr.rhOiList[i].oilLimitFlags = 0;
                rhPtr.rhOiList[i].oilLimitList = -1;
                num2++;
                if (rhPtr.rhOiList[i].oilOi + 1 > num)
                {
                    num = rhPtr.rhOiList[i].oilOi + 1;
                }
            }
        }
        qualToOiList = null;
        int j;
        if (nQualifiers > 0)
        {
            short[] array = new short[nQualifiers];
            for (short num3 = 0; num3 < nQualifiers; num3++)
            {
                short num4 = (short)(qualifiers[num3].qOi & 0x7FFF);
                array[num3] = 0;
                for (j = 0; j < rhPtr.rhMaxOI; j++)
                {
                    if (rhPtr.rhOiList[j].oilType != qualifiers[num3].qType)
                    {
                        continue;
                    }
                    for (int i = 0; i < 8 && rhPtr.rhOiList[j].oilQualifiers[i] != -1; i++)
                    {
                        if (num4 == rhPtr.rhOiList[j].oilQualifiers[i])
                        {
                            array[num3]++;
                        }
                    }
                }
            }
            qualToOiList = new CQualToOiList[nQualifiers];
            for (short num3 = 0; num3 < nQualifiers; num3++)
            {
                qualToOiList[num3] = new CQualToOiList();
                if (array[num3] != 0)
                {
                    qualToOiList[num3].qoiList = new short[array[num3] * 2];
                }
                int num5 = 0;
                short num4 = (short)(qualifiers[num3].qOi & 0x7FFF);
                for (j = 0; j < rhPtr.rhMaxOI; j++)
                {
                    if (rhPtr.rhOiList[j].oilType != qualifiers[num3].qType)
                    {
                        continue;
                    }
                    for (int i = 0; i < 8 && rhPtr.rhOiList[j].oilQualifiers[i] != -1; i++)
                    {
                        if (num4 == rhPtr.rhOiList[j].oilQualifiers[i])
                        {
                            qualToOiList[num3].qoiList[num5 * 2] = rhPtr.rhOiList[j].oilOi;
                            qualToOiList[num3].qoiList[num5 * 2 + 1] = (short)j;
                            num5++;
                        }
                    }
                }
                qualToOiList[num3].qoiActionCount = -1;
            }
        }
        colBuffer = new short[num * 100 * 2 + 1];
        int num6 = 0;
        short num9;
        for (int k = 0; k < events.Length; k++)
        {
            CEventGroup cEventGroup = events[k];
            for (int l = 0; l < cEventGroup.evgNAct + cEventGroup.evgNCond; l++)
            {
                CEvent cEvent = cEventGroup.evgEvents[l];
                cEvent.evtFlags &= (byte)(~CEvent.EVFLAGS_BADOBJECT);
                if (EVTTYPE(cEvent.evtCode) >= 0)
                {
                    cEvent.evtOiList = get_OiListOffset(cEvent.evtOi, EVTTYPE(cEvent.evtCode));
                }
                if (cEvent.evtNParams <= 0)
                {
                    continue;
                }
                for (int m = 0; m < cEvent.evtNParams; m++)
                {
                    CParam cParam = cEvent.evtParams[m];
                    switch (cParam.code)
                    {
                        case 25:
                            ((PARAM_INT)cParam).value_Renamed = 0;
                            break;
                        case 21:
                            if ((cEvent.evtOi & 0x8000) == 0)
                            {
                                for (CLO cLO = rhPtr.rhFrame.LOList.first_LevObj(); cLO != null; cLO = rhPtr.rhFrame.LOList.next_LevObj())
                                {
                                    if (cEvent.evtOi == cLO.loOiHandle)
                                    {
                                        ((CCreate)cParam).cdpHFII = cLO.loHandle;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                ((CCreate)cParam).cdpHFII = -1;
                            }
                            goto case 9;
                        case 9:
                        case 16:
                        case 18:
                            {
                                short num4 = ((CPosition)cParam).posOINUMParent;
                                if (num4 != -1)
                                {
                                    ((CPosition)cParam).posOiList = get_OiListOffset(num4, ((CPosition)cParam).posTypeParent);
                                }
                                break;
                            }
                        case 1:
                            ((PARAM_OBJECT)cParam).oiList = get_OiListOffset(((PARAM_OBJECT)cParam).oi, ((PARAM_OBJECT)cParam).type);
                            break;
                        case 15:
                        case 22:
                        case 23:
                        case 27:
                        case 28:
                        case 45:
                        case 46:
                        case 52:
                        case 53:
                        case 54:
                        case 59:
                            {
                                CParamExpression cParamExpression = (CParamExpression)cParam;
                                for (int i = 0; i < cParamExpression.tokens.Length; i++)
                                {
                                    if (EVTTYPE(cParamExpression.tokens[i].code) > 0)
                                    {
                                        CExpOi cExpOi = (CExpOi)cParamExpression.tokens[i];
                                        cExpOi.oiList = get_OiListOffset(cExpOi.oi, EVTTYPE(cExpOi.code));
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            ushort num7 = 0;
            ushort num8 = (ushort)(1 | CEventGroup.EVGFLAGS_LIMITED | 0x800);
            for (int l = 0; l < cEventGroup.evgNCond + cEventGroup.evgNAct; l++)
            {
                CEvent cEvent = cEventGroup.evgEvents[l];
                num9 = EVTTYPE(cEvent.evtCode);
                int evtCode = cEvent.evtCode;
                int i = 0;
                short num10 = 0;
                short num11 = 0;
                CParam cParam = null;
                if (num9 >= 2)
                {
                    switch (getEventCode(evtCode))
                    {
                        case 262144:
                        case 589824:
                            {
                                num7 |= 0x800;
                                short num4 = cEvent.evtOi;
                                if ((num4 & 0x8000) != 0)
                                {
                                    for (short num12 = qual_GetFirstOiList2(cEvent.evtOiList); num12 != -1; num12 = qual_GetNextOiList2())
                                    {
                                        num6 = make_ColList1(cEventGroup, num6, rhPtr.rhOiList[num12].oilOi);
                                    }
                                }
                                else
                                {
                                    num6 = make_ColList1(cEventGroup, num6, num4);
                                }
                                break;
                            }
                        case 1638400:
                            num7 |= 0x10;
                            break;
                        case -917504:
                            {
                                cParam = cEvent.evtParams[0];
                                PARAM_OBJECT pARAM_OBJECT = (PARAM_OBJECT)cEvent.evtParams[0];
                                addColList(cEvent.evtOiList, cEvent.evtOi, pARAM_OBJECT.oiList, pARAM_OBJECT.oi);
                                addColList(pARAM_OBJECT.oiList, pARAM_OBJECT.oi, cEvent.evtOiList, cEvent.evtOi);
                                short type = EVTTYPE(cEvent.evtCode);
                                num11 = (short)((!isTypeRealSprite(type)) ? 4480 : 384);
                                short type2 = pARAM_OBJECT.type;
                                num10 = (short)((!isTypeRealSprite(type2)) ? 4480 : 384);
                                i = 3;
                                break;
                            }
                        case -262144:
                            {
                                short type = EVTTYPE(cEvent.evtCode);
                                num11 = (short)((!isTypeRealSprite(type)) ? 4352 : 256);
                                cParam = cEvent.evtParams[0];
                                short type2 = ((PARAM_OBJECT)cEvent.evtParams[0]).type;
                                num10 = (short)((!isTypeRealSprite(type2)) ? 4352 : 256);
                                i = 3;
                                break;
                            }
                        case -786432:
                        case -720896:
                            num10 = 1024;
                            i = 1;
                            break;
                        case -851968:
                            num10 = 512;
                            i = 1;
                            break;
                    }
                }
                else
                {
                    switch (evtCode)
                    {
                        case -327681:
                            num8 &= 0xFFFE;
                            break;
                        case -393217:
                            num8 |= 8;
                            break;
                        case -262145:
                            num8 |= 8;
                            break;
                        case -196609:
                            num8 |= 6;
                            break;
                        case -196614:
                            num11 = 256;
                            cParam = cEvent.evtParams[0];
                            i = 2;
                            break;
                        case -393222:
                            num11 = 256;
                            cParam = cEvent.evtParams[1];
                            i = 2;
                            break;
                    }
                }
                if ((i & 1) != 0)
                {
                    for (short num12 = qual_GetFirstOiList(cEvent.evtOiList); num12 != -1; num12 = qual_GetNextOiList())
                    {
                        rhPtr.rhOiList[num12].oilLimitFlags |= num10;
                    }
                }
                if ((i & 2) != 0)
                {
                    for (short num12 = qual_GetFirstOiList(((PARAM_OBJECT)cParam).oiList); num12 != -1; num12 = qual_GetNextOiList())
                    {
                        rhPtr.rhOiList[num12].oilLimitFlags |= num11;
                    }
                }
            }
            cEventGroup.evgFlags &= (ushort)(~num8);
            cEventGroup.evgFlags |= num7;
        }
        colBuffer[num6] = -1;
        int[] array2 = new int[7 + num + 1];
        int num13 = 0;
        int num14 = 0;
        num9 = -7;
        while (num9 < 0)
        {
            array2[num14] = num13;
            num13 += nConditions[7 + num9];
            num9++;
            num14++;
        }
        j = 0;
        while (j < rhPtr.rhMaxOI)
        {
            array2[num14] = num13;
            num13 = ((rhPtr.rhOiList[j].oilType >= 32) ? (num13 + (rhPtr.rhApp.extLoader.getNumberOfConditions(rhPtr.rhOiList[j].oilType) + 80 + 1)) : (num13 + (nConditions[7 + rhPtr.rhOiList[j].oilType] + 80 + 1)));
            j++;
            num14++;
        }
        int num15 = num13;
        listPointers = new int[num15];
        for (int i = 0; i < num15; i++)
        {
            listPointers[i] = 0;
        }
        short num16 = 0;
        short[] array3 = new short[rhPtr.rhFrame.maxObjects];
        for (int k = 0; k < nEvents; k++)
        {
            CEventGroup cEventGroup = events[k];
            cEventGroup.evgFlags &= 64511;
            bool flag = true;
            int num17 = 0;
            for (int l = 0; l < cEventGroup.evgNCond; l++)
            {
                CEvent cEvent = cEventGroup.evgEvents[l];
                num9 = EVTTYPE(cEvent.evtCode);
                int evtCode = cEvent.evtCode;
                int num18 = -EVTNUM(evtCode);
                if (flag)
                {
                    if ((cEvent.evtFlags & 0x20) != 0)
                    {
                        num16++;
                    }
                    if (num9 < 0)
                    {
                        listPointers[array2[7 + num9] + num18]++;
                    }
                    else
                    {
                        int num19 = 0;
                        for (short num12 = qual_GetFirstOiList(cEvent.evtOiList); num12 != -1; num12 = qual_GetNextOiList())
                        {
                            listPointers[array2[7 + num12] + num18]++;
                            array3[num19++] = num12;
                        }
                        array3[num19] = -1;
                        if (getEventCode(evtCode) == -917504)
                        {
                            CParam cParam = cEvent.evtParams[0];
                            for (short num20 = qual_GetFirstOiList(((PARAM_OBJECT)cParam).oiList); num20 != -1; num20 = qual_GetNextOiList())
                            {
                                for (num19 = 0; array3[num19] != num20 && array3[num19] != -1; num19++)
                                {
                                }
                                if (array3[num19] == -1)
                                {
                                    listPointers[array2[7 + num20] + num18]++;
                                }
                            }
                        }
                    }
                }
                flag = false;
                if (cEvent.evtCode == -1507329 || cEvent.evtCode == -1572865)
                {
                    flag = true;
                    cEventGroup.evgFlags |= 1024;
                    if (num17 == 0)
                    {
                        num17 = cEvent.evtCode;
                    }
                    else
                    {
                        cEvent.evtCode = num17;
                    }
                    if (num17 == -1572865)
                    {
                        cEventGroup.evgFlags |= 4096;
                    }
                }
            }
        }
        int num21 = num16 + 1;
        int n;
        for (n = 0; n < num15; n++)
        {
            if (listPointers[n] != 0)
            {
                num13 = listPointers[n];
                listPointers[n] = num21;
                num21 += num13 + 1;
            }
        }
        eventPointersGroup = new CEventGroup[num21];
        eventPointersCnd = new sbyte[num21];
        for (int i = 0; i < num21; i++)
        {
            eventPointersGroup[i] = null;
            eventPointersCnd[i] = 0;
        }
        int[] array4 = new int[num15];
        for (int i = 0; i < num15; i++)
        {
            array4[i] = listPointers[i];
        }
        short num22 = 0;
        num16 = 0;
        for (int k = 0; k < nEvents; k++)
        {
            CEventGroup cEventGroup = events[k];
            bool flag = true;
            for (int l = 0; l < cEventGroup.evgNCond; l++)
            {
                CEvent cEvent = cEventGroup.evgEvents[l];
                num9 = EVTTYPE(cEvent.evtCode);
                int evtCode = cEvent.evtCode;
                int num18 = -EVTNUM(evtCode);
                if (flag)
                {
                    if ((cEvent.evtFlags & 0x20) != 0)
                    {
                        num16++;
                        eventPointersGroup[num22] = cEventGroup;
                        eventPointersCnd[num22] = (sbyte)l;
                        num22++;
                    }
                    if (num9 < 0)
                    {
                        int num23 = array2[7 + num9] + num18;
                        eventPointersGroup[array4[num23]] = cEventGroup;
                        eventPointersCnd[array4[num23]] = (sbyte)l;
                        array4[num23]++;
                    }
                    else
                    {
                        int num19 = 0;
                        for (short num12 = qual_GetFirstOiList(cEvent.evtOiList); num12 != -1; num12 = qual_GetNextOiList())
                        {
                            int num23 = array2[7 + num12] + num18;
                            eventPointersGroup[array4[num23]] = cEventGroup;
                            eventPointersCnd[array4[num23]] = (sbyte)l;
                            array4[num23]++;
                            array3[num19++] = num12;
                        }
                        array3[num19] = -1;
                        if (getEventCode(evtCode) == -917504)
                        {
                            CParam cParam = cEvent.evtParams[0];
                            for (short num20 = qual_GetFirstOiList(((PARAM_OBJECT)cParam).oiList); num20 != -1; num20 = qual_GetNextOiList())
                            {
                                for (num19 = 0; array3[num19] != num20 && array3[num19] != -1; num19++)
                                {
                                }
                                if (array3[num19] == -1)
                                {
                                    int num23 = array2[7 + num20] + num18;
                                    eventPointersGroup[array4[num23]] = cEventGroup;
                                    eventPointersCnd[array4[num23]] = (sbyte)l;
                                    array4[num23]++;
                                }
                            }
                        }
                    }
                }
                flag = false;
                if (cEvent.evtCode == -1507329 || cEvent.evtCode == -1572865)
                {
                    flag = true;
                }
            }
        }
        n = array2[3];
        int num24 = listPointers[n - EVTNUM(-131076)];
        limitBuffer = new short[num + 1 + num6 / 2];
        int num25 = 0;
        for (j = 0; j < rhPtr.rhMaxOI; j++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[j];
            n = (cObjInfo.oilEvents = array2[7 + j]);
            if ((cObjInfo.oilOEFlags & 0x10) == 0)
            {
                continue;
            }
            short num26 = 0;
            int num27 = EVTNUM(-786432);
            num13 = listPointers[n - num27];
            if (num13 != 0)
            {
                for (; eventPointersGroup[num13] != null; num13++)
                {
                    CEventGroup cEventGroup = eventPointersGroup[num13];
                    CEvent cEvent = cEventGroup.evgEvents[eventPointersCnd[num13]];
                    short value = ((PARAM_SHORT)cEvent.evtParams[0]).value;
                    int num28 = evg_FindAction(cEventGroup, 0);
                    int i = cEventGroup.evgNAct;
                    while (i > 0)
                    {
                        cEvent = cEventGroup.evgEvents[num28];
                        if (cEvent.evtCode == (0x80000 | (cObjInfo.oilType & 0xFFFF)))
                        {
                            num26 |= value;
                        }
                        i--;
                        num28++;
                    }
                }
            }
            cObjInfo.oilWrap = (byte)num26;
            short oilOi = cObjInfo.oilOi;
            num6 = 0;
            int num29 = 0;
            for (; colBuffer[num6] != -1; num6 += 2)
            {
                if (colBuffer[num6] != oilOi)
                {
                    continue;
                }
                short num30 = colBuffer[num6 + 1];
                if ((num30 & 0x8000) != 0)
                {
                    cObjInfo.oilLimitFlags |= num30;
                    continue;
                }
                int num31;
                for (num31 = 0; num31 < num29 && limitBuffer[num25 + num31] != num30; num31++)
                {
                }
                if (num31 == num29)
                {
                    limitBuffer[num25 + num29++] = num30;
                }
            }
            if (num29 > 0)
            {
                cObjInfo.oilLimitList = num25;
                limitBuffer[num25 + num29++] = -1;
                num25 += num29;
            }
        }
        rhEvents[0] = 0;
        for (int i = 1; i <= 7; i++)
        {
            rhEvents[i] = array2[7 - i];
        }
        for (j = 0; j < rhPtr.rhMaxOI; j++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[j];
            short num12 = cObjInfo.oilObject;
            if ((num12 & 0x8000) != 0)
            {
                continue;
            }
            do
            {
                CObject cObject = rhPtr.rhObjectList[num12];
                cObject.hoEvents = cObjInfo.oilEvents;
                cObject.hoOiList = cObjInfo;
                cObject.hoLimitFlags = cObjInfo.oilLimitFlags;
                if ((cObject.hoOEFlags & 0x10) != 0)
                {
                    cObject.rom.rmWrapping = cObjInfo.oilWrap;
                }
                if ((cObject.hoOEFlags & 0x200) != 0 && (cObject.hoLimitFlags & 0x100) == 0 && cObject.roc.rcSprite != null)
                {
                    cObject.roc.rcSprite.setSpriteColFlag(0u);
                }
                if ((cObject.hoOEFlags & 0x8000) == 0)
                {
                    cObject.hoOEFlags &= -16385;
                    if ((cObject.hoLimitFlags & 0x200) != 0 && (rhPtr.rhFrame.leFlags & 0x20) != 0)
                    {
                        cObject.hoOEFlags |= 16384;
                    }
                    if ((cObject.hoLimitFlags & 0x500) != 0)
                    {
                        cObject.hoOEFlags |= 16384;
                    }
                }
                num12 = cObject.hoNumNext;
            }
            while ((num12 & 0x8000) == 0);
        }
        finaliseColList();
        if (num16 != 0)
        {
            rhEventAlways = true;
        }
        else
        {
            rhEventAlways = false;
        }
        if (num24 != 0)
        {
            rh4TimerEventsBase = num24;
        }
        else
        {
            rh4TimerEventsBase = 0;
        }
        colBuffer = null;
        bReady = true;
    }

    public virtual void unBranchPrograms()
    {
        bReady = false;
        qualToOiList = null;
        limitBuffer = null;
        listPointers = null;
        eventPointersGroup = null;
        eventPointersCnd = null;
    }

    public void addColList(short oiList, short oiNum, short oiList2, short oiNum2)
    {
        if (oiNum < 0)
        {
            if (qualToOiList != null)
            {
                CQualToOiList cQualToOiList = qualToOiList[oiList & 0x7FFF];
                for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
                {
                    addColList(cQualToOiList.qoiList[i + 1], cQualToOiList.qoiList[i], oiList2, oiNum2);
                }
            }
            return;
        }
        if (oiNum2 < 0)
        {
            if (qualToOiList != null)
            {
                CQualToOiList cQualToOiList = qualToOiList[oiList2 & 0x7FFF];
                for (int i = 0; i < cQualToOiList.qoiList.Length; i += 2)
                {
                    addColList(oiList, oiNum, cQualToOiList.qoiList[i + 1], cQualToOiList.qoiList[i]);
                }
            }
            return;
        }
        CObjInfo cObjInfo = rhPtr.rhOiList[oiList];
        if (cObjInfo.oilColList == null)
        {
            cObjInfo.oilColList = new short[10];
            cObjInfo.oilColCount = 0;
        }
        else
        {
            for (int j = 0; j < cObjInfo.oilColCount; j += 2)
            {
                if (oiNum2 == cObjInfo.oilColList[j])
                {
                    return;
                }
            }
        }
        if (cObjInfo.oilColCount >= cObjInfo.oilColList.Length)
        {
            Array.Resize(ref cObjInfo.oilColList, cObjInfo.oilColList.Length + 10);
        }
        cObjInfo.oilColList[cObjInfo.oilColCount++] = oiNum2;
        cObjInfo.oilColList[cObjInfo.oilColCount++] = oiList2;
    }

    public void finaliseColList()
    {
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            CObjInfo cObjInfo = rhPtr.rhOiList[i];
            if (cObjInfo != null && cObjInfo.oilColList != null)
            {
                Array.Resize(ref cObjInfo.oilColList, cObjInfo.oilColCount);
            }
        }
    }

    internal virtual short get_OiListOffset(short oi, short type)
    {
        if ((oi & 0x8000) != 0)
        {
            int i;
            for (i = 0; oi != qualifiers[i].qOi || type != qualifiers[i].qType; i++)
            {
            }
            return (short)(i | 0x8000);
        }
        int j;
        for (j = 0; j < rhPtr.rhMaxOI && rhPtr.rhOiList[j].oilOi != oi; j++)
        {
        }
        return (short)j;
    }

    internal virtual bool isTypeRealSprite(short type)
    {
        for (int i = 0; i < rhPtr.rhMaxOI; i++)
        {
            if (rhPtr.rhOiList[i].oilOi != -1 && rhPtr.rhOiList[i].oilType == type)
            {
                if ((rhPtr.rhOiList[i].oilOEFlags & 0x200) != 0 && (rhPtr.rhOiList[i].oilOEFlags & 0x1000) == 0)
                {
                    return true;
                }
                return false;
            }
        }
        return true;
    }

    internal virtual short qual_GetFirstOiList(short o)
    {
        if ((o & 0x8000) == 0)
        {
            qualOilPtr = -1;
            return o;
        }
        if (o == -1)
        {
            return -1;
        }
        o &= 0x7FFF;
        qualOilPtr = o;
        qualOilPos = 0;
        return qual_GetNextOiList();
    }

    internal virtual short qual_GetNextOiList()
    {
        if (qualOilPtr == -1)
        {
            return -1;
        }
        if (qualOilPos >= qualToOiList[qualOilPtr].qoiList.Length)
        {
            return -1;
        }
        short result = qualToOiList[qualOilPtr].qoiList[qualOilPos + 1];
        qualOilPos += 2;
        return result;
    }

    internal virtual short qual_GetFirstOiList2(short o)
    {
        if ((o & 0x8000) == 0)
        {
            qualOilPtr2 = -1;
            return o;
        }
        if (o == -1)
        {
            return -1;
        }
        o &= 0x7FFF;
        qualOilPtr2 = o;
        qualOilPos2 = 0;
        return qual_GetNextOiList2();
    }

    internal virtual short qual_GetNextOiList2()
    {
        if (qualOilPtr2 == -1)
        {
            return -1;
        }
        if (qualOilPos2 >= qualToOiList[qualOilPtr2].qoiList.Length)
        {
            return -1;
        }
        short result = qualToOiList[qualOilPtr2].qoiList[qualOilPos2 + 1];
        qualOilPos2 += 2;
        return result;
    }

    internal virtual int make_ColList1(CEventGroup evgPtr, int colList, short oi1)
    {
        for (int i = 0; i < evgPtr.evgNCond; i++)
        {
            CEvent cEvent = evgPtr.evgEvents[i];
            if (EVTTYPE(cEvent.evtCode) < 2)
            {
                continue;
            }
            short num = -32752;
            int eventCode = getEventCode(cEvent.evtCode);
            int num2 = eventCode;
            CParam cParam;
            if (num2 != -917504)
            {
                if (num2 != -851968)
                {
                    if (num2 != -786432)
                    {
                        continue;
                    }
                    cParam = cEvent.evtParams[0];
                    num = (short)(32768 + ((PARAM_SHORT)cParam).value);
                }
                for (short num3 = qual_GetFirstOiList(cEvent.evtOiList); num3 != -1; num3 = qual_GetNextOiList())
                {
                    short oilOi = rhPtr.rhOiList[num3].oilOi;
                    if (oi1 == oilOi)
                    {
                        colBuffer[colList++] = oi1;
                        colBuffer[colList++] = num;
                    }
                }
                continue;
            }
            cParam = cEvent.evtParams[0];
            for (short num3 = qual_GetFirstOiList(cEvent.evtOiList); num3 != -1; num3 = qual_GetNextOiList())
            {
                short oilOi = rhPtr.rhOiList[num3].oilOi;
                if (oi1 == oilOi)
                {
                    num = 0;
                    colList = make_ColList2(colList, oi1, ((PARAM_OBJECT)cParam).oiList);
                }
            }
            if (num == 0)
            {
                continue;
            }
            for (short num3 = qual_GetFirstOiList(((PARAM_OBJECT)cParam).oiList); num3 != -1; num3 = qual_GetNextOiList())
            {
                short oilOi = rhPtr.rhOiList[num3].oilOi;
                if (oi1 == oilOi)
                {
                    colList = make_ColList2(colList, oi1, cEvent.evtOiList);
                }
            }
        }
        return colList;
    }

    internal virtual int make_ColList2(int colList, short oi1, short ol)
    {
        for (short num = qual_GetFirstOiList(ol); num != -1; num = qual_GetNextOiList())
        {
            short oilOi = rhPtr.rhOiList[num].oilOi;
            int i;
            for (i = 0; i < colList && (colBuffer[i] != oi1 || colBuffer[i + 1] != oilOi); i += 2)
            {
            }
            if (i == colList)
            {
                colBuffer[colList++] = oi1;
                colBuffer[colList++] = oilOi;
            }
        }
        return colList;
    }

    public int getCollisionFlags()
    {
        int num = 0;
        for (int i = 0; i < events.Length; i++)
        {
            CEventGroup cEventGroup = events[i];
            for (int j = 0; j < cEventGroup.evgNAct + cEventGroup.evgNCond; j++)
            {
                CEvent cEvent = cEventGroup.evgEvents[j];
                if (cEvent.evtNParams <= 0)
                {
                    continue;
                }
                for (int k = 0; k < cEvent.evtNParams; k++)
                {
                    CParam cParam = cEvent.evtParams[k];
                    if (cParam.code == 43)
                    {
                        PARAM_SHORT pARAM_SHORT = (PARAM_SHORT)cParam;
                        switch (pARAM_SHORT.value)
                        {
                            case 1:
                                num |= 1;
                                break;
                            case 2:
                                num |= 2;
                                break;
                        }
                    }
                }
            }
        }
        return num;
    }

    public virtual void enumSounds(IEnum sounds)
    {
        for (int i = 0; i < nEvents; i++)
        {
            CEventGroup cEventGroup = events[i];
            for (int j = 0; j < cEventGroup.evgNCond + cEventGroup.evgNAct; j++)
            {
                CEvent cEvent = cEventGroup.evgEvents[j];
                for (int k = 0; k < cEvent.evtNParams; k++)
                {
                    short code = cEvent.evtParams[k].code;
                    if (code == 6 || code == 35)
                    {
                        PARAM_SAMPLE pARAM_SAMPLE = (PARAM_SAMPLE)cEvent.evtParams[k];
                        sounds.enumerate(pARAM_SAMPLE.sndHandle);
                    }
                }
            }
        }
    }

    internal virtual int evg_FindAction(CEventGroup evgPtr, int n)
    {
        return evgPtr.evgNCond + n;
    }

    public virtual short EVTTYPE(int code)
    {
        return (short)(code & 0xFFFF);
    }

    public virtual short EVTNUM(int code)
    {
        return (short)((code >> 16) & 0xFFFF);
    }

    public virtual int getEventCode(int code)
    {
        return code & -65536;
    }
}