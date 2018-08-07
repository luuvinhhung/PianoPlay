using PianoPlay_BE.Model;

namespace PianoPlay_BE.ViewModels
{
    public class CreateAccountModel
    {
        public string username { get; set; }
        public string fullName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public bool Status { get; set; }
    }
    public class EditAccountModel : CreateAccountModel
    {
        public string Id { get; set; }
    }
    public class AccountModel
    {
        public bool Status { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string Id { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public AccountModel() { }
        public AccountModel(Account account)
        {
            this.email = account.Email;
            this.username = account.UserName;
            this.Id = account.Id;
            this.fullName = account.FullName;
            this.phoneNumber = account.PhoneNumber;
            this.Status = account.Status;
        }
    }
}