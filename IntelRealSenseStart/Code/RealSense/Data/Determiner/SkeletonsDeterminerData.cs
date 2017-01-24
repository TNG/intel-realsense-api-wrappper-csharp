using System.Collections.Generic;
using System.Linq;

namespace IntelRealSenseStart.Code.RealSense.Data.Determiner
{
    public class SkeletonsDeterminerData
    {
        private readonly List<SkeletonDeterminerData> skeletons;

        public SkeletonsDeterminerData()
        {
            skeletons = new List<SkeletonDeterminerData>();
        }

        public List<SkeletonDeterminerData> Skeletons
        {
            get { return skeletons; }
        } 

        public class Builder
        {
            private readonly SkeletonsDeterminerData skeletonsDeterminerData;

            public Builder()
            {
                skeletonsDeterminerData = new SkeletonsDeterminerData();
            }

            public Builder WithSkeleton(SkeletonDeterminerData.Builder skeletonData)
            {
                skeletonsDeterminerData.skeletons.Add(skeletonData.Build());
                return this;
            }

            public Builder WithSkeletons(IEnumerable<SkeletonDeterminerData.Builder> skeletonsData)
            {
                skeletonsDeterminerData.skeletons.AddRange(skeletonsData.Select(skeletonBuilder => skeletonBuilder.Build()));
                return this;
            }

            public SkeletonsDeterminerData Build()
            {
                return skeletonsDeterminerData;
            }
        } 
    }
}