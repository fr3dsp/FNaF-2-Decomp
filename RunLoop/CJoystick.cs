// Decompiled with JetBrains decompiler
// Type: RuntimeXNA.RunLoop.CJoystick
// Assembly: FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE6B2A3E-C4C5-4015-85E2-E94FD57E0079
// Assembly location: C:\Users\zakga\Desktop\FNAF2\FiveNightsatFreddys2.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using RuntimeXNA.Application;
using RuntimeXNA.Sprites;
using System;

namespace RuntimeXNA.RunLoop
{

    public class CJoystick : ITouches
    {
      public const int KEY_JOYSTICK = 0;
      public const int KEY_FIRE1 = 1;
      public const int KEY_FIRE2 = 2;
      public const int KEY_NONE = -1;
      public const int MAX_TOUCHES = 3;
      public const int JFLAG_JOYSTICK = 1;
      public const int JFLAG_FIRE1 = 2;
      public const int JFLAG_FIRE2 = 4;
      public const int JFLAG_LEFTHANDED = 8;
      public const int JPOS_NOTDEFINED = -2147483648 /*0x80000000*/;
      private CRunApp app;
      private Texture2D joyBack;
      private Texture2D joyFront;
      private Texture2D fire1U;
      private Texture2D fire2U;
      private Texture2D fire1D;
      private Texture2D fire2D;
      public int[] imagesX = new int[3];
      public int[] imagesY = new int[3];
      private int joystickX;
      private int joystickY;
      private byte joystick;
      private int flags;
      private int[] touches = new int[3];
      private Rectangle rect = new Rectangle();

      public CJoystick(CRunApp a)
      {
        this.app = a;
        this.joystick = (byte) 0;
      }

      public void reset(int f)
      {
        this.flags = f;
        if ((this.flags & 1) != 0 && this.joyBack == null)
        {
          this.joyBack = this.app.content.Load<Texture2D>("joyback");
          this.joyFront = this.app.content.Load<Texture2D>("joyfront");
        }
        if ((this.flags & 2) != 0 && this.fire1U == null)
        {
          this.fire1U = this.app.content.Load<Texture2D>("fire1U");
          this.fire1D = this.app.content.Load<Texture2D>("fire1D");
        }
        if ((this.flags & 4) != 0 && this.fire2U == null)
        {
          this.fire2U = this.app.content.Load<Texture2D>("fire2U");
          this.fire2D = this.app.content.Load<Texture2D>("fire2D");
        }
        this.setPositions();
      }

      public void setPositions()
      {
        int gaCxWin = this.app.gaCxWin;
        int gaCyWin = this.app.gaCyWin;
        if ((this.flags & 8) == 0)
        {
          if ((this.flags & 1) != 0)
          {
            this.imagesX[0] = 16 /*0x10*/ + this.joyBack.Width / 2;
            this.imagesY[0] = gaCyWin - 16 /*0x10*/ - this.joyBack.Height / 2;
          }
          if ((this.flags & 2) != 0 && (this.flags & 4) != 0)
          {
            this.imagesX[1] = gaCxWin - this.fire1U.Width / 2 - 32 /*0x20*/;
            this.imagesY[1] = gaCyWin - this.fire1U.Height / 2 - 16 /*0x10*/;
            this.imagesX[2] = gaCxWin - this.fire2U.Width / 2 - 16 /*0x10*/;
            this.imagesY[2] = gaCyWin - this.fire2U.Height / 2 - this.fire1U.Height - 24;
          }
          else if ((this.flags & 2) != 0)
          {
            this.imagesX[1] = gaCxWin - this.fire1U.Width / 2 - 16 /*0x10*/;
            this.imagesY[1] = gaCyWin - this.fire1U.Height / 2 - 16 /*0x10*/;
          }
          else
          {
            if ((this.flags & 4) == 0)
              return;
            this.imagesX[2] = gaCxWin - this.fire2U.Width / 2 - 16 /*0x10*/;
            this.imagesY[2] = gaCyWin - this.fire2U.Height / 2 - 16 /*0x10*/;
          }
        }
        else
        {
          if ((this.flags & 1) != 0)
          {
            this.imagesX[0] = gaCxWin - 16 /*0x10*/ - this.joyBack.Width / 2;
            this.imagesY[0] = gaCyWin - 16 /*0x10*/ - this.joyBack.Height / 2;
          }
          if ((this.flags & 2) != 0 && (this.flags & 4) != 0)
          {
            this.imagesX[1] = this.fire1U.Width / 2 + 16 /*0x10*/ + this.fire2U.Width * 2 / 3;
            this.imagesY[1] = gaCyWin - this.fire1U.Height / 2 - 16 /*0x10*/;
            this.imagesX[2] = this.fire2U.Width / 2 + 16 /*0x10*/;
            this.imagesY[2] = gaCyWin - this.fire2U.Height / 2 - this.fire1U.Height - 24;
          }
          else if ((this.flags & 2) != 0)
          {
            this.imagesX[1] = this.fire1U.Width / 2 + 16 /*0x10*/;
            this.imagesY[1] = gaCyWin - this.fire1U.Height / 2 - 16 /*0x10*/;
          }
          else
          {
            if ((this.flags & 4) == 0)
              return;
            this.imagesX[2] = this.fire2U.Width / 2 + 16 /*0x10*/;
            this.imagesY[2] = gaCyWin - this.fire2U.Height / 2 - 16 /*0x10*/;
          }
        }
      }

