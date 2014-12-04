﻿using IntelRealSenseStart.Code.RealSense.Component.Determiner;

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

        public DeviceDeterminerComponent.Builder Device()
        {
            return new DeviceDeterminerComponent.Builder();
        }
    }
}