using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OCL_Apis.Models
{
    public enum TinplateStatus
    {
        Arrived,
        Production,
        Consumed
    }

    public enum CanType
    {
        _502, _401, Combination
    }
    public class Tinplate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Brand { get; set; }
        public string Batch { get; set; }
        public int TinplateQty { get; set; }
        public int Rejection { get; set; }
        public int Good { get; set; }
        public int Bad { get; set; }
        public CanType CanType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TinplateStatus TinplateStatus { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Note { get; set; }
        public Order Order { get; set; }
    }
}
