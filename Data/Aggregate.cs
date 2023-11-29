public class AggregateExample{

    public static List<int> Price = new List<int>{2,3,4 ,5,6,7,4};
    public static int TotalPrice()
    {
        return Price.Aggregate((x,y)=> x+y);
    }
    public static int SumTwoNumbers(int a, int b)
    {
        return a+b;
    }
}