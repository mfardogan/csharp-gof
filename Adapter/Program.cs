namespace Adapter
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Log(new Overflow()); //uygun
            Log(new DivideByZero()); //uygun

            /* Log(new COMError()); //HATALI!
               COMError sınıfı Error sınıfından türemediği için Log fonksiyonuna argüman olamaz! 
              */
            var adapter = new COMErrorAdapter(
                               new COMError()
                                             );
            Log(adapter); //uygun
        }

        static void Log(Error exception)
        {
            /* Bu kısmın oluşan istisnaları bir yere kaydeden ayrı bir servis olduğunu düşünün.   */

            Console.WriteLine($"{exception.CreatedAt} {exception.Message}");
        }
        /* ÇIKTI:
           <Tarih> Bellek taşması oldu!
           <Tarih> Sıfıra bölme girişiminde bulunuldu!
           <Tarih> Uzak sunucu yanıt vermedi!
        */


    }

    //Target soyut tipi:
    abstract class Error
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }


    class DivideByZero : Error
    {
        public DivideByZero()
        {
            CreatedAt = DateTime.Now;
            Message = "Sıfıra bölme girişiminde bulunuldu!";
        }
    }

    class Overflow : Error
    {
        public Overflow()
        {
            CreatedAt = DateTime.Now;
            Message = "Bellek taşması oldu!";
        }
    }




    //Adaptee sınıfı:

    class COMError
    {

        /* MyException sınıfndan türemediği için mevcut sisteme uyumsuzdur! */

        public string ErrorMessage { get; set; }

        public COMError()
        {
            ErrorMessage = "Uzak sunucu yanıt vermedi!";
        }
    }


    //Adapter sınıfı:

    class COMErrorAdapter : Error
    {
        public COMErrorAdapter(COMError exception)
        {
            CreatedAt = DateTime.Now;
            Message = exception.ErrorMessage;
            /*
                Bu kısımda yapılan işlemler bir nevi adaptasyon işlemidir. Ne de olsa mevcut system, hata mesajlarını 
                Message özelliğinde tutumaktadır. Bu durumda sisteme uyumsuz olan istisna sınıfının ErrorMessage değerini 
                Message özelliğine yazarak bir adaptasyon sağlanmış olunur.
             */
        }
    }


}