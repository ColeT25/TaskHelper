using System;
using System.ComponentModel.DataAnnotations;

namespace TaskHelperWebApp;

public class Tasks
{
    public Guid ID { get; set; }
    public Guid BoardID { get; set; }
    public Guid ProjectID { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid? IsSubtaskOf { get; set; } public bool IsComplete { get; set; } = false;

    [Display(Name = "Created Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; }
}