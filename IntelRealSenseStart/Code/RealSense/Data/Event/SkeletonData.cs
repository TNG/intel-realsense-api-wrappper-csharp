using System;
using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    [Serializable]
    public class SkeletonData
    {
        private readonly Dictionary<SkeletonLandmark, DetectionPoint> detectionPoints;

        private int personId;

        private SkeletonData()
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
            private readonly SkeletonData skeletonData;

            public Builder()
            {
                skeletonData = new SkeletonData();
            }

            public Builder WithDetectionPoint(SkeletonLandmark landmark, DetectionPoint.Builder detectionPoint)
            {
                skeletonData.detectionPoints[landmark] = detectionPoint.Build();
                return this;
            }

            public Builder WithPersonId(int personId)
            {
                skeletonData.personId = personId;
                return this;
            }

            public SkeletonData Build()
            {
                return skeletonData;
            }
        }
    }
}