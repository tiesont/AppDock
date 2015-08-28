namespace AppDock.Entities
{
    public interface ILabApplication
    {
        int ID { get; set; }
        int TypeID { get; set; }
        string Label { get; set; }
        string Path { get; set; }
        string StartInPath { get; set; }
        bool IsActive { get; set; }

        ITypeEntity Type { get; set; }
    }
}
