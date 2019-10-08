using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ComicsStoreBack.Data
{
    [DataContract]
    public class Comics
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public string Publisher { get; set; }

    }
}