﻿using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    public class FaceLandmarksData
    {
        private readonly Dictionary<FaceLandmark, DetectionPoint> detectionPoints;
        private float heartRate;

        private FaceLandmarksData()
        {
            detectionPoints = new Dictionary<FaceLandmark, DetectionPoint>();
        }

        protected Dictionary<FaceLandmark, DetectionPoint> DetectionPoints
        {
            get { return detectionPoints; }
        }

        public DetectionPoint GetPoint(FaceLandmark landmark)
        {
            return detectionPoints[landmark];
        }

        public float HeartRate
        {
            get { return heartRate; }
        }

        public class Builder
        {
            private readonly FaceLandmarksData faceLandmarksData;

            public Builder()
            {
                faceLandmarksData = new FaceLandmarksData();
            }

            public Builder WithDetectionPoint(FaceLandmark landmark, DetectionPoint.Builder detectionPoint)
            {
                faceLandmarksData.detectionPoints[landmark] = detectionPoint.Build();
                return this;
            }

            public Builder WithPulseData(PXCMFaceData.PulseData pulseData)
            {
                if (pulseData != null)
                {
                    faceLandmarksData.heartRate = pulseData.QueryHeartRate();
                }
                return this;
            }


            public FaceLandmarksData Build()
            {
                return faceLandmarksData;
            }
        }
    }
}