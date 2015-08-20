using System;

namespace IntelRealSenseStart.Code.RealSense.Event.Data
{
    [Serializable]
    public class SpeechRecognitionMatch
    {
        private readonly string sentence;
        private readonly int confidence;

        public SpeechRecognitionMatch(string sentence, int confidence)
        {
            this.sentence = sentence;
            this.confidence = confidence;
        }

        public string Sentence
        {
            get { return sentence; }
        }

        public int Confidence
        {
            get { return confidence; }
        }
    }
}