namespace ODataExample.Models
{
    public class Ilce
    {
        public int Id { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string Ad { get; set; } = string.Empty;
        
        // Foreign key
        public int SehirId { get; set; }
        
        // Navigation property
        public virtual Sehir Sehir { get; set; } = null!;
    }
}
