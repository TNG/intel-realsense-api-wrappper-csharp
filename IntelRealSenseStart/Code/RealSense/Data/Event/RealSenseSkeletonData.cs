using System;
using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    [Serializable]
    public class RealSenseSkeletonData
    {
        private readonly Dictionary<SkeletonLandmark, DetectionPoint> detectionPoints;

        private int personId;

        private RealSenseSkeletonData()
        {
            detectionPoints = new Dictionary<SkeletonLandmark, DetectionPoint>();
        }

        public Dictionary<SkeletonLandmark, DetectionPoint> DetectionPoints
        {
            get { return detectionPoints; }
        }

        public DetectionPoint GetPoint(SkeletonLandmark landmark)
        {
            return detectionPoints.ContainsKey(landmark) ? detectionPoints[landmark] : null;
        }

        public int PersonId
        {
            get { return personId; }
        }

        public class Builder
        {
            private readonly RealSenseSkeletonData realSenseSkeletonData;

            public Builder()
            {
                realSenseSkeletonData = new RealSenseSkeletonData();
            }

            public Builder WithDetectionPoint(SkeletonLandmark landmark, DetectionPoint.Builder detectionPoint)
            {
                realSenseSkeletonData.detectionPoints[landmark] = detectionPoint.Build();
                return this;
            }

            public Builder WithDetectionPoint(SkeletonLandmark landmark, DetectionPoint detectionPoint)
            {
                realSenseSkeletonData.detectionPoints[landmark] = detectionPoint;
                return this;
            }

            public Builder WithPersonId(int personId)
            {
                realSenseSkeletonData.personId = personId;
                return this;
            }

            public RealSenseSkeletonData Build()
            {
                return realSenseSkeletonData;
            }
        }
    }
}