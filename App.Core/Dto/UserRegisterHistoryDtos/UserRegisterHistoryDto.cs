namespace App.Core.Dto
{
    public class UserRegisterHistoryDto : BaseDtoModel
    {
        public int UserId { get; set; }
        public DateTime? ConfirmDate { get; set; }
    }
}
