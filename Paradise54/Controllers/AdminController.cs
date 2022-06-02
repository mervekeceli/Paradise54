using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using ZXing.QrCode;

namespace Paradise54.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnviroment;

        public AdminController(IWebHostEnvironment hostEnviroment)
        {
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QRCode()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult QRCode(IFormCollection formCollection)
        {
            var writer = new QRCodeWriter();
            string url = "paradise54.com/Home/Foods?tablenum=" + formCollection["QRCodeString"];
            var resultBit = writer.encode(url, ZXing.BarcodeFormat.QR_CODE, 200, 200);
            
            var matrix = resultBit;
            int scale = 2;
            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);
            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.White;
                    for (int i = 0; i < scale; i++)
                    {
                        for (int j = 0; j < scale; j++)
                        {
                            result.SetPixel(x * scale + i, y * scale + j, pixel);
                        }
                    }
                }
            }
            string QR = url;//TABLENUMA KADAR GIT 
            
            //paradise54.com/Home/Foods?tablenum=1

            string webRootPath = _hostEnviroment.WebRootPath;
            result.Save(webRootPath + "\\Images\\"+formCollection["QRCodeString"] + ".png");
            ViewBag.URL = "\\Images\\"+ formCollection["QRCodeString"] + ".png";
            return View();
        }


        #region PartialAdmin
        public PartialViewResult AdminPartialNav()
        {
            return PartialView();
        }

        public PartialViewResult AdminPartialHeader()
        {
            return PartialView();
        }
        #endregion

        //public IActionResult AktifSiparisler()
        //{
        //    GetAllOrders(List < CartItem > deneme)
        //    return View();
        //}

    }
}
