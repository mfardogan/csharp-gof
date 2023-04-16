//namespace State
//{

abstract class Computer
{
    public ComputerState State { get; set; }
}


abstract class ComputerState
{
    public abstract void Handle(Computer computer);
}


class MyComputer : Computer
{
    public MyComputer()
    {
        /* 
           Başlangıçta bilgisayarın kapalı konumda olduğunu varsayabiliriz.  
         */

        State = new OffState();
    }

    public void Power()
    {
        /* Düğmeye basılması anında meydana gelecek davranışın karmaşık
           detaylarını Power fonksiyonu bilmemelidir! Bunun yerine State
           sınıflarına başvurmalıdır. Bu durum sisteme ilerleyen zamanlarda
           entegre edeceğiniz yeni durum sınıflarına programının kolayca adapte
           olmasını sağlar. Fonksiyon, gelişime açık ve değişime kapalı
           (Open-Closed) olarak tasarlanmış olur. 
         */

        State.Handle(this);
    }
}


class OffState : ComputerState
{
    public override void Handle(Computer computer)
    {
        /*
           Bilgisayar, kapalı durumdayken düğmeye basılırsa açık duruma geçer.
         */

        computer.State = new OnState();
        Console.WriteLine("Bilgisayar açıldı!");

    }
}

class OnState : ComputerState
{
    public override void Handle(Computer computer)
    {
        /*
           Bilgisayar, açık durumdayken düğmeye basılırsa kapalı duruma geçer.
         */

        computer.State = new OffState();
        Console.WriteLine("Bilgisayar kapandı!");

    }
}


internal class Program
{
    static void Main(string[] args)
    {
        /*
           Aynı Power fonksiyonunun, mevcut durumu kaale alarak
           farklı bir davranışı yerine getirmesini izleyelim. 
         */

        MyComputer computer = new MyComputer(); //kapalı durumda
        computer.Power(); //açık konuma geçti
        computer.Power(); //kapalı konuma geçti

    }
    /* ÇIKTI:
       Bilgisayar açıldı!
       Bilgisayar kapandı!
     */

}
//}