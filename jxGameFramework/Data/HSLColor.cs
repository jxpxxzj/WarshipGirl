using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace jxGameFramework.Data
{
    struct HSLColor
    {
        public int H;
        public int S;
        public int L;
        public static Color HSLToRGB(int H, int S, int L)
        {
            double p1, p2;
            double r, g, b;
            Color rgb = new Color();
            if (L <= 0.5)
            {
                p2 = L * (1 + S);
            }
            else
            {
                p2 = L + S - (L * S);
            }
            p1 = 2 * L - p2;
            if (S == 0)
            {
                r = L;
                g = L;
                b = L;
            }
            else
            {
                r = toRgb(p1, p2, H + 120);
                g = toRgb(p1, p2, H);
                b = toRgb(p1, p2, H - 120);
            }
            rgb.R = (byte)Math.Round(r * 255);
            rgb.G = (byte)Math.Round(g * 255);
            rgb.B = (byte)Math.Round(b * 255);
            return rgb;
        }
        public Color ToRGB()
        {
            return HSLToRGB(H, S, L);
        }
        public static HSLColor FromRGB(Color rgb)
        {
            var tempcolor = new HSLColor();
            double R = rgb.R / 255.00;
            double G = rgb.G / 255.00;
            double B = rgb.B / 255.00;
            double max, min, diff, r_dist, g_dist, b_dist;
            double h, s, l;
            int[] hsl = new int[3];
            max = Math.Max(Math.Max(R, G), B);
            min = Math.Min(Math.Min(R, G), B);
            diff = max - min;
            l = (max + min) / 2.00 * 100.00;
            if (diff == 0)
            {
                h = 0;
                s = 0;
            }
            else
            {
                if (l < 50)
                {
                    s = diff / (max + min) * 100.00;
                }
                else
                {
                    s = diff / (2 - max - min) * 100.00;
                }
                r_dist = (max - R) / diff;
                g_dist = (max - G) / diff;
                b_dist = (max - B) / diff;
                h = b_dist - g_dist;
                if (R == max)
                {
                    h = b_dist - g_dist;
                }
                else if (G == max)
                {
                    h = 2 + r_dist - b_dist;
                }
                else if (B == max)
                {
                    h = 4 + g_dist - r_dist;
                }
                h *= 60;
                if (h < 0)
                {
                    h += 360;
                }
                if (h >= 360)
                {
                    h -= 360;
                }
            }
            tempcolor.H = (int)h;
            tempcolor.S = (int)s;
            tempcolor.L = (int)l;

            return tempcolor;
        }
        private static double toRgb(double q1, double q2, double hue)
        {
            if (hue > 360)
            {
                hue = hue - 360;
            }
            if (hue < 0)
            {
                hue = hue + 360;
            }
            if (hue < 60)
            {
                return (q1 + (q2 - q1) * hue / 60);
            }
            else if (hue < 180)
            {
                return (q2);
            }
            else if (hue < 240)
            {
                return (q1 + (q2 - q1) * (240 - hue) / 60);
            }
            else
            {
                return (q1);
            }
        }
    }
}
