// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.Objects.CRCom
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Sprites;

namespace RuntimeXNA.Objects
{

    public class CRCom
    {
      public int rcPlayer;
      public int rcMovementType;
      public CSprite rcSprite;
      public int rcAnim;
      public short rcImage = -1;
      public float rcScaleX = 1f;
      public float rcScaleY = 1f;
      public int rcAngle;
      public int rcDir;
      public int rcSpeed;
      public int rcMinSpeed;
      public int rcMaxSpeed;
      public bool rcChanged;
      public bool rcCheckCollides;
      public int rcOldX;
      public int rcOldY;
      public short rcOldImage = -1;
      public int rcOldAngle;
      public int rcOldDir;
      public int rcOldX1;
      public int rcOldY1;
      public int rcOldX2;
      public int rcOldY2;

      public void init()
      {
        this.rcScaleX = 1f;
        this.rcScaleY = 1f;
        this.rcAngle = 0;
        this.rcMovementType = -1;
      }

      public void kill(bool bFast)
      {
      }
    }
}
