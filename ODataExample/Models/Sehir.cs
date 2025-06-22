using System.ComponentModel.DataAnnotations.Schema;

namespace ODataExample.Models
{
    public class Sehir
    {
        public int Id { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        
        // Foreign key
        public int UlkeId { get; set; }
        
        // Computed property - veritabanında saklanmaz
        [NotMapped]
        public int Nufusu => FnSehirNufusuGetir(this.Id, this.Ad);
        
        // Navigation properties
        public virtual Ulke Ulke { get; set; } = null!;
        public virtual ICollection<Ilce> Ilceler { get; set; } = new List<Ilce>();
        
        // Nüfus hesaplama fonksiyonu
        private static int FnSehirNufusuGetir(int sehirId, string sehirAd)
        {
            // Bu fonksiyon gerçek hayatta bir veritabanı sorgusu, 
            // web servis çağrısı veya başka bir kaynak olabilir
            return sehirAd switch
            {
                "İstanbul" => 15_840_000,
                "Ankara" => 5_700_000,
                "İzmir" => 4_400_000,
                "New York" => 8_400_000,
                "California" => 39_500_000,
                "Berlin" => 3_700_000,
                _ => Random.Shared.Next(100_000, 2_000_000) // Varsayılan rastgele değer
            };
        }
    }
}
