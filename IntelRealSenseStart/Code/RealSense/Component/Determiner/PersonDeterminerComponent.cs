using IntelRealSenseStart.Code.RealSense.Component.Determiner.Person;
using IntelRealSenseStart.Code.RealSense.Config.RealSense;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Helper;
using IntelRealSenseStart.Code.RealSense.Provider;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner
{
    public class PersonDeterminerComponent : FrameDeterminerComponent
    {
        private readonly SkeletonComponent skeletonComponent;
        private readonly TrackingComponent trackingComponent;

        private readonly RealSenseConfiguration configuration;
        private readonly NativeSense nativeSense;

        private PXCMPersonTrackingData trackingData;
        private PXCMPersonTrackingModule personModule;

        private PersonDeterminerComponent(RealSenseConfiguration configuration, NativeSense nativeSense, SkeletonComponent skeletonComponent, TrackingComponent trackingComponent)
        {
            this.configuration = configuration;
            this.nativeSense = nativeSense;
            this.skeletonComponent = skeletonComponent;
            this.trackingComponent = trackingComponent;
        }

        public bool ShouldBeStarted
        {
            get { return configuration.PersonTrackingEnabled; }
        }

        public void EnableFeatures()
        {
            if (configuration.PersonTrackingEnabled)
            {
                nativeSense.SenseManager.EnablePersonTracking();
                personModule = nativeSense.SenseManager.QueryPersonTracking();
                PXCMPersonTrackingConfiguration personTrackingConfiguration = personModule.QueryConfiguration();
                skeletonComponent.PersonTrackingConfiguration = personTrackingConfiguration;
                trackingComponent.PersonTrackingConfiguration = personTrackingConfiguration;
            }
        }

        public void Configure()
        {
            skeletonComponent.Configure(configuration.PersonTracking.MaxTrackedPersons, configuration.PersonTracking.SkeletonMode);
            trackingComponent.Configure();
            trackingData = personModule.QueryOutput();
        }

        public void Stop()
        {
            // Nothing to do
        }

        public void Process(DeterminerData.Builder determinerData)
        {
            trackingComponent.Process(trackingData);
            skeletonComponent.Process(determinerData, trackingData);
        }

        public class Builder
        {
            private NativeSense nativeSense;
            private RealSenseConfiguration configuration;
            private SkeletonComponent skeletonComponent;
            private TrackingComponent trackingComponent;

            public Builder WithNativeSense(NativeSense nativeSense)
            {
                this.nativeSense = nativeSense;
                return this;
            }

            public Builder WithConfiguration(RealSenseConfiguration configuration)
            {
                this.configuration = configuration;
                return this;
            }

            public Builder WithSkeletonComponent(SkeletonComponent skeletonComponent)
            {
                this.skeletonComponent = skeletonComponent;
                return this;
            }

            public Builder WithTrackingComponent(TrackingComponent trackingComponent)
            {
                this.trackingComponent = trackingComponent;
                return this;
            }

            public PersonDeterminerComponent Build()
            {
                nativeSense.Check(Preconditions.IsNotNull,
                    "The RealSense manager must be set in order to create the person determiner component");
                configuration.Check(Preconditions.IsNotNull,
                    "The RealSense configuration must be set in order to create the person determiner component");
                skeletonComponent.Check(Preconditions.IsNotNull, "The skeleton component must be set in order to create the person determiner component");
                trackingComponent.Check(Preconditions.IsNotNull, "The tracking component must be set in order to create the person determiner component");

                return new PersonDeterminerComponent(configuration, nativeSense, skeletonComponent, trackingComponent);
            }
        }
    }
}