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
    //static void Main(string[] args)
    //{
    //    /*
    //       Aynı Power fonksiyonunun, mevcut durumu kaale alarak
    //       farklı bir davranışı yerine getirmesini izleyelim. 
    //     */

    //    MyComputer computer = new MyComputer(); //kapalı durumda
    //    computer.Power(); //açık konuma geçti
    //    computer.Power(); //kapalı konuma geçti

    //}
    ///* ÇIKTI:
    //   Bilgisayar açıldı!
    //   Bilgisayar kapandı!
    // */

    static void Main(string[] args)
    {

        DriveModeFactory factory = new DriveModeFactory();
        Car bmw = new Car();
        bmw.StepOnTheGas();

        bmw.State = factory.Create(Gears.R);
        bmw.StepOnTheGas();

        bmw.State = factory.Create(Gears.D);
        bmw.StepOnTheGas();

    }
    /* ÇIKTI:
       Araç park modunda.
       Araç geriye doğru gidiyor.
       Araç ileriye doğru gidiyor.
     */
}
//}


abstract class DriveMode
{
    public abstract void Handle(Car car);
}

class Drive : DriveMode
{
    public override void Handle(Car car)
    {
        //D modundayken gaza basılırsa araç ilerler.
        Console.WriteLine("Araç ileriye doğru gidiyor.");
    }
}

class Reverse : DriveMode
{
    public override void Handle(Car car)
    {
        //R modundayken gaza basılırsa araç geri gider.
        Console.WriteLine("Araç geriye doğru gidiyor.");
    }
}

class Park : DriveMode
{
    public override void Handle(Car car)
    {
        //P modundayken gaza basılırsa araç ilerlemez.
        Console.WriteLine("Araç park modunda.");
    }
}

class Neutral : DriveMode
{
    public override void Handle(Car car)
    {
        //N modundayken gaza basılırsa araç ilerlemez.
        Console.WriteLine("Araç boşta.");
    }
}

enum Gears
{
    P, R, N, D
}

class DriveModeFactory
{
    public DriveMode Create(Gears gears)
    {   //Bkz. Factory Method (Factory) pattern.
        switch (gears)
        {
            case Gears.P: return new Park();
            case Gears.R: return new Reverse();
            case Gears.N: return new Neutral();
            default: case Gears.D: return new Drive();
        }
    }
}


class Car
{
    public Car()
    {
        State = new Park();
    }

    public DriveMode State { get; set; }

    public void StepOnTheGas()
    {
        State.Handle(this);
    }
}