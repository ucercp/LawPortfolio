namespace MyPortolio.DAL.Entities
{
    public class Testimonial
    {
        public int TestimonialId { get; set; }
        public string NameSurname { get; set; } // Yorum yapan kişinin adı
        public string Title { get; set; } // Yorumun başlığı
        public string Description { get; set; } // Yorumun açıklaması
        public DateTime CreatedAt { get; set; } // Yorumun oluşturulma tarihi
    }
}
