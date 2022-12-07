using System.ComponentModel.DataAnnotations.Schema;

namespace AssessmentTask.Models
{
    public class ApplicationLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ApplicationLogID { get; set; }
        public DateTime LogDate { get; set; }

        public string LogOriginator { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public Guid UserID { get; set; }
    }
}
