namespace Observer
{

    class Context
    {
        public bool IsTeacherInTheClass { get; set; }
    }

    interface IObserver
    {
        void Update(Context context);
    }

    interface ISubject
    {
        void Notify();
        void Register(IObserver observer);
    }


    class Ahmet : IObserver
    {
        public void Update(Context context)
        {
            string message = "Ahmet " + (context.IsTeacherInTheClass ?
                "öğretmenini dinliyor..." :
                "teneffüse çıkıyor...");

            Console.WriteLine(message);
        }
    }

    class Mehmet : IObserver
    {
        public void Update(Context context)
        {
            string message = "Mehmet " + (context.IsTeacherInTheClass ?
                "öğretmenini dinliyor..." :
                "teneffüse çıkıyor...");

            Console.WriteLine(message);
        }
    }


    class Teacher : ISubject
    {
        private bool isInTheClass = false;
        /*
          Konu sınıfı, kendisini dinleyen gözlemcileri içeren bir listeye sahiptir. 
         */
        private List<IObserver> observers = new List<IObserver>();

        /*
          Öğretmenin sınıftan ayrılması veya sınıfa girmesi durumlarını
          gözlemcilere yani öğrencilere bildirelim. Her iki durum için birer
          fonksiyon yapalım ve fonksiyonlar sınıf içindeki isInTheClass özelliğini     
          etkilesin.
        */

        public void EnterTheClass()
        {
            isInTheClass = true;
            Notify();
        }

        public void LeaveFromClass()
        {
            isInTheClass = false;
            Notify();
        }

        public void Notify()
        {
            /*
              Konu(Teacher) nesnesi üzerinde herhangi bir değişim/olay
              meydana geldiğinde ilgili olay gözlemcilere iletilir.
             */

            Context context = new Context();
            context.IsTeacherInTheClass = isInTheClass;

            foreach (IObserver observer in observers)
            {
                observer.Update(context);
            }
        }

        public void Register(IObserver observer)
        {
            /*
               İstemci tarafından sisteme bir gözlemci eklemek için kullanılır.
               Konu sınıfı(Teacher), gözlemcilerin gerçek tipleri hakkında bilgi sahibi 
               değildir.
             */
            observers.Add(observer);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Teacher teacher = new Teacher();
            //Gözlemcileri ekleyelim.
            teacher.Register(new Ahmet());
            teacher.Register(new Mehmet());

            teacher.EnterTheClass(); //sınıfa gir
            teacher.LeaveFromClass(); //sınıftan ayrıl
        }
        /* ÇIKTI:
           Ahmet öğretmenini dinliyor...
           Mehmet öğretmenini dinliyor...
           Ahmet teneffüse çıkıyor...
           Mehmet teneffüse çıkıyor...
         */

    }
}