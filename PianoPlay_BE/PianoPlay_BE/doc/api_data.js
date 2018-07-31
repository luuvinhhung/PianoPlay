define({ "api": [
  {
    "type": "DELETE",
    "url": "/Accounts/Delete?code={code}",
    "title": "Xóa 1 nhân viên",
    "group": "Accounts",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     \"Success\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 404 Not Found\n{\n  \"error\": \"Not Found\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/AccountsController.cs",
    "groupTitle": "Accounts",
    "name": "DeleteAccountsDeleteCodeCode"
  },
  {
    "type": "Get",
    "url": "/Accounts/GetAll?page={page}&pageSize={pageSize}&filter={filter}",
    "title": "Lấy thông tin tất cả nhân viên",
    "group": "Accounts",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "username",
            "description": "<p>Username nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "email",
            "description": "<p>email nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "fullName",
            "description": "<p>Họ tên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "phoneNumber",
            "description": "<p>Số điện thoại</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "branch_id",
            "description": "<p>Mã chi nhánh</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n  \"username\": \"admin\",\n \"email\": \"admin@test.com\",\n \"Id\": \"36cd045c-94a3-48a2-9a3d-05c603011b3f\",\n \"fullName\": \"Tấn Triều\",\n \"phoneNumber\": \"0123456789\",\n \"Branch_id\" : 1\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/AccountsController.cs",
    "groupTitle": "Accounts",
    "name": "GetAccountsGetallPagePagePagesizePagesizeFilterFilter"
  },
  {
    "type": "Get",
    "url": "/Accounts/GetByUserName?username={username}",
    "title": "Lấy thông tin nhân viên theo username",
    "group": "Accounts",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "username",
            "description": "<p>Username nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "email",
            "description": "<p>email nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "fullName",
            "description": "<p>Họ tên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "phoneNumber",
            "description": "<p>Số điện thoại</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "branch_id",
            "description": "<p>Mã chi nhánh</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n  \"username\": \"admin\",\n \"email\": \"admin@test.com\",\n \"Id\": \"36cd045c-94a3-48a2-9a3d-05c603011b3f\",\n \"fullName\": null,\n \"phoneNumber\": null\n \"Branch_id\" : 1\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 404 Not Found\n{\n  \"error\": \"Not Found\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/AccountsController.cs",
    "groupTitle": "Accounts",
    "name": "GetAccountsGetbyusernameUsernameUsername"
  },
  {
    "type": "POST",
    "url": "/Accounts/Create",
    "title": "Tạo mới nhân viên",
    "group": "Accounts",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "avatar",
            "description": "<p>Avatar của nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "username",
            "description": "<p>User name nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "password",
            "description": "<p>Password của nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "email",
            "description": "<p>Email nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "fullname",
            "description": "<p>Họ tên nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "BranchId",
            "description": "<p>Id chi nhánh</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "phonenumber",
            "description": "<p>Sdt nhân viên</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n        avatar:\"avatar.jpg\",\n        username:\"doduchuy123\",\n        password:\"Banhmi123\",\n        email:\"dodu1123c@gmail.com\",\n        phonenumber:\"016826373\",\n        fullname:\"Do duc huy\",\n        BranchId:1\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "username",
            "description": "<p>User name nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "email",
            "description": "<p>Email nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "BranchId",
            "description": "<p>Id chi nhánh nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "fullName",
            "description": "<p>Họ tên nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "phoneNumber",
            "description": "<p>Sdt nhân viên</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n    \n     Avatar: \"avatar.jpg\",\n     username: \"doduchuy123\",\n     email: \"dodu1123c@gmail.com\",\n     Id: \"a0f9fba2-1c04-4900-b51f-bea8fb7d41ad\",\n     fullName: \"Do duc huy\",\n     phoneNumber: \"016826373\",\n     BranchId: 1\n    \n    \n    \n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n  \"error\": \"BranchId is required\",\n  \"error\": \"Vui lòng nhập sdt\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/AccountsController.cs",
    "groupTitle": "Accounts",
    "name": "PostAccountsCreate"
  },
  {
    "type": "PUT",
    "url": "/Accounts/Update",
    "title": "Sửa thông tin nhân viên",
    "group": "Accounts",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Id",
            "description": "<p>Id Nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "int",
            "optional": false,
            "field": "BranchId",
            "description": "<p>Id chi nhánh</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Avatar",
            "description": "<p>Ảnh đại diện của nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Username",
            "description": "<p>User name nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Email",
            "description": "<p>Email nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "FullName",
            "description": "<p>Họ Tên nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Password",
            "description": "<p>Password tài khoản nhân viên</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "PhoneNumber",
            "description": "<p>Sdt nhân viên</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n     Avatar: \"abc.jpg\",\n     username: \"duchuy123\",\n     email: \"doduchuy123@gmail.com\",\n     Id: \"2d844a6c-402d-4286-b230-3179f644addf\",\n     fullName: \"Do Duc Huy\",\n     password:\"abc123B\",\n     phoneNumber: \"017393938\",\n     BranchId: 1\n       \n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Avatar",
            "description": "<p>Ảnh đại diện của nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "username",
            "description": "<p>User name nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "email",
            "description": "<p>Email nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "int",
            "optional": false,
            "field": "BranchId",
            "description": "<p>Id chi nhánh nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "BranchName",
            "description": "<p>Tên chi nhánh nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "fullName",
            "description": "<p>Họ tên nhân viên</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "phoneNumber",
            "description": "<p>Sdt nhân viên</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     \"Avatar\": \"abc.jpg\",\n     \"username\": \"duchuy123\",\n     \"email\": \"doduchuy123@gmail.com\",\n     \"Id\": \"2d844a6c-402d-4286-b230-3179f644addf\",\n     \"fullName\": \"Do Duc Huy\",\n     \"phoneNumber\": \"017393938\",\n     \"BranchId\": 1,\n     \"BranchName\": \"Highland 1\"\n    \n     \n     \n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n   \"error\": \"BranchId is required\",\n   \"error\": \"Phone number is not correct!\"\n   \"error\": \"User Name is required\"\n   \"error\": \"Full Name is required\"\n   \"error\": \"Password is required\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/AccountsController.cs",
    "groupTitle": "Accounts",
    "name": "PutAccountsUpdate"
  },
  {
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "optional": false,
            "field": "varname1",
            "description": "<p>No type.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "varname2",
            "description": "<p>With type.</p>"
          }
        ]
      }
    },
    "type": "",
    "url": "",
    "version": "0.0.0",
    "filename": "./doc/main.js",
    "group": "D__Web_ANGULAR_sources_ThucTap_PianoPlay_BE_HiglandCoffee_doc_main_js",
    "groupTitle": "D__Web_ANGULAR_sources_ThucTap_PianoPlay_BE_HiglandCoffee_doc_main_js",
    "name": ""
  },
  {
    "type": "DELETE",
    "url": "/Songs/Delete?id={id}",
    "title": "Xóa 1 bài hát theo id",
    "group": "Songs",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     \"Success\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 404 Not Found\n{\n  \"error\": \"Not Found\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/SongsController.cs",
    "groupTitle": "Songs",
    "name": "DeleteSongsDeleteIdId"
  },
  {
    "type": "GET",
    "url": "/Songs/GetAll",
    "title": "Lấy thông tin tất cả bài hát",
    "group": "Songs",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của bài hát</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "SongsCode",
            "description": "<p>Mã của bài hát</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Name",
            "description": "<p>Tên bài hát</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     Id:1,\n     SongsCode: \"BH1\",\n     Name: \"bài hát 1\",\n     KeyIds: \"16161616\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/SongsController.cs",
    "groupTitle": "Songs",
    "name": "GetSongsGetall"
  },
  {
    "type": "GET",
    "url": "/Songs/GetByCode?code={code}",
    "title": "Lấy thông tin bài hát theo code",
    "group": "Songs",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của bài hát</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "SongsCode",
            "description": "<p>Mã của bài hát</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Name",
            "description": "<p>Tên bài hát</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     Id:1,\n     SongsCode: \"BH1\",\n     Name: \"bài hát 1\",\n     KeyIds: \"16161616\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 404 Not Found\n{\n  \"error\": \"Not Found\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/SongsController.cs",
    "groupTitle": "Songs",
    "name": "GetSongsGetbycodeCodeCode"
  },
  {
    "type": "POST",
    "url": "/Songs/Create",
    "title": "Tạo mới bài hát",
    "group": "Songs",
    "permission": [
      {
        "name": "none"
      }
    ],
    "version": "1.0.0",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "Name",
            "description": "<p>Tên bài hát</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "SongCode",
            "description": "<p>Mã bài hát</p>"
          },
          {
            "group": "Parameter",
            "type": "string",
            "optional": false,
            "field": "KeyIds",
            "description": "<p>KeyId</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n     Name: \"bài hát 1\",\n     KeyIds: \"16161616\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "long",
            "optional": false,
            "field": "Id",
            "description": "<p>Id của bài hát vửa tạo</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "SongsCode",
            "description": "<p>Mã của bài hát vửa tạo</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "Name",
            "description": "<p>Tên bài hát vửa tạo</p>"
          },
          {
            "group": "Success 200",
            "type": "string",
            "optional": false,
            "field": "KeyIds",
            "description": "<p>KeyId</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Response:",
          "content": "{\n     Id:1,\n     SongsCode: \"BH1\",\n     Name: \"bài hát 1\",\n     KeyIds: \"16161616\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 404": [
          {
            "group": "Error 404",
            "type": "string",
            "optional": false,
            "field": "Errors",
            "description": "<p>Mảng các lỗi</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n  \"error\": \"Name is required\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "./Controllers/SongsController.cs",
    "groupTitle": "Songs",
    "name": "PostSongsCreate"
  }
] });
