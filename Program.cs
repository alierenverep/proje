using System;
using System.Collections.Generic;

// Ürün sınıfı
public class Urun
{
    public string Ad { get; set; }
    public double Fiyat { get; set; }
    public int Stok { get; set; }

    public Urun(string ad, double fiyat, int stok)
    {
        Ad = ad;
        Fiyat = fiyat;
        Stok = stok;
    }

    public void UrunBilgisiGoster()
    {
        Console.WriteLine($"{Ad} - Fiyat: {Fiyat} TL, Stok: {Stok} adet");
    }
}

// Kullanıcı sınıfı
public class Kullanici
{
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Eposta { get; set; }
    public Sepet Sepetim { get; set; }

    public Kullanici(string ad, string soyad, string eposta)
    {
        Ad = ad;
        Soyad = soyad;
        Eposta = eposta;
        Sepetim = new Sepet();
    }

    public void UrunSepeteEkle(Urun urun, int miktar)
    {
        Sepetim.UrunEkle(urun, miktar);
    }

    public void SepetGoruntule()
    {
        Sepetim.SepetDetaylariniGoruntule();
    }

    public void SiparisTamamla()
    {
        Sepetim.SiparisTamamla();
    }
}

// Sepet sınıfı
public class Sepet
{
    public List<Urun> Urunler { get; set; }

    public Sepet()
    {
        Urunler = new List<Urun>();
    }

    public void UrunEkle(Urun urun, int miktar)
    {
        if (urun.Stok >= miktar)
        {
            for (int i = 0; i < miktar; i++)
            {
                Urunler.Add(urun);
            }
            urun.Stok -= miktar;
            Console.WriteLine($"{miktar} {urun.Ad} sepete eklendi.");
        }
        else
        {
            Console.WriteLine($"Yeterli stok yok! {urun.Ad} ürününden sadece {urun.Stok} adet mevcut.");
        }
    }

    public void SepetDetaylariniGoruntule()
    {
        Console.WriteLine("\nSepetiniz:");
        double toplamFiyat = 0;
        foreach (var urun in Urunler)
        {
            Console.WriteLine($"{urun.Ad} - {urun.Fiyat} TL");
            toplamFiyat += urun.Fiyat;
        }
        Console.WriteLine($"Toplam Fiyat: {toplamFiyat} TL");
    }

    public void SiparisTamamla()
    {
        double toplamFiyat = 0;
        foreach (var urun in Urunler)
        {
            toplamFiyat += urun.Fiyat;
        }

        if (toplamFiyat > 0)
        {
            Console.WriteLine($"Toplam ödeme tutarınız: {toplamFiyat} TL. Ödeme alınıyor...");
            // Ödeme simülasyonu
            Console.WriteLine("Ödeme başarılı! Siparişiniz alınmıştır.");
            Urunler.Clear();
        }
        else
        {
            Console.WriteLine("Sepetiniz boş.");
        }
    }
}

// Ana Program
class Program
{
    static void Main(string[] args)
    {
        // Kullanıcıdan bilgiler alıyoruz
        Console.WriteLine("Kullanıcı bilgilerini giriniz:");

        Console.Write("Adınızı giriniz: ");
        string ad = Console.ReadLine();

        Console.Write("Soyadınızı giriniz: ");
        string soyad = Console.ReadLine();

        Console.Write("E-posta adresinizi giriniz: ");
        string eposta = Console.ReadLine();

        // Kullanıcıyı oluşturma
        Kullanici kullanici = new Kullanici(ad, soyad, eposta);

        // Ürünleri oluşturma
        Urun telefon = new Urun("Telefon", 20000, 10);   // Fiyatı 20,000 TL olarak güncellendi
        Urun laptop = new Urun("Laptop", 30000, 5);      // Fiyatı 30,000 TL olarak güncellendi
        Urun kulaklik = new Urun("Kulaklık", 5000, 20);   // Fiyatı 5,000 TL olarak güncellendi

        bool devam = true;
        while (devam)
        {
            // Kullanıcıya ürün seçenekleri sunuluyor
            Console.WriteLine("\nLütfen bir ürün seçin:");
            Console.WriteLine("1 - Telefon");
            Console.WriteLine("2 - Laptop");
            Console.WriteLine("3 - Kulaklık");
            Console.WriteLine("4 - Sepeti Görüntüle");
            Console.WriteLine("5 - Siparişi Tamamla");
            Console.Write("Seçiminizi yapın (1-5): ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    // Telefon seçildiğinde
                    Console.Write("Kaç adet Telefon almak istersiniz?: ");
                    int telefonAdet = Convert.ToInt32(Console.ReadLine());
                    kullanici.UrunSepeteEkle(telefon, telefonAdet);
                    break;
                case "2":
                    // Laptop seçildiğinde
                    Console.Write("Kaç adet Laptop almak istersiniz?: ");
                    int laptopAdet = Convert.ToInt32(Console.ReadLine());
                    kullanici.UrunSepeteEkle(laptop, laptopAdet);
                    break;
                case "3":
                    // Kulaklık seçildiğinde
                    Console.Write("Kaç adet Kulaklık almak istersiniz?: ");
                    int kulaklikAdet = Convert.ToInt32(Console.ReadLine());
                    kullanici.UrunSepeteEkle(kulaklik, kulaklikAdet);
                    break;
                case "4":
                    // Sepet görüntüle
                    kullanici.SepetGoruntule();
                    break;
                case "5":
                    // Siparişi tamamla
                    kullanici.SiparisTamamla();
                    devam = false; // Sipariş tamamlandığında döngüyü bitir
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim! Lütfen 1-5 arasında bir seçenek girin.");
                    break;
            }
        }
    }
}
