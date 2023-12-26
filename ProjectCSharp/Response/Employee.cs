namespace ProjectCSharp.Response
{
    public class Employee
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string StarTimeUtc { get; set; } = string.Empty;
        public string EndTimeUtc { get; set; } = string.Empty;
        public string EntryNotes { get; set; } = string.Empty;  
        public string? DeletedOn { get; set; }
    }
}
