namespace IntelRealSenseStart.Code.RealSense.Data.Determiner
{
    public class GestureDeterminerData
    {
        private PXCMHandData.GestureData gesture;

        private GestureDeterminerData()
        {
        }

        public PXCMHandData.GestureData Gesture
        {
            get { return gesture; }
        }

        public bool HasGesture 
        {
            get { return gesture != null; }
        }
        public class Builder
        {
            private readonly GestureDeterminerData frameEvent;

            public Builder()
            {
                frameEvent = new GestureDeterminerData();
            }

            public Builder WithGesture(PXCMHandData.GestureData gesture)
            {
                frameEvent.gesture = gesture;
                return this;
            }

            public GestureDeterminerData Build()
            {
                return frameEvent;
            }
        }
         
    }
}