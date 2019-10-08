using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace ComicsStoreBack.Data
{
    [DataContract]
    public class Author
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int DateOfBirth { get; set; }
    }
}