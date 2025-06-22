namespace ODataExample.Models
{
    public class Ulke
    {
        public int Id { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        
        // Navigation property
        public virtual ICollection<Sehir> Sehirler { get; set; } = new List<Sehir>();
    }
}
