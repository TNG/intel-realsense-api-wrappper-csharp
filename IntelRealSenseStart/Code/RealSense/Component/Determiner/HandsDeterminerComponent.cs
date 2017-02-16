using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Gesture;
using IntelRealSenseStart.Code.RealSense.Config.RealSense;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Factory.Data;
using IntelRealSenseStart.Code.RealSense.Helper;
using IntelRealSenseStart.Code.RealSense.Provider;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner
{
    public class HandsDeterminerComponent : FrameDeterminerComponent
    {
        private readonly List<GestureComponent> gestureComponents;
        private readonly RealSenseConfiguration configuration;
        private readonly DeterminerDataFactory factory;
        private readonly NativeSense nativeSense;

        private PXCMHandData handData;

        private HandsDeterminerComponent(DeterminerDataFactory factory, NativeSense nativeSense, RealSenseConfiguration configuration, List<GestureComponent> gestureComponents)
        {
            this.gestureComponents = gestureComponents;
            this.factory = factory;
            this.nativeSense = nativeSense;
            this.configuration = configuration;
        }

        public bool ShouldBeStarted
        {
            get { return configuration.HandsDetectionEnabled; }
        }

        public void EnableFeatures()
        {
            if (configuration.HandsDetectionEnabled)
            {
                nativeSense.SenseManager.EnableHand();
            }
        }
        
        public void Configure()
        {
            PXCMHandModule handModule = nativeSense.SenseManager.QueryHand();
            handData = handModule.CreateOutput();
            PXCMHandConfiguration handConfiguration = handModule.CreateActiveConfiguration();

            ConfigureHandOptions(handConfiguration);

            handConfiguration.ApplyChanges();
            handConfiguration.Update();
        }

        private void ConfigureHandOptions(PXCMHandConfiguration handConfiguration)
        {
            if (configuration.HandsDetection.SegmentationImageEnabled)
            {
                handConfiguration.EnableSegmentationImage(true);
            }
            ConfigureComponents(handConfiguration);
        } 

        private void ConfigureComponents(PXCMHandConfiguration handConfiguration)
        {
            gestureComponents.Do(gestureComponent => gestureComponent.Configure(handConfiguration));
        }

        public void Stop()
        {
            // Nothing to do
        }

        public void Process(DeterminerData.Builder determinerData)
        {
            PXCMCapture.Sample realSenseSample = nativeSense.SenseManager.QuerySample();
            PXCMCapture.Sample handSample = nativeSense.SenseManager.QueryHandSample();

            if (realSenseSample != null && handSample != null)
            {
                handData.Update();
                determinerData.WithHandsData(GetHandsData());
            }
        }

        private HandsDeterminerData.Builder GetHandsData()
        {   
            var handsDeterminerData = factory.Hands();
            handsDeterminerData.WithHands(GetIndividualHandSamples().Select(GetIndividualHandData));
            ProcessComponents(handsDeterminerData);
            return handsDeterminerData;
        }

        private void ProcessComponents(HandsDeterminerData.Builder handsDeterminerData)
        {
            gestureComponents.Do(gestureComponent => gestureComponent.Process(handData, handsDeterminerData, factory));
        }

        private IEnumerable<PXCMHandData.IHand> GetIndividualHandSamples()
        {
            return 0.To(handData.QueryNumberOfHands()).ToArray().Select(index =>
            {
                PXCMHandData.IHand oneHandData;
                handData.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_BY_TIME, index, out oneHandData);
                return oneHandData;
            }).Where(oneHandData => oneHandData != null);
        }

        private HandDeterminerData.Builder GetIndividualHandData(PXCMHandData.IHand individualHandSample)
        {
            return factory.Hand()
                .WithBodySide(GetUserId(individualHandSample))
                .WithJoints(GetJointData(individualHandSample))
                .WithSegmentationImage(GetSegmentationImage(individualHandSample));
        }

        private PXCMHandData.BodySideType GetUserId(PXCMHandData.IHand individualHandSample)
        {
            return individualHandSample.QueryBodySide();
        }

        private Dictionary<PXCMHandData.JointType, PXCMHandData.JointData> GetJointData(
            PXCMHandData.IHand individualHandSample)
        {
            return 0.To(21).ToArray().ToDictionary(
                index => (PXCMHandData.JointType) index,
                index =>
                {
                    PXCMHandData.JointData jointData;
                    individualHandSample.QueryTrackedJoint((PXCMHandData.JointType) index, out jointData);
                    return jointData;
                });
        }

        private PXCMImage GetSegmentationImage(PXCMHandData.IHand individualHandSample)
        {
            if (configuration.HandsDetection.SegmentationImageEnabled)
            {
                PXCMImage segmentationImage;
                individualHandSample.QuerySegmentationImage(out segmentationImage);
                return segmentationImage;
            }

            return null;
        }

        public class Builder
        {
            private DeterminerDataFactory factory;
            private NativeSense nativeSense;
            private RealSenseConfiguration configuration;
            private readonly List<GestureComponent> gestureComponents;

            public Builder()
            {
                gestureComponents = new List<GestureComponent>();
            }

            public Builder WithFactory(DeterminerDataFactory factory)
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

            public Builder WithGestureComponent(GestureComponent gestureComponent)
            {
                gestureComponents.Add(gestureComponent);
                return this;
            }

            public Builder WithGestureComponents(List<GestureComponent> gestureComponents)
            {
                this.gestureComponents.AddRange(gestureComponents); 
                return this;
            }

            public HandsDeterminerComponent Build()
            {
                factory.Check(Preconditions.IsNotNull,
                    "The factory must be set in order to create the hands determiner component");
                nativeSense.Check(Preconditions.IsNotNull,
                    "The RealSense manager must be set in order to create the hands determiner component");
                configuration.Check(Preconditions.IsNotNull,
                    "The RealSense configuration must be set in order to create the hands determiner component");

                return new HandsDeterminerComponent(factory, nativeSense, configuration, gestureComponents);
            }
        }
    }
}