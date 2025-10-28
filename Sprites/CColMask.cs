// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// FiveNightsatFreddys2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// RuntimeXNA.Sprites.CColMask
using System;
using RuntimeXNA.Sprites;

public class CColMask
{
    public const short CM_TEST_OBSTACLE = 0;

    public const short CM_TEST_PLATFORM = 1;

    public const int CM_OBSTACLE = 1;

    public const int CM_PLATFORM = 2;

    public const int COLMASK_XMARGIN = 64;

    public const int COLMASK_YMARGIN = 16;

    public const int HEIGHT_PLATFORM = 6;

    public static ushort[] lMask = new ushort[16]
    {
        65535, 32767, 16383, 8191, 4095, 2047, 1023, 511, 255, 127,
        63, 31, 15, 7, 3, 1
    };

    public static ushort[] rMask = new ushort[17]
    {
        0, 32768, 49152, 57344, 61440, 63488, 64512, 65024, 65280, 65408,
        65472, 65504, 65520, 65528, 65532, 65534, 65535
    };

    public ushort[] obstacle;

    public ushort[] platform;

    public int lineWidth;

    public int width;

    public int height;

    public int mX1;

    public int mX2;

    public int mY1;

    public int mY2;

    public int mX1Clip;

    public int mX2Clip;

    public int mY1Clip;

    public int mY2Clip;

    public int mDxScroll;

    public int mDyScroll;

    public static CColMask create(int xx1, int yy1, int xx2, int yy2, int flags)
    {
        CColMask cColMask = new CColMask();
        cColMask.mDxScroll = 0;
        cColMask.mDyScroll = 0;
        cColMask.mX1 = (cColMask.mX1Clip = xx1);
        cColMask.mY1 = (cColMask.mY1Clip = yy1);
        cColMask.mX2 = (cColMask.mX2Clip = xx2);
        cColMask.mY2 = (cColMask.mY2Clip = yy2);
        cColMask.width = xx2 - xx1;
        cColMask.height = yy2 - yy1;
        cColMask.lineWidth = ((cColMask.width + 15) & -16) / 16;
        if ((flags & 1) != 0)
        {
            cColMask.obstacle = new ushort[cColMask.lineWidth * cColMask.height + 1];
        }
        if ((flags & 2) != 0)
        {
            cColMask.platform = new ushort[cColMask.lineWidth * cColMask.height + 1];
        }
        return cColMask;
    }

    public void setOrigin(int dx, int dy)
    {
        mDxScroll = dx;
        mDyScroll = dy;
    }

    public void fill(ushort value)
    {
        int num = lineWidth * height;
        if (obstacle != null)
        {
            for (int i = 0; i < num; i++)
            {
                obstacle[i] = value;
            }
        }
        if (platform != null)
        {
            for (int i = 0; i < num; i++)
            {
                platform[i] = value;
            }
        }
    }

    public void fillRectangle(int x1, int y1, int x2, int y2, int val)
    {
        x1 += mDxScroll;
        x2 += mDxScroll;
        y1 += mDyScroll;
        y2 += mDyScroll;
        if (x1 < mX1Clip)
        {
            x1 = mX1Clip;
        }
        if (x2 > mX2Clip)
        {
            x2 = mX2Clip;
        }
        if (x1 >= x2)
        {
            return;
        }
        if (y1 < mY1Clip)
        {
            y1 = mY1Clip;
        }
        if (y2 > mY2Clip)
        {
            y2 = mY2Clip;
        }
        if (y1 < y2)
        {
            x1 -= mX1;
            x2 -= mX1;
            y1 -= mY1;
            y2 -= mY1;
            if (obstacle != null)
            {
                fillRect(obstacle, x1, y1, x2, y2, val & 1);
            }
            if (platform != null)
            {
                fillRect(platform, x1, y1, x2, y2, (val >> 1) & 1);
            }
        }
    }

