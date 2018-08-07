using PianoPlay_BE.Infrastructure;
using PianoPlay_BE.Model;
using PianoPlay_BE.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace PianoPlay_BE.Controllers
{
    public class AccountsController : ApiController
    {
        private ApiDbContext db;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ErrorModel error;

        public AccountsController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            db = new ApiDbContext();
            error = new ErrorModel();
        }

        //Lấy thông tin tất cả nhân viên
        /**
         * @api {Get} /Accounts/GetAll?page={page}&pageSize={pageSize}&filter={filter} Lấy thông tin tất cả nhân viên
         * @apigroup Accounts
         * @apiPermission none
         * @apiVersion 1.0.0
         *
         * @apiSuccess {long} Id Id của nhân viên
         * @apiSuccess {string} username Username nhân viên
         * @apiSuccess {string} email email nhân viên
         * @apiSuccess {string} fullName Họ tên
         * @apiSuccess {string} phoneNumber Số điện thoại
         * @apiSuccess {string} branch_id Mã chi nhánh
         *
         * @apiSuccessExample {json} Response:
         * {
         *   "username": "admin",
         *  "email": "admin@test.com",
         *  "Id": "36cd045c-94a3-48a2-9a3d-05c603011b3f",
         *  "fullName": "Tấn Triều",
         *  "phoneNumber": "0123456789",
         *  "Branch_id" : 1
         * }
         */

        [HttpGet]
        public IHttpActionResult GetAllByPage(int page, int pageSize, string filter = null)
        {
            int totalRow = 0;
            var model = _userManager.Users.Select(x => new AccountModel
            {
                email = x.Email,
                username = x.UserName,
                Id = x.Id,
                fullName = x.FullName
            });
            totalRow = model.Count();
            if (!string.IsNullOrEmpty(filter))
                model = model.Where(x => x.username.Contains(filter) || x.fullName.Contains(filter));
            var list = model.OrderBy(x => x.fullName).Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(list);
        }
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            int totalRow = 0;
            var model = _userManager.Users.Select(x => new AccountModel
            {
                email = x.Email,
                username = x.UserName,
                Id = x.Id,
                fullName = x.FullName,
                phoneNumber = x.PhoneNumber,
                Status = x.Status
            });
            totalRow = model.Count();
            model = model.Where(x => x.Status == true);
            var list = model.OrderBy(x => x.fullName);
            return Ok(list);
        }
        

        [HttpGet]
        public IHttpActionResult GetByUsername(string username)
        {
            IHttpActionResult httpActionResult;
            var user = _userManager.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            else
            {
                httpActionResult = Ok(new AccountModel
                {
                    email = user.Email,
                    username = user.UserName,
                    Id = user.Id,
                    fullName = user.FullName,
                    phoneNumber = user.PhoneNumber,
                    Status = user.Status
                });
            }
            return httpActionResult;
        }
        
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateAccountModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.fullName))
            //{
            //    error.Add("Full Name is required");
            //    httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            //}
            if (string.IsNullOrEmpty(model.password))
            {
                error.Add("Password is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (string.IsNullOrEmpty(model.email))
            {
                error.Add("Email is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (!CheckPhoneNumber.CheckCorrectPhoneNumber(model.phoneNumber))
            {
                error.Add("Phone number is not correct!");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (error.errors.Count==0 && CheckPhoneNumber.CheckCorrectPhoneNumber(model.phoneNumber))
            {
                var userManager = new ApplicationUserManager(new UserStore<Account>(this.db));
                Account acc = new Account();
                acc.UserName = model.username;
                acc.FullName = model.fullName;
                acc.Email = model.email;
                acc.Status = true;
                acc.PhoneNumber = model.phoneNumber;
                userManager.Create(acc, model.password);
                await userManager.AddToRolesAsync(acc.Id, "User");
                httpActionResult = Ok(new AccountModel(acc));
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            return httpActionResult;
        }
        
        [HttpPut]
        public IHttpActionResult Update(EditAccountModel model)
        {
            IHttpActionResult httpActionResult;
            var userManager = new ApplicationUserManager(new UserStore<Account>(this.db));
            Account account = userManager.Users.FirstOrDefault(x => x.Id == model.Id);
            if (account == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            if (string.IsNullOrEmpty(model.username))
            {
                error.Add("User Name is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (string.IsNullOrEmpty(model.fullName))
            {
                error.Add("Full Name is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (string.IsNullOrEmpty(model.email))
            {
                error.Add("Email is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (!CheckPhoneNumber.CheckCorrectPhoneNumber(model.phoneNumber))
            {
                error.Add("Phone number is not correct!");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            if (error.errors.Count != 0)
            {
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            else
            {
                account.UserName = model.username ?? model.username;
                account.FullName = model.fullName ?? model.fullName;
                account.Email = model.email ?? model.email;
                account.PhoneNumber = model.phoneNumber ?? model.phoneNumber;
                account.Status = model.Status;
                if (!string.IsNullOrEmpty(model.password))
                {
                    userManager.RemovePassword(model.Id);
                    userManager.AddPassword(model.Id, model.password);
                }
                else
                {
                    error.Add("Password is required!");
                    httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
                }
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new AccountModel(account));
            }
            return httpActionResult;
        }

       
        [HttpDelete]
        public IHttpActionResult Delete(string code)
        {
            IHttpActionResult httpActionResult;
            var userManager = new ApplicationUserManager(new UserStore<Account>(this.db));
            var user = userManager.Users.FirstOrDefault(x => x.Id == code);
            if (user == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            else
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                httpActionResult = Ok("Success");
            }
            return httpActionResult;
        }
    }

}