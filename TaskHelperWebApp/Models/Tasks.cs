namespace TaskHelperWebApp;

public class Tasks
{
    public Guid ID { get; set; }
    public Guid boardID { get; set; }
    public Guid projectID { get; set; }
    public string name { get; set; } = default!;
    public string description { get; set; } = default!;
    public Guid? isSubtaskOf { get; set; }
    public bool isComplete { get; set; } = false;
}
