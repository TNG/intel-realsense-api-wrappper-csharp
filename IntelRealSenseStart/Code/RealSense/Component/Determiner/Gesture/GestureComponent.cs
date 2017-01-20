using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Gestures;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Factory;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner.Gesture
{
    public class GestureComponent
    {
        private readonly string gestureName;

        public GestureComponent(GestureTypes.GestureTypesEnum gestureName)
        {
            CheckGestureName(gestureName);
            this.gestureName = GestureTypes.GestureTypesDictionary[gestureName];
        }

        private void CheckGestureName(GestureTypes.GestureTypesEnum gestureName)
        {
            if (GestureTypes.GestureTypesDictionary.ContainsKey(gestureName))
            {
                return;
            }
            throw new System.Exception($"Gesture {gestureName} not known.");
        }

        public void Configure(PXCMHandConfiguration handConfiguration)
        {
            handConfiguration.EnableGesture(gestureName);
        }

        public void Process(PXCMHandData handData, HandsDeterminerData.Builder handsDeterminerData, RealSenseFactory factory)
        {
            handsDeterminerData.WithGestures(GetIndividualGestureSamples(handData, factory));
        }

        private IEnumerable<GestureDeterminerData> GetIndividualGestureSamples(PXCMHandData handData, RealSenseFactory factory)
        {
            return 0.To(handData.QueryFiredGesturesNumber()).ToArray().Select(index =>
            {
                PXCMHandData.GestureData gestureData;
                handData.QueryFiredGestureData(index, out gestureData);
                if (gestureData != null && gestureData.name == gestureName)
                {
                    GestureDeterminerData.Builder gestureDeterminerData = factory.Data.Determiner.Gesture();
                    gestureDeterminerData.WithGesture(gestureData);
                    return gestureDeterminerData.Build();
                }
                return null;
            }).Where(gestureData => gestureData!=null);
        } 

        public class Builder
        {
            private GestureTypes.GestureTypesEnum gestureName;

            public Builder WithGestureName(GestureTypes.GestureTypesEnum gestureName)
            {
                this.gestureName = gestureName;
                return this;
            } 

            public GestureComponent Build()
            { 
                return new GestureComponent(gestureName);
            }
        }

        public static Builder Create()
        {
            return new Builder();
        }
    }
}