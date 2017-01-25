using System.Collections.Generic;
using IntelRealSenseStart.Code.RealSense.Data.Common;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Data.Event;
using IntelRealSenseStart.Code.RealSense.Factory;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Creator
{
    public class SkeletonsBuilder
    {
        private readonly RealSenseFactory factory;

        public SkeletonsBuilder(RealSenseFactory factory)
        {
            this.factory = factory;
        }

        public RealSenseSkeletonsData GetSkeletonsData(List<SkeletonDeterminerData> skeletonsDeterminerData)
        {
            RealSenseSkeletonsData.Builder skeletonsData = factory.Data.Events.Skeletons();
            skeletonsDeterminerData?.Do(skeletonDeterminerData => skeletonsData.WithSkeletonData(GetSkeletonData(skeletonDeterminerData)));
            return skeletonsData.Build();
        }

        private RealSenseSkeletonData.Builder GetSkeletonData(SkeletonDeterminerData skeletonDeterminerData)
        {
            RealSenseSkeletonData.Builder skeletonData = factory.Data.Events.Skeleton();
            if (skeletonDeterminerData.SkeletonPoints != null)
            {
                0.To(skeletonDeterminerData.SkeletonPoints.Length - 1).ToArray().Do(index =>
                    skeletonData.WithDetectionPoint(GetLandmarkName(skeletonDeterminerData.SkeletonPoints[index]),
                        GetDetectionPoint(skeletonDeterminerData.SkeletonPoints[index])));
            }
            return skeletonData.WithPersonId(skeletonDeterminerData.PersonId);
        }

        private SkeletonLandmark GetLandmarkName(PXCMPersonTrackingData.PersonJoints.SkeletonPoint skeletonPoint)
        {
            PXCMPersonTrackingData.PersonJoints.JointType landmark = skeletonPoint.jointType;
            return (SkeletonLandmark) landmark;
        }

        private DetectionPoint.Builder GetDetectionPoint(PXCMPersonTrackingData.PersonJoints.SkeletonPoint skeletonPoint)
        {
            return factory.Data.Events.DetectionPoint()
                .WithImagePosition(GetPoint2DFrom(skeletonPoint.image, skeletonPoint.confidenceImage))
                .WithWorldPosition(GetPoint3DFrom(skeletonPoint.world, skeletonPoint.confidenceWorld));
        }

        private Point2D.Builder GetPoint2DFrom(PXCMPointF32 point, int confidence)
        {
            return factory.Data.Common.Point2D().From(point).WithConfidence(confidence);
        }

        private Point3D.Builder GetPoint3DFrom(PXCMPoint3DF32 point, int confidence)
        {
            return factory.Data.Common.Point3D().From(point).WithConfidence(confidence);
        }

        public class Builder
        {
            private readonly SkeletonsBuilder skeletonsBuilder;

            public Builder(RealSenseFactory factory)
            {
                skeletonsBuilder = new SkeletonsBuilder(factory);
            }

            public SkeletonsBuilder Build()
            {
                return skeletonsBuilder;
            }
        }
    }
}