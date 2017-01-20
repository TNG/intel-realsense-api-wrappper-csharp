using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Data.Determiner
{
    public class HandsDeterminerData
    {
        private readonly List<HandDeterminerData> hands;
        private readonly List<GestureDeterminerData> gestures; 

        public HandsDeterminerData()
        {
            hands = new List<HandDeterminerData>();
            gestures = new List<GestureDeterminerData>();
        }

        public List<HandDeterminerData> Hands
        {
            get { return hands; }
        }

        public List<GestureDeterminerData> Gestures
        {
            get { return gestures; }
        } 

        public class Builder
        {
            private readonly HandsDeterminerData handsDeterminerData;

            public Builder()
            {
                handsDeterminerData = new HandsDeterminerData();
            }

            public Builder WithHand(HandDeterminerData.Builder handData)
            {
                handsDeterminerData.Hands.Add(handData.Build());
                return this;
            }

            public Builder WithHands(IEnumerable<HandDeterminerData.Builder> handData)
            {
                handsDeterminerData.Hands.AddRange(handData.Select(handDataBuilder => handDataBuilder.Build()));
                return this;
            }

            public void WithGesture(GestureDeterminerData gestureData)
            {
                handsDeterminerData.Gestures.Add(gestureData);
            }

            public HandsDeterminerData Build()
            {
                return handsDeterminerData;
            }

            public void WithGestures(IEnumerable<GestureDeterminerData> gestureDatas)
            {
                gestureDatas.Do(WithGesture);
            }
        }
    }
}