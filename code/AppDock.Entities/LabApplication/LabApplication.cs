namespace AppDock.Entities
{
    public class LabApplication : ILabApplication
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string StartInPath { get; set; }
        public bool IsActive { get; set; }

        public ITypeEntity Type { get; set; }
    }
}
