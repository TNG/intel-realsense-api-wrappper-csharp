using System;
using System.Drawing.Text;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    [Serializable]
    public class GestureData
    {
        private readonly int handId;
        private readonly string gestureName;

        public GestureData(int handId, string gestureName)
        {
            this.handId = handId;
            this.gestureName = gestureName;
        }

        public int HandId
        {
            get { return handId; }
        }

        public string GestureName
        {
            get { return gestureName; }
        }

        public class Builder
        {
            private int handId;
            private string gestureName;


            public Builder WithGestureData(PXCMHandData.GestureData gestureData)
            {
                if (gestureData != null)
                {
                    handId = gestureData.handId;
                    gestureName = gestureData.name;
                }
                return this;
            }

            public GestureData Build()
            {
                return new GestureData(handId, gestureName);
            }
        }
    }
}