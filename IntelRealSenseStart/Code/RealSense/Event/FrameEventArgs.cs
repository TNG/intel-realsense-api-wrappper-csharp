﻿using IntelRealSenseStart.Code.RealSense.Component.Creator;
using IntelRealSenseStart.Code.RealSense.Config.RealSense;
using IntelRealSenseStart.Code.RealSense.Data;
using IntelRealSenseStart.Code.RealSense.Factory;

namespace IntelRealSenseStart.Code.RealSense.Event
{
    public class FrameEventArgs
    {
        private HandsImageBuilder handsImageBuilder;

        private PXCMCapture.Device device;

        private HandsData handsData;
        private ImageData imageData;
        
        public HandsImageBuilder CreateImage()
        {
            return handsImageBuilder;
        }

        public class Builder
        {
            private readonly FrameEventArgs frameEventArgs;
            private readonly HandsImageBuilder.Builder handsImageBuilderBuilder;

            private readonly RealSenseFactory factory;
            private readonly Configuration realSenseConfiguration;

            public Builder(RealSenseFactory factory, Configuration realSenseConfiguration)
            {
                handsImageBuilderBuilder = factory.Components.Creator.HandsImageBuilder();
                frameEventArgs = new FrameEventArgs();

                this.factory = factory;
                this.realSenseConfiguration = realSenseConfiguration;
            }

            public Builder WithDevice(PXCMCapture.Device device)
            {
                frameEventArgs.device = device;
                return this;
            }

            public Builder WithImageData(ImageData.Builder imageData)
            {
                frameEventArgs.imageData = imageData.Build();
                return this;
            }

            public Builder WithHandsData(HandsData.Builder handsData)
            {
                frameEventArgs.handsData = handsData.Build();
                return this;
            }

            public FrameEventArgs Build()
            {
                frameEventArgs.handsImageBuilder = handsImageBuilderBuilder
                    .WithFactory(factory)
                    .WithDevice(frameEventArgs.device)
                    .WithConfiguration(realSenseConfiguration)
                    .WithHandsData(frameEventArgs.handsData)
                    .WithImageData(frameEventArgs.imageData)
                    .Build();
                return frameEventArgs;
            }
        }
    }
}