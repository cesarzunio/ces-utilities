using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Ces.Utilities
{
    [BurstCompile]
    public static class CesMath
    {
        // https://math.stackexchange.com/questions/1098487/atan2-faster-approximation
        [MethodImpl(MethodImplOptions.AggressiveInlining), BurstCompile]
        public static double FastAtan2(double y, double x)
        {
            double abs_y = math.abs(y);
            double abs_x = math.abs(x);
            double a = math.min(abs_x, abs_y) / math.max(abs_x, abs_y);
            double s = a * a;
            double r = ((-0.0464964749 * s + 0.15931422) * s - 0.327622764) * s * a + a;

            r = abs_y > abs_x ? 1.57079637 - r : r;
            r = x < 0 ? 3.14159274 - r : r;
            r = y < 0 ? -r : r;

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(bool a, bool b)
        {
            return a && b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(bool a, bool b, bool c)
        {
            return a && b && c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(bool a, bool b, bool c, bool d)
        {
            return a && b && c && d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(bool a, bool b)
        {
            return a || b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(bool a, bool b, bool c)
        {
            return a || b || c;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(bool a, bool b, bool c, bool d)
        {
            return a || b || c || d;
        }
    }
}