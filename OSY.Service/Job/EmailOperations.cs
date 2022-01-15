using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using OSY.DB.Entities.DataContext;
using OSY.Model;
using OSY.Model.ModelResident;
using System.Linq;

namespace OSY.Service.Job
{
    public class EmailOperations : IEmailOperations
    {
        private readonly IMapper mapper; //Mapper çagrildi

        public EmailOperations(IMapper _mapper)
        {
            mapper = _mapper;
        }

        //Mail gönderme methodu
        public void sendWelcomeEmail()
        {
            var result = new General<ResidentViewModel>();
            using (var context = new OSYContext())
            {
                //Kullanci tablosundan Silinmemis ve Mail gönderilmemis olanları çekiyoruz.
                var userList = context.Resident.Where(x => !x.IsDelete && !x.IsSendWelcomeMail).OrderBy(x => x.Id);


                foreach (var user in userList)
                {
                    var message = new MimeMessage(); //Mailkit objesi
                    message.From.Add(new MailboxAddress("OSY", "osyapp@yandex.com")); //kimden gidecek
                    message.To.Add(new MailboxAddress("User", user.Email)); //kime gidecek
                    message.Subject = "Hoşgeldin"; //Mailin konusu
                    message.Body = new TextPart("html") //Mailin body si
                    {
                        Text = "Sayın " + user.Name + ", Online Site Yönetimine Hoşgeldiniz! <br>" + "Uygulama Şifreniz : " + user.Password
                    };

                    using (var client = new SmtpClient()) //smptp sağlayıcı
                    {
                        client.Connect("smtp.yandex.com", 465, true); //port a bağlanıyoruz
                        client.Authenticate("osyapp@yandex.com", "evucujfdsrryzloe");
                        client.Send(message); //mesajı gönderiyoruz
                        client.Disconnect(true);
                    }

                    user.IsSendWelcomeMail = true;


                }
                context.SaveChanges();
            }

        }

    }
}