      public void setXPosition(int f, int p)
      {
        if ((f & 1) != 0)
          this.imagesX[0] = p;
        else if ((f & 2) != 0)
        {
          this.imagesX[1] = p;
        }
        else
        {
          if ((f & 4) == 0)
            return;
          this.imagesX[2] = p;
        }
      }

      public void setYPosition(int f, int p)
      {
        if ((f & 1) != 0)
          this.imagesY[0] = p;
        else if ((f & 2) != 0)
        {
          this.imagesY[1] = p;
        }
        else
        {
          if ((f & 4) == 0)
            return;
          this.imagesY[2] = p;
        }
      }

      public void draw(SpriteBatchEffect batch)
      {
        if ((this.flags & 1) != 0)
        {
          this.rect.X = this.imagesX[0] - this.joyBack.Width / 2;
          this.rect.Y = this.imagesY[0] - this.joyBack.Height / 2;
          this.rect.Width = this.joyBack.Width;
          this.rect.Height = this.joyBack.Height;
          batch.Draw(this.joyBack, this.rect, new Rectangle?(), Color.White);
          this.rect.X = this.imagesX[0] + this.joystickX - this.joyFront.Width / 2;
          this.rect.Y = this.imagesY[0] + this.joystickY - this.joyFront.Height / 2;
          this.rect.Width = this.joyFront.Width;
          this.rect.Height = this.joyFront.Height;
          batch.Draw(this.joyFront, this.rect, new Rectangle?(), Color.White);
        }
        if ((this.flags & 2) != 0)
        {
          Texture2D texture = ((int) this.joystick & 16 /*0x10*/) == 0 ? this.fire1U : this.fire1D;
          this.rect.X = this.imagesX[1] - texture.Width / 2;
          this.rect.Y = this.imagesY[1] - texture.Height / 2;
          this.rect.Width = texture.Width;
          this.rect.Height = texture.Height;
          batch.Draw(texture, this.rect, new Rectangle?(), Color.White);
        }
        if ((this.flags & 4) == 0)
          return;
        Texture2D texture1 = ((int) this.joystick & 32 /*0x20*/) == 0 ? this.fire2U : this.fire2D;
        this.rect.X = this.imagesX[2] - texture1.Width / 2;
        this.rect.Y = this.imagesY[2] - texture1.Height / 2;
        this.rect.Width = texture1.Width;
        this.rect.Height = texture1.Height;
        batch.Draw(texture1, this.rect, new Rectangle?(), Color.White);
      }

