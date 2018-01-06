namespace Resources.Entities
{
    public class ResourceEntry
    {
        public ResourceEntry()
        {
            Type = "string";
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string Culture { get; set; }
        public string Type { get; set; }
    }
}