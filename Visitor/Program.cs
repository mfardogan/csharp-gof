namespace Visitor
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    /*
        //       IVisitor ara birimini uygulamış
        //       her sınıf Cellphone sınıfından türemiş
        //       tüm sınıflar için kullanılabilir birer fonksiyon teşkil eder.

        //       Ek özellikler tasarlamak için yeni Visitor sınıfları yazmak yeterli 
        //       olacaktır. Örneğin görüntülü konuşma özelliğini alt sınıflara 
        //       kazandırmak isterseniz VideoChatVisitor : IVisitor 
        //       sınıfını tasarlayabiliriz.
        //     */

        //    IVisitor visitor = new PhotoVisitor();

        //    Cellphone phone = new Galaxy();
        //    phone.Accept(visitor);

        //    phone = new IPhone();
        //    phone.Accept(visitor);
        //}
        ///* ÇIKTI:
        //   Photo was taken by the Galaxy
        //   Photo was taken by the IPhone
        //*/

        static void Main(string[] args)
        {
            IVisitor gameVisitor = new SnakeGameVisitor();
            Cellphone nokia3310 = new Nokia3310();
            nokia3310.Accept(gameVisitor);
        }
        /* ÇIKTI:
          The Snake game is being played with the Nokia3310
         */
    }

    interface IVisitor
    {
        /*
            Visitor sınıflarının uygulayacağı ara birim
            eklenmek istenen fonksiyonun etki edeceği sınıf tipinde bir parametre
            alan Visit fonsiyonuna sahiptir.
        */
        void Visit(Cellphone phone);
    }

    abstract class Cellphone
    {
        /* Temel sınıfta fotoğraf çekmeye dair herhangi bir işlev olmasın! */

        public virtual void Accept(IVisitor visitor)
        {
            /*
               Temel sınıf  Visitor sınıflarının uyguladığı interface
               tipinde parametre alan bir Accept fonsiyonuna sahiptir.
               Fonksiyon gövdesinde ilgili Visitor sınıfın Visit fonsiyonuna
               sınıfın çalışma zamanındaki nesne örneği parametre olarak verilir.
             */
            visitor.Visit(this);
        }
    }

    class SnakeGameVisitor : IVisitor
    {
        public void Visit(Cellphone phone)
        {
            Console.WriteLine("The Snake game is being played with the "
                              + phone.GetType().Name);
        }
    }

    class PhotoVisitor : IVisitor
    {
        /*
           Fotoğraf çekme işlevi için bir ziyaretçi sınıfı tasarlanır.
           Ziyaretçi sınıfı IMobileVisitor ara birimini uygular ve
           Visit fonksiyonunda fotoğraf çekmeye dair gereken işlemleri
           yerine getirir.
         */
        public void Visit(Cellphone phone)
        {
            Console.WriteLine("Photo was taken by the " + phone.GetType().Name);
        }
    }

    class IPhone : Cellphone
    {
        /* Alt sınıflarda fotoğraf çekmeye dair herhangi bir işlev olmasın! */
    }

    class Galaxy : Cellphone
    {
    }

    class Nokia3310 : Cellphone
    {
    }
}