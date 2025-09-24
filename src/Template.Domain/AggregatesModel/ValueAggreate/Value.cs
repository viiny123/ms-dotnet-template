using Template.Domain.Base;

namespace Template.Domain.AggregatesModel.ValueAggreate
{
    public class Value : EntityBase<Value>
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
