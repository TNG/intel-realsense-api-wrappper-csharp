using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using IntelRealSenseStart.Code.RealSense.Data.Properties;
using IntelRealSenseStart.Code.RealSense.Factory.Data;
using IntelRealSenseStart.Code.RealSense.Helper;
using IntelRealSenseStart.Code.RealSense.Provider;

namespace IntelRealSenseStart.Code.RealSense.Component.Property
{
    public class VideoDevicePropertiesDeterminer : PropertiesComponent<VideoProperties.Builder>
    {
        private readonly PropertiesDataFactory factory;
        private readonly PXCMSession session;

        private VideoDevicePropertiesDeterminer(PropertiesDataFactory factory, NativeSense nativeSense)
        {
            this.factory = factory;
            session = nativeSense.Session;
        }

        public void UpdateProperties(VideoProperties.Builder videoProperties)
        {
            DetermineVideoDeviceProperties(videoProperties);
        }

        private void DetermineVideoDeviceProperties(VideoProperties.Builder videoProperties)
        {
            var videoDeviceDescription = CreateVideoDeviceDescription();
            for (int i = 0;; i++)
            {
                PXCMSession.ImplDesc deviceDescription;
                PXCMCapture deviceCapture;
                
                if (session.QueryImpl(videoDeviceDescription, i, out deviceDescription) <
                    pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    break;
                }
                if (session.CreateImpl(deviceDescription, out deviceCapture) <
                    pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    continue;
                }
                for (int j = 0;; j++)
                {
                    PXCMCapture.DeviceInfo deviceInfo;
                    if (deviceCapture.QueryDeviceInfo(j, out deviceInfo) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        break;
                    }
                    PXCMCapture.Device device;
                    device = deviceCapture.CreateDevice(deviceInfo.didx);
                    videoProperties.WithVideoDevice(GetDevicePropertiesFrom(deviceInfo, device));
                }

                deviceCapture.Dispose();
            }
        }

        private PXCMSession.ImplDesc CreateVideoDeviceDescription()
        {
            var videoDeviceDescription = new PXCMSession.ImplDesc();
            videoDeviceDescription.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            videoDeviceDescription.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            return videoDeviceDescription;
        } 

        private VideoDeviceProperties.Builder GetDevicePropertiesFrom(PXCMCapture.DeviceInfo deviceInfo, PXCMCapture.Device device)
        {
            return factory.VideoDevice()
                .WithDeviceName(deviceInfo.name)
                .WithDeviceInfo(deviceInfo)
                .WithSupportedColorStreams(GetStreamsFor(device, deviceInfo, PXCMCapture.StreamType.STREAM_TYPE_COLOR))
                .WithSupportedDepthStreams(GetStreamsFor(device, deviceInfo, PXCMCapture.StreamType.STREAM_TYPE_DEPTH));
        }

        private IEnumerable<StreamProperties.Builder> GetStreamsFor(PXCMCapture.Device device,
            PXCMCapture.DeviceInfo deviceInfo, PXCMCapture.StreamType streamType)
        {
            return GetStreamProperties(GetAllowedStreamsFor(device, deviceInfo, streamType), streamType);
        }

        private IEnumerable<PXCMCapture.Device.StreamProfile> GetAllowedStreamsFor(PXCMCapture.Device device,
            PXCMCapture.DeviceInfo deviceInfo, PXCMCapture.StreamType streamType)
        {
            var streamProfiles = new List<PXCMCapture.Device.StreamProfile>();
            if (((int) deviceInfo.streams & (int) streamType) == 0)
            {
                return streamProfiles;
            }

            int numberOfStreamProfiles = device.QueryStreamProfileSetNum(streamType);
            for (int i = 0; i < numberOfStreamProfiles; i++)
            {
                PXCMCapture.Device.StreamProfileSet streamProfileSet;
                if (device.QueryStreamProfileSet(streamType, i, out streamProfileSet) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                {
                    continue;
                }
                streamProfiles.Add(streamProfileSet[streamType]);
            }

            return streamProfiles;
        }

        private IEnumerable<StreamProperties.Builder> GetStreamProperties(
            IEnumerable<PXCMCapture.Device.StreamProfile> streamProfiles, PXCMCapture.StreamType streamType)
        {
            return streamProfiles.Select(streamProfile =>
                factory.Stream()
                    .WithStreamType(streamType)
                    .WithResolution(new Size(streamProfile.imageInfo.width, streamProfile.imageInfo.height))
                    .WithFrameRate((int) streamProfile.frameRate.min)
                    .WithFormat(streamProfile.imageInfo.format)
                );
        }

        public class Builder
        {
            private PropertiesDataFactory factory;
            private NativeSense nativeSense;

            public Builder WithFactory(PropertiesDataFactory factory)
            {
                this.factory = factory;
                return this;
            }

            public Builder WithNativeSense(NativeSense nativeSense)
            {
                this.nativeSense = nativeSense;
                return this;
            }

            public VideoDevicePropertiesDeterminer Build()
            {
                factory.Check(Preconditions.IsNotNull,
                    "The factory must be set in order to create the video device properties determiner");
                nativeSense.Check(Preconditions.IsNotNull,
                    "The native set must be set in order to create the video device properties determiner");

                return new VideoDevicePropertiesDeterminer(factory, nativeSense);
            }
        }
    }
}