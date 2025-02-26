using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Ces.Utilities
{
    public static class GeoUtilitiesDouble
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 GeoCoordToUnitSphere(double2 geoCoords)
        {
            double r = math.cos(geoCoords.y);
            double x = -math.sin(geoCoords.x) * r;
            double y = -math.sin(geoCoords.y);
            double z = math.cos(geoCoords.x) * r;

            return new double3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 UnitSphereToGeoCoord(double3 unitSphere)
        {
            double lonRad = math.atan2(-unitSphere.x, unitSphere.z);
            double latRad = math.asin(-unitSphere.y);

            return new double2(lonRad, latRad);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 UnitSphereToGeoCoordAtan(double3 unitSphere)
        {
            double lonRad = math.atan2(-unitSphere.x, unitSphere.z);
            double radiusXZ = math.sqrt(unitSphere.x * unitSphere.x + unitSphere.z * unitSphere.z);
            double latRad = math.atan2(-unitSphere.y, radiusXZ);

            return new double2(lonRad, latRad);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 GeoCoordsToPlaneUv(double2 geoCoords)
        {
            double u = (geoCoords.x / (2.0 * math.PI_DBL)) + 0.5;
            double v = 0.5 - (geoCoords.y / math.PI_DBL);

            return new double2(u, v);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 PlaneUvToGeoCoord(double2 uv)
        {
            double lonRad = (uv.x - 0.5) * 2.0 * math.PI_DBL;
            double latRad = (0.5 - uv.y) * math.PI_DBL;

            return new double2(lonRad, latRad);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double3 PlaneUvToUnitSphere(double2 planeUv)
        {
            return GeoCoordToUnitSphere(PlaneUvToGeoCoord(planeUv));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 UnitSphereToPlaneUv(double3 unitSphere)
        {
            return GeoCoordsToPlaneUv(UnitSphereToGeoCoord(unitSphere));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 UnitSphereToPlaneUvAtan(double3 unitSphere)
        {
            return GeoCoordsToPlaneUv(UnitSphereToGeoCoordAtan(unitSphere));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PlaneUvToBoth(double2 planeUv, out double2 geoCoords, out double3 unitSphere)
        {
            geoCoords = PlaneUvToGeoCoord(planeUv);
            unitSphere = GeoCoordToUnitSphere(geoCoords);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnitSphereToBoth(double3 unitSphere, out double2 geoCoords, out double2 planeUv)
        {
            geoCoords = UnitSphereToGeoCoord(unitSphere);
            planeUv = GeoCoordsToPlaneUv(geoCoords);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 PixelCoordToPlaneUv(int2 pixelCoord, int2 textureSize)
        {
            double x = (pixelCoord.x + 0.5) / textureSize.x;
            double y = (pixelCoord.y + 0.5) / textureSize.y;

            return new double2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 EdgeCoordToPlaneUv(int2 pixelCoord, int2 textureSize)
        {
            double x = pixelCoord.x / (double)textureSize.x;
            double y = pixelCoord.y / (double)textureSize.y;

            return new double2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int2 PlaneUvToPixelCoord(double2 uv, int2 textureSize)
        {
            int x = (int)(uv.x * textureSize.x);
            int y = (int)(uv.y * textureSize.y);

            return TexUtilities.ClampPixelCoord(new int2(x, y), textureSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(double3 unitSphereA, double3 unitSphereB)
        {
            double dot = math.dot(unitSphereA, unitSphereB);
            dot = math.clamp(dot, -1.0, 1.0);

            return math.acos(dot);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceAtan(double3 unitSphereA, double3 unitSphereB)
        {
            var crossV = math.cross(unitSphereA, unitSphereB);
            double sinTheta = math.length(crossV);
            double cosTheta = math.dot(unitSphereA, unitSphereB);

            return math.atan2(sinTheta, cosTheta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceAtanFast(double3 unitSphereA, double3 unitSphereB)
        {
            var crossV = math.cross(unitSphereA, unitSphereB);
            double sinTheta = math.length(crossV);
            double cosTheta = math.dot(unitSphereA, unitSphereB);

            return CesMath.FastAtan2(sinTheta, cosTheta);
        }
    }
}