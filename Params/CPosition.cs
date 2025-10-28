// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Params.CPosition
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;
using RuntimeXNA.Banks;
using RuntimeXNA.Movements;
using RuntimeXNA.Objects;
using RuntimeXNA.RunLoop;

namespace RuntimeXNA.Params
{

    public abstract class CPosition : CParam
    {
      public const short CPF_DIRECTION = 1;
      public const short CPF_ACTION = 2;
      public const short CPF_INITIALDIR = 4;
      public const short CPF_DEFAULTDIR = 8;
      public short posOINUMParent;
      public short posFlags;
      public short posX;
      public short posY;
      public short posSlope;
      public short posAngle;
      public int posDir;
      public short posTypeParent;
      public short posOiList;
      public short posLayer;

      public virtual bool read_Position(CRun rhPtr, int getDir, CPositionInfo pInfo)
      {
        pInfo.layer = -1;
        if (this.posOINUMParent == (short) -1)
        {
          if (getDir != 0)
          {
            pInfo.dir = -1;
            if (((int) this.posFlags & 8) == 0)
              pInfo.dir = rhPtr.get_Direction(this.posDir);
          }
          pInfo.x = (int) this.posX;
          pInfo.y = (int) this.posY;
          int num = (int) this.posLayer;
          if (num > rhPtr.rhFrame.nLayers - 1)
            num = rhPtr.rhFrame.nLayers - 1;
          pInfo.layer = num;
          pInfo.bRepeat = false;
        }
        else
        {
          rhPtr.rhEvtProg.rh2EnablePick = false;
          CObject currentObjects = rhPtr.rhEvtProg.get_CurrentObjects(this.posOiList);
          pInfo.bRepeat = rhPtr.rhEvtProg.repeatFlag;
          if (currentObjects == null)
            return false;
          pInfo.x = currentObjects.hoX;
          pInfo.y = currentObjects.hoY;
          pInfo.layer = currentObjects.hoLayer;
          if (((int) this.posFlags & 2) != 0 && (currentObjects.hoOEFlags & 32 /*0x20*/) != 0 && currentObjects.roc.rcImage != (short) 0)
          {
            CImage imageInfoEx = rhPtr.rhApp.imageBank.getImageInfoEx(currentObjects.roc.rcImage, currentObjects.roc.rcAngle, currentObjects.roc.rcScaleX, currentObjects.roc.rcScaleY);
            pInfo.x += (int) imageInfoEx.xAP - (int) imageInfoEx.xSpot;
            pInfo.y += (int) imageInfoEx.yAP - (int) imageInfoEx.ySpot;
          }
          if (((int) this.posFlags & 1) != 0)
          {
            int angle = (int) this.posAngle + currentObjects.roc.rcDir & 31 /*0x1F*/;
            int deltaX = CMove.getDeltaX((int) this.posSlope, angle);
            int deltaY = CMove.getDeltaY((int) this.posSlope, angle);
            pInfo.x += deltaX;
            pInfo.y += deltaY;
          }
          else
          {
            pInfo.x += (int) this.posX;
            pInfo.y += (int) this.posY;
          }
          if ((getDir & 1) != 0)
            pInfo.dir = ((int) this.posFlags & 8) == 0 ? (((int) this.posFlags & 4) == 0 ? rhPtr.get_Direction(this.posDir) : currentObjects.roc.rcDir) : -1;
        }
        return (getDir & 2) == 0 || pInfo.x >= rhPtr.rh3XMinimumKill && pInfo.x <= rhPtr.rh3XMaximumKill && pInfo.y >= rhPtr.rh3YMinimumKill && pInfo.y <= rhPtr.rh3YMaximumKill;
      }

      public abstract override void load(CRunApp app);
    }
}
