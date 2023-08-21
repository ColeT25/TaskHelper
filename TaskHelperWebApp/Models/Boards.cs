using System;
using System.ComponentModel.DataAnnotations;

namespace TaskHelperWebApp;

public class Boards
{
    public Guid ID { get; set; }
    public Guid ProjectID { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Color { get; set; } = default!;

    [Display(Name = "Created Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; }

    [Display(Name = "Closed Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? ClosedDate { get; set; } = null;
}
