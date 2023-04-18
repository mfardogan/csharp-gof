namespace Adapter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Log(new OverflowException()); //uygun
            Log(new DivideByZeroException()); //uygun

            /* Log(new ThirdPartyException()); //HATALI!
               ThirdPartyException sınıfı MyException sınıfından türemediği için
               Log fonksiyonuna argüman olamaz! 
            */

            var adapter = new ThirdPartyExceptionAdapter(new ThirdPartyException());
            Log(adapter); //uygun
        }

        static void Log(MyException exception)
        {
            Console.WriteLine($"{exception.CreatedAt} {exception.Message}");
        }
        /* ÇIKTI:
            <Tarih> Bellek taşması oldu!
            <Tarih> Sıfıra bölme girişiminde bulunuldu!
            <Tarih> Uzak sunucu yanıt vermedi!
         */

    }

    abstract class MyException //ITarget olarak düşünülür.
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    class DivideByZeroException : MyException
    {
        public DivideByZeroException()
        {
            CreatedAt = DateTime.Now;
            Message = "Sıfıra bölme girişiminde bulunuldu!";
        }
    }

    class OverflowException : MyException
    {
        public OverflowException()
        {
            CreatedAt = DateTime.Now;
            Message = "Bellek taşması oldu!";
        }
    }



    class ThirdPartyException //Adaptee olarak düşünülür.
    {
        public string ErrorMessage { get; set; }

        public ThirdPartyException()
        {
            ErrorMessage = "Uzak sunucu yanıt vermedi!";
        }
    }

    class ThirdPartyExceptionAdapter : MyException //Adapter olarak düşünülür.
    {
        public ThirdPartyExceptionAdapter(ThirdPartyException exception)
        {
            CreatedAt = DateTime.Now;
            Message = exception.ErrorMessage;
            /*
                Bu kısımda yapılan işlemler bir nevi adaptasyon işlemidir. Ne de olsa  
                mevcut sistem hata mesajlarını Message alanında tutumaktadır. Bu 
                durumda sisteme uyumsuz olan istisna sınıfının ErrorMessage değerini 
                Message özelliğine yazarak bir adaptasyon sağlanmış olunur.
            */
        }
    }

}