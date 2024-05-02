namespace MyApp
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TrackingPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public TrackingPropertyAttribute()
        {
        }

        public TrackingPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}