    private void fillRect(ushort[] mask, int x1, int y1, int x2, int y2, int val)
    {
        int num = y1 * lineWidth + (x1 & -16) / 16;
        int num2 = y2 - y1;
        int num3 = x2 / 16 - x1 / 16 + 1;
        if (num3 > 1)
        {
            ushort num4;
            ushort num5;
            if (val == 0)
            {
                num4 = (ushort)(~lMask[x1 & 0xF]);
                num5 = (ushort)(~rMask[x2 & 0xF]);
                for (int i = 0; i < num2; i++)
                {
                    int num6 = num + i * lineWidth;
                    mask[num6] &= num4;
                    int j;
                    for (j = 1; j < num3 - 1; j++)
                    {
                        mask[num6 + j] = 0;
                    }
                    if (j == num3 - 1)
                    {
                        mask[num6 + j] &= num5;
                    }
                }
                return;
            }
            num4 = lMask[x1 & 0xF];
            num5 = rMask[x2 & 0xF];
            for (int i = 0; i < num2; i++)
            {
                int num6 = num + i * lineWidth;
                mask[num6] |= num4;
                int j;
                for (j = 1; j < num3 - 1; j++)
                {
                    mask[num6 + j] = ushort.MaxValue;
                }
                if (j == num3 - 1)
                {
                    mask[num6 + j] |= num5;
                }
            }
        }
        else if (val == 0)
        {
            ushort num4 = (ushort)(~(lMask[x1 & 0xF] & rMask[x2 & 0xF]));
            for (int i = 0; i < num2; i++)
            {
                int num6 = num + i * lineWidth;
                mask[num6] &= num4;
            }
        }
        else
        {
            ushort num4 = (ushort)(lMask[x1 & 0xF] & rMask[x2 & 0xF]);
            for (int i = 0; i < num2; i++)
            {
                int num6 = num + i * lineWidth;
                mask[num6] |= num4;
            }
        }
    }

    public void orMask(CMask mask, int xx, int yy, int plans, int val)
    {
        if ((plans & 1) != 0 && obstacle != null)
        {
            orIt(obstacle, mask, xx, yy, (val & 1) != 0);
        }
        if ((plans & 2) != 0 && platform != null)
        {
            orIt(platform, mask, xx, yy, ((val >> 1) & 1) != 0);
        }
    }

