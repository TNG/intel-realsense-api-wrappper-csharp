namespace IntelRealSenseStart.Code.RealSense.Config.RealSense
{
    public class PersonTrackingConfiguration
    {
        private bool skeletonTrackingEnabled;
        private int maxTrackedPersons;
        private PXCMPersonTrackingConfiguration.SkeletonJointsConfiguration.SkeletonMode skeletonMode;

        private PersonTrackingConfiguration()
        {
            skeletonTrackingEnabled = false;
        }

        public bool SkeletonTrackingEnabled
        {
            get { return skeletonTrackingEnabled; }
        }

        public int MaxTrackedPersons
        {
            get { return maxTrackedPersons; }
        }

        public PXCMPersonTrackingConfiguration.SkeletonJointsConfiguration.SkeletonMode SkeletonMode
        {
            get { return skeletonMode; }
        }

        public class Builder
        {
            private readonly PersonTrackingConfiguration personTrackingConfiguration;

            public Builder()
            {
                personTrackingConfiguration = new PersonTrackingConfiguration();
            }

            public Builder WithSkeletonTrackingEnabled()
            {
                personTrackingConfiguration.skeletonTrackingEnabled = true;
                return this;
            }

            public Builder WithMaxTrackedPersons(int maxTrackedPersons)
            {
                personTrackingConfiguration.maxTrackedPersons = maxTrackedPersons;
                return this;
            }

            public Builder WithSkeletonMode(
                PXCMPersonTrackingConfiguration.SkeletonJointsConfiguration.SkeletonMode skeletonMode)
            {
                personTrackingConfiguration.skeletonMode = skeletonMode;
                return this;
            }

            public PersonTrackingConfiguration Build()
            {
                return personTrackingConfiguration;
            }
        }
    }
}