using PianoPlay_BE.Infrastructure;
using PianoPlay_BE.Model;
using PianoPlay_BE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PianoPlay_BE.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class SongsController : ApiController
    {
        private ApiDbContext db;
        private ErrorModel error;

        public SongsController()
        {
            db = new ApiDbContext();
            error = new ErrorModel();
        }

        //Lấy thông tin tất cả bài hát
        /**
         * @api {GET} /Songs/GetAll Lấy thông tin tất cả bài hát
         * @apigroup Songs
         * @apiPermission none
         * @apiVersion 1.0.0
         *
         * @apiSuccess {long} Id Id của bài hát
         * @apiSuccess {string} Name Tên bài hát
         * @apiSuccess {string} KeyIds KeyId
         * @apiSuccessExample {json} Response:
         * {
         *      Id:1,
         *      UserId: 1,
         *      Name: "bài hát 1",
         *      KeyIds: "16161616"
         * }
         */
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            int totalRow = 0;
            var model = db.Songs.Select(x => new SongsModel
            {
                Id = x.Id,
                UserId = x.UserId,
                Name = x.Name,
                KeyIds = x.KeyIds
            });
            totalRow = model.Count();

            var list = model.OrderBy(x => x.Id);
            return Ok(list);
        }
        //Lấy thông tin bài hát theo tên bài hát

        /**
         * @api {GET} /Songs/GetByCode?code={code} Lấy thông tin bài hát theo tên
         * @apigroup Songs
         * @apiPermission none
         * @apiVersion 1.0.0
         *
         * @apiSuccess {long} Id Id của bài hát
         * @apiSuccess {string} Name Tên bài hát
         * @apiSuccess {string} KeyIds KeyIds
         *
         * @apiSuccessExample {json} Response:
         * {
         *      Id:1,
         *      UserId: 1,
         *      Name: "bài hát 1",
         *      KeyIds: "16161616"
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
        public IHttpActionResult GetByCode(string name)
        {
            IHttpActionResult httpActionResult;
            Songs Songs = db.Songs.FirstOrDefault(x => x.Name == name);
            if (Songs == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            else
            {
                httpActionResult = Ok(new SongsModel(Songs));
            }
            return httpActionResult;
        }

        //Thêm mới bài hát
        /**
        * @api {POST} /Songs/Create Tạo mới bài hát
        * @apigroup Songs
        * @apiPermission none
        * @apiVersion 1.0.0
        *
         * @apiParam {string} Name Tên bài hát
         * @apiParam {string} KeyIds KeyId
        *
        * @apiParamExample {json} Request-Example:
        * {
        *      UserId: 1
        *      Name: "bài hát 1",
        *      KeyIds: "16161616"
        * }
        *
         * @apiSuccess {long} Id Id của bài hát vừa tạo
         * @apiSuccess {string} SongsCode Mã của bài hát vừa tạo
         * @apiSuccess {string} Name Tên bài hát vừa tạo
         * @apiSuccess {string} KeyIds KeyId
        *
         * @apiSuccessExample {json} Response:
         * {
         *      Id:1,
         *      UserId: 1,
         *      Name: "bài hát 1",
         *      KeyIds: "16161616"
         * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *       "error": "Name is required"
        *     }
        *
        */

        [HttpPost]
        public IHttpActionResult Create(CreateSongModel model)
        {
            IHttpActionResult httpActionResult;

            if (string.IsNullOrEmpty(model.Name))
            {
                error.Add("Name is required");
            }
            if (error.errors.Count == 0)
            {
                Songs Songs = new Songs();
                Songs.UserId = model.UserId;
                Songs.Name = StandardString.StandardSongName(model.Name);
                Songs.KeyIds = model.KeyIds;
                Songs = db.Songs.Add(Songs);
                db.SaveChanges();
                httpActionResult = Ok(new SongsModel(Songs));
            }
            else
            {
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            return httpActionResult;
        }

        //Sửa thông tin bài hát
        /**
        * @api {PUT} /Songs/Update Sửa thông tin bài hát
        * @apigroup Songs
        * @apiPermission none
        * @apiVersion 1.0.0
        *
        * @apiParam {long} Id Id của bài hát
        * @apiParam {string} Name Tên bài hát
        *
        * @apiParamExample {json} Request-Example:
        * {
        *      Id:1,
        *      Name: "bài hát 2",
        * }
        *
         * @apiSuccess {long} Id Id của bài hát vừa sửa
         * @apiSuccess {string} SongCode Mã của bài hát vừa sửa
         * @apiSuccess {string} Name Tên bài hát vừa sửa
         * @apiSuccess {string} KeyIds keyids cửa bài hát vừa sửa
        *
         * @apiSuccessExample {json} Response:
         * {
         *      Id:1,
         *      Name: "bài hát 2",
         *      KeyIds: "16161616"
         * }
        *
        * @apiError (Error 404) {string} Errors Mảng các lỗi
        * @apiErrorExample {json} Error-Response:
        *     HTTP/1.1 400 Bad Request
        *     {
        *        "error": "Name is required",
        *     }
        *
        */
        [HttpPut]
        public IHttpActionResult Update(EditSongModel model)
        {
            IHttpActionResult httpActionResult;
            Songs Songs = db.Songs.FirstOrDefault(x => x.Id == model.id);
            if (Songs == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                error.Add("Name is required");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.BadRequest, error);
            }
            else
            {
                Songs.Name = StandardString.StandardSongName(model.Name) ?? model.Name;
                db.Entry(Songs).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                httpActionResult = Ok(new SongsModel(Songs));
            }
            return httpActionResult;
        }

        //Xóa 1 bài hát
        /**
         * @api {DELETE} /Songs/Delete?id={id} Xóa 1 bài hát theo Id
         * @apigroup Songs
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
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult httpActionResult;
            Songs Songs = db.Songs.FirstOrDefault(x => x.Id == id);
            if (Songs == null)
            {
                error.Add("Not Found");
                httpActionResult = new ErrorActionResult(Request, HttpStatusCode.NotFound, error);
            }
            else
            {
                db.Entry(Songs).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                httpActionResult = Ok("Success");
            }
            return httpActionResult;
        }
    }
}
