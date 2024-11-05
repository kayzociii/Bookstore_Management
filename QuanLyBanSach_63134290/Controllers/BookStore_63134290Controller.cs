using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyBanSach_63134290.Models;

namespace QuanLyBanSach_63134290.Controllers
{
    public class BookStore_63134290Controller : Controller
    {
        // GET: BookStore

        //Tạo 1 đối tượng 
        QLBSEntities data = new QLBSEntities();

        public ActionResult Index(int? page)
        {
            if(page  == null) page = 1; 
            var sach = data.SACHes.ToList().OrderBy(b=>b.MaSach);

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(sach.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChuDe()
        {
            var chude = data.CHUDEs.ToList();
            return PartialView(chude);
        }
        public ActionResult TimKiemTheoChuDe(int Id)
        {
            HomeModel_63134290 model = new HomeModel_63134290();
            model.ListchuDe = data.CHUDEs.ToList();

            model.ListSach = data.SACHes.Where(n=>n.CHUDE.MaChuDe == Id).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh)
        {
            if (ModelState.IsValid)
            {
                var check = data.KHACHHANGs.FirstOrDefault(s => s.TaiKhoan ==  kh.TaiKhoan);  
                if (check == null)
                {
                    int maMax = data.KHACHHANGs.ToList().Select(n => n.MaKH).Max();
                    kh.MaKH = maMax + 1;
                    data.Configuration.ValidateOnSaveEnabled = false;
                    data.KHACHHANGs.Add(kh);
                    data.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Tên tài khoản đã tồn tại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string taikhoan, string matkhau)
        {
            if (ModelState.IsValid)
            {
                var db = data.KHACHHANGs.Where(s => s.TaiKhoan.Equals(taikhoan) && s.MatKhau.Equals(matkhau)).ToList();
                if (db.Count() > 0)
                {
                    //add session
                    Session["TaiKhoan"] = db.FirstOrDefault().TaiKhoan;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Thongbao = "Đăng nhập thất bại!";
                    return RedirectToAction("DangNhap");
                }
            }
            return View();
        }
    }
}