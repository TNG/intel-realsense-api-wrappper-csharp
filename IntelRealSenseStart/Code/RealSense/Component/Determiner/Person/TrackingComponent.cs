using System.Collections.Generic;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner.Person
{
    public class TrackingComponent
    {
        private readonly PXCMPersonTrackingConfiguration personTrackingConfiguration;
        private readonly List<int> trackedPersons; 

        private TrackingComponent(PXCMPersonTrackingConfiguration personTrackingConfiguration)
        {
            this.personTrackingConfiguration = personTrackingConfiguration;
            trackedPersons = new List<int>();
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
            var idsToTrack = currentIds as IList<int> ?? currentIds.ToList();
            foreach (var currentId in idsToTrack)
            {
                StartTrackingOfId(currentId, trackingData);
            }
            StopTrackingOfNotRecognizedIds(idsToTrack, trackingData);
        } 

        private void StartTrackingOfId(int currentId, PXCMPersonTrackingData trackingData)
        {
            if (trackedPersons.Contains(currentId))
            {
                return;
            }
            trackedPersons.Add(currentId);
            trackingData.StartTracking(currentId);
        }

        private void StopTrackingOfNotRecognizedIds(IEnumerable<int> currentIds, PXCMPersonTrackingData trackingData)
        {
            var idsToRemove = new List<int>();
            foreach (var trackedPerson in trackedPersons.Where(trackedPerson => !currentIds.Contains(trackedPerson)))
            {   
                idsToRemove.Add(trackedPerson);
                trackingData.StopTracking(trackedPerson);
            }
            RemoveIds(idsToRemove);
        }

        private void RemoveIds(IEnumerable<int> idsToRemove)
        {
            foreach (var id in idsToRemove)
            {
                trackedPersons.Remove(id);
            }
        }

        public static Builder Create()
        {
            return new Builder();
        }

        public class Builder
        {
            private PXCMPersonTrackingConfiguration personTrackingConfiguration;

            public TrackingComponent Build()
            {
                return new TrackingComponent(personTrackingConfiguration);
            }

            public Builder WithPersonModule(PXCMPersonTrackingModule personModule)
            {
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