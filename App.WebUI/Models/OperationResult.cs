namespace App.WebUI.Models
{
    public class OperationResult
    {
        public OperationResult()
        {
            Success = false;
            Errors = new List<string> { "Bir hata oluştu!" };
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool IsRedirect { get; set; }
        public List<string> Errors { get; set; }

    }
}
