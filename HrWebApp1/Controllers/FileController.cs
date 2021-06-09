using HrWebApp1.Models;
using HrWebApp1.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HrWebApp1.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IWebHostEnvironment appEnvironment) 
        {
            _appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SendFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendFile1(FileViewModel file)
        {
            if (ModelState.IsValid)
            {
                file.UserId = 1;
                file.FileCatsId = 2;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    //var content = new StringContent(
                    //System.Text.Json.JsonSerializer.Serialize(sendcon),
                    //Encoding.UTF8,
                    //"application/json");

                    HttpContent useremail = new StringContent(User.Identity.Name);
                    HttpContent catid = new StringContent("2");


                    MultipartFormDataContent multiContent = new MultipartFormDataContent();
                    multiContent.Add(useremail, "useremail");
                    multiContent.Add(catid, "catid");


                    ByteArrayContent bytes1;
                    int count=0;
                    foreach (IFormFile fi in file.Udv)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Udv_"+count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Specialization)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Specialization_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Pension)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Pension_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Certificates)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Certificates_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Employhis)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Employhis_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Addres)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Addres_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Conviction)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Сonviction_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Narcodisp)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Narcodisp_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Psychodisp)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Psychodisp_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Military)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Military_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Docphoto)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Docphoto_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Refmainjob)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Refmainjob_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Marriage)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Marriage_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Cash)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Cash_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    count = 0;
                    foreach (IFormFile fi in file.Forma086)
                    {
                        bytes1 = new ByteArrayContent(Fileconvert(fi));
                        multiContent.Add(bytes1, "file", "Forma086_" + count.ToString() + Path.GetExtension(fi.FileName));
                        count++;
                    }
                    //using (var br = new BinaryReader(file.Specialization.OpenReadStream()))
                    //    data2 = br.ReadBytes((int)file.Specialization.OpenReadStream().Length);

                    //ByteArrayContent bytes2 = new ByteArrayContent(data2);
                    //multiContent.Add(bytes2, "file", "Specialization"+ Path.GetExtension(file.Specialization.FileName));


                    var response = client.PostAsync("api/UserDocs", multiContent).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    return Content(response.StatusCode.ToString()=="Created"?"OKEY!":"SOMETHING WRONG!");
                };
            }
            else
                ViewBag.Message = "Model.IsInvalid";
            return View(file);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendFile(FileViewModel file)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    var response = client.PostAsync("api/UserDocs", ContainFormData(3, file.Udv, "Udv_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(2, file.Specialization, "Specialization_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(4, file.Pension, "Pension_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(5, file.Certificates, "Certificates_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(6, file.Employhis, "Employhis_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(7, file.Addres, "Addres_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(8, file.Conviction, "Conviction_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(9, file.Narcodisp, "Narcodisp_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(10, file.Psychodisp, "Psychodisp_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(11, file.Military, "Military_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(12, file.Docphoto, "Docphoto_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(13, file.Refmainjob, "Refmainjob_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(14, file.Marriage, "Marriage")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(15, file.Cash, "Cash_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                    response = client.PostAsync("api/UserDocs", ContainFormData(16, file.Forma086, "Forma086_")).Result;
                    ViewBag.Status = response.StatusCode.ToString();
                }
            }
            return View(file);
        }

        private MultipartFormDataContent ContainFormData(int filecatid, IFormFile[] file, string name) {
            HttpContent useremail = new StringContent(User.Identity.Name);
            HttpContent catid = new StringContent(filecatid.ToString());

            MultipartFormDataContent multiContent = new MultipartFormDataContent();
            multiContent.Add(useremail, "useremail");
            multiContent.Add(catid, "filecat");

            ByteArrayContent bytes1;
            int count = 0;
            foreach (IFormFile fi in file)
            {
                bytes1 = new ByteArrayContent(Fileconvert(fi));
                multiContent.Add(bytes1, "file", name + count.ToString() + Path.GetExtension(fi.FileName));
                count++;
            }

            return multiContent;
        }

        public async Task<IActionResult> EditFile()
        {
            AuthController auth;
            EditFileViewModel file;
            User u = CheckUser();
            if (u != null)
            {
                string status = "Вы не отправили файлы", progressbar = "", progress = "0%";
                if (u.Status == 0)
                {
                    status = "Ждет проверки";
                    progress = "30%";
                }
                if (u.Status == 1)
                {
                    status = "Проверяется";
                    progressbar = "bg-warning";
                    progress = "60%";
                }
                if (u.Status == 2)
                {
                    status = "Нужны исправление";
                    progressbar = "bg-danger";
                    progress = "40%";
                }
                if (u.Status == 3)
                {
                    status = "Пройдена";
                    progressbar = "bg-success";
                    progress = "100%";
                }

                ViewData["Status"] = status;
                ViewData["ProgressBar"] = "progress-bar " + progressbar + " progress-bar-striped progress-bar-animated";
                ViewData["Progress"] = progress;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44340/");
                    var response = client.GetAsync("api/userdocs/GetUserDocByUserId?userid=" + u.Id + "&filecatid=3").Result;
                    var result = response.Content.ReadAsStringAsync().Result;

                    file = JsonConvert.DeserializeObject<EditFileViewModel>(result);
                    if (file == null)
                        return RedirectToAction("SendFile");



                    return View(SetComment(u, file));
                }
            }
            return Content("Something wrong");
        }

        private EditFileViewModel SetComment(User u, EditFileViewModel file)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");
                var response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=2").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                CheckCatViewModel comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.SpecializationComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=3").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.UdvComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=4").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.PensionComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=5").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.CertificatesComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=6").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.EmployhisComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=7").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.AddresComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=8").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.ConvictionComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=9").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.NarcodispComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=10").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.PsychodispComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=11").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.MilitaryComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=12").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.DocphotoComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=13").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.RefmainjobComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=14").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.MarriageComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=15").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.CashComm = comm.Comments;

                response = client.GetAsync("api/checkcats/GetCheckCatByUser?userid=" + u.Id + "&catid=16").Result;
                result = response.Content.ReadAsStringAsync().Result;
                comm = JsonConvert.DeserializeObject<CheckCatViewModel>(result);
                file.Forma086Comm = comm.Comments;


                return file;
            } 
        }

        public async Task<string> SaveFile(IFormFile file, string filename)
        {

            string res = filename;
            string path = "/imgwhile/" + res;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return res;
        }
        public async Task<string> DeleteFile(IFormFile file, string filename)
        {

            string res = filename;
            string path = _appEnvironment.WebRootPath + "/imgwhile/" + res;
            System.IO.File.Delete(path);

            return res;
        }

        public byte[] Fileconvert(IFormFile files) {
            byte[] res;
            using (var br = new BinaryReader(files.OpenReadStream()))
            {
                
                res = br.ReadBytes((int)files.OpenReadStream().Length);

                return res;
            }
        }

        [Route("GetImage/{image}")]
        [HttpGet]
        public IActionResult GetImage(Byte[] b, string format)
        {

            return File(b, format);
        }

        private User CheckUser()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44340/");

                var response = client.GetAsync(string.Format("api/auth/{0}", User.Identity.Name)).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                //Dictionary<string, string> resDes =
                //    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                ViewBag.Status = response.StatusCode.ToString();
                if (response.StatusCode.ToString() != "NotFound")
                {
                    try
                    {
                        User tmp = JsonConvert.DeserializeObject<User>(result);
                        return tmp;
                    }
                    catch (JsonSerializationException)
                    {
                        return null;
                    }
                }
                return null;
            }
        }

    }
}
