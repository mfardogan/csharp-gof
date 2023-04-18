//namespace Factory
//{
internal class Program
{
    static void Main(string[] args)
    {
        VehicleFactory factory = new VehicleFactory();
        var car = factory.Create(VehicleType.Mercedes);
        car.Drive();

        /*
          Alt tip kullanılmak istendiğinde casting işlemi yapılabilir.
         */
        Bmw bmw = (Bmw)factory.Create(VehicleType.Bmw);
        bmw.Drive();
    }
    /* ÇIKTI:
       Mercedes is moving...
       Bmw is moving...
     */

}

class Vehicle
{
    /*
       Factory Method, aynı ürün ailesinden meydana gelmiş sınıflardan nesne 
       oluşturmayı amaçlar. Aynı ürün ailesinden kasıt, aynı temel sınıf veya ara 
       birimden türemiş sınıflardır. Örneğimiz için ürün ailesi Vehicle isimli bu 
       sınıfı uygulamış alt sınıflar olacaktır.
     */
    public string Type { get; }

    public Vehicle(string type)
    {
        Type = type;
    }

    public void Drive()
    {
        Console.WriteLine(Type + " is moving...");
    }
}

//Ürün-1
class Mercedes : Vehicle
{
    public Mercedes() : base("Mercedes")
    {
    }
}

//Ürün-2
class Bmw : Vehicle
{
    public Bmw() : base("Bmw")
    {
    }
}


enum VehicleType : byte
{
    Mercedes,
    Bmw
}

class VehicleFactory
{
    public Vehicle Create(VehicleType type)
    {
        /*
             "type" parametresinin değerine göre hangi türde
             nesne üretileceğine karar verilir.
             Üretilir ve istemci tarafına iletilir.
         */
        Vehicle vehicle = null;
        switch (type)
        {
            case VehicleType.Bmw:
                vehicle = new Bmw();
                break;

            case VehicleType.Mercedes:
                vehicle = new Mercedes();
                break;
        }

        return vehicle;
    }
}




//}