    public void orIt(ushort[] dMask, CMask sMask, int xx, int yy, bool bOr)
    {
        int num = xx;
        int num2 = yy;
        int num3 = xx + sMask.width;
        int num4 = yy + sMask.height;
        int num5 = 0;
        int num6 = 0;
        int num7 = sMask.width;
        int num8 = sMask.height;
        if (num < mX1Clip)
        {
            num5 = mX1Clip - num;
            if (num5 > sMask.width)
            {
                return;
            }
            num = mX1Clip;
        }
        if (num3 > mX2Clip)
        {
            num7 = sMask.width - (num3 - mX2Clip);
            if (num7 < 0)
            {
                return;
            }
            num3 = mX2Clip;
        }
        if (num2 < mY1Clip)
        {
            num6 = mY1Clip - num2;
            if (num6 > sMask.height)
            {
                return;
            }
            num2 = mY1Clip;
        }
        if (num4 > mY2Clip)
        {
            num8 = sMask.height - (num4 - mY2Clip);
            if (num8 < 0)
            {
                return;
            }
            num4 = mY2Clip;
        }
        num -= mX1;
        num3 -= mX1;
        num2 -= mY1;
        num4 -= mY1;
        int num9 = num8 - num6;
        int num10 = num7 / 16 - num5 / 16 + 1;
        int num11 = num & 0xF;
        if (num11 != 0)
        {
            switch (num10)
            {
                case 1:
                    {
                        if (bOr)
                        {
                            for (uint num12 = 0u; num12 < num9; num12++)
                            {
                                int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                                uint num14 = (uint)((ulong)(sMask.mask[(num6 + num12) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[num7 & 0xF]) & 0xFFFFuL);
                                dMask[num13] |= (ushort)(num14 >> num11);
                                if (num / 16 + 1 < lineWidth)
                                {
                                    dMask[++num13] |= (ushort)(num14 << 15 - num11);
                                }
                            }
                            return;
                        }
                        for (uint num12 = 0u; num12 < num9; num12++)
                        {
                            int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                            uint num14 = (uint)((ulong)(sMask.mask[(num6 + num12) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[num7 & 0xF]) & 0xFFFFuL);
                            dMask[num13] &= (ushort)(~(num14 >> num11));
                            num13++;
                            if (num / 16 + 1 < lineWidth)
                            {
                                dMask[++num13] &= (ushort)(~(num14 << 15 - num11));
                            }
                        }
                        return;
                    }
                case 2:
                    {
                        if (bOr)
                        {
                            for (uint num12 = 0u; num12 < num9; num12++)
                            {
                                int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                                int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                                uint num14 = (uint)((ulong)(sMask.mask[num15] & lMask[num5 & 0xF]) & 0xFFFFuL);
                                dMask[num13] |= (ushort)(num14 >> num11);
                                num13++;
                                dMask[num13] |= (ushort)(num14 << 16 - num11);
                                num14 = (uint)((ulong)(sMask.mask[num15 + 1] & rMask[num7 & 0xF]) & 0xFFFFuL);
                                dMask[num13] |= (ushort)(num14 >> num11);
                                if (num / 16 + 2 < lineWidth)
                                {
                                    dMask[++num13] |= (ushort)(num14 << 16 - num11);
                                }
                            }
                            return;
                        }
                        for (uint num12 = 0u; num12 < num9; num12++)
                        {
                            int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                            int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                            uint num14 = (uint)((ulong)(sMask.mask[num15] & lMask[num5 & 0xF]) & 0xFFFFuL);
                            dMask[num13] &= (ushort)(~(num14 >> num11));
                            num13++;
                            dMask[num13] &= (ushort)(~(num14 << 16 - num11));
                            num14 = (uint)((ulong)(sMask.mask[num15 + 1] & rMask[num7 & 0xF]) & 0xFFFFuL);
                            dMask[num13] &= (ushort)(~(num14 >> num11));
                            if (num / 16 + 2 < lineWidth)
                            {
                                dMask[++num13] &= (ushort)(~(num14 << 16 - num11));
                            }
                        }
                        return;
                    }
            }
            if (bOr)
            {
                for (uint num12 = 0u; num12 < num9; num12++)
                {
                    int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                    int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                    uint num14 = (uint)((ulong)(sMask.mask[num15] & lMask[num5 & 0xF]) & 0xFFFFuL);
                    dMask[num13] |= (ushort)(num14 >> num11);
                    num13++;
                    dMask[num13] |= (ushort)(num14 << 16 - num11);
                    uint num16;
                    for (num16 = 1u; num16 < num10 - 1; num16++)
                    {
                        num14 = (uint)(sMask.mask[num15 + num16] & 0xFFFF);
                        dMask[num13] |= (ushort)(num14 >> num11);
                        num13++;
                        dMask[num13] |= (ushort)(num14 << 16 - num11);
                    }
                    num14 = (uint)((ulong)(sMask.mask[num15 + num16] & rMask[num7 & 0xF]) & 0xFFFFuL);
                    dMask[num13] |= (ushort)(num14 >> num11);
                    if (num / 16 + num16 < lineWidth)
                    {
                        dMask[++num13] |= (ushort)(num14 << 16 - num11);
                    }
                }
                return;
            }
            for (uint num12 = 0u; num12 < num9; num12++)
            {
                int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                uint num14 = (uint)((ulong)(sMask.mask[num15] & lMask[num5 & 0xF]) & 0xFFFFuL);
                dMask[num13] &= (ushort)(~(num14 >> num11));
                num13++;
                dMask[num13] &= (ushort)(~(num14 << 16 - num11));
                uint num16;
                for (num16 = 1u; num16 < num10 - 1; num16++)
                {
                    num14 = (uint)(sMask.mask[num15 + num16] & 0xFFFF);
                    dMask[num13] &= (ushort)(~(num14 >> num11));
                    num13++;
                    dMask[num13] &= (ushort)(~(num14 << 16 - num11));
                }
                num14 = (uint)((ulong)(sMask.mask[num15 + num16] & rMask[num7 & 0xF]) & 0xFFFFuL);
                dMask[num13] &= (ushort)(~(num14 >> num11));
                if (num / 16 + num16 < lineWidth)
                {
                    dMask[++num13] &= (ushort)(~(num14 << 16 - num11));
                }
            }
            return;
        }
        switch (num10)
        {
            case 1:
                if (bOr)
                {
                    for (uint num12 = 0u; num12 < num9; num12++)
                    {
                        ushort num17 = (ushort)(sMask.mask[(num6 + num12) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[num7 & 0xF]);
                        dMask[(num2 + num12) * lineWidth + num / 16] |= num17;
                    }
                }
                else
                {
                    for (uint num12 = 0u; num12 < num9; num12++)
                    {
                        ushort num17 = (ushort)(sMask.mask[(num6 + num12) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[num7 & 0xF]);
                        dMask[(num2 + num12) * lineWidth + num / 16] &= (ushort)(~num17);
                    }
                }
                return;
            case 2:
                if (bOr)
                {
                    for (uint num12 = 0u; num12 < num9; num12++)
                    {
                        int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                        int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                        ushort num17 = (ushort)(sMask.mask[num15] & lMask[num5 & 0xF]);
                        dMask[num13] |= num17;
                        num17 = (ushort)(sMask.mask[num15 + 1] & rMask[num7 & 0xF]);
                        dMask[num13 + 1] |= num17;
                    }
                }
                else
                {
                    for (uint num12 = 0u; num12 < num9; num12++)
                    {
                        int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                        int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                        ushort num17 = (ushort)(sMask.mask[num15] & lMask[num5 & 0xF]);
                        dMask[num13] &= (ushort)(~num17);
                        num17 = (ushort)(sMask.mask[num15 + 1] & rMask[num7 & 0xF]);
                        dMask[num13 + 1] &= (ushort)(~num17);
                    }
                }
                return;
        }
        if (bOr)
        {
            for (uint num12 = 0u; num12 < num9; num12++)
            {
                int num13 = (int)((num2 + num12) * lineWidth + num / 16);
                int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
                ushort num17 = (ushort)(sMask.mask[num15] & lMask[num5 & 0xF]);
                dMask[num13] |= num17;
                uint num16;
                for (num16 = 1u; num16 < num10 - 1; num16++)
                {
                    num17 = sMask.mask[num15 + num16];
                    dMask[num13 + num16] |= num17;
                }
                if ((num7 & 0x10) > 0)
                {
                    num17 = (ushort)(sMask.mask[num15 + num16] & rMask[num7 & 0xF]);
                    dMask[num13 + num16] |= num17;
                }
            }
            return;
        }
        for (uint num12 = 0u; num12 < num9; num12++)
        {
            int num13 = (int)((num2 + num12) * lineWidth + num / 16);
            int num15 = (int)((num6 + num12) * sMask.lineWidth + num5 / 16);
            ushort num17 = (ushort)(sMask.mask[num15] & lMask[num5 & 0xF]);
            dMask[num13] &= (ushort)(~num17);
            uint num16;
            for (num16 = 1u; num16 < num10 - 1; num16++)
            {
                num17 = sMask.mask[num15 + num16];
                dMask[num13 + num16] &= (ushort)(~num17);
            }
            if ((num7 & 0x10) > 0)
            {
                num17 = (ushort)(sMask.mask[num15 + num16] & rMask[num7 & 0xF]);
                dMask[num13 + num16] &= (ushort)(~num17);
            }
        }
    }

    public void orPlatformMask(CMask sMask, int xx, int yy)
    {
        int num = xx;
        int num2 = yy;
        int num3 = xx + sMask.width;
        int num4 = yy + sMask.height;
        int num5 = 0;
        int num6 = 0;
        int num7 = sMask.width;
        int num8 = sMask.height;
        if (num < mX1Clip)
        {
            num5 = mX1Clip - num;
            if (num5 > sMask.width)
            {
                return;
            }
            num = mX1Clip;
        }
        if (num3 > mX2Clip)
        {
            num7 = sMask.width - (num3 - mX2Clip);
            if (num7 < 0)
            {
                return;
            }
            num3 = mX2Clip;
        }
        if (num2 < mY1Clip)
        {
            num6 = mY1Clip - num2;
            if (num6 > sMask.height)
            {
                return;
            }
            num2 = mY1Clip;
        }
        if (num4 > mY2Clip)
        {
            num8 = sMask.height - (num4 - mY2Clip);
            if (num8 < 0)
            {
                return;
            }
            num4 = mY2Clip;
        }
        num -= mX1;
        num3 -= mX1;
        num2 -= mY1;
        num4 -= mY1;
        int num9 = num8 - num6;
        int num10 = num7 - num5;
        ushort[] mask = sMask.mask;
        for (int i = 0; i < num10; i++)
        {
            int num11 = (num5 + i) / 16;
            ushort num12 = (ushort)(32768 >> ((num5 + i) & 0xF));
            int j;
            for (j = 0; j < num9 && (mask[(num6 + j) * sMask.lineWidth + num11] & num12) == 0; j++)
            {
            }
            if (j >= num9)
            {
                continue;
            }
            int num13 = Math.Min(j + 6, num9);
            int num14 = (num + i) / 16;
            ushort num15 = (ushort)(32768 >> ((num + i) & 0xF));
            for (; j < num13; j++)
            {
                if ((mask[(num6 + j) * sMask.lineWidth + num11] & num12) != 0)
                {
                    platform[(num2 + j) * lineWidth + num14] |= num15;
                }
            }
        }
    }

    public bool testPoint(int x, int y, int plans)
    {
        if (plans == 0 && obstacle != null && testPt(obstacle, x, y))
        {
            return true;
        }
        if (plans == 1)
        {
            if (platform != null)
            {
                if (testPt(platform, x, y))
                {
                    return true;
                }
            }
            else if (obstacle != null && testPt(obstacle, x, y))
            {
                return true;
            }
        }
        return false;
    }

    private bool testPt(ushort[] mask, int x, int y)
    {
        x += mDxScroll;
        y += mDyScroll;
        if (x < mX1Clip || x > mX2Clip)
        {
            return false;
        }
        if (y < mY1Clip || y > mY2Clip)
        {
            return false;
        }
        x -= mX1;
        y -= mY1;
        int num = y * lineWidth + x / 16;
        ushort num2 = (ushort)(32768 >> (x & 0xF));
        return (mask[num] & num2) != 0;
    }

    public bool testRect(int x, int y, int w, int h, int plans)
    {
        if (plans == 0 && obstacle != null && testRc(obstacle, x, y, w, h))
        {
            return true;
        }
        if (plans == 1)
        {
            if (platform != null)
            {
                if (testRc(platform, x, y, w, h))
                {
                    return true;
                }
            }
            else if (obstacle != null && testRc(obstacle, x, y, w, h))
            {
                return true;
            }
        }
        return false;
    }

    private bool testRc(ushort[] mask, int xx, int yy, int sx, int sy)
    {
        int num = xx;
        int num2 = yy;
        num += mDxScroll;
        num2 += mDyScroll;
        int num3 = num + sx;
        int num4 = num2 + sy;
        if (num < mX1Clip)
        {
            num = mX1Clip;
        }
        if (num3 > mX2Clip)
        {
            num3 = mX2Clip;
        }
        if (num2 < mY1Clip)
        {
            num2 = mY1Clip;
        }
        if (num4 > mY2Clip)
        {
            num4 = mY2Clip;
        }
        if (num3 <= num || num4 <= num2)
        {
            return false;
        }
        num -= mX1;
        num3 -= mX1;
        num2 -= mY1;
        num4 -= mY1;
        int num5 = num4 - num2;
        int num6 = (num3 - 1) / 16 - num / 16 + 1;
        switch (num6)
        {
            case 1:
                {
                    ushort num8 = (ushort)(lMask[num & 0xF] & rMask[((num3 - 1) & 0xF) + 1]);
                    for (int i = 0; i < num5; i++)
                    {
                        int num7 = (num2 + i) * lineWidth + num / 16;
                        if ((mask[num7] & num8) != 0)
                        {
                            return true;
                        }
                    }
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < num5; i++)
                    {
                        int num7 = (num2 + i) * lineWidth + num / 16;
                        if ((mask[num7] & lMask[num & 0xF]) != 0)
                        {
                            return true;
                        }
                        if ((mask[num7 + 1] & rMask[((num3 - 1) & 0xF) + 1]) != 0)
                        {
                            return true;
                        }
                    }
                    break;
                }
            default:
                {
                    for (int i = 0; i < num5; i++)
                    {
                        int num7 = (num2 + i) * lineWidth + num / 16;
                        if ((mask[num7] & lMask[num & 0xF]) != 0)
                        {
                            return true;
                        }
                        int j;
                        for (j = 1; j < num6 - 1; j++)
                        {
                            if (mask[num7 + j] != 0)
                            {
                                return true;
                            }
                        }
                        if ((mask[num7 + j] & rMask[((num3 - 1) & 0xF) + 1]) != 0)
                        {
                            return true;
                        }
                    }
                    break;
                }
        }
        return false;
    }

    public bool testMask(CMask mask, int yBase, int xx, int yy, int plans)
    {
        if (plans == 0 && obstacle != null && testIt(obstacle, mask, yBase, xx, yy))
        {
            return true;
        }
        if (plans == 1)
        {
            if (platform != null)
            {
                if (testIt(platform, mask, yBase, xx, yy))
                {
                    return true;
                }
            }
            else if (obstacle != null && testIt(obstacle, mask, yBase, xx, yy))
            {
                return true;
            }
        }
        return false;
    }

    public bool testIt(ushort[] dMask, CMask sMask, int yBase, int xx, int yy)
    {
        int num = xx;
        int num2 = yy;
        num += mDxScroll;
        num2 += mDyScroll;
        int num3 = num + sMask.width;
        int num4 = num2 + sMask.height;
        int num5 = 0;
        int num6 = yBase;
        int num7 = sMask.width;
        int num8 = sMask.height;
        if (num < mX1Clip)
        {
            num5 = mX1Clip - num;
            if (num5 > sMask.width)
            {
                return false;
            }
            num = mX1Clip;
        }
        if (num3 > mX2Clip)
        {
            num7 = sMask.width - (num3 - mX2Clip);
            if (num7 < 0)
            {
                return false;
            }
            num3 = mX2Clip;
        }
        if (num2 < mY1Clip)
        {
            num6 = mY1Clip - num2;
            if (num6 > sMask.height)
            {
                return false;
            }
            num2 = mY1Clip;
        }
        if (num4 > mY2Clip)
        {
            num8 = sMask.height - (num4 - mY2Clip);
            if (num8 < 0)
            {
                return false;
            }
            num4 = mY2Clip;
        }
        if (num7 <= num5)
        {
            return false;
        }
        num -= mX1;
        num3 -= mX1;
        num2 -= mY1;
        num4 -= mY1;
        int num9 = num8 - num6;
        int num10 = (num7 - num5 + 15) / 16;
        int num11 = num & 0xF;
        if (num11 != 0)
        {
            switch (num10)
            {
                case 1:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            int num12 = (num2 + i) * lineWidth + num / 16;
                            uint num14 = (uint)(sMask.mask[(num6 + i) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[((num7 - 1) & 0xF) + 1] & 0xFFFF);
                            if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                            {
                                return true;
                            }
                            if (num / 16 + 1 < lineWidth && (dMask[++num12] & (ushort)(num14 << 15 - num11)) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            int num12 = (num2 + i) * lineWidth + num / 16;
                            int num13 = (num6 + i) * sMask.lineWidth + num5 / 16;
                            uint num14 = (uint)((ulong)(sMask.mask[num13] & lMask[num5 & 0xF]) & 0xFFFFuL);
                            if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                            {
                                return true;
                            }
                            num12++;
                            if ((dMask[num12] & (ushort)(num14 << 16 - num11)) != 0)
                            {
                                return true;
                            }
                            num14 = (uint)(sMask.mask[num13 + 1] & rMask[((num7 - 1) & 0xF) + 1] & 0xFFFF);
                            if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                            {
                                return true;
                            }
                            if (num / 16 + 2 < lineWidth && (dMask[++num12] & (ushort)(num14 << 16 - num11)) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            int num12 = (num2 + i) * lineWidth + num / 16;
                            int num13 = (num6 + i) * sMask.lineWidth + num5 / 16;
                            uint num14 = (uint)(sMask.mask[num13] & lMask[num5 & 0xF] & 0xFFFF);
                            if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                            {
                                return true;
                            }
                            num12++;
                            if ((dMask[num12] & (ushort)(num14 << 16 - num11)) != 0)
                            {
                                return true;
                            }
                            int j;
                            for (j = 1; j < num10 - 1; j++)
                            {
                                num14 = (uint)(sMask.mask[num13 + j] & 0xFFFF);
                                if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                                {
                                    return true;
                                }
                                num12++;
                                if ((dMask[num12] & (ushort)(num14 << 16 - num11)) != 0)
                                {
                                    return true;
                                }
                            }
                            num14 = (uint)(sMask.mask[num13 + j] & rMask[((num7 - 1) & 0xF) + 1] & 0xFFFF);
                            if ((dMask[num12] & (ushort)(num14 >> num11)) != 0)
                            {
                                return true;
                            }
                            if (num / 16 + j < lineWidth && (dMask[++num12] & (ushort)(num14 << 16 - num11)) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }
        }
        else
        {
            switch (num10)
            {
                case 1:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            ushort num15 = (ushort)(sMask.mask[(num6 + i) * sMask.lineWidth + num5 / 16] & lMask[num5 & 0xF] & rMask[((num7 - 1) & 0xF) + 1]);
                            if ((dMask[(num2 + i) * lineWidth + num / 16] & num15) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            int num12 = (num2 + i) * lineWidth + num / 16;
                            int num13 = (num6 + i) * sMask.lineWidth + num5 / 16;
                            ushort num15 = (ushort)(sMask.mask[num13] & lMask[num5 & 0xF]);
                            if ((dMask[num12] & num15) != 0)
                            {
                                return true;
                            }
                            num15 = (ushort)(sMask.mask[num13 + 1] & rMask[((num7 - 1) & 0xF) + 1]);
                            if ((dMask[num12 + 1] & num15) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < num9; i++)
                        {
                            int num12 = (num2 + i) * lineWidth + num / 16;
                            int num13 = (num6 + i) * sMask.lineWidth + num5 / 16;
                            ushort num15 = (ushort)(sMask.mask[num13] & lMask[num5 & 0xF]);
                            if ((dMask[num12] & num15) != 0)
                            {
                                return true;
                            }
                            int j;
                            for (j = 1; j < num10 - 1; j++)
                            {
                                num15 = sMask.mask[num13 + j];
                                if ((dMask[num12 + j] & num15) != 0)
                                {
                                    return true;
                                }
                            }
                            num15 = (ushort)(sMask.mask[num13 + j] & rMask[((num7 - 1) & 0xF) + 1]);
                            if ((dMask[num12 + j] & num15) != 0)
                            {
                                return true;
                            }
                        }
                        break;
                    }
            }
        }
        return false;
    }
}