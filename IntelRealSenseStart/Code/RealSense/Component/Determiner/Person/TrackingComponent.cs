using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner.Person
{
    public class TrackingComponent
    {
        private readonly PXCMPersonTrackingModule personModule;
        private readonly PXCMPersonTrackingConfiguration personTrackingConfiguration;
        private List<int> TrackedPersons; 

        private TrackingComponent(PXCMPersonTrackingModule personModule, PXCMPersonTrackingConfiguration personTrackingConfiguration)
        {
            this.personModule = personModule;
            this.personTrackingConfiguration = personTrackingConfiguration;
            TrackedPersons = new List<int>();
        }

        public void Configure()
        {
            PXCMPersonTrackingConfiguration.TrackingConfiguration trackingConfiguration =
                personTrackingConfiguration.QueryTracking();
            trackingConfiguration.Enable();
        }

        public void Process(PXCMPersonTrackingData trackingData)
        {
            StartAndStopTracking(GetCurrentIds(trackingData), trackingData);
        }

        private IEnumerable<int> GetCurrentIds(PXCMPersonTrackingData trackingData)
        {
            return 0.To(trackingData.QueryNumberOfPeople() - 1).ToArray()
                .Select(
                    index =>
                        trackingData.QueryPersonData(PXCMPersonTrackingData.AccessOrderType.ACCESS_ORDER_BY_ID, index)
                            .QueryTracking()
                            .QueryId());
        }

        private void StartAndStopTracking(IEnumerable<int> currentIds, PXCMPersonTrackingData trackingData)
        {
            foreach (int currentId in currentIds)
            {
                StartTrackingOfId(currentId, trackingData);
            }
            StopTrackingOfNotRecognizedIds(currentIds, trackingData);
        } 

        private void StartTrackingOfId(int currentId, PXCMPersonTrackingData trackingData)
        {
            if (!TrackedPersons.Contains(currentId))
            {
                TrackedPersons.Add(currentId);
                trackingData.StartTracking(currentId);
            }
        }

        private void StopTrackingOfNotRecognizedIds(IEnumerable<int> currentIds, PXCMPersonTrackingData trackingData)
        {
            List<int> IdsToRemove = new List<int>();
            foreach (var trackedPerson in TrackedPersons.Where(trackedPerson => !currentIds.Contains(trackedPerson)))
            {   
                IdsToRemove.Add(trackedPerson);
                trackingData.StopTracking(trackedPerson);
            }
            RemoveIds(IdsToRemove);
        }

        private void RemoveIds(List<int> idsToRemove)
        {
            foreach (int id in idsToRemove)
            {
                TrackedPersons.Remove(id);
            }
        }

        public static Builder Create()
        {
            return new Builder();
        }

        public class Builder
        {
            private PXCMPersonTrackingModule personModule;
            private PXCMPersonTrackingConfiguration personTrackingConfiguration;

            public TrackingComponent Build()
            {
                return new TrackingComponent(personModule, personTrackingConfiguration);
            }

            public Builder WithPersonModule(PXCMPersonTrackingModule personModule)
            {
                this.personModule = personModule;
                return this;
            }

            public Builder WithConfiguration(PXCMPersonTrackingConfiguration personTrackingConfiguration)
            {
                this.personTrackingConfiguration = personTrackingConfiguration;
                return this;
            }
        }
    }
}