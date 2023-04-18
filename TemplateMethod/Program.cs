namespace TemplateMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Drink drink = new Tea();
            drink.Prepare();
        }
        /* ÇIKTI:
           Ana malzeme eklendi.
           Şeker eklendi.
           Su eklendi.
           Karıştırılıyor...
         */

    }

    abstract class Drink
    {
        public void Prepare()
        {
            /*
               Sıcak içecek hazırlamak (çay, kahve vs.)
               aşağıdaki dört adımdan ibarettir.
               Bu adımların teker teker uygulanmasının yükü
               istemci tarafından alınmak istendiği için şablon
               bir fonksiyon içinde çağırılır.
             */

            PutMainMaterial();
            PutSugar();
            PutWater();
            Stir();
        }

        public abstract void PutMainMaterial();
        public abstract void PutSugar();
        public abstract void PutWater();
        public abstract void Stir();
    }

    class Tea : Drink
    {
        public override void PutSugar()
        {
            Console.WriteLine("Şeker eklendi.");
        }

        public override void PutWater()
        {
            Console.WriteLine("Su eklendi.");
        }

        public override void PutMainMaterial()
        {
            Console.WriteLine("Ana malzeme eklendi.");
        }

        public override void Stir()
        {
            Console.WriteLine("Karıştırılıyor...");
        }
    }
}