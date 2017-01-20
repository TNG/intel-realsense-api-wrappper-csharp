using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    public class HandsData
    {
        private readonly List<HandData> hands;
        private readonly List<GestureData> gestures; 

        private HandsData()
        {
            hands = new List<HandData>();
            gestures = new List<GestureData>();
        }

        public List<HandData> Hands
        {
            get { return hands; }
        }

        public List<GestureData> Gestures
        {
            get { return gestures; }
        } 

        public class Builder
        {
            private readonly HandsData handsData;

            public Builder()
            {
                handsData = new HandsData();
            }

            public Builder WithFaceLandmarks(HandData.Builder detectionsPoints)
            {
                handsData.hands.Add(detectionsPoints.Build());
                return this;
            }

            public Builder WithGestureData(GestureData.Builder gestureData)
            {
                handsData.gestures.Add(gestureData.Build());
                return this;
            }

            public HandsData Build()
            {
                return handsData;
            }
        }
    }
}