using System.Collections.Generic;

namespace IntelRealSenseStart.Code.RealSense.Component.Determiner.Gestures
{
    public class GestureTypes
    {
        public enum GestureTypesEnum
        {
            Click,
            Fist,
            FullPinch,
            Spreadfingers,
            SwipeDown,
            SwipeLeft,
            SwipeRight,
            SwipeUp,
            Tap,
            ThumbDown,
            ThumbUp,
            TwoFingersPinchOpen,
            VSign,
            Wave
        }

        public static Dictionary<GestureTypesEnum, string> GestureTypesDictionary { get; } = new Dictionary
            <GestureTypesEnum, string>()
        {
            {GestureTypesEnum.Click, "click" },
            {GestureTypesEnum.Fist, "fist" },
            {GestureTypesEnum.FullPinch, "full_pinch" },
            {GestureTypesEnum.Spreadfingers, "spreadfingers" },
            {GestureTypesEnum.SwipeDown, "swipe_down" },
            {GestureTypesEnum.SwipeLeft, "swipe_left" },
            {GestureTypesEnum.SwipeRight, "swipe_right" },
            {GestureTypesEnum.SwipeUp, "swipe_up" },
            {GestureTypesEnum.Tap, "tap" },
            {GestureTypesEnum.ThumbDown, "thumb_down" },
            {GestureTypesEnum.ThumbUp, "thumb_up" },
            {GestureTypesEnum.TwoFingersPinchOpen, "two_fingers_pinch_open" },
            {GestureTypesEnum.VSign, "v_sign" },
            {GestureTypesEnum.Wave, "wave" }
        };
    }
}