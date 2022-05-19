using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Models
{
    public class UserRegisterViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage = "Lütfen Adınızı Soyadınız Giriniz..")]
        public string NameSurname { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz..")]
        public string Password { get; set; }


        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor!")] // eşleştirme atribute
        public string ConfirmPassword { get; set; }


        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Lütfen Mail Adresinizi Giriniz..")]
        public string Mail { get; set; }


        [Display(Name = "kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen Kullanıcı Adınızı Giriniz..")]
        public string UserName { get; set; }
    }
}