      public bool touchBegan(TouchLocation touch)
      {
        bool flag = false;
        int key = this.getKey((int) touch.Position.X, (int) touch.Position.Y);
        if (key != -1)
        {
          this.touches[key] = touch.Id;
          if (key == 0)
          {
            this.joystick &= (byte) 240 /*0xF0*/;
            flag = true;
          }
          if (key == 1)
          {
            this.joystick |= (byte) 16 /*0x10*/;
            flag = true;
          }
          else if (key == 2)
          {
            this.joystick |= (byte) 32 /*0x20*/;
            flag = true;
          }
        }
        return flag;
      }

      public void touchMoved(TouchLocation touch)
      {
        this.getKey((int) touch.Position.X, (int) touch.Position.Y);
        if (touch.Id != this.touches[0])
          return;
        this.joystickX = (int) touch.Position.X - this.imagesX[0];
        this.joystickY = (int) touch.Position.Y - this.imagesY[0];
        if (this.joystickX < -this.joyBack.Width / 4)
          this.joystickX = -this.joyBack.Width / 4;
        if (this.joystickX > this.joyBack.Width / 4)
          this.joystickX = this.joyBack.Width / 4;
        if (this.joystickY < -this.joyBack.Height / 4)
          this.joystickY = -this.joyBack.Height / 4;
        if (this.joystickY > this.joyBack.Height / 4)
          this.joystickY = this.joyBack.Height / 4;
        this.joystick &= (byte) 240 /*0xF0*/;
        if (Math.Sqrt((double) (this.joystickX * this.joystickX + this.joystickY * this.joystickY)) < (double) (this.joyBack.Width / 4))
          return;
        double num = Math.Atan2((double) -this.joystickY, (double) this.joystickX);
        this.joystick |= num < 0.0 ? (num <= -1.0 * Math.PI / 8.0 ? (num <= -3.0 * Math.PI / 8.0 ? (num <= -5.0 * Math.PI / 8.0 ? (num <= -7.0 * Math.PI / 8.0 ? (byte) 4 : (byte) 6) : (byte) 2) : (byte) 10) : (byte) 8) : (num >= Math.PI / 8.0 ? (num >= 3.0 * Math.PI / 8.0 ? (num >= 5.0 * Math.PI / 8.0 ? (num >= 7.0 * Math.PI / 8.0 ? (byte) 4 : (byte) 5) : (byte) 1) : (byte) 9) : (byte) 8);
      }

      public void touchEnded(TouchLocation touch)
      {
        for (int index = 0; index < 3; ++index)
        {
          if (this.touches[index] == touch.Id)
          {
            this.touches[index] = 0;
            switch (index)
            {
              case 0:
                this.joystickX = 0;
                this.joystickY = 0;
                this.joystick &= (byte) 240 /*0xF0*/;
                return;
              case 1:
                this.joystick &= (byte) 239;
                return;
              case 2:
                this.joystick &= (byte) 223;
                return;
              default:
                return;
            }
          }
        }
      }

      public void touchCancelled(TouchLocation touch) => this.touchEnded(touch);

      public int getKey(int x, int y)
      {
        if ((this.flags & 1) != 0 && x >= this.imagesX[0] - this.joyBack.Width / 2 && x < this.imagesX[0] + this.joyBack.Width / 2 && y > this.imagesY[0] - this.joyBack.Height / 2 && y < this.imagesY[0] + this.joyBack.Height / 2)
          return 0;
        if ((this.flags & 2) != 0 && x >= this.imagesX[1] - this.fire1U.Width / 2 && x < this.imagesX[1] + this.fire1U.Width / 2 && y > this.imagesY[1] - this.fire1U.Height / 2 && y < this.imagesY[1] + this.fire1U.Height / 2)
          return 1;
        return (this.flags & 4) != 0 && x >= this.imagesX[2] - this.fire2U.Width / 2 && x < this.imagesX[2] + this.fire2U.Width / 2 && y > this.imagesY[2] - this.fire2U.Height / 2 && y < this.imagesY[2] + this.fire2U.Height / 2 ? 2 : -1;
      }

      public byte getJoystick() => this.joystick;
    }
}
