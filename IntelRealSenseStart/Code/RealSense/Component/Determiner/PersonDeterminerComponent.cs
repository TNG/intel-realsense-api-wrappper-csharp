using System.Linq.Expressions;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Person;
using IntelRealSenseStart.Code.RealSense.Config.RealSense;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Factory;
using IntelRealSenseStart.Code.RealSense.Helper;
using IntelRealSenseStart.Code.RealSense.Provider;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner
{
    public class PersonDeterminerComponent : FrameDeterminerComponent
    {
        private SkeletonComponent skeletonComponent;
        private TrackingComponent trackingComponent;

        private readonly RealSenseConfiguration configuration;
        private readonly RealSenseFactory factory;
        private readonly NativeSense nativeSense;

        public PXCMPersonTrackingData trackingData;
        private PXCMPersonTrackingModule personModule;

        private PersonDeterminerComponent(RealSenseConfiguration configuration, RealSenseFactory factory, NativeSense nativeSense)
        {
            this.factory = factory;
            this.configuration = configuration;
            this.nativeSense = nativeSense;
            skeletonComponent = SkeletonComponent.Create().Build();
            trackingComponent = TrackingComponent.Create().Build();
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
                skeletonComponent = SkeletonComponent.Create().WithConfiguration(personTrackingConfiguration).WithFactory(factory).Build();  // TODO: COMPONENTS IN BUILDER BAUEN -> SIEHE FACE COMPONENT (GESTURE AUCH)
                trackingComponent = TrackingComponent.Create().WithPersonModule(personModule).WithConfiguration(personTrackingConfiguration).Build();
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
            private RealSenseFactory factory;
            private NativeSense nativeSense;
            private RealSenseConfiguration configuration;

            public Builder WithFactory(RealSenseFactory factory)
            {
                this.factory = factory;
                return this;
            }

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

            public PersonDeterminerComponent Build()
            {
                factory.Check(Preconditions.IsNotNull,
                    "The factory must be set in order to create the face determiner component");
                nativeSense.Check(Preconditions.IsNotNull,
                    "The RealSense manager must be set in order to create the face determiner component");
                configuration.Check(Preconditions.IsNotNull,
                    "The RealSense configuration must be set in order to create the face determiner component");

                return new PersonDeterminerComponent(configuration, factory, nativeSense);
            }
        }
    }
}