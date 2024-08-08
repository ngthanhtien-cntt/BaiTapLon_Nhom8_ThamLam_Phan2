using System;
using System.Collections.Generic;

class Item
{
    public double Weight { get; set; }
    public double Value { get; set; }

    public Item(double weight, double value)
    {
        Weight = weight;
        Value = value;
    }

    // Tính giá trị trên trọng lượng
    public double ValuePerWeight()
    {
        return Value / Weight;
    }
}

class Program
{
    static void Main()
    {
        // Danh sách các vật phẩm (trọng lượng, giá trị)
        List<Item> items = new List<Item>
        {
            new Item(10, 60),
            new Item(20, 100),
            new Item(30, 120)
        };

        // Trọng lượng tối đa của balo
        double maxWeight = 50;

        // Gọi hàm giải bài toán balo phân số
        double maxValue = FractionalKnapsack(items, maxWeight);

        // In ra giá trị tối đa
        Console.WriteLine("Gia tri toi da cua balo la: " + maxValue);
    }

    static double FractionalKnapsack(List<Item> items, double maxWeight)
    {
        // Sắp xếp các vật phẩm theo giá trị trên trọng lượng giảm dần
        items.Sort((x, y) => y.ValuePerWeight().CompareTo(x.ValuePerWeight()));

        double totalValue = 0;
        double currentWeight = 0;

        // Duyệt qua từng vật phẩm
        foreach (var item in items)
        {
            if (currentWeight + item.Weight <= maxWeight)
            {
                // Nếu có thể lấy toàn bộ vật phẩm, thêm vào balo
                currentWeight += item.Weight;
                totalValue += item.Value;
            }
            else
            {
                // Nếu không thể lấy toàn bộ vật phẩm, lấy một phần
                double remainWeight = maxWeight - currentWeight;
                totalValue += item.ValuePerWeight() * remainWeight;
                break;
            }
        }

        return totalValue;
    }
}
