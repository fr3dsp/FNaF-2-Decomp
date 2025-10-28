// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Movements.CMoveStatic
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Objects;

namespace RuntimeXNA.Movements
{

    internal class CMoveStatic : CMove
    {
      public override void init(CObject ho, CMoveDef mvPtr)
      {
        this.hoPtr = ho;
        this.hoPtr.roc.rcSpeed = 0;
        this.hoPtr.roc.rcCheckCollides = true;
        this.hoPtr.roc.rcChanged = true;
      }

      public override void move()
      {
        if (this.hoPtr.roa != null)
          this.hoPtr.roa.animate();
        if (!this.hoPtr.roc.rcCheckCollides)
          return;
        this.hoPtr.roc.rcCheckCollides = false;
        this.hoPtr.hoAdRunHeader.newHandle_Collisions(this.hoPtr);
      }

      public override void setXPosition(int x)
      {
        if (this.hoPtr.hoX != x)
        {
          this.hoPtr.hoX = x;
          this.hoPtr.rom.rmMoveFlag = true;
          this.hoPtr.roc.rcChanged = true;
        }
        this.hoPtr.roc.rcCheckCollides = true;
      }

      public override void setYPosition(int y)
      {
        if (this.hoPtr.hoY != y)
        {
          this.hoPtr.hoY = y;
          this.hoPtr.rom.rmMoveFlag = true;
          this.hoPtr.roc.rcChanged = true;
        }
        this.hoPtr.roc.rcCheckCollides = true;
      }
    }
}
