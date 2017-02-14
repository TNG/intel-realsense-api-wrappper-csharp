using IntelRealSenseStart.Code.RealSense.Component.Determiner;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Builder;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Face;
using IntelRealSenseStart.Code.RealSense.Component.Determiner.Person;

namespace IntelRealSenseStart.Code.RealSense.Factory.Component
{
    public class DeterminerComponentsFactory
    {
        public ImageDeterminerComponent.Builder Image()
        {
            return new ImageDeterminerComponent.Builder();
        }

        public HandsDeterminerComponent.Builder Hands()
        {
            return new HandsDeterminerComponent.Builder();
        }

        public FaceDeterminerComponent.Builder Face()
        {
            return new FaceDeterminerComponent.Builder();
        }

        public PersonDeterminerComponent.Builder Skeletons()
        {
            return new PersonDeterminerComponent.Builder();
        }

        public SkeletonComponent.Builder SkeletonComponent()
        {
            return new SkeletonComponent.Builder();
        }

        public TrackingComponent.Builder TrackingComponent()
        {
            return new TrackingComponent.Builder();
        }

        public FaceLandmarksDeterminerComponent.Builder FaceLandmarks()
        {
            return new FaceLandmarksDeterminerComponent.Builder();
        }

        public FaceRecognitionDeterminerComponent.Builder FaceRecognition()
        {
            return new FaceRecognitionDeterminerComponent.Builder();
        }

        public PulseDeterminerComponent.Builder Pulse()
        {
            return new PulseDeterminerComponent.Builder();
        } 

        public VideoDeviceDeterminerComponent.Builder VideoDevice()
        {
            return new VideoDeviceDeterminerComponent.Builder();
        }

        public SpeechRecognitionDeterminerComponent.Builder SpeechRecognition()
        {
            return new SpeechRecognitionDeterminerComponent.Builder(new GrammarBuilder());
        }
    }
}