namespace Interpreter
{
    class Context
    {
        public int Number { get; set; }
    }

    abstract class Expression
    {
        public abstract void Interpret(Context context);
    }


    class NonterminalExpression : Expression
    {
        public NonterminalExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public Expression Left { get; set; }
        public Expression Right { get; set; }

        public override void Interpret(Context context)
        {
            /*
               NonterminalExpression, sağ ve sol tarafı ayrı expression olan
               sınıfları ifade eder. Sağ veya sol taraf, Expression veya 
               NonterminalExpression olabilir.
             */

            Left.Interpret(context);
            Right.Interpret(context);
        }
    }

    class Sum : Expression
    {
        public int Operator { get; set; }

        public Sum(int @operator)
        {
            Operator = @operator;
        }

        public override void Interpret(Context context)
        {
            context.Number += Operator;
        }
    }

    class Extract : Expression
    {
        public int Operator { get; set; }

        public Extract(int @operator)
        {
            Operator = @operator;
        }

        public override void Interpret(Context context)
        {
            context.Number -= Operator;
        }
    }

    class Power : Expression
    {
        public int Operator { get; set; }

        public Power(int @operator)
        {
            Operator = @operator;
        }

        public override void Interpret(Context context)
        {
            double power = Math.Pow(context.Number, Operator);
            context.Number = (int)power;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();

            /*             5 + 7 ifadesi:
                             +
                           *  *
                         *     *
                       *        *
                     5           7
            */
            NonterminalExpression sum = new NonterminalExpression(
                new Sum(5),
                new Sum(7)
            );

            /*          (5 + 7) - 8 ifadesi:
                             -
                           *  *
                         *     *
                       *        *
                    5 + 7        8
            */

            NonterminalExpression extract = new NonterminalExpression(
                sum,
                new Extract(8)
            );

            /*          [ (5 + 7) - 8 ]^ 2 ifadesi:
                             ^
                           *  *
                         *     *
                       *        *
              [ (5 + 7) - 8 ]    2
            */
            NonterminalExpression power = new NonterminalExpression(
                extract,
                new Power(2)
            );

            power.Interpret(context);
            Console.WriteLine(context.Number);
        }
        /* ÇIKTI:
         16
         */
    }
}