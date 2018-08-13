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

        //Lấy thông tin tất cả account
        /**
         * @api {GET} /Accounts/GetAll Lấy thông tin tất cả account
         * @apigroup Accounts
         * @apiPermission none
         * @apiVersion 1.0.0
         *
         * @apiSuccess {Boolean} Status đánh dấu trạng thái tồn tại account
         * @apiSuccess {string} username username đăng nhập account
         * @apiSuccess {string} email email
         * @apiSuccess {string} Id Id account
         * @apiSuccess {string} fullName tên người dùng account
         * @apiSuccess {string} phoneNumber số đt người dùng account
         * @apiSuccessExample {json} Response:
         * {
         *      "Status": true,
         *      "username": "hung",
         *      "email": "hung@gmail.com",
         *      "Id": "40906a23-cccf-4d10-816c-de9122e291e3",
         *      "fullName": "Lưu Vĩnh Hùng",
         *      "phoneNumber": "0901397512"
         * }
         */

        //[Authorize(Roles = "Admin")]
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

        //Thêm mới account
        /**
        * @api {POST} /Accounts/create Tạo mới account
        * @apigroup Accounts
        * @apiPermission none
        * @apiVersion 1.0.0
        *
        * @apiParam {string} username username đăng nhập account
        * @apiParam {string} email email
        * @apiParam {string} Id Id account
        * @apiParam {string} fullName tên người dùng account
        * @apiParam {string} phoneNumber số đt người dùng account
        *
        * @apiParamExample {json} Request-Example:
        * {
        *      "username":"hung",
	    *      "password":"Abc123!!!",
	    *      "fullName": "Vinh Hung",
	    *      "email": "hung@gmail.com",
	    *      "phoneNumber":"0901397512"
        * }
        *
        * @apiSuccess {Boolean} Status đánh dấu trạng thái tồn tại account
        * @apiSuccess {string} username username đăng nhập account
        * @apiSuccess {string} email email
        * @apiSuccess {string} Id Id account
        * @apiSuccess {string} fullName tên người dùng account
        * @apiSuccess {string} phoneNumber số điện thoại người dùng account
        * @apiSuccessExample {json} Response:
        * {
        *      "Status": true,
        *      "username": "hung",
        *      "email": "hung@gmail.com",
        *      "Id": "40906a23-cccf-4d10-816c-de9122e291e3",
        *      "fullName": "Lưu Vĩnh Hùng",
        *      "phoneNumber": "0901397512"
        * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *       "error": "User Name is required"
        *     }
        *
        */

        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateAccountModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.username))
            {
                error.Add("User Name is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
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

        //Sửa thông tin account
        /**
        * @api {PUT} /Accounts/Update Sửa thông tin account
        * @apigroup Accounts
        * @apiPermission none
        * @apiVersion 1.0.0
        *
        * @apiParam {string} id id account cần chỉnh sửa
        * @apiParam {string} username username đăng nhập account
        * @apiParam {string} email email
        * @apiParam {string} fullName tên người dùng account
        * @apiParam {string} phoneNumber số điện thoại người dùng account
        *
        * @apiParamExample {json} Request-Example:
        * {
        *      "Id": "40906a23-cccf-4d10-816c-de9122e291e3",
        *      "Status": true,
        *      "username": "hung",
        *      "email": "hung@gmail.com",
        *      "Id": "40906a23-cccf-4d10-816c-de9122e291e3",
        *      "fullName": "Update",
        *      "phoneNumber": "0901397512"
        * }
        *
        * @apiParam {string} id id account vừa chỉnh sửa
        * @apiParam {string} username username đăng nhập vừa chỉnh sửa
        * @apiParam {string} email email vừa chỉnh sửa
        * @apiParam {string} fullName tên người dùng vừa chỉnh sửa account
        * @apiParam {string} phoneNumber số điện thoại người dùng account vừa chỉnh sửa
        *
         * @apiSuccessExample {json} Response:
         * {
         *      "Status": true,
         *      "username": "hung",
         *      "email": "hung@gmail.com",
         *      "Id": "40906a23-cccf-4d10-816c-de9122e291e3",
         *      "fullName": "Update",
         *      "phoneNumber": "0901397512"
         * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *        "error": "Not Found",
        *     }
        *
        */

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

        //Xóa 1 account
        /**
         * @api {DELETE} /Accounts/Delete?id={id} Xóa 1 account theo Id
         * @apigroup Accounts
         * @apiPermission none
         * @apiVersion 1.0.0
         *
         * @apiSuccessExample {json} Response:
         * {
         *      "Success"
         * }
         * @apiError (Error 404) {string} Errors Mảng các lỗi
         * @apiErrorExample {json} Error-Response:
         *     HTTP/1.1 404 Not Found
         *     {
         *       "error": "Not Found"
         *     }
         *
         */

        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            IHttpActionResult httpActionResult;
            var userManager = new ApplicationUserManager(new UserStore<Account>(this.db));
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
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