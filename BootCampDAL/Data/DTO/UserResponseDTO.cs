namespace BootCampNetFullStack.BootCampDAL.Data.DTO
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Tel { get; set; } 
    }
}
