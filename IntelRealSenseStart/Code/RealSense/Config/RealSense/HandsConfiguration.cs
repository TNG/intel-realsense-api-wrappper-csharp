using System.Collections.Generic;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Gestures;

namespace IntelRealSenseStart.Code.RealSense.Config.RealSense
{
    public class HandsConfiguration
    {
        public static readonly HandsConfiguration DEFAULT_CONFIGURATION = new HandsConfiguration();

        private bool segmentationImageEnabled;
        private readonly List<GestureTypes.GestureTypesEnum> gestureNames; 

        private HandsConfiguration()
        {
            segmentationImageEnabled = false;
            gestureNames = new List<GestureTypes.GestureTypesEnum>();
        }

        public bool SegmentationImageEnabled
        {
            get { return segmentationImageEnabled; }
        }

        public List<GestureTypes.GestureTypesEnum> GestureNames
        {
            get { return gestureNames;}
        } 

        public class Builder
        {
            private readonly HandsConfiguration configuration;

            public Builder()
            {
                configuration = new HandsConfiguration();
            }

            public Builder WithSegmentationImage()
            {
                configuration.segmentationImageEnabled = true;
                return this;
            }

            public Builder UsingGestures(params GestureTypes.GestureTypesEnum[] gestureNames)
            {
                foreach (GestureTypes.GestureTypesEnum gestureName in gestureNames)
                {
                    configuration.gestureNames.Add(gestureName);
                }
                return this;
            }

            public HandsConfiguration Build()
            {
                return configuration;
            }
        }
    }
}