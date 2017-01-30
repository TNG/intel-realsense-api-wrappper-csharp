using System;

namespace IntelRealSenseStart.Code.RealSense.Data.Common
{
    [Serializable]
    public class Point3D
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Z { get; private set; }

        public int Confidence { get; private set; }

        private Point3D()
        {
        }

        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D operator /(Point3D point3D, float factor)
        {
            return new Point3D(point3D.X/factor, point3D.Y/factor, point3D.Z/factor);
        }

        public static Point3D operator +(Point3D point3D, Point3D otherPoint3D)
        {
            return new Point3D(point3D.X+otherPoint3D.X, point3D.Y+otherPoint3D.Y, point3D.Z+otherPoint3D.Z);
        }

        public static Point3D operator *(Point3D point3D, float factor)
        {
            return new Point3D(point3D.X*factor, point3D.Y*factor, point3D.Z*factor);
        }

        public static Point3D operator *(float factor, Point3D point3D)
        {
            return point3D*factor;
        }

        public class Builder
        {
            private readonly Point3D point3D;

            public Builder()
            {
                point3D = new Point3D();
            }

            public Builder From(PXCMPoint3DF32 point)
            {
                point3D.X = -point.x/1000;  // World Position is given in mm but wanted in m
                point3D.Y = point.y/1000;
                point3D.Z = point.z/1000;
                return this;
            }
            
            public Builder WithX(float x)
            {
                point3D.X = x;
                return this;
            }

            public Builder WithY(float y)
            {
                point3D.Y = y;
                return this;
            }

            public Builder WithZ(float z)
            {
                point3D.Z = z;
                return this;
            }
            
            public Builder WithConfidence(int confidence)
            {
                point3D.Confidence = confidence;
                return this;
            }

            public Point3D Build()
            {
                return point3D;
            }
        }
    }
}