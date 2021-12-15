using Froala.Web.Models;
using Microsoft.Security.Application;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Froala.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // Microsoft .NET CultureInfo "Japanese (Japan)" (ja-JP)
            // https://www.localeplanet.com/dotnet/ja-JP/index.html
            Debug.WriteLine(Thread.CurrentThread.CurrentCulture.Name); // = new System.Globalization.CultureInfo("ja-JP");
            Debug.WriteLine(Thread.CurrentThread.CurrentCulture.DisplayName); // = new System.Globalization.CultureInfo("ja-JP");

            Debug.WriteLine(Thread.CurrentThread.CurrentUICulture.Name); // = new System.Globalization.CultureInfo("ja-JP");
            Debug.WriteLine(Thread.CurrentThread.CurrentUICulture.DisplayName); // = new System.Globalization.CultureInfo("ja-JP");

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                FullName = "藤原竜也 (ふじわらたつや)",
                Description = @"
<table class='prof-tbl'>
    <tbody>
        <tr>
		    <th><p>出身地</p></th>
		    <td><p>埼玉県</p></td>
	    </tr>
		<tr>
		    <th><p>生年月日</p></th>
		    <td><p>1982年5月15日</p></td>
	    </tr>
		<tr>
		    <th><p>血液型</p></th>
		    <td><p>A型</p></td>
	    </tr>
		<tr>
		    <th><p>身長</p></th>
		    <td><p>178cm</p></td>
	    </tr>
		<tr>
		    <th><p>ファンクラブ</p></th>
		    <td><p><a href='http://fc.horipro.jp/tatsuyafujiwara/' target='blank' onclick='_gaq.push(['_trackPageview','http://fc.horipro.jp/tatsuyafujiwara/'])'>藤原竜也オフィシャルファンクラブ ”DRAGON aRROWS”</a></p></td>
	    </tr>
    </tbody>
</table>
"
            };

            return View(user);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(User user)
        {
            var body = Sanitizer.GetSafeHtml(user.Description);

            SaveBody(body);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FroalaAutoSave(string body, Guid? userId)
        {
            body = Sanitizer.GetSafeHtml(body);

            SaveBody(body);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult FroalaUploadImage(HttpPostedFileBase file, Guid? userId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/images/");
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = "images/" + fileName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FroalaUploadFile(HttpPostedFileBase file, Guid? userId)
        {
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/files/");
            file.SaveAs(Path.Combine(rootPath, fileName));
            return Json(new { link = "files/" + fileName }, JsonRequestBehavior.AllowGet);
        }

        private void SaveBody(string body)
        {
            //TODO: save body ...
            WaiteFewSeconds();
        }

        private void WaiteFewSeconds()
        {
            var fewSeconds = new TimeSpan(0, 0, 10);
            Thread.Sleep(fewSeconds);
        }
    }
}