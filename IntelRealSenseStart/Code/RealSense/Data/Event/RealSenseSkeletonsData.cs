using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    public class RealSenseSkeletonsData
    {
        private readonly List<RealSenseSkeletonData> skeletons;

        private RealSenseSkeletonsData()
        {
            skeletons = new List<RealSenseSkeletonData>();
        }

        public List<RealSenseSkeletonData> Skeletons
        {
            get { return skeletons; }
        }

        public class Builder
        {
            private readonly RealSenseSkeletonsData realSenseSkeletonsData;

            public Builder()
            {
                realSenseSkeletonsData = new RealSenseSkeletonsData();
            }

            public Builder WithSkeletonData(RealSenseSkeletonData.Builder skeletonData)
            {
                realSenseSkeletonsData.skeletons.Add(skeletonData.Build());
                return this;
            }

            public RealSenseSkeletonsData Build()
            {
                return realSenseSkeletonsData;
            }
        }
    }
}