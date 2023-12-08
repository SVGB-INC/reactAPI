using System;

namespace reactAPI.Models
{
    public class Bundle
    {
        public int BundleId { get; set; }

        public string BundleName { get; set; }

        public string BundleDesc { get; set; }

        public string BundlePrice { get; set; }

        public int BundleParticipants { get; set; }

        public string BundleFeatures { get; set; }

        public DateTime BundleDate { get; set; }

        public String BundleStatus { get; set; }

    }
}
