using Froala.Web.Models;
using Microsoft.Security.Application;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Froala.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(Guid? userId)
        {
            PrintCultureInfo();

            //Default User
            User user = new User();

            if (!userId.HasValue)
            {
                user.UserId = Guid.NewGuid();
                user.FullName = "藤原竜也 (ふじわらたつや)";
                user.Description = @"
< table class='prof-tbl'>
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
";
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Index126(Guid? userId)
        {
            PrintCultureInfo();

            //Default User
            User user = new User();

            if (!userId.HasValue)
            {
                user.UserId = Guid.NewGuid();
                user.FullName = "藤原竜也 (ふじわらたつや)";
                user.Description = @"
< table class='prof-tbl'>
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
";
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Index407(Guid? userId)
        {
            PrintCultureInfo();

            //Default User
            User user = new User();

            if (!userId.HasValue)
            {
                user.UserId = Guid.NewGuid();
                user.FullName = "藤原竜也 (ふじわらたつや)";
                user.Description = @"
< table class='prof-tbl'>
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
";
            }

            return View(user);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(User user)
        {
            string statusMessage;
            string errorMessage;
            string toastDataKind;
            int code;
            try
            {
                if (ModelState.IsValid)
                {
                    var body = Sanitizer.GetSafeHtml(user.Description);
                    statusMessage = SaveBody(body);
                    toastDataKind = ToastDataKind.Success.ToString().ToLower();
                    code = (int)HttpStatusCode.OK;
                }
                else
                {
                    return View(nameof(Index), user);
                }
            }
            catch (Exception ex)
            {
                code = (int)HttpStatusCode.InternalServerError;
                statusMessage = ex.Message;
                errorMessage = ex.ToString();
                Debug.WriteLine(errorMessage);
                Logger.Log.Error(errorMessage);
                toastDataKind = ToastDataKind.Error.ToString().ToLower();
            }
            return Json(new { Success = true, Code = code, Message = statusMessage, ToastDataKind = toastDataKind }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FroalaAutoSave(string body, Guid? userId)
        {
            body = Sanitizer.GetSafeHtml(body);
            string str;
            int code;
            try
            {
                str = SaveBody(body);
                code = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                str = ex.ToString();
                Debug.WriteLine(str);
                Logger.Log.Error(str);
                code = (int)HttpStatusCode.InternalServerError;
            }
            return Json(new { Success = true, Code = code, Message = str }, JsonRequestBehavior.AllowGet);
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

        private void PrintCultureInfo()
        {
            // Microsoft .NET CultureInfo "Japanese (Japan)" (ja-JP)
            // https://www.localeplanet.com/dotnet/ja-JP/index.html
            Debug.WriteLine(Thread.CurrentThread.CurrentCulture.Name); // = new System.Globalization.CultureInfo("ja-JP");
            Debug.WriteLine(Thread.CurrentThread.CurrentCulture.DisplayName); // = new System.Globalization.CultureInfo("ja-JP");

            Debug.WriteLine(Thread.CurrentThread.CurrentUICulture.Name); // = new System.Globalization.CultureInfo("ja-JP");
            Debug.WriteLine(Thread.CurrentThread.CurrentUICulture.DisplayName); // = new System.Globalization.CultureInfo("ja-JP");
        }

        private string SaveBody(string body)
        {
            ThrowException();
            var rootPath = Server.MapPath("~/files/");
            var fileName = "body.html";
            var filePath = Path.Combine(rootPath, fileName);
            System.IO.File.WriteAllText(filePath, body, Encoding.UTF8);
            WaiteFewSeconds();
            return Message.SaveSuccessfully;
        }

        private void ThrowException()
        {
            try
            {
                var y = 0;
                var x = 1 / y;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private void WaiteFewSeconds()
        {
            var fewSeconds = new TimeSpan(0, 0, 1);
            Thread.Sleep(fewSeconds);
        }
    }
}