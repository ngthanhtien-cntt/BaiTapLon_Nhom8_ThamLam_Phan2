using System;
using System.Collections.Generic;
using System.Linq;

// Lớp Node đại diện cho mỗi nút trong cây Huffman
class Node : IComparable<Node>
{
    public char KyTu { get; set; }
    public int TanSo { get; set; }
    public Node Trai { get; set; }
    public Node Phai { get; set; }

    // So sánh các Node dựa trên tần số để sắp xếp trong PriorityQueue
    public int CompareTo(Node other)
    {
        return TanSo - other.TanSo;
    }
}

class HuffmanCoding
{
    // Tạo cây Huffman từ các ký tự và tần số của chúng
    public Node XayDungCayHuffman(Dictionary<char, int> tanSo)
    {
        List<Node> danhSachNode = new List<Node>();

        foreach (var symbol in tanSo)
        {
            danhSachNode.Add(new Node { KyTu = symbol.Key, TanSo = symbol.Value });
        }

        while (danhSachNode.Count > 1)
        {
            danhSachNode.Sort();
            var trai = danhSachNode[0];
            var phai = danhSachNode[1];

            var cha = new Node
            {
                KyTu = '\0', // Node này không đại diện cho ký tự nào
                TanSo = trai.TanSo + phai.TanSo,
                Trai = trai,
                Phai = phai
            };

            danhSachNode.Remove(trai);
            danhSachNode.Remove(phai);
            danhSachNode.Add(cha);
        }

        return danhSachNode[0];
    }

    // Duyệt cây Huffman để tạo bảng mã
    public Dictionary<char, string> TaoMa(Node goc)
    {
        Dictionary<char, string> maHuffman = new Dictionary<char, string>();
        TaoMaDeQuy(goc, "", maHuffman);
        return maHuffman;
    }

    private void TaoMaDeQuy(Node node, string ma, Dictionary<char, string> maHuffman)
    {
        if (node == null)
            return;

        // Nếu node này là lá, thêm mã vào bảng mã
        if (node.Trai == null && node.Phai == null)
        {
            maHuffman[node.KyTu] = ma;
        }

        TaoMaDeQuy(node.Trai, ma + "0", maHuffman);
        TaoMaDeQuy(node.Phai, ma + "1", maHuffman);
    }

    static void Main()
    {
        string vanBan = "Day la 1 vi du ve ma hoa huffman";

        // Đếm tần số xuất hiện của các ký tự
        Dictionary<char, int> tanSo = new Dictionary<char, int>();
        foreach (var kyTu in vanBan)
        {
            if (!tanSo.ContainsKey(kyTu))
            {
                tanSo[kyTu] = 0;
            }
            tanSo[kyTu]++;
        }

        HuffmanCoding huffmanCoding = new HuffmanCoding();
        Node goc = huffmanCoding.XayDungCayHuffman(tanSo);
        Dictionary<char, string> maHuffman = huffmanCoding.TaoMa(goc);

        Console.WriteLine("Ma Huffman:");
        foreach (var ma in maHuffman)
        {
            Console.WriteLine($"{ma.Key}: {ma.Value}");
        }

        // Mã hóa văn bản
        string vanBanMaHoa = string.Join("", vanBan.Select(c => maHuffman[c]));
        Console.WriteLine("\nVan ban da ma hoa:");
        Console.WriteLine(vanBanMaHoa);
    }
}
