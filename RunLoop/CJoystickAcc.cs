// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.RunLoop.CJoystickAcc
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using RuntimeXNA.Application;

namespace RuntimeXNA.RunLoop
{

    public class CJoystickAcc
    {
      public byte getJoystick()
      {
        byte joystick = 0;
        if ((double) CRunApp.filteredAccX < -0.15)
          joystick |= (byte) 4;
        if ((double) CRunApp.filteredAccX > 0.15)
          joystick |= (byte) 8;
        if ((double) CRunApp.filteredAccY < -0.15)
          joystick |= (byte) 1;
        if ((double) CRunApp.filteredAccY > 0.15)
          joystick |= (byte) 2;
        return joystick;
      }
    }
}
