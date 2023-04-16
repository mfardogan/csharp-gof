namespace Command
{
    abstract class Command
    {
        /* Her bir komut için ayrı bir sınıf tasarlayıp Command sınıfından 
           türeteceğiz. Command sınıfından türeyen sınıflar aslında kumandanın 
           düğmelerine karşılık gelir. Sınıflar, AirConditioner parametresi alan 
           Execute fonksiyonuna sahip olacaklar. İlgili komutun işletilmesi tam 
           olarak o fonksiyon içinde gerçekleşecek.
        */
        public abstract void Execute(AirConditioner device);
    }

    class AirConditioner
    {
        public bool IsWorking { get; set; }
        public int Degree { get; set; }

        public string GetStatus() => IsWorking ?
            "Klima çalışıyor..." :
            "Klima çalışmıyor...";

        public virtual void Execute(Command command)
        {
            command.Execute(this);
        }

        /* Komut setini dış kaynaklara döndürecek olan fonksiyon */
        public Invoker CreateInvoker()
        {
            return new Invoker(this);
        }
    }

    class OnCommand : Command
    {
        public override void Execute(AirConditioner device)
        {
            /* İlgili işlemin yapılması için gereken detaylar oldukça karmaşık 
               olabilir. Gerçek hayatta yaptıklarımız genellikle büyük bir 
               karmaşıklık içerir.*/
            device.IsWorking = true;
        }
    }

    class OffCommand : Command
    {
        public override void Execute(AirConditioner device)
        {
            /* Aracın dururulması veya çalıştırılması için gereken işlemlerin  
               birbirinden ayrılması, bileşen başına düşen komplekslik seviyesini 
               azaltarak tek sorumluluk prensibine daha uygun bileşenler
               geliştirmenize yardımcı olur.    
             */
            device.IsWorking = false;
        }
    }

    class IncreaseDegreeCommand : Command
    {
        public override void Execute(AirConditioner device)
        {
            device.Degree += 5;
        }
    }

    class DecreaseDegreeCommand : Command
    {
        public override void Execute(AirConditioner device)
        {
            device.Degree -= 5;
        }
    }

    class Invoker
    {
        private readonly AirConditioner device;
        private readonly IList<Command> commands;

        public Invoker(AirConditioner device)
        {
            this.device = device;

            /* Invoker, klima kumandası gibi düşünülebilir. Birçok komutu 
               topluca üzerinde barındıran ve istemcinin tüm komutlara kolayca 
               ulaşmasını sağlayan yardımcı bir sınıf gibi çalışır. */
            commands = new List<Command>() {
            new OnCommand(), new OffCommand(),
            new IncreaseDegreeCommand(),  new DecreaseDegreeCommand()
        };
        }

        public void Invoke(int i)
        {
            /* Komut bir int veya enum sabiti olarak dışarıdan alınır ve hangi 
               komutun icra edileceğine karar verilip ilgili komut işletilir. */
            Command command = commands[i];
            command.Execute(device);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            AirConditioner device = new AirConditioner();
            Invoker invoker = device.CreateInvoker();

            invoker.Invoke(0); // 0. indiste OnCommand var.
            Console.WriteLine(device.GetStatus());

            invoker.Invoke(2); // 2. indiste IncreaseDegreeCommand var.
            Console.WriteLine("Klima derecesi: " + device.Degree);
        }
        /* ÇIKTI:
          Klima çalışıyor...
          Klima derecesi: 5
       */
    }
}