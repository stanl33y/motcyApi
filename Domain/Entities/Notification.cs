using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Message")]
    public string Message { get; set; }

    [Required(ErrorMessage = "CreatedAt")]
    public DateTime CreatedAt { get; set; }

}
