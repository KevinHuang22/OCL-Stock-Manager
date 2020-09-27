
//using Newtonsoft.Json.Converters;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace OCL_Apis.Models
//{
//    public enum ResourceStatus
//    {
//        Arrived,
//        Production,
//        Consumed
//    }

//    public class Resource
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        public long Id { get; set; }
//        public string Order { get; set; }
//        public string Brand { get; set; }
//        public string Batch { get; set; }
//        public int Tinplate { get; set; }  
//        public int Rejection{ get; set; }
//        public int Good { get; set; }
//        public int Bad { get; set; }
//        public CanType CanType { get; set; }
//        [JsonConverter(typeof(JsonStringEnumConverter))]
//        public ResourceStatus ResourceStatus { get; set; }
//        public string Note { get; set; }
//    }
//}
