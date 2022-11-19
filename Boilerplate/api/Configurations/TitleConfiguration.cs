using System.ComponentModel.DataAnnotations;

namespace api.Configurations
{
    public class TitleConfiguration
    {
        [Required]
        [MaxLength(60)]
        public string WelcomeMessage { get; set; }
        public bool ShowWelcomeMessage { get; set; }
        public string Color { get; set; }
    }
}
