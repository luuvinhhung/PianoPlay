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
        public IHttpActionResult GetAll(int page, int pageSize, string filter = null)
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

        //Lấy thông tin nhân viên theo username
        /**
         * @api {Get} /Accounts/GetByUserName?username={username} Lấy thông tin nhân viên theo username
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
         *  "fullName": null,
         *  "phoneNumber": null
         *  "Branch_id" : 1
         * }
         * @apiError (Error 404) {string} Errors Mảng các lỗi
         * @apiErrorExample {json} Error-Response:
         *     HTTP/1.1 404 Not Found
         *     {
         *       "error": "Not Found"
         *     }
         *
         */

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
                    phoneNumber = user.PhoneNumber
                });
            }
            return httpActionResult;
        }
        //Thêm mới nhân viên 
        /**
        * @api {POST} /Accounts/Create Tạo mới nhân viên 
        * @apigroup Accounts
        * @apiPermission none
        * @apiVersion 1.0.0
        *
         * @apiParam {string} avatar Avatar của nhân viên
         * @apiParam {string} username User name nhân viên
         * @apiParam {string} password Password của nhân viên
         * @apiParam {string} email Email nhân viên
         * @apiParam {string} fullname Họ tên nhân viên
         * @apiParam {int} BranchId Id chi nhánh
         * @apiParam {string} phonenumber Sdt nhân viên
        *
        * @apiParamExample {json} Request-Example:
        * {
        *         avatar:"avatar.jpg",
        *         username:"doduchuy123",
	    *         password:"Banhmi123",
	    *         email:"dodu1123c@gmail.com",
        *         phonenumber:"016826373",
	    *         fullname:"Do duc huy",
	    *         BranchId:1
        * }
        *
         *  @apiSuccess {string} Id Id của nhân viên
         *  @apiSuccess {string} username User name nhân viên
         *  @apiSuccess {string} email Email nhân viên
         *  @apiSuccess {long} BranchId Id chi nhánh nhân viên
         *  @apiSuccess {string} fullName Họ tên nhân viên
         *  @apiSuccess {string} phoneNumber Sdt nhân viên
         * 
        *
         * @apiSuccessExample {json} Response:
         * {
         *     
         *      Avatar: "avatar.jpg",
         *      username: "doduchuy123",
         *      email: "dodu1123c@gmail.com",
         *      Id: "a0f9fba2-1c04-4900-b51f-bea8fb7d41ad",
         *      fullName: "Do duc huy",
         *      phoneNumber: "016826373",
         *      BranchId: 1
         *     
         *     
         *     
         * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *       "error": "BranchId is required",
        *       "error": "Vui lòng nhập sdt"
        *     }
        *
        */
        [HttpPost]
        public async Task<IHttpActionResult> Create(CreateAccountModel model)
        {
            IHttpActionResult httpActionResult;
            if (string.IsNullOrEmpty(model.fullName))
            {
                error.Add("Full Name is required");
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
                acc.PhoneNumber = model.phoneNumber;
                userManager.Create(acc, model.password);
                await userManager.AddToRolesAsync(acc.Id, "User");
                //acc.Song = db.Branches.FirstOrDefault(x=>x.Id==model.BranchId);
                httpActionResult = Ok(new AccountModel(acc));
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            return httpActionResult;
        }
        //Sửa thông tin nhân viên
        /**
        * @api {PUT} /Accounts/Update Sửa thông tin nhân viên
        * @apigroup Accounts
        * @apiPermission none
        * @apiVersion 1.0.0
        *
        * @apiParam {string} Id Id Nhân viên
        *  @apiParam {int} BranchId Id chi nhánh
         * @apiParam {string} Avatar Ảnh đại diện của nhân viên
         * @apiParam {string} Username User name nhân viên
         * @apiParam {string} Email Email nhân viên
         * @apiParam {string} FullName Họ Tên nhân viên
         * @apiParam {string} Password Password tài khoản nhân viên
         * @apiParam {string} PhoneNumber Sdt nhân viên
        *
        * @apiParamExample {json} Request-Example:
        * {
        *      Avatar: "abc.jpg",
        *      username: "duchuy123",
        *      email: "doduchuy123@gmail.com",
        *      Id: "2d844a6c-402d-4286-b230-3179f644addf",
        *      fullName: "Do Duc Huy",
        *      password:"abc123B",
        *      phoneNumber: "017393938",
        *      BranchId: 1
        *        
        * }
        *
        * @apiSuccess {string} Avatar Ảnh đại diện của nhân viên
        *  @apiSuccess {string} Id Id của nhân viên
         *  @apiSuccess {string} username User name nhân viên
         *  @apiSuccess {string} email Email nhân viên
         *  @apiSuccess {int} BranchId Id chi nhánh nhân viên
         *  @apiSuccess {string} BranchName Tên chi nhánh nhân viên
         *  @apiSuccess {string} fullName Họ tên nhân viên
         *  @apiSuccess {string} phoneNumber Sdt nhân viên
         * 
        *
         * @apiSuccessExample {json} Response:
         * {
         *      "Avatar": "abc.jpg",
         *      "username": "duchuy123",
         *      "email": "doduchuy123@gmail.com",
         *      "Id": "2d844a6c-402d-4286-b230-3179f644addf",
         *      "fullName": "Do Duc Huy",
         *      "phoneNumber": "017393938",
         *      "BranchId": 1,
         *      "BranchName": "Highland 1"
         *     
         *      
         *      
         * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *        "error": "BranchId is required",
        *        "error": "Phone number is not correct!"
        *        "error": "User Name is required"
        *        "error": "Full Name is required"
        *        "error": "Password is required"
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
                //account.Branch = db.Branches.FirstOrDefault(x => x.Id == model.BranchId);
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

        //Xóa 1 nhân viên

        /**
     * @api {DELETE} /Accounts/Delete?code={code} Xóa 1 nhân viên
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