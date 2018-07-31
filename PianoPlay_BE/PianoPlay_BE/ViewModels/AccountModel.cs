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
        public string Avatar { get; set; }
        public long BranchId { get; set; }
    }
    public class EditAccountModel : CreateAccountModel
    {
        public string Id { get; set; }
    }
    public class AccountModel
    {
        public string Avatar { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string Id { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public AccountModel() { }
        public AccountModel(Account account)
        {
            this.email = account.Email;
            this.username = account.UserName;
            this.Id = account.Id;
            this.fullName = account.FullName;
            this.phoneNumber = account.PhoneNumber;
            //BranchId = account.Branch.Id;
            //BranchName = account.Branch.Name;
        }
    }
}