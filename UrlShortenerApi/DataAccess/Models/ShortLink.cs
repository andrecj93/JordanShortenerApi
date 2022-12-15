using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortenerApi.DataAccess.Models
{
    [Table("ShortLink")]
    public class ShortLink
    {
        [Key]
        [MaxLength(50)]
        public string Token { get; set; }
        public string FullLink { get; set; }
        public string ShortenedLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Clicks { get; set; }
        public DateTime? LastClickDate { get; set; }
        public bool Active { get; set; }
        public string CreatedByIp { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }
    }
}