using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach_63134290.Models;
using System.IO;
using System.Data.Entity;
using System.Data.SqlTypes;

namespace QuanLyBanSach_63134290.Controllers
{
    public class Admin_63134290Controller : Controller
    {
        // GET: Admin
        public QLBSEntities data = new QLBSEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sach()
        {
            return View(data.SACHes.ToList());
        }
        [HttpGet]

        public ActionResult DangNhapAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhapAdmin(FormCollection collection)
        {
            var useradmin = collection["username"];
            var passadmin = collection["password"];

            ADMIN ad = data.ADMINs.SingleOrDefault(n => n.UserAdmin ==  useradmin && n.PassAdmin == passadmin);
                if (ad == null)
                {
                    //add session
                    Session["UserAdmin"] = ad;
                    return RedirectToAction("Sach", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Đăng nhập thất bại!";
                    return RedirectToAction("DangNhapAdmin", "Admin");
                }
        }

        public ActionResult ThemSach()
        {
            ViewBag.MaChuDe = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            return View();
        }

        [HttpPost]
        public ActionResult ThemSach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaChuDe = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.AnhBiaSach = fileName;
                    data.SACHes.Add(sach);
                    data.SaveChanges();
                }
                return RedirectToAction("Sach");
            }
        }

        [HttpPost]

        public ActionResult ChiTietSach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MaSach == id);
            ViewBag.MaSach = sach.MaSach;
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpGet]

        public ActionResult XoaSach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MaSach == id);
            ViewBag.MaSach = sach.MaSach;   
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost,ActionName("XoaSach")]

        public ActionResult XacNhanXoa(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MaSach == id);
            ViewBag.MaSach = sach.MaSach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SACHes.Remove(sach);
            data.SaveChanges();
            return RedirectToAction("Sach");   
        }
        public ActionResult SuaSach(int id)
        {
            SACH sach = data.SACHes.SingleOrDefault(n => n.MaSach == id);

            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //
            ViewBag.MaChuDe = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            return View(sach);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSach(SACH sach, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaChuDe = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            //
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    if(System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    sach.AnhBiaSach = fileName;
                    //
                    data.Entry(sach).State = EntityState.Modified;
                    //UpdateModel(sach);
                    data.SaveChanges();
                }
                return RedirectToAction("Sach");
            }
        }
    }
}