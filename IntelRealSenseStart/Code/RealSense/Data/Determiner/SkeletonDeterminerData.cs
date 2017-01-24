namespace IntelRealSenseStart.Code.RealSense.Data.Determiner
{
    public class SkeletonDeterminerData
    {
        private PXCMPersonTrackingData.PersonJoints.SkeletonPoint[] skeletonPoints;
        private int personId;

        public PXCMPersonTrackingData.PersonJoints.SkeletonPoint[] SkeletonPoints
        {
            get { return skeletonPoints; }
        }

        public int PersonId
        {
            get { return personId; }
        }

        public class Builder
        {
            private readonly SkeletonDeterminerData skeletonDeterminerData;

            public Builder()
            {
                skeletonDeterminerData = new SkeletonDeterminerData();
            }

            public SkeletonDeterminerData Build()
            {
                return skeletonDeterminerData;
            }

            public Builder WithPersonId(int personId)
            {
                skeletonDeterminerData.personId = personId;
                return this;
            }

            public Builder WithSkeletonPoints(PXCMPersonTrackingData.PersonJoints.SkeletonPoint[] skeletonPoints)
            {
                skeletonDeterminerData.skeletonPoints = skeletonPoints;
                return this;
            }
        }
    }
}