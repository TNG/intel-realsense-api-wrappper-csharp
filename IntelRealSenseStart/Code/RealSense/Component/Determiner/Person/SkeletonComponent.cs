using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Data.Determiner;
using IntelRealSenseStart.Code.RealSense.Factory.Data;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner.Person
{
    public class SkeletonComponent
    {
        public PXCMPersonTrackingConfiguration PersonTrackingConfiguration { get; set; }
        private readonly DeterminerDataFactory factory;

        private SkeletonComponent(DeterminerDataFactory factory)
        {
            this.factory = factory;
        }

        public void Configure(int maxTrackedPersons, PXCMPersonTrackingConfiguration.SkeletonJointsConfiguration.SkeletonMode skeletonMode)
        {
            PersonTrackingConfiguration.Check(Preconditions.IsNotNull, "The person tracking Configuration has to be set");
            PXCMPersonTrackingConfiguration.SkeletonJointsConfiguration skeletonJointsConfiguration =
                PersonTrackingConfiguration.QuerySkeletonJoints();
            skeletonJointsConfiguration.Enable();
            skeletonJointsConfiguration.SetMaxTrackedPersons(maxTrackedPersons);
            skeletonJointsConfiguration.SetTrackingArea(skeletonMode);
        }

        public void Process(DeterminerData.Builder determinerData, PXCMPersonTrackingData trackingData)
        {
            determinerData.WithSkeletonsData(GetSkeletonsData(trackingData));
        }

        private SkeletonsDeterminerData.Builder GetSkeletonsData(PXCMPersonTrackingData trackingData)
        {
            return factory.Skeletons().WithSkeletons(
                GetIndividualSkeletons(trackingData).Select(GetIndividualSkeletonData));
        }

        private IEnumerable<PXCMPersonTrackingData.Person> GetIndividualSkeletons(PXCMPersonTrackingData trackingData)
        {
            return 0.To(trackingData.QueryNumberOfPeople() - 1).ToArray()
                .Select(
                    index =>
                        trackingData.QueryPersonData(PXCMPersonTrackingData.AccessOrderType.ACCESS_ORDER_BY_ID, index))
                .Where(skeleton => skeleton != null);
        }

        private SkeletonDeterminerData.Builder GetIndividualSkeletonData(PXCMPersonTrackingData.Person skeleton)
        {
            SkeletonDeterminerData.Builder skeletonDeterminerData = factory.Skeleton();
            skeletonDeterminerData.WithSkeletonPoints(GetSkeletonPoints(skeleton));
            skeletonDeterminerData.WithPersonId(GetPersonId(skeleton));
            return skeletonDeterminerData;
        }

        private PXCMPersonTrackingData.PersonJoints.SkeletonPoint[] GetSkeletonPoints(PXCMPersonTrackingData.Person skeleton)
        {
            PXCMPersonTrackingData.PersonJoints personJointData = skeleton.QuerySkeletonJoints();
            PXCMPersonTrackingData.PersonJoints.SkeletonPoint[] joints = new PXCMPersonTrackingData.PersonJoints.SkeletonPoint[personJointData.QueryNumJoints()];
            personJointData.QueryJoints(joints);
            return joints;
        }

        private int GetPersonId(PXCMPersonTrackingData.Person skeleton)
        {
            int personId = skeleton.QueryTracking().QueryId();
            return personId;
        }

        public class Builder
        {
            private DeterminerDataFactory factory;

            public SkeletonComponent Build()
            {
                return new SkeletonComponent(factory);
            }

            public Builder WithFactory(DeterminerDataFactory factory)
            {
                this.factory = factory;
                return this;
            }
        }
    }
}