using System.Collections.Generic;
using IntelRealSenseStart.Code.RealSense.Data.Properties;
using IntelRealSenseStart.Code.RealSense.Factory.Data;
using IntelRealSenseStart.Code.RealSense.Helper;

namespace IntelRealSenseStart.Code.RealSense.Component.Property
{
    public class AudioPropertiesDeterminer : PropertiesComponent<RealSenseProperties.Builder>
    {
        private PropertiesDataFactory factory;
        private readonly List<PropertiesComponent<AudioProperties.Builder>> videoPropertiesComponents;

        private AudioPropertiesDeterminer()
        {
            videoPropertiesComponents = new List<PropertiesComponent<AudioProperties.Builder>>();
        }

        public void UpdateProperties(RealSenseProperties.Builder realSenseProperties)
        {
            var audioProperties = factory.Audio();
            videoPropertiesComponents.Do(audioPropertiesComponent => audioPropertiesComponent.UpdateProperties(audioProperties));
            realSenseProperties.WithAudioProperties(audioProperties);
        }

        public class Builder
        {
            private readonly AudioPropertiesDeterminer audioPropertiesDeterminer;

            public Builder()
            {
                audioPropertiesDeterminer = new AudioPropertiesDeterminer();
            }

            public Builder WithFactory(PropertiesDataFactory factory)
            {
                audioPropertiesDeterminer.factory = factory;
                return this;
            }

            public Builder WithAudioPropertiesComponent(PropertiesComponent<AudioProperties.Builder> audioPropertiesComponent)
            {
                audioPropertiesDeterminer.videoPropertiesComponents.Add(audioPropertiesComponent);
                return this;
            }

            public AudioPropertiesDeterminer Build()
            {
                audioPropertiesDeterminer.factory.Check(Preconditions.IsNotNull,
                    "The factory must be set in order to create the audio properties determiner");

                return audioPropertiesDeterminer;
            }
        }
    }
}