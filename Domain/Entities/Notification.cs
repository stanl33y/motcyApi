using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Notification"/> class.
    /// </summary>
    /// <param name="message">Notification message.</param>
    /// <param name="createdAt">Notification creation date and time.</param>
    public Notification(string message, DateTime createdAt)
    {
        Message = message;
        CreatedAt = createdAt;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Message")]
    public string Message { get; set; }

    [Required(ErrorMessage = "CreatedAt")]
    public DateTime CreatedAt { get; set; }
}
