using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    public class SkeletonsData
    {
        private readonly List<SkeletonData> skeletons;

        private SkeletonsData()
        {
            skeletons = new List<SkeletonData>();
        }

        public List<SkeletonData> Skeletons
        {
            get { return skeletons; }
        }

        public class Builder
        {
            private readonly SkeletonsData skeletonsData;

            public Builder()
            {
                skeletonsData = new SkeletonsData();
            }

            public Builder WithSkeletonData(SkeletonData.Builder skeletonData)
            {
                skeletonsData.skeletons.Add(skeletonData.Build());
                return this;
            }

            public SkeletonsData Build()
            {
                return skeletonsData;
            }
        }
    }
}